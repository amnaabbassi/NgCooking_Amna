using BLL.Repository;
using NgCooking.CrossCutting.Enum;
using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public class CategorieBLL
    {
        #region declaration
        Repository<Categorie> repcat;
        #endregion
        public CategorieBLL()
        {
            repcat = new Repository<Categorie>(DataBaseEnum.NgCookingdev);

        }
        public List<Categorie> getCat(Expression<Func<Categorie, bool>> myFunc, bool s)
        {
            return repcat.GetAll(myFunc, s).ToList();
        }
        public int addCat(Categorie cat)
        {
            return repcat.Add(cat);
        }
        public int UpdateCat(Categorie cat)
        {
            return repcat.Update(cat);
        }
        public int deleteCat(Categorie cat)
        {
            return repcat.Delete(cat);
        }
    }
}
