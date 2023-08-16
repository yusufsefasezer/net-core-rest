using Microsoft.EntityFrameworkCore;
using System.Reflection;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<net_core_rest.Data.ContactDbContext>(options =>
{
    options.UseInMemoryDatabase("Contact");
});

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = ".NET Core Contact",
        Description = ".NET Core Contact API",
        TermsOfService = new Uri("https://www.yusufsezer.com"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Yusuf SEZER", Email = "yusufsezer@mail.com", Url = new Uri("https://www.yusufsezer.com") },
        License = new Microsoft.OpenApi.Models.OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = ".NET Core Contact API";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Core Contact API");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
