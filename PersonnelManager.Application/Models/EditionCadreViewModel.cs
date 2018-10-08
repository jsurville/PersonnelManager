using System;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Models
{
    public class EditionCadreViewModel
    {
        public int? Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public DateTime? DateEmbauche { get; set; }

        public decimal? SalaireMensuel { get; set; }

        internal void UpdateCadre(Cadre cadre)
        {
            cadre.Nom = this.Nom;
            cadre.Prenom = this.Prenom;
            cadre.DateEmbauche = this.DateEmbauche.Value;
            cadre.SalaireMensuel = this.SalaireMensuel.Value;
        }
    }
}