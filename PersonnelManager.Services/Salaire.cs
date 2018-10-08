using System;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Business
{
    public class Salaire
    {
        public Salaire(DateTime mois, Employe employe)
        {

        }

        public DateTime Mois { get; set; }

        public Employe Employe { get; set; }


        public decimal SalaireTotal { get; set; }
    }
}
