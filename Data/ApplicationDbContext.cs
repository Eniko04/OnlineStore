namespace OnlineShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using OnlineShop.Models;

    // Клас за контекста на базата данни
    public class ApplicationDbContext : DbContext
    {
        // Конструктор, който предава опциите към базовия клас DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet представлява таблицата "Store" в базата данни, базирана на модела StoreEntity
        public DbSet<StoreEntity> Store { get; set; }
    }
}
