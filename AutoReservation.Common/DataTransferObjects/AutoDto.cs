using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    public class AutoDto : DtoBase<AutoDto>
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

        private int basistarif;
        public int Basistarif
        {
            get
            {
                return basistarif;
            }
            set
            {
                if (basistarif != value)
                {
                    basistarif = value;
                    OnPropertyChanged(nameof(basistarif));
                }
            }
        }

        private string marke;
        public string Marke
        {
            get
            {
                return marke;
            }
            set
            {
                if (marke != value)
                {
                    marke = value;
                    OnPropertyChanged(nameof(marke));
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

        private int tagestarif;
        public int Tagestarif
        {
            get
            {
                return tagestarif;
            }
            set
            {
                if (tagestarif != value)
                {
                    tagestarif = value;
                    OnPropertyChanged(nameof(tagestarif));
                }
            }
        }

        private AutoKlasse autoKlasse;
        public AutoKlasse AutoKlasse
        {
            get
            {
                return autoKlasse;
            }
            set
            {
                if (autoKlasse != value)
                {
                    autoKlasse = value;
                    OnPropertyChanged(nameof(autoKlasse));
                }
            }
        }
        
        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}";

    }
}
