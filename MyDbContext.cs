using Microsoft.EntityFrameworkCore;

namespace ActionFilterDemo;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books {get; set;}
    public DbSet<Person> People {get; set;}
}
