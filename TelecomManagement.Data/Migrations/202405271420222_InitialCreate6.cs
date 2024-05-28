namespace TelecomManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bonus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nume = c.String(),
                        MinuteBonus = c.Int(),
                        SMSuriBonus = c.Int(),
                        TraficDateBonus = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractBonus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        BonusId = c.Int(nullable: false),
                        DataIncheiere = c.DateTime(nullable: false),
                        DataExpirare = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonus", t => t.BonusId)
                .ForeignKey("dbo.Contracts", t => t.ContractId)
                .Index(t => t.ContractId)
                .Index(t => t.BonusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractBonus", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.ContractBonus", "BonusId", "dbo.Bonus");
            DropIndex("dbo.ContractBonus", new[] { "BonusId" });
            DropIndex("dbo.ContractBonus", new[] { "ContractId" });
            DropTable("dbo.ContractBonus");
            DropTable("dbo.Bonus");
        }
    }
}
