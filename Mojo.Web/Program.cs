using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Forum.Data;
using Mojo.Modules.Identity.Data;
using Mojo.Modules.Identity.Domain.Entities;
using Mojo.Modules.Identity.Features.Services;
using Mojo.Modules.SiteStructure.Data;
using Mojo.Modules.SiteStructure.Features.GetFeatureContext;
using Mojo.Modules.SiteStructure.Features.GetSite;
using Mojo.Shared.Interfaces.Identity;
using Mojo.Shared.Interfaces.SiteStructure;
using Mojo.Web.Extensions;
using Mojo.Web.Middleware;
using Serilog;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.Persistence.Durability;
using Wolverine.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(connectionString),
    // Significant performance gain as per Wolverine docs
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(connectionString),
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddDbContext<SiteStructureDbContext>(options =>
        options.UseSqlServer(connectionString),
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddDbContext<ForumDbContext>(options =>
        options.UseSqlServer(connectionString),
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<IdentityDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddGoogle(opt =>
    {
        opt.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
        opt.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
    })
    .AddMicrosoftAccount(opt =>
    {
        opt.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"] ?? "";
        opt.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"] ?? "";
    })
    .AddFacebook(opt =>
    {
        opt.ClientId = builder.Configuration["Authentication:Facebook:ClientId"] ?? "";
        opt.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"] ?? "";
    });

builder.Services.AddScoped<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? new ClaimsPrincipal());
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IFeatureContextResolver, FeatureContextResolver>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<ISiteResolver, SiteResolver>();

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(BlogDbContext).Assembly);
    opts.Discovery.IncludeAssembly(typeof(IdentityDbContext).Assembly);
    opts.Discovery.IncludeAssembly(typeof(SiteStructureDbContext).Assembly);
    opts.Discovery.IncludeAssembly(typeof(ForumDbContext).Assembly);
    
    // Register Audit Logging Middleware for all handlers
    opts.Policies.AddMiddleware<AuditLoggingBehavior>();
    
    opts.Durability.MessageStorageSchemaName = "wolverine";
    
    opts.UseEntityFrameworkCoreTransactions();
    opts.Services.AddDbContextWithWolverineIntegration<BlogDbContext>(x => x.UseSqlServer(connectionString));
    opts.PersistMessagesWithSqlServer(connectionString, role:MessageStoreRole.Ancillary).Enroll<BlogDbContext>();
    
    opts.Services.AddDbContextWithWolverineIntegration<IdentityDbContext>(x => x.UseSqlServer(connectionString));
    opts.PersistMessagesWithSqlServer(connectionString, role:MessageStoreRole.Ancillary).Enroll<IdentityDbContext>();
    
    opts.Services.AddDbContextWithWolverineIntegration<SiteStructureDbContext>(x => x.UseSqlServer(connectionString));
    opts.PersistMessagesWithSqlServer(connectionString, role:MessageStoreRole.Ancillary).Enroll<SiteStructureDbContext>();
    
    opts.Services.AddDbContextWithWolverineIntegration<ForumDbContext>(x => x.UseSqlServer(connectionString));
    opts.PersistMessagesWithSqlServer(connectionString, role:MessageStoreRole.Ancillary).Enroll<ForumDbContext>();
    
    opts.UseFluentValidation(); 
});

builder.Services.AddCors();


builder.Services.AddMemoryCache();
builder.Services.AddWolverineHttp();

var app = builder.Build();

await app.ApplyDatabaseMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(opt => opt.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapWolverineEndpoints(opts =>
{
    opts.UseFluentValidationProblemDetailMiddleware();
    
    // https://wolverinefx.net/guide/http/#eager-warmup
    // Wolverine.HTTP has a known issue with applications that make simultaneous requests to the same endpoint at start up
    // where the runtime code generation can blow up if the first requests come in together.
    opts.WarmUpRoutes = RouteWarmup.Eager;
});

app.Run();