using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;
using PersonnelManager.Tests;

namespace PersonnelManager.Test
{
    [TestClass]
    public class ServiceEmployeTest
    {
        [TestMethod]
        public void ValiderLongueurNomPrenom()
        {

            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            string text = new string('x', 60);
            var ouvrier = new Ouvrier
            {
                Nom = text,
                Prenom = text,
                DateEmbauche = new DateTime(2018, 01, 01),
                TauxHoraire = 7
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(ouvrier);
            });
            Assert.AreEqual("Le Nom ou Prénom est trop long", exception.Message);
        }


        [TestMethod]
        public void ValiderNomEtPrenomRequis()
        {
            Assert.IsTrue(
                TestsHelper.HasAttribute<Employe, RequiredAttribute>(x => x.Nom));
            Assert.IsTrue(
                TestsHelper.HasAttribute<Employe, RequiredAttribute>(x => x.Prenom));
        }

        [TestMethod]
        public void DateEmbaucheOuvrierPosterieureA1920()
        {
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var fauxDataEmploye = new Mock<IDataEmploye>();

            //fauxDataEmploye.Setup(x => x.EnregistrerOuvrier(It.IsAny<Ouvrier>()));

           // var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var ouvrier = new Ouvrier
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(1920, 12, 31),
                TauxHoraire = 12
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(ouvrier);
            });

            Assert.AreEqual("La date d'embauche doit être > 1920",
                exception.Message);
        }

        [TestMethod]
        public void DateEmbaucheCadrePosterieureA1920()
        {
            // var fauxDataEmploye = new Mock<IDataEmploye>();

            //fauxDataEmploye.Setup(x => x.EnregistrerCadre(It.IsAny<Cadre>()));
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var cadre = new Cadre
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(1920, 12, 31),
                SalaireMensuel = 1500
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerCadre(cadre);
            });

            Assert.AreEqual("La date d'embauche doit être > 1920",
                exception.Message);
        }

        [TestMethod]
        public void DateEmbaucheCadreAnterieureAujourdhuiPlus3Mois()
        {
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var cadre = new Cadre
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(2020, 05, 20),
                SalaireMensuel = 1500
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerCadre(cadre);
            });
            Assert.AreEqual("La date d'embauche ne peut être dans plus de 3 mois",
                exception.Message);

        }

        [TestMethod]
        public void DateEmbaucheOuvrierAnterieureAujourdhuiPlus3Mois()
        {
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var ouvrier = new Ouvrier
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(2019, 05, 20),
                TauxHoraire = 9
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(ouvrier);
            });
            Assert.AreEqual("La date d'embauche ne peut être dans plus de 3 mois",
                exception.Message);
        }

        [TestMethod]
        public void SalaireCadrePositif()
        {
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var cadre = new Cadre
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(2017, 10, 20),
                SalaireMensuel = -1000
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerCadre(cadre);
            });
            Assert.AreEqual("Le salaire mensuel doit être positif",
                exception.Message);
        }

        [TestMethod]
        public void TauxHoraireOuvrierPositif()
        {
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var ouvrier = new Ouvrier
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(2017, 05, 20),
                TauxHoraire = -9
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(ouvrier);
            });
            Assert.AreEqual("Le taux horraire doit être positif",
                exception.Message);
        }

        [TestMethod]
        public void InterdireCaracteresSpeciauxDansNomEtPrenomCadre()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();

            //ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var cadre = new Cadre
            {
                Nom = "Dup-ont dfgd@",
                Prenom = "Géràrd",
                DateEmbauche = new DateTime(2017, 05, 20),
                SalaireMensuel = 4000
            };

            //serviceEmploye.EnregistrerCadre(cadre);
            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerCadre(cadre);
            });
            Assert.AreEqual("Les caractères spéciaux ne sont pas autorisés",
                exception.Message);
        }

        [TestMethod]
        public void InterdireCaracteresSpeciauxDansNomEtPrenomOuvrier()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();

            //ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var ouvrier = new Ouvrier
            {
                Nom = "Dup-ont@ opazeirjz",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(2017, 05, 20),
                TauxHoraire = 10
            };
          // serviceEmploye.EnregistrerOuvrier(ouvrier);

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(ouvrier);
            });
            Assert.AreEqual("Les caractères spéciaux ne sont pas autorisés",
                exception.Message);
        }

        [TestMethod]
        public void OuvrierEstNonNull()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();
            var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            Assert.ThrowsException<InvalidOperationException>(
                () => serviceEmploye.EnregistrerOuvrier(null));

        }

        [TestMethod]
        public void CadreEstNonNull()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();
            var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            Assert.ThrowsException<InvalidOperationException>(
                () => serviceEmploye.EnregistrerCadre(null));

        }
    }


}
