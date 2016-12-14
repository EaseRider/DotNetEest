using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract(Name = "IAutoReservationService")]
    public interface IAutoReservationService
    {
        List<AutoDto> Autos
        {
            [OperationContract]
            get;
        }
        List<KundeDto> Kunden
        {
            [OperationContract]
            get;
        }
        List<ReservationDto> Reservationen
        {
            [OperationContract]
            get;
        }

        [OperationContract]
        AutoDto GetAutoById(int id);

        [OperationContract]
        KundeDto GetKundeById(int id);

        [OperationContract]
        ReservationDto GetReservationByNr(int reservationsNr);

        [OperationContract]
        AutoDto InsertAuto(AutoDto auto);

        [OperationContract]
        KundeDto InsertKunde(KundeDto kunde);

        [OperationContract]
        ReservationDto InsertReservation(ReservationDto reservation);

        [OperationContract]
        AutoDto UpdateAuto(AutoDto auto);

        [OperationContract]
        KundeDto UpdateKunde(KundeDto kunde);

        [OperationContract]
        ReservationDto UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void DeleteAuto(AutoDto auto);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);

    }
}
