namespace CodeFirstSample.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Categories",
                c => new
                    {
                        CategoryId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "Suppliers",
                c => new
                    {
                        SupplierCode = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierCode);
            
        }
        
        public override void Down()
        {
            DropIndex("Products", new[] { "CategoryId" });
            DropForeignKey("Products", "CategoryId", "Categories");
            DropTable("Suppliers");
            DropTable("Products");
            DropTable("Categories");
        }
    }
}
