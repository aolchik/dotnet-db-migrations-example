// Tutorial EntityFramework
// Ver passo a passo em http://blogs.msdn.com/b/adonet/archive/2011/09/28/ef-4-2-code-first-walkthrough.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity; 
// Necessário pra trabalhar com Entity Framework
// Adicionar no projeto Project -> Add Library Package Reference...
//  "Online" tab
//  "EntityFramework" package
// Ou no Package Manage Console: Install-Package EntityFramework

using System.ComponentModel.DataAnnotations;
// Necessário para DataAnnotation no EntityFramework

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


namespace CodeFirstSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<ProductContext>(new DropCreateDatabaseIfModelChanges<ProductContext>());
            // Recria o banco cada vez que o esquema é modificado.
            // Não usa migrações

            using (var db = new ProductContext())
            {
                // Use Find to locate the Food category 
                var food = db.Categories.Find("FOOD");
                if (food == null)
                {
                    food = new Category { CategoryId = "FOOD", Name = "Foods" };
                    db.Categories.Add(food);
                }
                // Create a new Food product 
                Console.Write("Please enter a name for a new food: ");
                var productName = Console.ReadLine();
                var product = new Product { Name = productName, Category = food };
                db.Products.Add(product);
                int recordsAffected = db.SaveChanges();
                Console.WriteLine("Saved {0} entities to the database.", recordsAffected);
                // Query for all Food products using LINQ 
                var allFoods = from p in db.Products
                               where p.CategoryId == "FOOD"
                               orderby p.Name
                               select p;
                Console.WriteLine("All foods in database:");
                foreach (var item in allFoods)
                {
                    Console.WriteLine(" - {0}", item.Name);
                }
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        } 

    }
}
