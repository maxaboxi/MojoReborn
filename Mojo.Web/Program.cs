using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Core.Data;
using Mojo.Modules.Core.Features.Identity;
using Mojo.Modules.Core.Features.Identity.Entities;
using Mojo.Modules.Core.Features.SiteStructure.GetModule;
using Mojo.Modules.Core.Features.SiteStructure.GetSite;
using Mojo.Modules.Forum.Data;
using Mojo.Web.Extensions;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.Persistence.Durability;
using Wolverine.SqlServer;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(connectionString),
    // Significant performance gain as per Wolverine docs
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddDbContext<CoreDbContext>(options =>
    options.UseSqlServer(connectionString),
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddDbContext<ForumDbContext>(options =>
        options.UseSqlServer(connectionString),
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<CoreDbContext>()
.AddDefaultTokenProviders();

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

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ModuleResolver>();
builder.Services.AddScoped<SiteResolver>();

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(BlogDbContext).Assembly);
    opts.Discovery.IncludeAssembly(typeof(CoreDbContext).Assembly);
    opts.Discovery.IncludeAssembly(typeof(ForumDbContext).Assembly);
    
    opts.Durability.MessageStorageSchemaName = "wolverine";
    
    opts.UseEntityFrameworkCoreTransactions();
    opts.Services.AddDbContextWithWolverineIntegration<BlogDbContext>(x => x.UseSqlServer(connectionString));
    opts.PersistMessagesWithSqlServer(connectionString, role:MessageStoreRole.Ancillary).Enroll<BlogDbContext>();
    
    opts.Services.AddDbContextWithWolverineIntegration<CoreDbContext>(x => x.UseSqlServer(connectionString));
    opts.PersistMessagesWithSqlServer(connectionString, role:MessageStoreRole.Ancillary).Enroll<CoreDbContext>();
    
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

app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.MapWolverineEndpoints(opts =>
{
    opts.UseFluentValidationProblemDetailMiddleware();
    
    // https://wolverinefx.net/guide/http/#eager-warmup
    // Wolverine.HTTP has a known issue with applications that make simultaneous requests to the same endpoint at start up
    // where the runtime code generation can blow up if the first requests come in together.
    opts.WarmUpRoutes = RouteWarmup.Eager;
});

app.Run();