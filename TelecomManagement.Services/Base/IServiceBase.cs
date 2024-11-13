// <copyright file="IServiceBase.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Base Service Interface class. </summary>

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
        
        void Create(T toCreate);

        /// <summary>
        /// Updates the specified to update.
        /// </summary>
        
        void Update(T toUpdate);

        /// <summary>
        /// Deletes the specified to delete.
        /// </summary>
        
        void Delete(T toDelete);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        
        void Delete(int id);

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        
        T Find(int id);

        /// <summary>
        /// Finds all.
        /// </summary>
        
        IQueryable<T> FindAll();

        /// <summary>
        /// Gets all.
        /// </summary>

        IQueryable<T> GetAll();
    }

}
