using shared.Model;

namespace ordination_test.Ordination;

[TestClass]
public class DaligSkæv
{
    [TestMethod]
    public void TestDaligDosis()



    {
        Laegemiddel l = new Laegemiddel("Panodil", 1, 1, 1, "tablet");
        DagligSkæv d1= new DagligSkæv(new DateTime(2021, 1, 23, 12,  0, 0), new DateTime(2021, 1, 24 ), l);
        
        

    }
}