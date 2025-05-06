namespace ordination_test;

using shared.Model;
using System.Security.Cryptography.X509Certificates;

[TestClass]
public class PatientTest
{

    [TestMethod]
    public void PatientHasName()
    {
        string cpr = "160563-1234";
        string navn = "John";
        double vægt = 83;

        Patient patient = new Patient(cpr, navn, vægt);
        Assert.AreEqual(navn, patient.navn);
    }


    [TestMethod]
    public void TestDerAltidFejler()
    {
        string cpr = "160563-1234";
        string navn = "John";
        double vægt = 83;

        Patient patient = new Patient(cpr, navn, vægt);
        Assert.AreNotEqual("Egon", patient.navn);
    }

    [TestMethod]
    public void CPRTest()
    {
        Patient[] validPatients = new Patient[]
        {
            new Patient("160563-1234", "John", 83),
            new Patient("221185-1234", "Joy", 65),
            new Patient("030699-1234", "Law", 22)
        };

        //too long
        Assert.ThrowsException<ArgumentException>(() => Patient.validateCPR("Hallo WORLD, ohh this is a test?!"));
        Assert.ThrowsException<ArgumentException>(() => Patient.validateCPR("3010991234"));

        //not numbers
        Assert.ThrowsException<ArgumentException>(() => Patient.validateCPR("12e456-1234"));
        Assert.ThrowsException<ArgumentException>(() => Patient.validateCPR("123456-a234"));

        //illegal date - somewhat, dates are tricky!!
        Assert.ThrowsException<ArgumentException>(() => Patient.validateCPR("320599-1234"));
        Assert.ThrowsException<ArgumentException>(() => Patient.validateCPR("011399-1234"));
    }


}
