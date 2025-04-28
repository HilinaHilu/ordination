using shared.Model;

namespace ordination_test.Ordination;

[TestClass]
public class DaligFast
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
}