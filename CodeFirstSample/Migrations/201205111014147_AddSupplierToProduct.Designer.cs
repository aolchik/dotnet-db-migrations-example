// <auto-generated />
namespace CodeFirstSample.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    
    public sealed partial class AddSupplierToProduct : IMigrationMetadata
    {
        string IMigrationMetadata.Id
        {
            get { return "201205111014147_AddSupplierToProduct"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return "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"; }
        }
    }
}
