using Limak.az.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Limak.az.Contexts
{
    public class LimakDbContext : IdentityDbContext<CustomAppUser>
    {
        public LimakDbContext(DbContextOptions<LimakDbContext> dbcontext) : base(dbcontext) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<DeclarationStatus> DeclarationStatuses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderStatus>().HasData(
                   new OrderStatus() { Id = 1, Name = "Bəyan gözləyir" }
                   //new OrderStatus() { Id = 2, Name = "Sifariş verildi" },
                   //new OrderStatus() { Id = 3, Name = "Xarici anbarda" },
                   //new OrderStatus() { Id = 4, Name = "Yolda" },
                   //new OrderStatus() { Id = 5, Name = "Gömrük yoxlanışında" },
                   //new OrderStatus() { Id = 6, Name = "Anbarda" },
                   //new OrderStatus() { Id = 7, Name = "Kuryer çatdırması" },
                   //new OrderStatus() { Id = 8, Name = "Təhvil verildi" }
               );

            modelBuilder.Entity<DeclarationStatus>().HasData(
                   new DeclarationStatus() { Id = 1, Name = "Sifariş verildi" },
                   new DeclarationStatus() { Id = 2, Name = "Ödənildi" },
                   new DeclarationStatus() { Id = 3, Name = "Xarici anbarda" },
                   new DeclarationStatus() { Id = 4, Name = "Yolda" },
                   new DeclarationStatus() { Id = 5, Name = "Gömrük yoxlanışında" },
                   new DeclarationStatus() { Id = 6, Name = "Anbarda" },
                   new DeclarationStatus() { Id = 7, Name = "Kuryer çatdırması" },
                   new DeclarationStatus() { Id = 8, Name = "Təhvil verildi" }
               );

            modelBuilder.Entity<Country>().HasData(
                   new Country() { Id = 1, Name = "USA" },
                   new Country() { Id = 2, Name = "Turkey" },
                   new Country() { Id = 3, Name = "Azerbaijan" }
             );
            modelBuilder.Entity<Currency>().HasData(
                   new Currency() { Id = 1, Name = "USD" },
                   new Currency() { Id = 2, Name = "TRY" },
                   new Currency() { Id = 3, Name = "AZN" }
             );

            modelBuilder.Entity<Warehouse>().HasData(
                   new Warehouse() { Id = 1, Name = "Bakı (İçərişəhər)" },
                   new Warehouse() { Id = 2, Name = "Bakı (Xalqlar Dostluğu)" },
                   new Warehouse() { Id = 3, Name = "Gəncə" },
                   new Warehouse() { Id = 4, Name = "Sumqayıt" },
                   new Warehouse() { Id = 5, Name = "Zaqatala" }
          );
        }
    }

}
