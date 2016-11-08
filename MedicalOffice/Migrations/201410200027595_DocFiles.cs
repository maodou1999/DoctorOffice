namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 1024),
                        Content = c.Binary(nullable: false),
                        FileType = c.String(nullable: false, maxLength: 512),
                        FileName = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DocFiles");
        }
    }
}
