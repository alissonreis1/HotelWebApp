using HotelWebApp.Repositorio;
using HotelWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Adicione servi�os ao cont�iner.
builder.Services.AddControllersWithViews();

// Adicione o contexto do banco de dados antes de construir o aplicativo
builder.Services.AddDbContext<HotelContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IHotelRepository, HotelRepository>();

var app = builder.Build();

// Configure o pipeline de solicita��o HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padr�o HSTS � de 30 dias. Voc� pode querer alterar isso para cen�rios de produ��o, consulte https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hotel}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "delete",
    pattern: "{controller=Hotel}/{action=DeleteConfirmed}/{id?}");


app.Run();