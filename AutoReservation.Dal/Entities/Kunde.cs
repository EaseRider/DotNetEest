using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Kunde : IEntitiesInterface
    {
        public DateTime Geburtsdatum { get; set; }
        public int Id { get; set; }
        public string Nachname { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public string Vorname { get; set; }
        [InverseProperty("Kunde")]
        public virtual List<Reservation> Reservationen { get; set; }
    }
}
