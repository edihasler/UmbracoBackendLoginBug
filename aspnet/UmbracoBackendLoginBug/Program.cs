using Microsoft.AspNetCore.HttpOverrides;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor;
    //options.KnownNetworks.Clear(); // Removes restrictions on proxy IP addresses
    //options.KnownProxies.Clear(); // Allows Azure proxies to be trusted
});

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

app.UseForwardedHeaders();

app.Use(async (context, next) =>
{
    var x = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
    var headers = context.Request.Headers;

    // Call next middleware
    await next(context);

    // After request processing
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
