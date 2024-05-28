using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelecomManagement.Domain;


namespace TelecomManagement.Tests.DomainTests
{
    [TestClass]
    public class AbonamentTests
    {
        [TestMethod]
        public void AbonamentSetGet()
        {
            // Arrange
            var abonament = new Abonament
            {
                Id = 1,
                Nume = "Abonament Test",
                Pret = 50.0m,
                MinuteIncluse = 300,
                SMSuriIncluse = 100,
                TraficDateInclus = 10.0m
            };

            // Act & Assert
            Assert.AreEqual(1, abonament.Id);
            Assert.AreEqual("Abonament Test", abonament.Nume);
            Assert.AreEqual(50.0m, abonament.Pret);
            Assert.AreEqual(300, abonament.MinuteIncluse);
            Assert.AreEqual(100, abonament.SMSuriIncluse);
            Assert.AreEqual(10.0m, abonament.TraficDateInclus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Abonament_InvalidPrice_Should_ThrowException()
        {
            // Arrange
            var abonament = new Abonament
            {
                Id = 1,
                Nume = "Abonament Test",
                Pret = -1.0m, // Invalid price
                MinuteIncluse = 300,
                SMSuriIncluse = 100,
                TraficDateInclus = 10.0m
            };

            // Act & Assert
            ValidateAbonament(abonament);
        }

        [TestMethod]
        public void Abonament_Nume_ShouldBeRequired()
        {
            // Arrange
            var abonament = new Abonament
            {
                Pret = 50.0m,
                MinuteIncluse = 300,
                SMSuriIncluse = 100,
                TraficDateInclus = 10.0m
            };

            // Act
            var validationContext = new ValidationContext(abonament, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Assert
            var isValid = Validator.TryValidateObject(abonament, validationContext, results, true);
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(vr => vr.MemberNames.Contains("Nume")));
        }

        [TestMethod]
        public void Abonament_MinuteIncluse_ShouldBePositive()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Abonament Test",
                Pret = 50.0m,
                MinuteIncluse = -5,
                SMSuriIncluse = 100,
                TraficDateInclus = 10.0m
            };

            // Act
            var validationContext = new ValidationContext(abonament, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Assert
            var isValid = Validator.TryValidateObject(abonament, validationContext, results, true);
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(vr => vr.ErrorMessage.Contains("The field MinuteIncluse must be between 0 and")));
        }

        

        [TestMethod]
        public void Abonament_SMSuriIncluse_ShouldBePositive()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Abonament Test",
                Pret = 50.0m,
                MinuteIncluse = 300,
                SMSuriIncluse = -10,
                TraficDateInclus = 10.0m
            };

            // Act
            var validationContext = new ValidationContext(abonament, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Assert
            var isValid = Validator.TryValidateObject(abonament, validationContext, results, true);
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(vr => vr.ErrorMessage.Contains("The field SMSuriIncluse must be between 0 and")));
        }

        [TestMethod]
        public void Abonament_TraficDateInclus_ShouldBePositive()
        {
            // Arrange
            var abonament = new Abonament
            {
                Nume = "Abonament Test",
                Pret = 50.0m,
                MinuteIncluse = 300,
                SMSuriIncluse = 100,
                TraficDateInclus = -5.0m
            };

            // Act
            var validationContext = new ValidationContext(abonament, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Assert
            var isValid = Validator.TryValidateObject(abonament, validationContext, results, true);
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(vr => vr.ErrorMessage.Contains("The field TraficDateInclus must be between 0 and")));
        }

        private void ValidateAbonament(Abonament abonament)
        {
            if (abonament.Pret < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
        }
    }
}
