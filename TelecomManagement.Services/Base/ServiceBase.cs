// <copyright file="ServiceBase.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Service Base class. </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data;
using TelecomManagement.Domain;

namespace TelecomManagement.Services.Base
{
    public class ServiceBase<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ServiceBase(IRepositoryBase<T> repository)
        {
            this.Repository = repository;
          

        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        private IRepositoryBase<T> Repository { get; set; }

        /// <summary>
        /// Creates the specified to create.
        /// </summary>
        /// <param name="toCreate">To create.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        /// <exception cref="System.ArgumentNullException">Null toCreate.</exception>
        public void Create(T toCreate)
        {
            if (toCreate is null)
            {
                throw new ArgumentNullException(nameof(toCreate));
            }

            this.Repository.Create(toCreate);
        }

        /// <summary>
        /// Updates the specified to update.
        /// </summary>
        /// <param name="toUpdate">To update.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        /// <exception cref="System.ArgumentNullException">Null toUpdate.</exception>
        public void Update(T toUpdate)
        {
            if (toUpdate is null)
            {

                throw new System.ArgumentNullException(nameof(toUpdate));
            }



            this.Repository.Update(toUpdate);

        }

        /// <summary>
        /// Deletes the specified to delete.
        /// </summary>
        /// <param name="toDelete">To delete.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        /// <exception cref="System.ArgumentNullException">Null toDelete.</exception>
        public void Delete(T toDelete)
        {
            if (toDelete is null)
            {

                throw new System.ArgumentNullException(nameof(toDelete));
            }



            this.Repository.Delete(toDelete);

        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.Repository.Delete(id);
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Valid entity <see cref="T"/>.</returns>
        public T Find(int id)
        {
            return this.Repository.Find(id);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>ICollection <typeparamref name="T" /> of entities.</returns>
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