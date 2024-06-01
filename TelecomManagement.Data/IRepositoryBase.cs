// <copyright file="IRepositoryBase.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Base Interface Repository class. </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Data
{
    public interface IRepositoryBase<T> 
    {
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Valid entity <see cref="T"/>.</returns>
        T Find(int id);

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>Returns all <typeparamref name="T"/> entities.</returns>
        IQueryable<T> FindAll();



        IQueryable<T> GetAll();

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Return entities that met the condition.</returns>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Create(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);
    }
}
