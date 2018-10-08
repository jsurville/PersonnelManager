using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManager.Dal.Entites
{
    [Table("RelevesJours")]
    public class ReleveJour
    {
        public int Id { get; set; }

        [Required]
        public int IdReleveMensuel { get; set; }

        [ForeignKey(nameof(IdReleveMensuel))]
        public ReleveMensuel ReleveMensuel { get; set; }

        public DateTime Jour { get; set; }

        public int NombreHeures { get; set; }
    }
}
