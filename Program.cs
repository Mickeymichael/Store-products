using Microsoft.EntityFrameworkCore;
using Session4.Models;
using Session4.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<CLSDBContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));

builder.Services.AddScoped<IProduct, ProductRepository>();







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
