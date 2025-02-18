using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Data;

namespace OnlineShop.Controllers
{
    // Контролер за управление на продукти в магазина
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Конструктор за инициализация на контекста на базата данни
        public StoreController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Действие за зареждане на списъка с продукти
        public IActionResult Index()
        {
            var stores = _db.Store.ToList(); // Извличане на всички продукти от базата данни
            return View(stores); // Предаване на данните към изгледа
        }

        // Действие за показване на формуляра за добавяне на нов продукт
        public IActionResult Create()
        {
            return View(); // Зареждане на изгледа за създаване на продукт
        }

        // Действие за добавяне на нов продукт
        [HttpPost]
        public IActionResult CreateProduct(StoreEntity store, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                // Генериране на уникално име за файла
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);

                // Определяне на пътя за съхранение на изображението
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // Създаване на папката, ако не съществува
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var filePath = Path.Combine(imagesPath, fileName);

                // Запис на файла в сървъра
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                // Запис на пътя към изображението в модела
                store.ImagePath = "/images/" + fileName;
            }

            _db.Store.Add(store); // Добавяне на продукта в базата данни
            _db.SaveChanges(); // Запазване на промените

            return RedirectToAction("Index"); // Пренасочване към списъка с продукти
        }

        // Действие за изтриване на продукт
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var storeItem = _db.Store.FirstOrDefault(s => s.Id == id);
            if (storeItem != null)
            {
                // Изтриване на изображението от сървъра
                if (!string.IsNullOrEmpty(storeItem.ImagePath))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", storeItem.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _db.Store.Remove(storeItem); // Премахване на продукта от базата данни
                _db.SaveChanges(); // Запазване на промените
            }

            return RedirectToAction("Index"); // Пренасочване към списъка с продукти
        }

        // Действие за зареждане на формуляра за редактиране на продукт
        public IActionResult Edit(int id)
        {
            var storeItem = _db.Store.FirstOrDefault(s => s.Id == id);
            if (storeItem == null)
            {
                return NotFound(); // Връщане на 404, ако продуктът не съществува
            }
            return View(storeItem); // Зареждане на формуляра за редактиране
        }

        // Действие за запазване на редактираните данни
        [HttpPost]
        public IActionResult Edit(StoreEntity store, IFormFile Image)
        {
            var storeItem = _db.Store.FirstOrDefault(s => s.Id == store.Id);
            if (storeItem == null)
            {
                return NotFound(); // Връщане на 404, ако продуктът не съществува
            }

            // Ако е качено ново изображение
            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var filePath = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                // Изтриване на старото изображение
                if (!string.IsNullOrEmpty(storeItem.ImagePath))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", storeItem.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Актуализация на пътя към новото изображение
                storeItem.ImagePath = "/images/" + fileName;
            }

            // Актуализация на останалите полета
            storeItem.Title = store.Title;
            storeItem.Price = store.Price;
            storeItem.Category = store.Category;

            _db.SaveChanges(); // Запазване на промените

            return RedirectToAction("Index"); // Пренасочване към списъка с продукти
        }
    }
}
