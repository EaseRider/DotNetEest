using System;
using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    public class ReservationDto : DtoBase<ReservationDto>
    {
        private int reservationsNr;
        public int ReservationsNr
        {
            get
            {
                return reservationsNr;
            }
            set
            {
                if (reservationsNr != value)
                {
                    reservationsNr = value;
                    OnPropertyChanged(nameof(reservationsNr));
                }
            }
        }

        private AutoDto auto;
        public AutoDto Auto
        {
            get
            {
                return auto;
            }
            set
            {
                if (auto != value)
                {
                    auto = value;
                    OnPropertyChanged(nameof(auto));
                }
            }
        }

        private KundeDto kunde;
        public KundeDto Kunde
        {
            get
            {
                return kunde;
            }
            set
            {
                if (kunde != value)
                {
                    kunde = value;
                    OnPropertyChanged(nameof(kunde));
                }
            }
        }

        private DateTime bis;
        public DateTime Bis
        {
            get
            {
                return bis;
            }
            set
            {
                if (bis != value)
                {
                    bis = value;
                    OnPropertyChanged(nameof(bis));
                }
            }
        }

        private DateTime von;
        public DateTime Von
        {
            get
            {
                return von;
            }
            set
            {
                if (von != value)
                {
                    von = value;
                    OnPropertyChanged(nameof(von));
                }
            }
        }

        private byte[] rowVersion;
        public byte[] RowVersion
        {
            get
            {
                return rowVersion;
            }
            set
            {
                if (rowVersion != value)
                {
                    rowVersion = value;
                    OnPropertyChanged(nameof(rowVersion));
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (Von == DateTime.MinValue)
            {
                error.AppendLine("- Von-Datum ist nicht gesetzt.");
            }
            if (Bis == DateTime.MinValue)
            {
                error.AppendLine("- Bis-Datum ist nicht gesetzt.");
            }
            if (Von > Bis)
            {
                error.AppendLine("- Von-Datum ist grösser als Bis-Datum.");
            }
            if (Auto == null)
            {
                error.AppendLine("- Auto ist nicht zugewiesen.");
            }
            else
            {
                string autoError = Auto.Validate();
                if (!string.IsNullOrEmpty(autoError))
                {
                    error.AppendLine(autoError);
                }
            }
            if (Kunde == null)
            {
                error.AppendLine("- Kunde ist nicht zugewiesen.");
            }
            else
            {
                string kundeError = Kunde.Validate();
                if (!string.IsNullOrEmpty(kundeError))
                {
                    error.AppendLine(kundeError);
                }
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";

    }
}
