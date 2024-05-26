namespace TelecomManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abonaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nume = c.String(),
                        Pret = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinuteIncluse = c.Int(nullable: false),
                        SMSuriIncluse = c.Int(nullable: false),
                        TraficDateInclus = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Abonaments");
        }
    }
}
