using shared.Model;

namespace ordination_test.Ordination;

[TestClass]
public class DaligFastTest
{
    [TestMethod]
    public void TestDaligDosis()
    {
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1, 1, "tablet");
        DagligFast d1 = new DagligFast(new DateTime(2021, 1, 10), new DateTime(2021, 1, 12), l, 2, 0, 1, 0);
        DagligFast d2 = new DagligFast(new DateTime(2021, 1, 10), new DateTime(2021, 1, 12), l, 3, 2, 1, 0);
        
        Assert.AreEqual(3, d1.doegnDosis());
        Assert.AreEqual(6, d2.doegnDosis());
    }
    [TestMethod]
    public void TestSamletDosis_MultipliesCorrectly()
    {
        // Arrange: 3 days, 2+1+1+0 = 4 doses per day
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1, 1, "tablet");
        DagligFast d = new DagligFast(new DateTime(2021, 1, 1), new DateTime(2021, 1, 3), l, 2, 1, 1, 0);

        // Act
        double samlet = d.samletDosis();

        // Assert
        Assert.AreEqual(12, samlet); // 4 * 3 = 12
    }

    [TestMethod]
    public void TestGetDoser_ReturnsAllFour()
    {
        Laegemiddel l = new Laegemiddel("Ibuprofen", 1, 1.5, 2, "tablet");
        DagligFast d = new DagligFast(new DateTime(2022, 2, 1), new DateTime(2022, 2, 3), l, 1, 1, 1, 1);

        var doses = d.getDoser();

        Assert.AreEqual(4, doses.Length);
        Assert.AreEqual(1, doses[0].antal); // Morgen
        Assert.AreEqual(1, doses[1].antal); // Middag
        Assert.AreEqual(1, doses[2].antal); // Aften
        Assert.AreEqual(1, doses[3].antal); // Nat
    }
}