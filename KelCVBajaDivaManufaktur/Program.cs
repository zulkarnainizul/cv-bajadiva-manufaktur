using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KelCVBajaDivaManufaktur.Data;
using Microsoft.AspNetCore.Identity;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<KelCVBajaDivaManufakturContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KelCVBajaDivaManufakturContext") ?? throw new InvalidOperationException("Connection string 'KelCVBajaDivaManufakturContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Tambahkan baris ini untuk mengaktifkan otentikasi
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

