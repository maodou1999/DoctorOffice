namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoggingMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoggingMessages",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Category = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(maxLength: 256),
                        EntryDate = c.DateTime(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoggingMessages");
        }
    }
}
