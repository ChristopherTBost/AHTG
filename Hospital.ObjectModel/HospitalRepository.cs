using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AHTG.Hospital.ObjectModel
{
    public class HospitalRepository
    {

#if IN_MEMORY_CONTEXT
        static object SyncRoot = new object();

        static int nextKey = 0;
        
        static int NextKey()
        {
            lock (SyncRoot)
                return ++nextKey;
        }

        static T Add<T>(Context.HospitalContext hospitalContext, T entity)
        {
            var rc = hospitalContext.Add(entity);
            if (!rc.IsKeySet)
                entity.GetType().GetProperty("Id")?.GetGetMethod()?.Invoke(entity, new object[] { NextKey() });
            return (T)rc.Entity;
        }
#endif

        Context.HospitalContext hospitalContext;

        public IQueryable<Entities.Hospital> Hospitals => hospitalContext.Hospitals;

        public T Add<T>(T entity)
        {
#if IN_MEMORY_CONTEXT
            return (T)Add(hospitalContext, entity);
#else
            return (T)hospitalContext.Add(entity).Entity;
#endif
        }
        public T Delete<T>(T entity) =>(T)hospitalContext.Remove(entity).Entity;
        public T Update<T>(T entity) => (T)hospitalContext.Update(entity).Entity;

        public HospitalRepository(Context.HospitalContext hospitalContext)
            :base()
        {
            this.hospitalContext = hospitalContext ?? throw new ArgumentNullException(nameof(hospitalContext));
        }
    }
}
