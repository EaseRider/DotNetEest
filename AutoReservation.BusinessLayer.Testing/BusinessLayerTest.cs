using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {

        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }
        
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void UpdateAutoTest()
        {
            Auto auto = Target.GetById<Auto>(1);
            auto.Marke = "Test 1234";
            Target.SaveObject(auto);
            Auto autoChanged = Target.GetById<Auto>(1);
            Assert.AreEqual("Test 1234", autoChanged.Marke, "BusinessLayer: Auto not Updated in DB!");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Kunde kunde = Target.GetById<Kunde>(1);
            kunde.Vorname = "TestingVorname";
            Target.SaveObject(kunde);
            Kunde kundeChanged = Target.GetById<Kunde>(1);
            Assert.AreEqual("TestingVorname", kundeChanged.Vorname, "BusinessLayer: Auto not Updated in DB!");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation reservation = Target.GetById<Reservation>(1);
            reservation.Bis = new DateTime(2111, 11, 11);
            Target.SaveObject(reservation);
            Reservation reservationChanged = Target.GetById<Reservation>(1);
            Assert.IsTrue(new DateTime(2111, 11, 11).Equals(reservationChanged.Bis), 
                "BusinessLayer: Reservation not Updated in DB!");
        }

    }

}
