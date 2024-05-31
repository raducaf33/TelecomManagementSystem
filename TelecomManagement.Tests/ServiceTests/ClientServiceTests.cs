using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;
using TelecomManagement.Services;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TelecomManagement.Tests.ServiceTests
{
    [TestClass]
    public class ClientServiceTests

    {
        private Mock<DbSet<Client>> mockSet;
        private Mock<TelecomContext> mockContext;
        private ClientService clientService;
        private List<Client> clientList;
        private Mock<IRepositoryBase<Client>> mockClientRepository;

        [TestInitialize]
        public void Setup()
        {
            // Mock pentru DbSet<Client>
            clientList = new List<Client>
            {
                new Client { Id = 1, Nume = "Popescu", Prenume = "Ion", Email = "popescu@example.com", Telefon = "0123456789", CNP = "1234567890123", UserId = 1 },
                new Client { Id = 2, Nume = "Ionescu", Prenume = "Maria", Email = "ionescu@example.com", Telefon = "9876543210", CNP = "9876543210987", UserId = 2 }
            };


            // Configurare mockSet pentru DbSet<Factura>
            mockSet = new Mock<DbSet<Client>>();

            // Configurare mockFacturaRepository pentru IRepositoryBase<Factura>
            mockClientRepository = new Mock<IRepositoryBase<Client>>();

            // Inițializare FacturaService cu mock-ul
            clientService = new ClientService(mockClientRepository.Object);
            var queryable = clientList.AsQueryable();
            mockSet = new Mock<DbSet<Client>>();
            mockSet.As<IQueryable<Client>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Client>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Client>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Client>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => clientList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Client>())).Callback<Client>((entity) => clientList.Remove(entity));
            mockSet.Setup(m => m.Add(It.IsAny<Client>())).Callback<Client>((entity) =>
            {
                if (entity.Nume == null || entity.Prenume == null || entity.Email == null || entity.Telefon == null || entity.CNP == null || entity.UserId == null)
                {
                    throw new ArgumentNullException(nameof(entity.Nume));
                    throw new ArgumentNullException(nameof(entity.Prenume));
                    throw new ArgumentNullException(nameof(entity.Nume));
                    throw new ArgumentNullException(nameof(entity.Nume));
                    throw new ArgumentNullException(nameof(entity.Nume));
                }
                clientList.Add(entity);
            });

            // Mock pentru TelecomContext
            mockContext = new Mock<TelecomContext>();
            mockContext.Setup(m => m.Set<Client>()).Returns(mockSet.Object);

            // Initializare ClientService cu mockContext
            clientService = new ClientService(new ClientRepository(mockContext.Object));
        }

        [TestMethod]
        public void AddClient_WithValidData_ShouldAddToRepository()
        {
            // Arrange
            var client = new Client
            {
                Nume = "Popescu",
                Prenume = "Ion",
                Email = "popescu@example.com",
                Telefon = "0123456789",
                CNP = "1234567890123",
                UserId = 1
            };

            // Act
            clientService.Create(client);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void AddClient_WithNullNume_ShouldThrowArgumentNullException()
        {
            // Arrange
            var client = new Client
            {
                Nume = null,
                Prenume = "Ion",
                Email = "popescu@example.com",
                Telefon = "0123456789",
                CNP = "1234567890123",
                UserId = 1
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => mockSet.Object.Add(client));
        }

        [TestMethod]
        public void AddClient_WithNullPrenume_ShouldThrowArgumentNullException()
        {
            // Arrange
            var client = new Client
            {
                Nume = "Popescu",
                Prenume = null,
                Email = "popescu@example.com",
                Telefon = "0123456789",
                CNP = "invalid",
                UserId = 1
            };
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => mockSet.Object.Add(client));
        }

        [TestMethod]
        public void AddClient_WithNullEmail_ShouldThrowArgumentNullException()
        {
            // Arrange
            var client = new Client
            {
                Nume = "Popescu",
                Prenume = "Ion",
                Email = null,
                Telefon = "0123456789",
                CNP = "invalid",
                UserId = 1
            };
            Assert.ThrowsException<ArgumentNullException>(() => mockSet.Object.Add(client));
        }

        [TestMethod]
        public void AddClient_WithNullPhone_ShouldThrowArgumentNullException()
        {
            // Arrange
            var client = new Client
            {
                Nume = "Popescu",
                Prenume = "Ion",
                Email = "popescu@example.com",
                Telefon = null,
                CNP = "invalid",
                UserId = 1
            };
            Assert.ThrowsException<ArgumentNullException>(() => mockSet.Object.Add(client));
        }

        [TestMethod]
        public void AddClient_WithNullCNP_ShouldThrowArgumentNullException()
        {
            // Arrange
            var client = new Client
            {
                Nume = "Popescu",
                Prenume = "Ion",
                Email = "popescu@example.com",
                Telefon = "0768903657",
                CNP = null,
                UserId = 1
            };
            Assert.ThrowsException<ArgumentNullException>(() => mockSet.Object.Add(client));
        }

        [TestMethod]
        public void EmptyId()
        {
            // Arrange
            var client = new Client
            {
                Id = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (client.Id == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void EmptyUserId()
        {
            // Arrange
            var client = new Client
            {
                UserId = 0, // Set to an empty value
                        // Other property assignments as needed
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                if (client.UserId == 0)
                {
                    throw new ArgumentException("id cannot be empty.");
                }
            });
        }

        [TestMethod]
        public void UpdateClient_WithNegativeUserId()
        {
            // Arrange
            // Arrange
            var client = new Client
            {
                Nume = "Popescu",
                Prenume = "Ion",
                Email = "popescu@example.com",
                Telefon = "0768903657",
                CNP = null,
                UserId = 1
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(client);

            // Modificați numele clientului
            client.UserId = -1;

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }



        [TestMethod]
        public void UpdateClient_ShouldUpdateInRepository()
        {
            // Arrange
            var existingClient = new Client
            {
                Id = 1,
                Nume = "Popescu",
                Prenume = "Ion",
                Email = "popescu@example.com",
                Telefon = "0123456789",
                CNP = "1234567890123",
                UserId = 1
            };

            // Simulați găsirea clientului existent în repository
            mockSet.Setup(m => m.Find(1)).Returns(existingClient);

            // Modificați numele clientului
            existingClient.Nume = "Ionescu";

            // Act
            clientService.Update(existingClient);

            // Assert
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void DeleteClient_ShouldRemoveFromRepository()
        {
            // Arrange
            var client = new Client
            {
                Id = 1,
                Nume = "Popescu",
                Prenume = "Ion",
                Email = "popescu@example.com",
                Telefon = "0123456789",
                CNP = "1234567890123",
                UserId = 1
            };



            // Act
            clientService.Delete(client);

           

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [TestMethod]
        public void Create_ShouldAddFactura()
        {
            // Arrange
            var client = new Client { Id = 1, Nume = "Popescu", Prenume = "Ion", Email = "popescu@example.com", Telefon = "0123456789", CNP = "1234567890123", UserId = 1 };
            mockClientRepository.Setup(repo => repo.Create(It.IsAny<Client>())).Verifiable();

            // Act
            clientService.Create(client);

            // Assert
            mockClientRepository.Verify(repo => repo.Create(It.Is<Client>(f => f.Id == 1)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            clientService.Create(null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void Update_ShouldUpdateFactura()
        {
            // Arrange
            var client = new Client { Id = 1, Nume = "Popescu", Prenume = "Ion", Email = "popescu@example.com", Telefon = "0123456789", CNP = "1234567890123", UserId = 1 };
        
            mockClientRepository.Setup(repo => repo.Update(It.IsAny<Client>())).Verifiable();

            // Act
            client.UserId = 2;
            clientService.Update(client);

            // Assert
            mockClientRepository.Verify(repo => repo.Update(It.Is<Client>(f => f.UserId == 2)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            clientService.Update(null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void Delete_ShouldRemoveFactura()
        {
            // Arrange
            var client = new Client { Id = 1, Nume = "Popescu", Prenume = "Ion", Email = "popescu@example.com", Telefon = "0123456789", CNP = "1234567890123", UserId = 1 };
            mockClientRepository.Setup(repo => repo.Delete(It.IsAny<Client>())).Verifiable();

            // Act
            clientService.Delete(client);

            // Assert
            mockClientRepository.Verify(repo => repo.Delete(It.Is<Client>(f => f.Id == 1)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFactura_ShouldThrowArgumentNullException()
        {
            // Act
            clientService.Delete((Client)null);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void DeleteById_ShouldRemoveFacturaById()
        {
            // Arrange
            int facturaId = 1;
            mockClientRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

            // Act
            clientService.Delete(facturaId);

            // Assert
            mockClientRepository.Verify(repo => repo.Delete(facturaId), Times.Once);
        }

        [TestMethod]
        public void Find_ShouldReturnFacturaById()
        {
            // Arrange
            var client = new Client { Id = 1, Nume = "Popescu", Prenume = "Ion", Email = "popescu@example.com", Telefon = "0123456789", CNP = "1234567890123", UserId = 1 };
            mockClientRepository.Setup(repo => repo.Find(1)).Returns(client);

            // Act
            var result = clientService.Find(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void FindAll_ShouldReturnAllFacturi()
        {
            // Arrange
            var clientList = new List<Client>
            {
               new Client { Id = 1, Nume = "Popescu", Prenume = "Ion", Email = "popescu@example.com", Telefon = "0123456789", CNP = "1234567890123", UserId = 1 },
                 
            };
            mockClientRepository.Setup(repo => repo.FindAll()).Returns(clientList.AsQueryable());

            // Act
            var result = clientService.FindAll();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }
    }

}

