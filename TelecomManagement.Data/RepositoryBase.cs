// <copyright file="RepositoryBase.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Base Repository class. </summary>


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected TelecomContext telecomContext { get; set; }


        public RepositoryBase(TelecomContext context)
        {
            this.telecomContext = context;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Create(T entity)
        {
          
                this.telecomContext.Set<T>().Add(entity);
                this.telecomContext.SaveChanges();
            
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(T entity)
        {
           
                this.telecomContext.Entry(entity).State = EntityState.Deleted;
                this.telecomContext.Set<T>().Remove(entity);
                this.telecomContext.SaveChanges();
            
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
              var entityToDelete = this.Find(id);
                this.telecomContext.Entry(entityToDelete).State = EntityState.Deleted;
                this.telecomContext.Set<T>().Remove(entityToDelete);
                this.telecomContext.SaveChanges();
           
        }

        public virtual T Find(int id)
        {
            try
            {
                return this.telecomContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                // Poți să faci orice vrei cu excepția aici, de exemplu să o arunci mai departe sau să o înregistrezi în alt mod
                throw new Exception($"Error finding entity of type {typeof(T).Name} with id {id}. Message: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Return entities that met the condition.
        /// </returns>
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            try
            {
                return this.telecomContext.Set<T>().Where(expression).AsNoTracking();
            }
            catch (Exception ex)
            {
                // Poți să faci orice vrei cu excepția aici, de exemplu să o arunci mai departe sau să o înregistrezi în alt mod
                throw new Exception($"Error finding entities of type {typeof(T).Name} by condition. Message: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>Returns all <typeparamref name="T" /> entities.</returns>
        public virtual IQueryable<T> FindAll()
        {
            try
            {
                return this.telecomContext.Set<T>().AsNoTracking();
            }
            catch (Exception ex)
            {
                // Poți să faci orice vrei cu excepția aici, de exemplu să o arunci mai departe sau să o înregistrezi în alt mod
                throw new Exception($"Error finding all entities of type {typeof(T).Name}. Message: {ex.Message}", ex);
            }
        }

        public virtual IQueryable<T> GetAll()
        {
            try
            {
                return this.telecomContext.Set<T>().AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all entities of type {typeof(T).Name}. Message: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(T entity)
        {
            
                this.telecomContext.Entry(entity).State = EntityState.Modified;
                this.telecomContext.SaveChanges();
            
        }

       

    }
}
