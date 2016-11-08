namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageChanges",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 256),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        RecepientId = c.String(nullable: false, maxLength: 128),
                        SenderId = c.String(maxLength: 128),
                        ReadByRecepient = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecepientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.RecepientId)
                .Index(t => t.SenderId);
            
            AddColumn("dbo.DocFiles", "MessageChange_Id", c => c.Long());
            CreateIndex("dbo.DocFiles", "MessageChange_Id");
            AddForeignKey("dbo.DocFiles", "MessageChange_Id", "dbo.MessageChanges", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageChanges", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageChanges", "RecepientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DocFiles", "MessageChange_Id", "dbo.MessageChanges");
            DropIndex("dbo.MessageChanges", new[] { "SenderId" });
            DropIndex("dbo.MessageChanges", new[] { "RecepientId" });
            DropIndex("dbo.DocFiles", new[] { "MessageChange_Id" });
            DropColumn("dbo.DocFiles", "MessageChange_Id");
            DropTable("dbo.MessageChanges");
        }
    }
}
