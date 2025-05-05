namespace ordination_test;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<OrdinationContext>()
            .UseInMemoryDatabase("test-db")
            .Options;

        var context = new OrdinationContext(options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        var patienter = service.GetPatienter();
        Assert.IsTrue(patienter.Count > 0);
    }

    [TestMethod]
    public void OpretDagligFast_OpdatererListe()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            1, 1, 1, 1, DateTime.Today, DateTime.Today.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    [TestMethod]
    public void OpretDaligSkæv_OpdatereListe()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
        Dosis[] doser = new Dosis[0];

        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            doser , DateTime.Today, DateTime.Today.AddDays(3));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());

    }

    [TestMethod]
    public void OpretPN_OpdatereListe()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
       

        Assert.AreEqual(4, service.GetPNs().Count());

        service.OpretPN(patient.PatientId, lm.LaegemiddelId, 1.5
            , DateTime.Today, DateTime.Today.AddDays(3));

        Assert.AreEqual(5, service.GetPNs().Count());

    }

    [TestMethod]
    public void ServiceGivesPNDoseTest() {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        PN pn = service.OpretPN(patient.PatientId, lm.LaegemiddelId, 1.5
    , DateTime.Today.AddDays(-1), DateTime.Today.AddDays(3));

        Dato dato = new Dato { dato = DateTime.Now };

        Assert.AreEqual(0, pn.getAntalGangeGivet());

        service.AnvendOrdination(pn.OrdinationId, dato);
    }

}