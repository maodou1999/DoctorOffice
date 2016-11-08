namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Message : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ReadByRecepient", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Messages", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Content", c => c.String(nullable: false, maxLength: 4000));
            DropColumn("dbo.Messages", "ReadByRecepient");
        }
    }
}
