namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Messages", new[] { "Recepient_Id" });
            AlterColumn("dbo.Messages", "Recepient_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Messages", "Recepient_Id");
            AddForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Messages", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Recepient_Id" });
            AlterColumn("dbo.Messages", "Recepient_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "Recepient_Id");
            CreateIndex("dbo.Messages", "ApplicationUser_Id");
            AddForeignKey("dbo.Messages", "Recepient_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
