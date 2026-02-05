using dotnetAssignment.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddXmlSerializerFormatters();
builder.Services.AddSingleton<ITouristPlaceRepository, MockTouristPlaceRepository>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} 
else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapGet("/ping", () => "Hello World!");

app.Run();
