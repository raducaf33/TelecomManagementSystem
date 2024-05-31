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
    public class FacturaTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
        [TestMethod]
        public void Factura_ValidData_ShouldCreateFactura()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = 100,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Assert
            Assert.AreEqual(123, factura.ContractId);
            Assert.AreEqual(100, factura.SumaTotalaPlata);
            Assert.IsTrue(factura.DataEmitere <= DateTime.Now);
            Assert.IsTrue(factura.DataScadenta > factura.DataEmitere);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Factura_InvalidContractId_ShouldThrowException()
        {
            // Arrange
            var factura = new Factura
            {
                SumaTotalaPlata = 100,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Act & Assert
            Validator.ValidateObject(factura, new ValidationContext(factura), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Factura_InvalidSumaTotalaPlata_ShouldThrowException()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = -100,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Act & Assert
            Validator.ValidateObject(factura, new ValidationContext(factura), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Factura_InvalidDataEmitere_ShouldThrowException()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = 100,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Act & Assert
            Validator.ValidateObject(factura, new ValidationContext(factura), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Factura_InvalidDataScadenta_ShouldThrowException()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = 100,
                DataEmitere = DateTime.Now
            };

            // Act & Assert
            Validator.ValidateObject(factura, new ValidationContext(factura), true);
        }

        [TestMethod]
        public void Factura_ValidData_ShouldPassValidation()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = 100,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Act & Assert
            Validator.ValidateObject(factura, new ValidationContext(factura), true);
        }

        [TestMethod]
        [TestCategory("SumaTotalaPlata")]
        public void SumaTotalaPlata_ShouldBeNonNegative()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = -100,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Act
            var validationResults = ValidateModel(factura);

            // Assert
            Assert.IsTrue(validationResults.Exists(vr => vr.MemberNames.Contains("SumaTotalaPlata") && vr.ErrorMessage.Contains("Suma plății trebuie să fie mai mare sau egală cu 0.")));
        }

        [TestMethod]
        [TestCategory("DataEmitere")]
        public void DataEmitere_ShouldBeRequired()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = 100,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Act
            var validationResults = ValidateModel(factura);

            // Assert
            Assert.IsTrue(validationResults.Exists(vr => vr.MemberNames.Contains("DataEmitere") && vr.ErrorMessage.Contains("Data Emitere este obligatorie.")));
        }

        [TestMethod]
        [TestCategory("DataScadenta")]
        public void DataScadenta_ShouldBeRequired()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = 100,
                DataEmitere = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(factura);

            // Assert
            Assert.IsTrue(validationResults.Exists(vr => vr.MemberNames.Contains("DataScadenta") && vr.ErrorMessage.Contains("Data Scadenta este obligatorie.")));
        }

        [TestMethod]
        [TestCategory("ValidData")]
        public void Factura_WithValidData_ShouldPassValidation()
        {
            // Arrange
            var factura = new Factura
            {
                ContractId = 123,
                SumaTotalaPlata = 100,
                DataEmitere = DateTime.Now,
                DataScadenta = DateTime.Now.AddDays(30)
            };

            // Act
            var validationResults = ValidateModel(factura);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }
    }
}
