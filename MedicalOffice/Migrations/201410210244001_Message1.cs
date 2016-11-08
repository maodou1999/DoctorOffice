namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Message1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocFiles", "Message_Id", c => c.Long());
            CreateIndex("dbo.DocFiles", "Message_Id");
            AddForeignKey("dbo.DocFiles", "Message_Id", "dbo.Messages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocFiles", "Message_Id", "dbo.Messages");
            DropIndex("dbo.DocFiles", new[] { "Message_Id" });
            DropColumn("dbo.DocFiles", "Message_Id");
        }
    }
}
