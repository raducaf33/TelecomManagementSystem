namespace TelecomManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataIncheiere = c.DateTime(nullable: false),
                        DataExpirare = c.DateTime(nullable: false),
                        AbonamentId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Abonaments", t => t.AbonamentId)
                .ForeignKey("dbo.Clienti", t => t.ClientId)
                .Index(t => t.AbonamentId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "ClientId", "dbo.Clienti");
            DropForeignKey("dbo.Contracts", "AbonamentId", "dbo.Abonaments");
            DropIndex("dbo.Contracts", new[] { "ClientId" });
            DropIndex("dbo.Contracts", new[] { "AbonamentId" });
            DropTable("dbo.Contracts");
        }
    }
}
