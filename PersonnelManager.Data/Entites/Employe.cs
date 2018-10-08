using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManager.Dal.Entites
{
    public abstract class Employe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        public DateTime DateEmbauche { get; set; }

        public StatutEmploye Statut { get; protected set; }
    }
}
