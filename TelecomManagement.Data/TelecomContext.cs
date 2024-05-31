using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;


namespace TelecomManagement.Data
{
    public class TelecomContext : DbContext
    {
        public TelecomContext() : base("name=TelecomContext")
        {
        }
       public virtual DbSet<Abonament> Abonaments { get; set; }
       public virtual DbSet<Contract> Contracte { get; set; }
       public virtual DbSet<Client> Clienti { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Bonus> Bonus { get; set; }
        public virtual DbSet<ContractBonus> ContractBonus { get; set; }

        public virtual DbSet<Plata> Plata { get; set; }


        public virtual DbSet<Factura> Factura { get; set; }



        // Alte DbSet-uri pentru entități

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


            // Configurarea relației între Client și Abonament

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
