namespace OnlineShop.Models
{
    // Модел, представляващ продукт в магазина
    public class StoreEntity
    {
        public int Id { get; set; } // Първичен ключ, уникален идентификатор за всеки продукт
        public string? Title { get; set; } // Заглавие на продукта
        public double Price { get; set; } // Цена на продукта
        public string? Category { get; set; } // Категория на продукта
        public string? ImagePath { get; set; } // Път към изображението на продукта
    }
}
