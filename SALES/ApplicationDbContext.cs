using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SALES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES
{
    public class ApplicationDbContext : DbContext
    {
        //@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True"
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        readonly string stringConnect;
        public ApplicationDbContext() { }
        public ApplicationDbContext(string stringConnect)
        {
            this.stringConnect = stringConnect;
        }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Sale>().HasOne(x => x.Product).WithMany(x => x.Sales);
            //modelBuilder.Entity<Sale>().HasOne(x => x.Employee).WithMany(x => x.Sales);

            var emp = new Employee { Id = 1, Name = "Игорь" };
            var emp2 = new Employee { Id = 2, Name = "Степан" };
            var emp3 = new Employee { Id = 3, Name = "Аверьян" };

            var prod = new Product { Id = 1, Name = "Яблоки" };
            var prod2 = new Product { Id = 2, Name = "Апельсины" };
            var prod3 = new Product { Id = 3, Name = "Бананы" };

            modelBuilder.Entity<Employee>()
                 .HasData(emp, emp2, emp3);

            modelBuilder.Entity<Product>()
                .HasData(prod, prod2, prod3);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SaleDB;Trusted_Connection=True;");
        }
    }
    public class DesignTimeApplicationContext : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SaleDB;Trusted_Connection=True;");
            return new ApplicationDbContext(builder.Options);
        }
    }
}
