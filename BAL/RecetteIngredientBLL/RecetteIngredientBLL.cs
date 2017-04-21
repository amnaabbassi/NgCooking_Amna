using BLL.Repository;
using NgCooking.CrossCutting.Enum;
using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RecetteIngredientBLL
{
    public class RecetteIngredientBLL
    {
        #region declaration
        Repository<RecetteIngredient> reprecetIngre;
        #endregion
        public RecetteIngredientBLL()
        {
            reprecetIngre = new Repository<RecetteIngredient>(DataBaseEnum.NgCookingdev);

        }
        public List<RecetteIngredient> getCat(Expression<Func<RecetteIngredient, bool>> myFunc, bool s)
        {
            return reprecetIngre.GetAll(myFunc, s).ToList();
        }
        public int addCat(RecetteIngredient cat)
        {
            return reprecetIngre.Add(cat);
        }
        public int UpdateCat(RecetteIngredient cat)
        {
            return reprecetIngre.Update(cat);
        }
        public int deleteCat(RecetteIngredient cat)
        {
            return reprecetIngre.Delete(cat);
        }
    }
}
