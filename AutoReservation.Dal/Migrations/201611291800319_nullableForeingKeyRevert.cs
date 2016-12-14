namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableForeingKeyRevert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "AutoId", "dbo.Autoes");
            DropForeignKey("dbo.Reservations", "KundeId", "dbo.Kundes");
            DropIndex("dbo.Reservations", new[] { "AutoId" });
            DropIndex("dbo.Reservations", new[] { "KundeId" });
            AlterColumn("dbo.Reservations", "AutoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "KundeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "AutoId");
            CreateIndex("dbo.Reservations", "KundeId");
            AddForeignKey("dbo.Reservations", "AutoId", "dbo.Autoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "KundeId", "dbo.Kundes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "KundeId", "dbo.Kundes");
            DropForeignKey("dbo.Reservations", "AutoId", "dbo.Autoes");
            DropIndex("dbo.Reservations", new[] { "KundeId" });
            DropIndex("dbo.Reservations", new[] { "AutoId" });
            AlterColumn("dbo.Reservations", "KundeId", c => c.Int());
            AlterColumn("dbo.Reservations", "AutoId", c => c.Int());
            CreateIndex("dbo.Reservations", "KundeId");
            CreateIndex("dbo.Reservations", "AutoId");
            AddForeignKey("dbo.Reservations", "KundeId", "dbo.Kundes", "Id");
            AddForeignKey("dbo.Reservations", "AutoId", "dbo.Autoes", "Id");
        }
    }
}
