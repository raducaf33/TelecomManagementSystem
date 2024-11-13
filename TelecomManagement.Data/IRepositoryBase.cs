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
    /// <summary>
    /// Defines the IRepositoryBase interface.
    /// </summary>
    
    public interface IRepositoryBase<T> 
    {
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
       
       
        T Find(int id);

        /// <summary>
        /// Finds all.
        /// </summary>
     
        IQueryable<T> FindAll();



        IQueryable<T> GetAll();

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
       
        void Create(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        
        void Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        
        void Delete(T entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        
        void Delete(int id);
    }
}
