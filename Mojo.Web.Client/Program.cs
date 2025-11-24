using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Mojo.Modules.Blog.UI.Services;
using Mojo.Modules.Core.UI.Services;
using Mojo.Shared.Features.Blog;
using Mojo.Shared.Features.Core;
using Mojo.Web.Client.Infrastructure.Blog;
using Mojo.Web.Client.Infrastructure.Core;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRefitClient<IBlogApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddRefitClient<IPageApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped<IBlogService, ClientBlogService>();
builder.Services.AddScoped<IPageService, ClientPageService>();
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();