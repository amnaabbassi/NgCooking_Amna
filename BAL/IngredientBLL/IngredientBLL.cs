using BLL.Repository;
using NgCooking.CrossCutting.Enum;
using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.IngredientBLL
{
    public class IngredientBLL
    {
        #region declaration
        Repository<NgCooking.DAL.Entities.Ingredient> reping;
        #endregion
        int afficher { get; set; }
        public int Taille { get; set; }
        public IngredientBLL()
        {
            reping = new Repository<Ingredient>(DataBaseEnum.NgCookingdev);

        }
        public List<Ingredient> getIng(Expression<Func<Ingredient, bool>> myFunc, bool s)
        {
            //List<Ingredient> listfinal;
            //afficher = display;
            //if (afficher == 3)
            //{
            //    listfinal= reping.GetAll(myFunc, false).ToList();
            ////}
            //else
            //{
            //    listfinal = reping.GetAll(myFunc, false).ToList();
            //}
            return reping.GetAll(myFunc, false).ToList(); ;
        }
        public int addIng(Ingredient com)
        {
            return reping.Add(com);
        }
        public int UpdateIng(Ingredient com)
        {
            return reping.Update(com);
        }
        public int deleteIng(Ingredient com)
        {
            return reping.Delete(com);
        }
    }
}
