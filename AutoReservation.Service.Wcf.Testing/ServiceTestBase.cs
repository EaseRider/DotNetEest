using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            List<AutoDto> list = Target.Autos;
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            List<KundeDto> list = Target.Kunden;
            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            List<ReservationDto> list = Target.Reservationen;
            Assert.AreEqual(3, list.Count);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            AutoDto auto = Target.GetAutoById(1);
            Assert.AreEqual(1, auto.Id);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            KundeDto kunde = Target.GetKundeById(1);
            Assert.AreEqual(1, kunde.Id);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.GetReservationByNr(1);
            Assert.AreEqual(1, reservation.ReservationsNr);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            AutoDto auto = Target.GetAutoById(888);
            Assert.IsNull(auto);
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            KundeDto kunde = Target.GetKundeById(999);
            Assert.IsNull(kunde);
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            ReservationDto reservation = Target.GetReservationByNr(777);
            Assert.IsNull(reservation);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            AutoDto auto = new AutoDto {AutoKlasse = AutoKlasse.Standard, Tagestarif = 22, Marke = "GugusCar"};
            Target.InsertAuto(auto);
            Assert.AreEqual(4, Target.Autos.Count);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            KundeDto kunde = new KundeDto {Geburtsdatum = new DateTime(1977, 4, 15), Nachname = "Müller", Vorname = "Simon"};
            Target.InsertKunde(kunde);
            Assert.AreEqual(5, Target.Kunden.Count);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            ReservationDto res = new ReservationDto
            {
                Auto = Target.GetAutoById(1), Kunde = Target.GetKundeById(1),
                Bis = new DateTime(2017, 1, 1), Von = new DateTime(2016, 12, 31)
            };
            Target.InsertReservation(res);
            Assert.AreEqual(4, Target.Reservationen.Count);
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(Target.GetAutoById(3));
            Assert.AreEqual(2, Target.Autos.Count);
            Assert.AreEqual(4, Target.Kunden.Count);
            Assert.AreEqual(2, Target.Reservationen.Count);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Target.DeleteKunde(Target.GetKundeById(3));
            Assert.AreEqual(3, Target.Autos.Count);
            Assert.AreEqual(3, Target.Kunden.Count);
            Assert.AreEqual(2, Target.Reservationen.Count);
            Target.DeleteKunde(Target.GetKundeById(4));
            Assert.AreEqual(3, Target.Autos.Count);
            Assert.AreEqual(2, Target.Kunden.Count);
            Assert.AreEqual(2, Target.Reservationen.Count);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Target.DeleteReservation(Target.GetReservationByNr(2));
            Assert.AreEqual(3, Target.Autos.Count);
            Assert.AreEqual(4, Target.Kunden.Count);
            Assert.AreEqual(2, Target.Reservationen.Count);
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            AutoDto auto = Target.GetAutoById(1);
            auto.Marke = "NeueMArke";
            Target.UpdateAuto(auto);
            AutoDto autoNew = Target.GetAutoById(1);
            Assert.AreNotSame(auto, autoNew);
            Assert.AreEqual(auto.Marke, autoNew.Marke);
            Assert.AreEqual(auto.Id, autoNew.Id);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            KundeDto kunde = Target.GetKundeById(1);
            kunde.Nachname = "NewNachname";
            Target.UpdateKunde(kunde);
            KundeDto kundeNew = Target.GetKundeById(1);
            Assert.AreNotSame(kunde, kundeNew);
            Assert.AreEqual(kunde.Nachname, kundeNew.Nachname);
            Assert.AreEqual(kunde.Id, kundeNew.Id);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto res = Target.GetReservationByNr(1);
            res.Bis = new DateTime(2141, 3, 4);
            Target.UpdateReservation(res);
            ReservationDto resNew = Target.GetReservationByNr(1);
            Assert.AreNotSame(res, resNew);
            Assert.AreEqual(res.Bis, resNew.Bis);
            Assert.AreEqual(res.ReservationsNr, resNew.ReservationsNr);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto auto1 = Target.GetAutoById(1);
            auto1.Marke = "hansigugus";
            AutoDto auto2 = Target.GetAutoById(1);
            auto2.Tagestarif = 333;
            Target.UpdateAuto(auto1);
            Target.UpdateAuto(auto2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto kunde1 = Target.GetKundeById(1);
            kunde1.Nachname = "Mülller";
            KundeDto kunde2 = Target.GetKundeById(1);
            kunde2.Nachname = "Hansgugugs";
            Target.UpdateKunde(kunde1);
            Target.UpdateKunde(kunde2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto res1 = Target.GetReservationByNr(1);
            res1.Bis = new DateTime(2111, 11, 11);
            ReservationDto res2 = Target.GetReservationByNr(1);
            res2.Bis = new DateTime(2000, 10, 10);
            Target.UpdateReservation(res1);
            Target.UpdateReservation(res2);
        }

        #endregion
    }
}
