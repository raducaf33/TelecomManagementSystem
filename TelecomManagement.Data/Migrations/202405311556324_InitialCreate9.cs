namespace TelecomManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        SumaTotalaPlata = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataEmitere = c.DateTime(nullable: false),
                        DataScadenta = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Factura", "ContractId", "dbo.Contracts");
            DropIndex("dbo.Factura", new[] { "ContractId" });
            DropTable("dbo.Factura");
        }
    }
}
