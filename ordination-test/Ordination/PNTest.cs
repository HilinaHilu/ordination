using shared.Model;

namespace ordination_test.Ordination;

[TestClass]
public class PNTest {

    [TestMethod]
    public void TestPNLegalDates() {
        Laegemiddel l = new Laegemiddel();
        PN pn = new PN(new DateTime(2025, 5, 5, 12, 0, 0), new DateTime(2025, 5, 10, 12, 0, 0), 1, l);
        Dato dato1 = new Dato
        { DatoId = 0,
            dato = new DateTime(2025, 5, 5, 12, 0, 0)
        };
        Dato dato2 = new Dato
        {
            DatoId = 0,
            dato = new DateTime(2025, 5, 5, 11, 59, 59)
        };

        Dato dato3 = new Dato
        {
            DatoId = 0,
            dato = new DateTime(2025, 5, 10, 12, 0, 0)
        };
        Dato dato4 = new Dato
        {
            DatoId = 0,
            dato = new DateTime(2025, 5, 10, 12, 0, 0, 1)
        };

        Assert.AreEqual(true, pn.givDosis(dato1));
        Assert.AreEqual(false, pn.givDosis(dato2));
        Assert.AreEqual(true, pn.givDosis(dato3));
        Assert.AreEqual(false, pn.givDosis(dato4));
    }

    [TestMethod]
    public void testGivDosis() {
        Laegemiddel l = new Laegemiddel();
        PN pn = new PN(new DateTime(2025, 5, 5, 12, 0, 0), new DateTime(2025, 5, 10, 12, 0, 0), 2, l);
        Dato dato1 = new Dato
        {
            DatoId = 0,
            dato = new DateTime(2025, 5, 5, 12, 0, 0)
        };
        Dato dato4 = new Dato
        {
            DatoId = 0,
            dato = new DateTime(2025, 5, 10, 12, 0, 0, 1)
        };

        Assert.AreEqual(0, pn.samletDosis());
        pn.givDosis(dato1);
        Assert.AreEqual(2, pn.samletDosis());
        pn.givDosis(dato4);
        Assert.AreEqual(2, pn.samletDosis());
    }

    


[TestMethod]
    public void TestGetAntalGangeGivet()
    {
        // Arrange
        Laegemiddel l = new Laegemiddel();
        PN pn = new PN(new DateTime(2025, 5, 5), new DateTime(2025, 5, 10), 1, l);

        // Act & Assert
        Assert.AreEqual(0, pn.getAntalGangeGivet(), "Initially, no doses should have been given.");

        pn.dates.Add(new Dato { DatoId = 1, dato = new DateTime(2025, 5, 5) });
        Assert.AreEqual(1, pn.getAntalGangeGivet(), "After one dose given, count should be 1.");

        pn.dates.Add(new Dato { DatoId = 2, dato = new DateTime(2025, 5, 6) });
        pn.dates.Add(new Dato { DatoId = 3, dato = new DateTime(2025, 5, 7) });
        Assert.AreEqual(3, pn.getAntalGangeGivet(), "After three doses given, count should be 3.");
    }


    // ✅ Test 4: doegnDosis() with valid usage over several days
    [TestMethod]
    public void TestDoegnDosis()
    {
        Laegemiddel l = new Laegemiddel();
        PN pn = new PN(new DateTime(2025, 5, 1), new DateTime(2025, 5, 5), 2, l);

        // Add 2 valid doses
        pn.givDosis(new Dato { dato = new DateTime(2025, 5, 1) });
        pn.givDosis(new Dato { dato = new DateTime(2025, 5, 3) });

        // (2 doses * 2 units) / 5 days = 0.8 units/day
        Assert.AreEqual(0.8, pn.doegnDosis(), 0.001); // Use delta for floating point
    }

    // ✅ Test 5: doegnDosis() with invalid date range
    [TestMethod]
    public void TestDoegnDosis_InvalidPeriod()
    {
        Laegemiddel l = new Laegemiddel();
        PN pn = new PN(new DateTime(2025, 5, 10), new DateTime(2025, 5, 5), 1, l); // Invalid range

        Assert.AreEqual(0, pn.doegnDosis()); // Should safely return 0
    }

    // ✅ Test 6: getType() returns "PN"
    [TestMethod]
    public void TestGetType()
    {
        Laegemiddel l = new Laegemiddel();
        PN pn = new PN(new DateTime(2025, 5, 5), new DateTime(2025, 5, 10), 1, l);

        Assert.AreEqual("PN", pn.getType());
    }

    // ✅ Test 7: Default constructor sets sensible defaults
    [TestMethod]
    public void TestDefaultConstructor()
    {
        PN pn = new PN();

        Assert.AreEqual(0, pn.antalEnheder); // default double
        Assert.IsNotNull(pn.dates);          // list should be initialized
        Assert.AreEqual(0, pn.dates.Count);  // list should be empty
    }

}
