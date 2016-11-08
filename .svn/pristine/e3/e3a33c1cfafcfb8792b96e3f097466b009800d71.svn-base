namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Message3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Recepient_Id" });
            AlterColumn("dbo.Messages", "Recepient_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Messages", "Recepient_Id");
            AddForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Recepient_Id" });
            AlterColumn("dbo.Messages", "Recepient_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "Recepient_Id");
            AddForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
