using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PersonnelManager.Dal.Context;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Dal.Data
{
    public class DbDataEmploye : IDataEmploye
    {
        private readonly DataContext context = new DataContext();

        public void EnregistrerCadre(Cadre cadre)
        {
            if (cadre.Id == 0)
            {
                this.context.Cadres.Add(cadre);
            }
            else
            {
                this.context.Entry(cadre).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }

        public void EnregistrerOuvrier(Ouvrier ouvrier)
        {
            if (ouvrier.Id == 0)
            {
                this.context.Ouvriers.Add(ouvrier);
            }
            else
            {
                this.context.Entry(ouvrier).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }

        public IEnumerable<Cadre> GetListeCadres()
        {
            return this.context.Cadres.ToList();
        }

        public IEnumerable<Ouvrier> GetListeOuvriers()
        {
            return this.context.Ouvriers.ToList();
        }

        public Ouvrier GetOuvrier(int idOuvrier)
        {
            return this.context.Ouvriers.Find(idOuvrier);
        }

        public Cadre GetCadre(int idCadre)
        {
            return this.context.Cadres.Find(idCadre);
        }
    }
}
