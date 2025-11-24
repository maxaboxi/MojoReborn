using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Core.Data;
using Mojo.Shared.Features.Blog;
using Mojo.Shared.Features.Core;
using Mojo.Web.Components;
using Mojo.Web.Infrastructure.Blog;
using Mojo.Web.Infrastructure.Core;
using Wolverine;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CoreDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(BlogDbContext).Assembly);
    opts.Discovery.IncludeAssembly(typeof(CoreDbContext).Assembly);
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<IBlogService, ServerBlogService>();
builder.Services.AddScoped<IPageService, ServerPageService>();

builder.Services.AddMemoryCache();
builder.Services.AddWolverineHttp();
builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapWolverineEndpoints();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Mojo.Web.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(Mojo.Modules.Blog.UI.Features.GetPosts.BlogList).Assembly);


app.Run();