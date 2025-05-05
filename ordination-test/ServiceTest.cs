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

        Assert.AreEqual(2, service.GetDagligFaste());
    }
    [TestMethod]
   
    public void AnvendOrdination_ValidPNOrdination_ReturnsSuccess()
    {
        var pn = service.GetPNs().First();
        var dato = new Dato { dato = pn.startDen.AddDays(1) };

        var result = service.AnvendOrdination(pn.OrdinationId, dato);

        Assert.AreEqual("Dosis givet", result);
        Assert.IsTrue(pn.dates.Any(d => d.dato.Date == dato.dato.Date));
    }
 // Verifying the date when the dose was applied
    

    [TestMethod]
    public void AnvendOrdination_InvalidOrdinationId_ReturnsNotFound()
    {
        var result = service.AnvendOrdination(-99, new Dato { dato = DateTime.Today });

        Assert.AreEqual("Ordination findes ikke", result);  // Message when invalid ordination ID
    }

    [TestMethod]
    public void AnvendOrdination_InvalidOrdinationType_ReturnsInvalidType()
    {
        var dagligFast = service.GetDagligFaste().First();
        var result = service.AnvendOrdination(dagligFast.OrdinationId, new Dato { dato = DateTime.Today });

        Assert.AreEqual("Kun PN ordinationer kan anvendes.", result);
    }


    [TestMethod]
    public void AnvendOrdination_OutOfDateRange_ReturnsOutOfRangeError()
    {
        var pn = service.GetPNs().First();
        var dato = new Dato { dato = pn.slutDen.AddDays(5) };

        var result = service.AnvendOrdination(pn.OrdinationId, dato);

        Assert.AreEqual("Dato er udenfor gyldighedsperioden for ordinationen.", result);
    }


    [TestMethod]
    public void AnvendOrdination_DuplicateDate_ReturnsAlreadyUsedError()
    {
        var pn = service.GetPNs().First();
        var dato = new Dato { dato = pn.startDen.AddDays(2) };

        service.AnvendOrdination(pn.OrdinationId, dato); // First time
        var result = service.AnvendOrdination(pn.OrdinationId, dato); // Second time

        Assert.AreEqual("Ordination allerede anvendt på denne dato.", result);
    }

}