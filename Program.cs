using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавяне на услуги към контейнера
builder.Services.AddControllersWithViews(); // Активиране на MVC архитектура

// Добавяне на контекста на базата данни с връзка към SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Конфигурация на HTTP заявките
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Пренасочване към страницата за грешки при проблеми
    app.UseHsts(); // Активиране на HSTS (защитена връзка с HTTPS)
}

app.UseHttpsRedirection(); // Пренасочване от HTTP към HTTPS
app.UseStaticFiles(); // Разрешаване на използването на статични файлове (CSS, JS, изображения)

app.UseRouting(); // Активиране на маршрутизацията

app.UseAuthorization(); // Активиране на системата за авторизация

// Конфигурация на маршрута по подразбиране
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Стартиране на приложението
