using System;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Models
{
    public class EditionOuvrierViewModel
    {
        public int? Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public DateTime? DateEmbauche { get; set; }

        public decimal? TauxHoraire { get; set; }

        internal void UpdateOuvrier(Ouvrier ouvrier)
        {
            ouvrier.Nom = this.Nom;
            ouvrier.Prenom = this.Prenom;
            ouvrier.DateEmbauche = this.DateEmbauche.Value;
            ouvrier.TauxHoraire = this.TauxHoraire.Value;
        }
    }
}