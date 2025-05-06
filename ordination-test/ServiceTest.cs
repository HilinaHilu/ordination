namespace ordination_test;

using Microsoft.EntityFrameworkCore;
using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
        Assert.IsTrue(service.GetPatienter().Count > 0);
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        var patient = service.GetPatienter().First();
        var lm = service.GetLaegemidler().First();
        int initialCount = service.GetDagligFaste().Count();

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId, 2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(initialCount + 1, service.GetDagligFaste().Count());
    }

    [TestMethod]
    public void OpretDagligSkaev()
    {
        var patient = service.GetPatienter().First();
        var lm = service.GetLaegemidler().First();
        int initialCount = service.GetDagligSkæve().Count();

        var doser = new Dosis[]
        {
            new Dosis(DateTime.Now, 1),
            new Dosis(DateTime.Now.AddHours(1), 2),
            new Dosis(DateTime.Now.AddHours(2), 3),
        };

        var sk = service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId, doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.IsTrue(sk.OrdinationId > 0);
        Assert.AreEqual(initialCount + 1, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    public void OpretPN()
    {
        var patient = service.GetPatienter().First();
        var lm = service.GetLaegemidler().First();
        int initialCount = service.GetPNs().Count();

        var pn = service.OpretPN(patient.PatientId, lm.LaegemiddelId, 2, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.IsTrue(pn.OrdinationId > 0);
        Assert.AreEqual(initialCount + 1, service.GetPNs().Count());
    }

    [TestMethod]
    public void AnvendPN()
    {
        var patient = service.GetPatienter().First();
        var lm = service.GetLaegemidler().First();

        var pn = service.OpretPN(patient.PatientId, lm.LaegemiddelId, 2, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(3));
        int id = pn.OrdinationId;

        Assert.AreEqual(0, pn.getAntalGangeGivet());

        var response = service.AnvendOrdination(id, new Dato { dato = DateTime.Now });

        Assert.AreEqual("dosis have been given", response);

        pn = service.GetPNs().FirstOrDefault(x => x.OrdinationId == id);
        Assert.AreEqual(1, pn.getAntalGangeGivet());
    }
}
