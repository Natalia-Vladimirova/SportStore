using System.Data.Entity;
using DAL.Interfaces.Entities;

namespace DAL
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Tovar> Tovars { get; set; }

        public DbSet<Categ> Categs { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}