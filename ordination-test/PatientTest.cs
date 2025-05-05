using shared.Model;

namespace ordination_test;

[TestClass]
public class PatientTest
{
    [TestMethod]
    public void PatientConstructor_SetsPropertiesCorrectly()
    {
        string cpr = "160563-1234";
        string navn = "John";
        double vægt = 83;

        Patient patient = new Patient(cpr, navn, vægt);

        Assert.AreEqual("John", patient.navn);
        Assert.AreEqual("160563-1234", patient.cprnr);
        Assert.AreEqual(83, patient.vaegt);
    }

    [TestMethod]
    public void ToString_ReturnsCorrectFormat()
    {
        Patient patient = new Patient("010203-1234", "Anna", 55);
        string expected = "Anna 010203-1234";

        Assert.AreEqual(expected, patient.ToString());
    }

    [TestMethod]
    public void OrdinationList_IsEmptyByDefault()
    {
        Patient patient = new Patient("123456-7890", "Peter", 70);
        Assert.AreEqual(0, patient.ordinationer.Count);
    }
}
