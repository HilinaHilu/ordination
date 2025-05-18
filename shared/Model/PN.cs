using shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace shared.Model
{
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

        public PN() : base(null!, new DateTime(), new DateTime()) { }

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
            if (dates.Count == 0) return 0;
            DateTime første = dates.Min(d => d.dato);
            DateTime sidste = dates.Max(d => d.dato);
            int antalDage = (sidste - første).Days + 1;
            if (antalDage <= 0) return 0;
            return (getAntalGangeGivet() * antalEnheder) / (double)antalDage;
        }

        public override double samletDosis()
        {
            return dates.Count * antalEnheder;
        }

        public int getAntalGangeGivet()
        {
            return dates.Count;
        }

        public override string getType()
        {
            return "PN";
        }
    }
}