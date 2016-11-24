using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation: IEntitiesInterface
    {
        public int AutoId { get; set; }
        [ForeignKey("AutoId"), InverseProperty("Reservationen")]
        public Auto Auto { get; set; }
        public DateTime Bis { get; set; }
        public int KundeId { get; set; }
        [ForeignKey("KundeId"), InverseProperty("Reservationen")]
        public Kunde Kunde { get; set; }

        [NotMapped]
        public int ReservationsNr {
            get { return Id; }
            set { Id = value; } }

        [Key]
        public int Id {get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public DateTime Von { get; set; }


    }
}
