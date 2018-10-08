using System;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Business
{
    public class SalaireOuvrier : Salaire
    {
        public SalaireOuvrier(DateTime mois, Employe employe)
            : base(mois, employe)
        {
        }

        public decimal NombreHeures { get; set; }

        public decimal TauxHoraire { get; set; }
    }
}
