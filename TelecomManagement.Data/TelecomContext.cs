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
       public DbSet<Abonament> Abonaments { get; set; }
       public DbSet<Contract> Contracte { get; set; }
       public DbSet<Client> Clienti { get; set; }
        public DbSet<User> Users { get; set; }

        // Alte DbSet-uri pentru entități

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Abonament>().ToTable("Abonaments");
            modelBuilder.Entity<Contract>().ToTable("Contracts");
           modelBuilder.Entity<Client>().ToTable("Clienti");
            modelBuilder.Entity<User>().ToTable("Users");


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


            base.OnModelCreating(modelBuilder);
        }



    }
}
