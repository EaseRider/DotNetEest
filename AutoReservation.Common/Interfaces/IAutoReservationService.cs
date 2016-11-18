using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        List<AutoDto> Autos { get; }
        List<KundeDto> Kunden { get; }
        List<ReservationDto> Reservationen { get; }
        AutoDto GetAutoById(int id);
        KundeDto GetKundeById(int id);
        ReservationDto GetReservationByNr(int reservationsNr);
        AutoDto InsertAuto(AutoDto auto);
        KundeDto InsertKunde(KundeDto kunde);
        ReservationDto InsertReservation(ReservationDto reservation);
        AutoDto UpdateAuto(AutoDto auto);
        KundeDto UpdateKunde(KundeDto kunde);
        ReservationDto UpdateReservation(ReservationDto reservation);
        void DeleteAuto(AutoDto auto);
        void DeleteKunde(KundeDto kunde);
        void DeleteReservation(ReservationDto reservation);

    }
}
