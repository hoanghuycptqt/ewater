using EWATER.DAL;
using EWATER.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EWATER.Repository
{
    public class GenericRepository <T> : IRepository<T> where T: class
    {
        EWaterContext entity = null;
        private DbSet<T> entities = null;

        public GenericRepository(EWaterContext entity)
        {
            this.entity = entity;
            entities = entity.Set<T>();
        }

        public IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters)
        {
            using (entity = new EWaterContext())
            {
                return entity.Database.SqlQuery<T>(spQuery, parameters).ToList();
            }
        }

        public T ExecuteQuerySingle(string spQuery, object[] parameters)
        {
            using (entity = new EWaterContext())
            {
                return entity.Database.SqlQuery<T>(spQuery, parameters).FirstOrDefault();
            }
        }
        
        public int ExecuteCommand(string spQuery, object[] parameters)
        {
            int result = 0;
            try
            {
                using (entity = new EWaterContext())
                {
                    result = entity.Database.ExecuteSqlCommand(spQuery, parameters);
                        
                }
            }
            catch(Exception ex)
            { ex.ToString();
            }
            return result;
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entity.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}