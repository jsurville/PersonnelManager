using System;
using System.Collections.Generic;
using System.Linq;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Business.Services
{
    public class ServicePeriode
    {
        private readonly IDataPeriode dataPeriode;

        public ServicePeriode(IDataPeriode dataPeriode)
        {
            this.dataPeriode = dataPeriode;
        }

        public void OuvrirPeriode(DateTime date)
        {
            var premierJourMois = new DateTime(date.Year, date.Month, 1);
            if (this.GetListePeriodes().Any(x => x.PremierJour == premierJourMois))
            {
                throw new BusinessException("Cette période existe déjà.");
            }

            var periode = new Periode
            {
                PremierJour = premierJourMois,
                DernierJour = premierJourMois.AddMonths(1).AddDays(-1)
            };

            this.dataPeriode.EnregistrerPeriode(periode);
        }

        public void FermerPeriode(int idPeriode)
        {
            var periode = this.dataPeriode.GetPeriode(idPeriode);
            periode.EstFermee = true;
            this.dataPeriode.EnregistrerPeriode(periode);
        }

        public IEnumerable<Periode> GetListePeriodes()
        {
            return this.dataPeriode.GetListePeriodes().OrderBy(x => x.PremierJour);
        }

        public Periode GetPeriode(int idPeriode)
        {
            return this.dataPeriode.GetPeriode(idPeriode);
        }

        public IEnumerable<DateTime> GetListeJoursTravailles(DateTime debut, DateTime fin)
        {
            var listeDates = new List<DateTime>();

            var current = debut;
            while (current <= fin)
            {
                if (current.DayOfWeek != DayOfWeek.Saturday
                    && current.DayOfWeek != DayOfWeek.Sunday)
                {
                    listeDates.Add(current);
                }

                current = current.AddDays(1);
            }

            return listeDates;
        }
    }
}
