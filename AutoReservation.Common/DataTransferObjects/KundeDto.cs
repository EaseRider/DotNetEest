using System;
using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    public class KundeDto : DtoBase<KundeDto>
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
        }

        private DateTime geburtsDatum;
        public DateTime Geburtsdatum
        {
            get
            {
                return geburtsDatum;
            }
            set
            {
                if (geburtsDatum != value)
                {
                    geburtsDatum = value;
                    OnPropertyChanged(nameof(geburtsDatum));
                }
            }
        }

        private string nachname;
        public string Nachname
        {
            get
            {
                return nachname;
            }
            set
            {
                if (nachname != value)
                {
                    nachname = value;
                    OnPropertyChanged(nameof(nachname));
                }
            }
        }

        private string vorname;
        public string Vorname
        {
            get
            {
                return vorname;
            }
            set
            {
                if (vorname != value)
                {
                    vorname = value;
                    OnPropertyChanged(nameof(vorname));
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
            if (string.IsNullOrEmpty(Nachname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Vorname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Geburtsdatum == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}";

    }
}
