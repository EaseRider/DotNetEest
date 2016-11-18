using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        public int AutoId { get; set; }
        [NotMapped]
        public Auto Auto { get; set; }
        public DateTime Bis { get; set; }
        public int KundeId { get; set; }
        [NotMapped]
        public Kunde Kunde { get; set; }

        [Key]
        public int ReservationsNr { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime Von { get; set; }


    }
}
