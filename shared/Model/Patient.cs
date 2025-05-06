
namespace shared.Model;

public class Patient
{
    public int PatientId { get; set; }
    public String cprnr { get; set; }
    public String navn { get; set; }
    public double vaegt { get; set; }
    public List<Ordination> ordinationer { get; set; } = new List<Ordination>();

    public Patient(String cprnr, String navn, double vaegt)
    {
        validateCPR(cprnr);
        if (vaegt < 1)
        {
            throw new ArgumentException("Weight must be a positive number");
        }
        this.cprnr = cprnr;
        this.navn = navn;
        this.vaegt = vaegt;
    }

    public Patient()
    {
        this.cprnr = "";
        this.navn = "";
    }

    public static void validateCPR(string cpr)
    {
        if (cpr.Length != 11)
        {
            throw new ArgumentException("A CPR number must be 11 characters long, the first 6 must be able to create a date, and the last 4 a number separated by '-'");
        }
        string firstSix = cpr.Substring(0, 6);
        string lastFour = cpr.Substring(7);
        try
        {
            int.Parse(firstSix);
        }
        catch
        {
            throw new ArgumentException("the first six must be digits from 0-9");
        }
        try
        {
            int.Parse(lastFour);
        }
        catch
        {
            throw new ArgumentException("the last four must be digits from 0-9");
        }
        try
        {
            int day = int.Parse(firstSix.Substring(0, 2));
            int month = int.Parse(firstSix.Substring(2, 2));
            int year = int.Parse(firstSix.Substring(4, 2));
            DateTime date = new DateTime(2000 + year, month, day); //Stupid way to write dates!!
        }
        catch
        {
            throw new ArgumentException("This is not a legal date");
        }

    }

    public override String ToString()
    {
        return navn + " " + cprnr;
    }
}