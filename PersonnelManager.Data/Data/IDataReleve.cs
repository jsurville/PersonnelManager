using System.Collections.Generic;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Dal.Data
{
    public interface IDataReleve
    {
        void EnregistrerReleveMensuel(ReleveMensuel releveMensuel);

        IEnumerable<ReleveMensuel> GetListeRelevesMensuels(int idOuvrier);

        ReleveMensuel GetReleveMensuel(int idReleveMensuel);
    }
}
