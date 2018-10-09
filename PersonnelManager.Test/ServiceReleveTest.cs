using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Test
{
    [TestClass]
    public class ServiceReleveTest
    {
        [TestMethod]
        public void ValiderNombreHeuresJournalier()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();
            var fauxDataReleve = new Mock<IDataReleve>();
            
            fauxDataEmploye.Setup(x => x.GetOuvrier(It.IsAny<int>())).Returns(new Ouvrier());

            var serviceReleve = new ServiceReleve(fauxDataReleve.Object,fauxDataEmploye.Object);
            ReleveMensuel releveMensuel = new ReleveMensuel();
            
            releveMensuel.Jours.Add(new ReleveJour {
                Jour = DateTime.Now,
                NombreHeures = 15            
            } );

            //serviceReleve.EnregistrerReleveMensuel(releveMensuel);
            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceReleve.EnregistrerReleveMensuel(releveMensuel);
            });
            Assert.AreEqual("Vous avez dépassé la limite horraire journalière",
                exception.Message);
        }

        [TestMethod]
        public void ValiderNombreJoursTravaillés()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();
            var fauxDataReleve = new Mock<IDataReleve>();

            fauxDataEmploye.Setup(x => x.GetOuvrier(It.IsAny<int>())).Returns(new Ouvrier());

            var serviceReleve = new ServiceReleve(fauxDataReleve.Object, fauxDataEmploye.Object);
            ReleveMensuel releveMensuel = new ReleveMensuel();
           
            releveMensuel.Jours.Add(new ReleveJour
            {
                Jour = DateTime.Now,
                NombreHeures = 15
            });

            //serviceReleve.EnregistrerReleveMensuel(releveMensuel);
            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceReleve.EnregistrerReleveMensuel(releveMensuel);
            });
            Assert.AreEqual("Vous avez dépassé la limite horraire journalière",
                exception.Message);
        }
    }
}
