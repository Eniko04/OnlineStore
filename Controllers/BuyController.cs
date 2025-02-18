using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Data;

namespace OnlineShop.Controllers
{
    public class BuyController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BuyController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Зареждане на страницата с всички продукти за покупка
        public IActionResult Index()
        {
            var products = _db.Store.ToList(); // Вземете всички продукти от базата данни
            return View(products); // Подайте ги към View за визуализация
        }

        // GET: Страница за покупка на конкретен продукт
        public IActionResult Buy(int id)
        {
            var product = _db.Store.FirstOrDefault(p => p.Id == id); // Намерете продукта по неговото ID
            if (product == null)
            {
                return NotFound(); // Върнете грешка, ако продуктът не съществува
            }
            return View(product); // Покажете детайлите за покупка на продукта
        }

        // POST: Потвърждаване на покупката
        [HttpPost]
        public IActionResult ConfirmBuy(int id, string customerName, string address, string paymentMethod, string phoneNumber)
        {
            var product = _db.Store.FirstOrDefault(p => p.Id == id); // Намерете продукта по ID
            if (product == null)
            {
                return NotFound(); // Върнете грешка, ако продуктът не съществува
            }

            // Изтриване на продукта от таблицата Store след покупка
            _db.Store.Remove(product);
            _db.SaveChanges();

            // Логика за потвърждение на поръчката и изпращане на данните към View
            ViewBag.Message = $"Thank you, {customerName}! Your order for {product.Title} has been placed successfully.";
            ViewBag.CustomerName = customerName; // Име на клиента
            ViewBag.Address = address; // Адрес на доставка
            ViewBag.PaymentMethod = paymentMethod; // Метод на плащане
            ViewBag.PhoneNumber = phoneNumber; // Телефонен номер за контакт

            return View("OrderConfirmation", product); // Показване на страницата за потвърждение на поръчката
        }
    }
}
