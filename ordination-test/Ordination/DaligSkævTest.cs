using shared.Model;

namespace ordination_test.Ordination;

[TestClass]
public class DaligSkævTest
{
    [TestMethod]
    public void TestDaligDosis()



    {
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1, 1, "tablet");
        DagligSkæv d1 = new DagligSkæv(new DateTime(2025, 10, 10, 12,0,0), new DateTime(2025, 10, 12, 12, 0,0), l);



    }

    [TestMethod]
    public void TestDoegnDosis_CalculatesCorrectly()
    {
        // Arrange
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1.5, 2, "tablet");
        DagligSkæv d = new DagligSkæv(new DateTime(2025, 5, 1), new DateTime(2025, 5, 3), l);
        d.opretDosis(new DateTime(2025, 5, 1, 8, 0, 0), 1);
        d.opretDosis(new DateTime(2025, 5, 1, 12, 0, 0), 0.5);
        d.opretDosis(new DateTime(2025, 5, 1, 18, 0, 0), 1);

        // Act
        double doegnDosis = d.doegnDosis();

        // Assert
        Assert.AreEqual(2.5, doegnDosis, 0.001); // ✅ now correct
    }


    [TestMethod]
    public void TestSamletDosis_MultipliesCorrectly()
    {
        // Arrange
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1.5, 2, "tablet");
        DagligSkæv d = new DagligSkæv(new DateTime(2025, 5, 1), new DateTime(2025, 5, 3), l); // 3 days
        d.opretDosis(new DateTime(2025, 5, 1, 8, 0, 0), 2); // 1 dose per day

        // Act
        double samlet = d.samletDosis();

        // Assert
        Assert.AreEqual(6, samlet, 0.001); // 2 * 3 = 6
    }

    [TestMethod]
    public void TestNoDoses_ReturnsZero()
    {
        // Arrange
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1.5, 2, "tablet");
        DagligSkæv d = new DagligSkæv(new DateTime(2025, 5, 1), new DateTime(2025, 5, 3), l);

        // Act
        double samlet = d.samletDosis();
        double doegn = d.doegnDosis();

        // Assert
        Assert.AreEqual(0, samlet);
        Assert.AreEqual(0, doegn);
    }

    [TestMethod]
    public void TestGetType_ReturnsCorrect()
    {
        // Arrange
        DagligSkæv d = new DagligSkæv();

        // Act
        string type = d.getType();

        // Assert
        Assert.AreEqual("DagligSkæv", type);
    }

    [TestMethod]
    public void TestConstructorWithDosisArray()
    {
        // Arrange
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1.5, 2, "tablet");
        Dosis[] doses = new Dosis[]
        {
            new Dosis(new DateTime(2025, 5, 1, 8, 0, 0), 1),
            new Dosis(new DateTime(2025, 5, 1, 12, 0, 0), 0.5)
        };

        // Act
        DagligSkæv d = new DagligSkæv(new DateTime(2025, 5, 1), new DateTime(2025, 5, 3), l, doses);

        // Assert
        Assert.AreEqual(2, d.doser.Count);
        Assert.AreEqual(1.5, d.doegnDosis(), 0.001);
    }
}
