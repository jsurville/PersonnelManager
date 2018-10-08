using System.Collections.Generic;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Models
{
    public class ListeRelevesViewModel
    {
        public int IdOuvrier { get; set; }

        public IEnumerable<ReleveMensuel> RelevesMensuels { get; set; }

        public IEnumerable<Periode> PeriodesOuvertes { get; set; }
    }
}