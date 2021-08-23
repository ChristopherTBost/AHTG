using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AHTG.Hospital.ObjectModel
{
    /// <summary>
    /// a repository pattern wrapper for the HospitalContext
    /// </summary>
    public class HospitalRepository
    {
        /// <summary>
        /// the actual context that operations are delegated to
        /// </summary>
        Context.HospitalContext hospitalContext;

        /// <summary>
        /// wrapper of the Hospitals DbSet
        /// </summary>
        /// <remarks>
        /// I could write a thesis on static -vs- late binding and why this MUST BE IQueryable apposed to IEnumerable
        /// </remarks>
        public IQueryable<Entities.Hospital> Hospitals => hospitalContext.Hospitals;

        /// <summary>
        /// add an entity to the context and returns it
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="entity">the instance to add</param>
        /// <returns>the added entity</returns>
        public T Add<T>(T entity) => (T)hospitalContext.Add(entity).Entity;

        /// <summary>
        /// deletes an entity from the context and returns it
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="entity">the instance to delete</param>
        /// <returns>the deleted entity</returns>
        public T Delete<T>(T entity) =>(T)hospitalContext.Remove(entity).Entity;

        /// <summary>
        /// updates an entity in the context and returns it
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="entity">the instance to update</param>
        /// <returns>the updated entity</returns>
        public T Update<T>(T entity) => (T)hospitalContext.Update(entity).Entity;

        /// <summary>
        /// saves the changes in the context
        /// </summary>
        public void SaveChanges() => hospitalContext.SaveChanges();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="hospitalContext">the actual context</param>
        /// <exception cref="ArgumentNullException">if <paramref name="hospitalContext"/> is null</exception>
        public HospitalRepository(Context.HospitalContext hospitalContext)
            :base()
        {
            this.hospitalContext = hospitalContext ?? throw new ArgumentNullException(nameof(hospitalContext));
        }

    }
}
