namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourceLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResourceLinks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ResourceLinkName = c.String(),
                        ResourceLinkAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
           
        }
        
       
    }
}
