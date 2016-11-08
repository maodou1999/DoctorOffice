namespace CherryCitySoftware.MedicalOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OfficeHour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Telphone = c.String(),
                        Fax = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HolidayNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Holidays = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfficeHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monday = c.String(maxLength: 128),
                        Tuesday = c.String(maxLength: 128),
                        Wednesday = c.String(maxLength: 128),
                        Thursday = c.String(maxLength: 128),
                        Friday = c.String(maxLength: 128),
                        Saturday = c.String(maxLength: 128),
                        Sunday = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OfficeHours");
            DropTable("dbo.HolidayNotes");
            DropTable("dbo.ContactInformations");
        }
    }
}
