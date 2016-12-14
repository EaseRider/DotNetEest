using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Dal.Entities
{
    public interface IEntitiesInterface
    {
        int Id { get; set; }
        byte[] RowVersion { get; set; }
    }
}
