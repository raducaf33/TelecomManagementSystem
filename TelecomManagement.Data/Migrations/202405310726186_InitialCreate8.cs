namespace TelecomManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plata",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        EstePlatita = c.Boolean(nullable: false),
                        SumaPlata = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataPlata = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plata", "ContractId", "dbo.Contracts");
            DropIndex("dbo.Plata", new[] { "ContractId" });
            DropTable("dbo.Plata");
        }
    }
}
