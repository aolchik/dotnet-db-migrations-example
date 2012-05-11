using System;
using System.Collections.Generic;
using System.Text;

using System.Data.Entity;
// Necessário pra trabalhar com Entity Framework
// Adicionar no projeto Project -> Add Library Package Reference...
//  "Online" tab
//  "EntityFramework" package
// Ou no Package Manage Console: Install-Package EntityFramework

using System.ComponentModel.DataAnnotations;
// Necessário para DataAnnotation no EntityFramework

namespace MigrationsCodeDemo
{
    public class Category
    {
        public string CategoryId { get; set; }
        // Chave primária detectada por convenção a partir do uso de Id

        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }


    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string SupplierCode { get; set; }
        public virtual Supplier Supplier { get; set; }
    }

    public class Supplier
    {
        [Key]
        public string SupplierCode { get; set; }
        public string Name { get; set; }
    }

    public class ProductContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        // Uso de Fluent API para validações mais complicadas
        // Neste caso poderíamos ter utilizado Data Annotation RequiredAttribute no DbSet
        {
            modelBuilder.Entity<Supplier>()
                .Property(s => s.Name)
                .IsRequired();
        }
    }



}