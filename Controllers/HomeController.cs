using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

// Контролер за основната логика на началната страница и други общи действия
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    // Конструктор за инициализиране на логера (за записване на съобщения или грешки)
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Действие за зареждане на началната страница
    public IActionResult Index()
    {
        return View(); // Връща изгледа за началната страница
    }

    // Действие за зареждане на страницата за поверителност
    public IActionResult Privacy()
    {
        return View(); // Връща изгледа за страницата "Privacy"
    }

    // Действие за обработка на грешки и показване на информация за тях
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel 
        { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
        }); // Връща изгледа за грешка с детайли за текущата заявка
    }
}
