using shared.Model;
using System;

namespace shared.Model
{
    public class DagligFast : Ordination
    {
        public Dosis MorgenDosis { get; set; } = new Dosis();
        public Dosis MiddagDosis { get; set; } = new Dosis();
        public Dosis AftenDosis { get; set; } = new Dosis();
        public Dosis NatDosis { get; set; } = new Dosis();

        public DagligFast(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, double morgenAntal, double middagAntal, double aftenAntal, double natAntal)
            : base(laegemiddel, startDen, slutDen)
        {
            MorgenDosis = new Dosis(Util.CreateTimeOnly(6, 0, 0), morgenAntal);
            MiddagDosis = new Dosis(Util.CreateTimeOnly(12, 0, 0), middagAntal);
            AftenDosis = new Dosis(Util.CreateTimeOnly(18, 0, 0), aftenAntal);
            NatDosis = new Dosis(Util.CreateTimeOnly(23, 59, 0), natAntal);
        }

        public DagligFast() : base(null!, new DateTime(), new DateTime()) { }

        public override double samletDosis()
        {
            return base.antalDage() * doegnDosis();
        }

        public override double doegnDosis()
        {
            return MorgenDosis.antal + MiddagDosis.antal + AftenDosis.antal + NatDosis.antal;
        }

        public Dosis[] getDoser()
        {
            return new Dosis[] { MorgenDosis, MiddagDosis, AftenDosis, NatDosis };
        }

        public override string getType()
        {
            return "DagligFast";
        }
    }
}
