using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManager.Models
{
    public class Utilisateur
    {
        public Utilisateur(string type)
        {
            this.Type = type;
        }

        public string Type { get; }

        public int? IdOuvrier { get; set; }
    }

    public static class TypeUtilisateur
    {
        public const string Manager = "Manager";
        public const string Ouvrier = "Ouvrier";
        
    }

}