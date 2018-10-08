using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PersonnelManager.Dal.Context;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Dal.Data
{
    public class DbDataReleve : IDataReleve
    {
        private readonly DataContext context = new DataContext();

        public void EnregistrerReleveMensuel(ReleveMensuel releveMensuel)
        {
            if (releveMensuel.Id == 0)
            {
                this.context.RelevesMensuels.Add(releveMensuel);
            }
            else
            {
                this.context.Entry(releveMensuel).State = EntityState.Modified;
            }

            foreach (var jour in releveMensuel.Jours)
            {
                if (jour.Id == 0)
                {
                    this.context.RelevesJours.Add(jour);
                }
                else
                {
                    this.context.Entry(jour).State = EntityState.Modified;
                }
            }


            this.context.SaveChanges();
        }
        public IEnumerable<ReleveMensuel> GetListeRelevesMensuels(int idOuvrier)
        {
            return this.context.RelevesMensuels
                .Include(x => x.Periode)
                .Include(x => x.Jours)
                .Where(x => x.IdOuvrier == idOuvrier)
                .ToList();
        }

        public ReleveMensuel GetReleveMensuel(int idReleveMensuel)
        {
            return this.context.RelevesMensuels
                .Include(x => x.Periode)
                .Include(x => x.Jours)
                .Where(x => x.Id == idReleveMensuel)
                .SingleOrDefault();
        }
    }
}
