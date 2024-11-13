// <copyright file="ServiceBase.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Service Base class. </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Data.Logger;
using TelecomManagement.Domain;

namespace TelecomManagement.Services.Base
{
    public class ServiceBase<T> where T : class
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
       
        private Logger Logger { get; set; }
        /// <summary>
        /// Initializes a new instance of the ServiceBase class.
        /// </summary>
        


        public ServiceBase(IRepositoryBase<T> repository)
        {
            this.Repository = repository;
            this.Logger = new Logger();


        }

       

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        
        private IRepositoryBase<T> Repository { get; set; }

        /// <summary>
        /// Creates the specified to create.
        /// </summary>
        
        public void Create(T toCreate)
        {
            if (toCreate is null)
            {
                Logger.LogError($"{typeof(T).Name} is null", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(nameof(toCreate));
            }

            this.Repository.Create(toCreate);
        }

        /// <summary>
        /// Updates the specified to update.
        /// </summary>
        
        
        public void Update(T toUpdate)
        {
            if (toUpdate is null)
            {
                Logger.LogError($"{typeof(T).Name} is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(toUpdate));
            }



            this.Repository.Update(toUpdate);

        }

        /// <summary>
        /// Deletes the specified to delete.
        /// </summary>
       
        public void Delete(T toDelete)
        {
            if (toDelete is null)
            {
                Logger.LogError($"{typeof(T).Name} is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(toDelete));
            }



            this.Repository.Delete(toDelete);

        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
      
        public void Delete(int id)
        {
            this.Repository.Delete(id);
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
   
        public T Find(int id)
        {
            return this.Repository.Find(id);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
       
        public IQueryable<T> FindAll()
        {
            return this.Repository.FindAll();
        }


        public IQueryable<T> GetAll()
        {
            return this.Repository.GetAll();
        }
    }
}