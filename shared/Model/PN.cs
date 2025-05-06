using shared.Model;

public class PN : Ordination
{
    public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN(DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen)
    {
        if (slutDen < startDen)
        {
            throw new ArgumentException("Slutdato kan ikke være før startdato");
        }
        this.antalEnheder = antalEnheder;
    }

    public PN() : base(null!, new DateTime(), new DateTime())
    {
    }

    public bool givDosis(Dato givesDen)
    {
        if (givesDen.dato >= startDen && givesDen.dato <= slutDen)
        {
            dates.Add(givesDen);
            return true;
        }
        return false;
    }

    public override double doegnDosis()
    {
        int antalDage = (slutDen - startDen).Days + 1;
        if (antalDage <= 0) return 0;  // Defensive coding, in case dates are invalid

        return (getAntalGangeGivet() * antalEnheder) / (double)antalDage;
    }

    public override double samletDosis()
    {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet()
    {
        return dates.Count();
    }

    public override String getType()
    {
        return "PN";
    }
}
