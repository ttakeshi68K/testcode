using System.Data.Entity;

namespace MvcApp.Models
{
    public class MyMvcContext : DbContext
    {
        public DbSet<Book> Books { get; set; } // Booksテーブル
        public DbSet<Review> Reviews { get; set; } // Reviewsテーブル
    }
}