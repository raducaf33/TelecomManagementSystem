// <copyright file="TelecomContext.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Telecom Context class. </summary>


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// TelecomContext class.
/// </summary>

namespace TelecomManagement.Data
{

    /// <summary>
    /// Initializes a new instance of the TelecomContext class.
    /// </summary>
    public class TelecomContext : DbContext
    {

        public TelecomContext() : base("name=TelecomContext")
        {
        }

        /// <summary>
        /// Gets or sets the Abonaments.
        /// </summary>
       
        public virtual DbSet<Abonament> Abonaments { get; set; }

        /// <summary>
        /// Gets or sets the Contracts.
        /// </summary>
        public virtual DbSet<Contract> Contracte { get; set; }

        /// <summary>
        /// Gets or sets the Clients.
        /// </summary>
        public virtual DbSet<Client> Clienti { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Bonus.
        /// </summary>

        public virtual DbSet<Bonus> Bonus { get; set; }

        /// <summary>
        /// Gets or sets the ContractBonus.
        /// </summary>
        public virtual DbSet<ContractBonus> ContractBonus { get; set; }

        /// <summary>
        /// Gets or sets the Plata.
        /// </summary>

        public virtual DbSet<Plata> Plata { get; set; }

        /// <summary>
        /// Gets or sets the Factura.
        /// </summary>


        public virtual DbSet<Factura> Factura { get; set; }

        /// <summary>
        /// Setting up the database schema, 
        /// including table names and relationships between entities, 
        /// using Entity Framework's Fluent API.
        /// </summary>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Abonament>().ToTable("Abonaments");
            modelBuilder.Entity<Contract>().ToTable("Contracts");
           modelBuilder.Entity<Client>().ToTable("Clienti");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Bonus>().ToTable("Bonus");
            modelBuilder.Entity<ContractBonus>().ToTable("ContractBonus");
            modelBuilder.Entity<Plata>().ToTable("Plata");
            modelBuilder.Entity<Factura>().ToTable("Factura");

            /// <summary>
            /// Setting up the database Foreign Keys
            /// </summary>



            modelBuilder.Entity<Contract>()
                .HasRequired(c => c.Abonament)
                .WithMany()
                .HasForeignKey(c => c.AbonamentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasRequired(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .WillCascadeOnDelete(false);

           modelBuilder.Entity<Client>()
                .HasRequired(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractBonus>()
                .HasRequired(c => c.Contract)
                .WithMany()
                .HasForeignKey(c => c.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractBonus>()
                .HasRequired(c => c.Bonus)
                .WithMany()
                .HasForeignKey(c => c.BonusId)
                .WillCascadeOnDelete(false);




            base.OnModelCreating(modelBuilder);
        }



    }
}
