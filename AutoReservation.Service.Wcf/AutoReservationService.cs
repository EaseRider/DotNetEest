using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private AutoReservationBusinessComponent BusinessComponent;
        public AutoReservationService()
        {
            BusinessComponent = new AutoReservationBusinessComponent();
        }

        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }

        public List<AutoDto> Autos
        {
            get
            {
                WriteActualMethod();
                return BusinessComponent.GetAll<Auto>().ConvertToDtos();
            }
        }

        public List<KundeDto> Kunden
        {
            get
            {
                WriteActualMethod();
                return BusinessComponent.GetAll<Kunde>().ConvertToDtos();
            }
        }

        public List<ReservationDto> Reservationen
        {
            get
            {
                WriteActualMethod();
                return BusinessComponent.GetAll<Reservation>().ConvertToDtos();
            }
        }
        public AutoDto GetAutoById(int id)
        {
            WriteActualMethod();
            return BusinessComponent.GetById<Auto>(id).ConvertToDto();
        }

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            return BusinessComponent.GetById<Kunde>(id).ConvertToDto();
        }

        public ReservationDto GetReservationByNr(int reservationsNr)
        {
            WriteActualMethod();
            return BusinessComponent.GetById<Reservation>(reservationsNr).ConvertToDto();
        }

        public AutoDto InsertAuto(AutoDto autoDto)
        {
            WriteActualMethod();
            Auto auto = autoDto.ConvertToEntity();
            BusinessComponent.SaveObject(auto);
            return auto.ConvertToDto();
        }

        public KundeDto InsertKunde(KundeDto kundeDto)
        {
            WriteActualMethod();
            Kunde kunde = kundeDto.ConvertToEntity();
            BusinessComponent.SaveObject(kunde);
            return kunde.ConvertToDto();
        }

        public ReservationDto InsertReservation(ReservationDto reservationDto)
        {
            WriteActualMethod();
            Reservation reservation = reservationDto.ConvertToEntity();
            BusinessComponent.SaveObject(reservation);
            return reservation.ConvertToDto();
        }

        public AutoDto UpdateAuto(AutoDto autoDto)
        {
            WriteActualMethod();
            Auto auto = autoDto.ConvertToEntity();
            BusinessComponent.SaveObject(auto);
            return auto.ConvertToDto();
        }

        public KundeDto UpdateKunde(KundeDto kundeDto)
        {
            WriteActualMethod();
            Kunde kunde = kundeDto.ConvertToEntity();
            BusinessComponent.SaveObject(kunde);
            return kunde.ConvertToDto();
        }

        public ReservationDto UpdateReservation(ReservationDto reservationDto)
        {
            WriteActualMethod();
            Reservation reservation = reservationDto.ConvertToEntity();
            BusinessComponent.SaveObject(reservation);
            return reservation.ConvertToDto();
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            BusinessComponent.DeleteObject(auto.ConvertToEntity());
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            BusinessComponent.DeleteObject(kunde.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            BusinessComponent.DeleteObject(reservation.ConvertToEntity());
        }
    }
}