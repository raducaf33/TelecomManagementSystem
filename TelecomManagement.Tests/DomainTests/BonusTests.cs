// <copyright file=BonusTests.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Bonus Tests class. </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;

namespace TelecomManagement.Tests.DomainTests
{
  
        [TestClass]
        public class BonusTests
        {
            [TestMethod]
            public void Bonus_Nume_ShouldNotBeNull()
            {
                // Arrange
                var bonus = new Bonus();

                // Act
                bonus.Nume = null;

                // Assert
                Assert.IsNull(bonus.Nume);
            }

            [TestMethod]
            public void Bonus_MinuteBonus_ShouldBePositive()
            {
                // Arrange
                var bonus = new Bonus { MinuteBonus = null };

                // Act
                var result = Validator.TryValidateObject(bonus, new ValidationContext(bonus), null, true);

                // Assert
                Assert.IsTrue(result, "MinuteBonus ar trebui să fie pozitiv.");
            }

            [TestMethod]
            public void Bonus_SMSuriBonus_ShouldBePositive()
            {
                // Arrange
                var bonus = new Bonus { SMSuriBonus = null };

                // Act
                var result = Validator.TryValidateObject(bonus, new ValidationContext(bonus), null, true);

                // Assert
                Assert.IsTrue(result, "SMSuriBonus ar trebui să fie pozitiv.");
            }

            [TestMethod]
            public void Bonus_TraficDateBonus_ShouldBePositive()
            {
                // Arrange
                var bonus = new Bonus { TraficDateBonus = null };

                // Act
                var result = Validator.TryValidateObject(bonus, new ValidationContext(bonus), null, true);

                // Assert
                Assert.IsTrue(result, "TraficDateBonus ar trebui să fie pozitiv.");
            }
        }
    }

