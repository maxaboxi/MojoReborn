using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Core.Data;
using Mojo.Modules.Core.Features.GetModule;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;

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

builder.Services.AddScoped<ModuleResolver>();

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(BlogDbContext).Assembly);
    opts.Discovery.IncludeAssembly(typeof(CoreDbContext).Assembly);
    
    opts.UseFluentValidation(); 
});

builder.Services.AddCors();


builder.Services.AddMemoryCache();
builder.Services.AddWolverineHttp();

var app = builder.Build();

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
});

app.Run();