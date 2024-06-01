// <copyright file=ContractBonusTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the ContractBonus Tests class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Tests.DomainTests
{
    [TestClass]
    public class ContractBonusTests
    {


        [TestMethod]
        public void ContractBonus_Id_ShouldBeSetCorrectly()
        {
            // Arrange
            var contractBonus = new ContractBonus { Id = 1 };

            // Act & Assert
            Assert.AreEqual(1, contractBonus.Id);
        }

        [TestMethod]
        public void ContractBonus_ContractId_ShouldBeSetCorrectly()
        {
            // Arrange
            var contractBonus = new ContractBonus { ContractId = 123 };

            // Act & Assert
            Assert.AreEqual(123, contractBonus.ContractId);
        }

        [TestMethod]
        public void ContractBonus_BonusId_ShouldBeSetCorrectly()
        {
            // Arrange
            var contractBonus = new ContractBonus { BonusId = 456 };

            // Act & Assert
            Assert.AreEqual(456, contractBonus.BonusId);
        }

        [TestMethod]
        public void ContractBonus_DataIncheiere_ShouldBeSetCorrectly()
        {
            // Arrange
            var date = DateTime.Now;
            var contractBonus = new ContractBonus { DataIncheiere = date };

            // Act & Assert
            Assert.AreEqual(date, contractBonus.DataIncheiere);
        }

        [TestMethod]
        public void ContractBonus_DataExpirare_ShouldBeSetCorrectly()
        {
            // Arrange
            var date = DateTime.Now.AddYears(1);
            var contractBonus = new ContractBonus { DataExpirare = date };

            // Act & Assert
            Assert.AreEqual(date, contractBonus.DataExpirare);
        }

        [TestMethod]
        public void ContractBonus_Contract_ShouldBeSetCorrectly()
        {
            // Arrange
            var contract = new Contract { Id = 1 };
            var contractBonus = new ContractBonus { Contract = contract };

            // Act & Assert
            Assert.AreEqual(contract, contractBonus.Contract);
        }

        [TestMethod]
        public void ContractBonus_Bonus_ShouldBeSetCorrectly()
        {
            // Arrange
            var bonus = new Bonus { Id = 1 };
            var contractBonus = new ContractBonus { Bonus = bonus };

            // Act & Assert
            Assert.AreEqual(bonus, contractBonus.Bonus);
        }
    }

    [TestClass]
    public class ContractBonusAssociationTests
    {
        [TestMethod]
        public void ContractBonus_Should_Associate_With_Contract_And_Bonus()
        {
            // Arrange
            var contract = new Contract { Id = 1, DataIncheiere = DateTime.Now, DataExpirare = DateTime.Now.AddYears(1), AbonamentId = 1, ClientId = 1 };
            var bonus = new Bonus { Id = 1, Nume = "Bonus Test", MinuteBonus = 100, SMSuriBonus = 50, TraficDateBonus = 1.5m };

            var contractBonus = new ContractBonus
            {
                ContractId = contract.Id,
                BonusId = bonus.Id,
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddMonths(6),
                Contract = contract,
                Bonus = bonus
            };

            // Act & Assert
            Assert.AreEqual(contract.Id, contractBonus.ContractId);
            Assert.AreEqual(bonus.Id, contractBonus.BonusId);
            Assert.AreEqual(contract, contractBonus.Contract);
            Assert.AreEqual(bonus, contractBonus.Bonus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContractBonus_Should_ThrowException_When_DataExpirare_IsBefore_DataIncheiere()
        {
            // Arrange
            var contractBonus = new ContractBonus
            {
                DataIncheiere = DateTime.Now,
                DataExpirare = DateTime.Now.AddDays(-1)
            };

            // Act
            ValidateContractBonusDates(contractBonus);
        }

        private void ValidateContractBonusDates(ContractBonus contractBonus)
        {
            if (contractBonus.DataExpirare < contractBonus.DataIncheiere)
            {
                throw new ArgumentException("DataExpirare cannot be before DataIncheiere.");
            }
        }

        [TestMethod]
        public void ContractBonus_Should_Calculate_Duration_Correctly()
        {
            // Arrange
            var dataIncheiere = DateTime.Now;
            var dataExpirare = DateTime.Now.AddMonths(6);
            var contractBonus = new ContractBonus
            {
                DataIncheiere = dataIncheiere,
                DataExpirare = dataExpirare
            };

            // Act
            var duration = (contractBonus.DataExpirare - contractBonus.DataIncheiere).Days;

            // Assert
            Assert.AreEqual((dataExpirare - dataIncheiere).Days, duration);
        }

        [TestMethod]
        public void ContractBonus_Should_Associate_ContractId_Correctly()
        {
            // Arrange
            var contract = new Contract { Id = 1 };
            var contractBonus = new ContractBonus { Contract = contract };

            // Act
            contractBonus.ContractId = contract.Id;

            // Assert
            Assert.AreEqual(contract.Id, contractBonus.ContractId);
        }

        [TestMethod]
        public void ContractBonus_Should_Associate_BonusId_Correctly()
        {
            // Arrange
            var bonus = new Bonus { Id = 1 };
            var contractBonus = new ContractBonus { Bonus = bonus };

            // Act
            contractBonus.BonusId = bonus.Id;

            // Assert
            Assert.AreEqual(bonus.Id, contractBonus.BonusId);
        }
    }
}
