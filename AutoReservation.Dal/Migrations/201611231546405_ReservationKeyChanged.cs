namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationKeyChanged : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Reservations");
            DropColumn("dbo.Reservations", "ReservationsNr");
            AddColumn("dbo.Reservations", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Reservations", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Reservations");
            DropColumn("dbo.Reservations", "Id");
            AddColumn("dbo.Reservations", "ReservationsNr", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Reservations", "ReservationsNr");
        }
    }
}
