using System;
using System.Collections.Generic;
using System.Linq;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Models
{
    public class EditionReleveMensuelViewModel
    {
        public EditionReleveMensuelViewModel()
        {
            this.Jours = new List<EditionReleveJourViewModel>();
        }

        public int Id { get; set; }

        public int IdOuvrier { get; set; }

        public int IdPeriode { get; set; }

        public Periode Periode { get; internal set; }

        public List<EditionReleveJourViewModel> Jours { get; set; }

        public List<EditionReleveJourViewModel[]> GetTableauRelevesJours()
        {
            var tableau = new List<EditionReleveJourViewModel[]>();

            var ligne = new EditionReleveJourViewModel[5];
            tableau.Add(ligne);
            for (var i = DayOfWeek.Monday; i < this.Jours.First().Date.DayOfWeek; i++)
            {
                ligne[(int)i - 1] = null;
            }

            foreach (var releveJour in this.Jours)
            {
                if (releveJour.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    ligne = new EditionReleveJourViewModel[5];
                    tableau.Add(ligne);
                }

                ligne[(int)releveJour.Date.DayOfWeek - 1] = releveJour;
            }

            return tableau;
        }
    }
}