
using NgCooking.CrossCutting.Enum;
using NgCooking.DAL.Entities;
using NgCooking.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class Repository<T> : BaseService where T : class
    {
        public Repository(ngCooking_AmnaEntities dbContext)
             : base(dbContext)
        {
        }

        public Repository()
            : base()
        {

        }

        public Repository(DataBaseEnum database)
            : base(database)
        {
        }

        public int Add(T entity)
        {
            ((IObjectContextAdapter)this.DbContext).ObjectContext.AddObject(typeof(T).Name, entity);
            int result = this.DbContext.SaveChanges();
            ((IObjectContextAdapter)this.DbContext).ObjectContext.Refresh(RefreshMode.StoreWins, entity);

            this.DbContext.SaveChanges();
            ((IObjectContextAdapter)this.DbContext).ObjectContext.AcceptAllChanges();
            return result;
        }

        public int Update(T entity)
        {
            object originalItem;
            EntityKey key = ((IObjectContextAdapter)this.DbContext).ObjectContext.CreateEntityKey(typeof(T).Name, entity);
            object oObject = entity;
            if (((IObjectContextAdapter)this.DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ((IObjectContextAdapter)this.DbContext).ObjectContext.ApplyCurrentValues(key.EntitySetName, oObject);
            }

            int result = this.DbContext.SaveChanges();
            return result;
        }

        //lister tous les données selon un id données à l'entrée ou nn
        public IList<T> GetAll(Expression<Func<T, bool>> expression, bool bEnableLazyLoading)
        {
            this.DbContext.Configuration.LazyLoadingEnabled = bEnableLazyLoading;
            IList<T> list = expression != null ?
                ((IObjectContextAdapter)this.DbContext).ObjectContext.CreateQuery<T>("[" + typeof(T).Name + "]").Where(expression).ToList() :
                ((IObjectContextAdapter)this.DbContext).ObjectContext.CreateQuery<T>("[" + typeof(T).Name + "]").ToList();

            return list;
        }

        public int Delete(T entity)
        {
            object originalItem;
            EntityKey key = ((IObjectContextAdapter)this.DbContext).ObjectContext.CreateEntityKey(typeof(T).Name, entity);
            if (((IObjectContextAdapter)this.DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ((IObjectContextAdapter)this.DbContext).ObjectContext.DeleteObject(originalItem);
            }

            int result = this.DbContext.SaveChanges();
            return result;
        }
    }
}
