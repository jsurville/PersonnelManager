using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManager.Dal.Entites
{
    [Table("Periodes")]
    public class Periode
    {
        public int Id { get; set; }

        public DateTime PremierJour { get; set; }

        public DateTime DernierJour { get; set; }

        public bool EstFermee { get; set; }
    }
}
