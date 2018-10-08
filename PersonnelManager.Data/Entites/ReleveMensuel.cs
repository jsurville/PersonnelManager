using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManager.Dal.Entites
{
    [Table("RelevesMensuels")]
    public class ReleveMensuel
    {
        public ReleveMensuel()
        {
            this.Jours = new List<ReleveJour>();
        }

        public int Id { get; set; }

        [Required]
        public int IdPeriode { get; set; }

        [ForeignKey(nameof(IdPeriode))]
        public Periode Periode { get; set; }

        [Required]
        public int IdOuvrier { get; set; }

        [ForeignKey(nameof(IdOuvrier))]
        public Ouvrier Ouvrier { get; set; }

        public int NombreTotalHeures { get; set; }

        public decimal TauxHoraire { get; set; }

        public ICollection<ReleveJour> Jours { get; set; }
    }
}
