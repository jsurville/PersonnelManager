using System;

namespace PersonnelManager.Models
{
    public class EditionReleveJourViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int? NombreHeures { get; set; }

        public bool JourNonTravaille { get; set; }
    }
}