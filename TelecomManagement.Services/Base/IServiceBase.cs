using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelecomManagement.Services.Base
{
    public interface IServiceBase<T>
    {
        /// <summary>
        /// Creates the specified to create.
        /// </summary>
        /// <param name="toCreate">To create.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        void Create(T toCreate);

        /// <summary>
        /// Updates the specified to update.
        /// </summary>
        /// <param name="toUpdate">To update.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        void Update(T toUpdate);

        /// <summary>
        /// Deletes the specified to delete.
        /// </summary>
        /// <param name="toDelete">To delete.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        void Delete(T toDelete);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Valid entity <see cref="T"/>.</returns>
        T Find(int id);

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns><see cref="IQueryable"/> <typeparamref name="T" /> of entities.</returns>
        IQueryable<T> FindAll();

        IQueryable<T> GetAll();
    }

}
