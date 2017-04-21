using NgCooking.CrossCutting.Enum;
using NgCooking.CrossCutting.Utilies;
using NgCooking.DAL.Entities;
using System;

namespace NgCooking.Service.BaseService
{
    public class BaseService : IDisposable
    {

        bool disposed;
        protected ngCooking_AmnaEntities DbContext { get; set; }
        public BaseService()
        {

        }
        public BaseService(ngCooking_AmnaEntities dbContext)
        {
            this.DbContext = dbContext;
        }

        public BaseService(DataBaseEnum database)
        {
            this.DbContext = AppUtils.GetNewDbContext(database);
        }
        public void SetDbContext(DataBaseEnum database)
        {
            this.DbContext = AppUtils.GetNewDbContext(database);
        }
   

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
