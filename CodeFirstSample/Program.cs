using System;
using System.Data.Entity;
using System.Linq;


namespace MigrationsCodeDemo
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