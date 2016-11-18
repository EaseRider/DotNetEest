using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Auto
    {
        public int Id { get; set; }
        public string Marke { get; set; }
        public byte[] RowVersion { get; set; }
        public int Tagestarif { get; set; }
        public virtual List<Reservation> Reservationen { get; set; }
    }

    [Table("LuxusklasseAuto")]
    public class LuxusklasseAuto : Auto
    {
        public int Basistarif { get; set; }
    }

    [Table("MittelklasseAuto")]
    public class MittelklasseAuto : Auto
    {
        
    }

    [Table("StandardAuto")]
    public class StandardAuto : Auto
    {
        
    }
}
