using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PersonnelManager.Dal.Context;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Dal.Data
{
    public class DbDataPeriode : IDataPeriode
    {
        private readonly DataContext context = new DataContext();

        public void EnregistrerPeriode(Periode periode)
        {
            if (periode.Id == 0)
            {
                this.context.Periodes.Add(periode);
            }
            else
            {
                this.context.Entry(periode).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }

        public IEnumerable<Periode> GetListePeriodes()
        {
            return this.context.Periodes.ToList();
        }
        public Periode GetPeriode(int idPeriode)
        {
            return this.context.Periodes.Find(idPeriode);
        }
    }
}
