// <copyright file="RepositoryBase.cs" company="Transilvania University Of Brasov">
// Fintineru Raduca-Maria
// </copyright>
// <summary> Defines the Base Repository class. </summary>

using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Data.Logger;

namespace TelecomManagement.Data
{
    /// <summary>
    /// RepositoryBase class.
    /// </summary>
   
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Gets or sets the Telecom database context.
        /// </summary>
        protected TelecomContext telecomContext { get; set; }

        /// <summary>
        /// Gets or sets the Logger.
        /// </summary>
        private ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the RepositoryBase class.
        /// </summary>
        
        public RepositoryBase(TelecomContext context)
        {
            this.telecomContext = context;
            this.Logger = Logger;

        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        
        public virtual void Create(T entity)
        {
  
            try
            {
                this.telecomContext.Set<T>().Add(entity);
                this.telecomContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Error on creating {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        
        public virtual void Delete(T entity)
        {
           
               

            this.Logger.LogInfo($"Deleting {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                this.telecomContext.Entry(entity).State = EntityState.Deleted;
                this.telecomContext.Set<T>().Remove(entity);
                this.telecomContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Error on Deleting {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        
        public virtual void Delete(int id)
        {
            try
            {
                var entityToDelete = this.Find(id);
                this.telecomContext.Entry(entityToDelete).State = EntityState.Deleted;
                this.telecomContext.Set<T>().Remove(entityToDelete);
                this.telecomContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Error on Deleting {typeof(T).Name}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

        }
        /// <summary>
        /// Finds by  the specified identifier.
        /// </summary>

        public virtual T Find(int id)
        {

            this.Logger.LogInfo($"Find {typeof(T).Name} with id {id}", MethodBase.GetCurrentMethod());
            try
            {
                return this.telecomContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Find, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

            return null;
        }
        /// <summary>
        /// Finds the by condition.
        /// </summary>
        
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {   

            this.Logger.LogInfo($"Find {typeof(T).Name} entries by condition", MethodBase.GetCurrentMethod());
            try
            {
                return this.telecomContext.Set<T>().Where(expression).AsNoTracking();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Find by condition, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

            return null;
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        
        public virtual IQueryable<T> FindAll()
        {
         
            this.Logger.LogInfo($"Find all {typeof(T).Name} entries", MethodBase.GetCurrentMethod());
            try
            {
                return this.telecomContext.Set<T>().AsNoTracking();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Find all, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

            return null;
        }
        /// <summary>
        /// Gets all.
        /// </summary>

        public virtual IQueryable<T> GetAll()
        {
            this.Logger.LogInfo($"Get all {typeof(T).Name} entries", MethodBase.GetCurrentMethod());
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
      
        public virtual void Update(T entity)
        {
           

            this.Logger.LogInfo($"Update {entity.GetType()}", MethodBase.GetCurrentMethod());
            try
            {
                this.telecomContext.Entry(entity).State = EntityState.Modified;
                this.telecomContext.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning($"Update {entity.GetType()}, message {ex.Message}", MethodBase.GetCurrentMethod());
            }

        }

       

    }
}
