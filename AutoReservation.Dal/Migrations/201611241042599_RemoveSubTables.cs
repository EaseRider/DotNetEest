namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSubTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LuxusklasseAuto", "Id", "dbo.Autoes");
            DropForeignKey("dbo.MittelklasseAuto", "Id", "dbo.Autoes");
            DropForeignKey("dbo.StandardAuto", "Id", "dbo.Autoes");
            DropIndex("dbo.LuxusklasseAuto", new[] { "Id" });
            DropIndex("dbo.MittelklasseAuto", new[] { "Id" });
            DropIndex("dbo.StandardAuto", new[] { "Id" });
            AddColumn("dbo.Autoes", "Basistarif", c => c.Int());
            AddColumn("dbo.Autoes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.LuxusklasseAuto");
            DropTable("dbo.MittelklasseAuto");
            DropTable("dbo.StandardAuto");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StandardAuto",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MittelklasseAuto",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LuxusklasseAuto",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Basistarif = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Autoes", "Discriminator");
            DropColumn("dbo.Autoes", "Basistarif");
            CreateIndex("dbo.StandardAuto", "Id");
            CreateIndex("dbo.MittelklasseAuto", "Id");
            CreateIndex("dbo.LuxusklasseAuto", "Id");
            AddForeignKey("dbo.StandardAuto", "Id", "dbo.Autoes", "Id");
            AddForeignKey("dbo.MittelklasseAuto", "Id", "dbo.Autoes", "Id");
            AddForeignKey("dbo.LuxusklasseAuto", "Id", "dbo.Autoes", "Id");
        }
    }
}
