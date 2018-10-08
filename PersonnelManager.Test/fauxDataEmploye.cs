using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManager.Test
{
    class fauxDataEmploye : IDataEmploye
    {
        public void EnregistrerCadre(Cadre cadre)
        {
            throw new NotImplementedException();
        }

        public void EnregistrerOuvrier(Ouvrier ouvrier)
        {
            throw new NotImplementedException();
        }

        public Cadre GetCadre(int idCadre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cadre> GetListeCadres()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ouvrier> GetListeOuvriers()
        {
            throw new NotImplementedException();
        }

        public Ouvrier GetOuvrier(int idOuvrier)
        {
            throw new NotImplementedException();
        }
    }
}
