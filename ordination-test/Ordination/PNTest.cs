using Microsoft.VisualStudio.TestTools.UnitTesting;
using shared.Model;
using System;

namespace ordination_test.Ordination
{
    [TestClass]
    public class PNTest
    {
        [TestMethod]
        public void TestDoegnDosis_CorrectCalculation()
        {
            Laegemiddel l = new Laegemiddel();
            PN pn = new PN(new DateTime(2025, 5, 1), new DateTime(2025, 5, 5), 2, l);

            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 1) });
            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 3) });

            // 2 doses * 2 units = 4 / 3 days (May 1 to May 3) = 1.333...
            Assert.AreEqual(1.333, pn.doegnDosis(), 0.001);
        }

        [TestMethod]
        public void TestDoegnDosis_SameDayMultipleDoses()
        {
            Laegemiddel l = new Laegemiddel();
            PN pn = new PN(new DateTime(2025, 5, 1), new DateTime(2025, 5, 5), 1, l);

            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 2) });
            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 2) });

            // 2 doses * 1 unit = 2 / 1 day = 2.0
            Assert.AreEqual(2.0, pn.doegnDosis(), 0.001);
        }

        [TestMethod]
        public void TestDoegnDosis_NoDoses()
        {
            Laegemiddel l = new Laegemiddel();
            PN pn = new PN(new DateTime(2025, 5, 1), new DateTime(2025, 5, 5), 1, l);

            // No doses given
            Assert.AreEqual(0.0, pn.doegnDosis(), 0.001);
        }

        [TestMethod]
        public void TestSamletDosis_Correct()
        {
            Laegemiddel l = new Laegemiddel();
            PN pn = new PN(new DateTime(2025, 5, 1), new DateTime(2025, 5, 5), 1.5, l);

            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 2) });
            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 3) });

            Assert.AreEqual(3.0, pn.samletDosis(), 0.001);
        }

        [TestMethod]
        public void TestGetAntalGangeGivet_Counter()
        {
            Laegemiddel l = new Laegemiddel();
            PN pn = new PN(new DateTime(2025, 5, 1), new DateTime(2025, 5, 5), 1, l);

            Assert.AreEqual(0, pn.getAntalGangeGivet());

            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 1) });
            Assert.AreEqual(1, pn.getAntalGangeGivet());

            pn.givDosis(new Dato { dato = new DateTime(2025, 5, 2) });
            Assert.AreEqual(2, pn.getAntalGangeGivet());
        }

        [TestMethod]
        public void TestGetType_ReturnsPN()
        {
            PN pn = new PN();
            Assert.AreEqual("PN", pn.getType());
        }

        [TestMethod]
        public void TestDefaultConstructor_InitializesCorrectly()
        {
            PN pn = new PN();
            Assert.AreEqual(0, pn.antalEnheder);
            Assert.IsNotNull(pn.dates);
            Assert.AreEqual(0, pn.dates.Count);
        }
    }
}
