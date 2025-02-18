namespace OnlineShop.Models;

// Модел за управление на грешки в приложението
public class ErrorViewModel
{
    // ID на заявката, което се използва за проследяване на грешки
    public string? RequestId { get; set; }

    // Свойство, което показва дали RequestId е налично
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
