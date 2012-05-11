namespace MigrationsCodeDemo.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplierToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products", "SupplierCode", c => c.String(maxLength: 128));
            AddForeignKey("Products", "SupplierCode", "Suppliers", "SupplierCode");
            CreateIndex("Products", "SupplierCode");

            using (var db = new ProductContext())
            {
                db.Suppliers.AddOrUpdate(
                    s => s.SupplierCode,
                    new Supplier { SupplierCode = "UNKNOWN", Name = "Unknown Supplier" }
                );
            }

            Sql("UPDATE Products SET SupplierCode = 'UNKNOWN' WHERE SupplierCode is NULL");
            
        }
        
        public override void Down()
        {
            DropIndex("Products", new[] { "SupplierCode" });
            DropForeignKey("Products", "SupplierCode", "Suppliers");
            DropColumn("Products", "SupplierCode");

        }
    }
}
