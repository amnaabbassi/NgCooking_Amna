using BLL.Repository;
using NgCooking.CrossCutting.Enum;

using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.RecetteBLL
{
    public class RecetteBLL
    {
        #region declaration
        Repository<Recette> reprec;
        #endregion
        public RecetteBLL()
        {
            reprec = new Repository<Recette>(DataBaseEnum.NgCookingdev);

        }
        public List<Recette> getRec(Expression<Func<Recette, bool>> myFunc, bool s, string choix)
        {
            List<Recette> listfinal = new List<Recette>();
            if (choix == "az")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderByDescending(x=>x.name).ToList();
            }
            else if (choix == "za")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderBy(x => x.name).ToList();
            }
            else if (choix == "exp")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderByDescending(x => x.name).ToList();
            }
            else if (choix == "nov")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderByDescending(x => x.name).ToList();
            }
            else if (choix == "best")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderBy(x => x.calories).ToList();
            }
            else if (choix == "worst")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderByDescending(x => x.calories).ToList();
            }
            else if (choix == "cal")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderBy(x => x.calories).ToList();
            }
            else if (choix == "cal_desc")
            {
                listfinal = reprec.GetAll(myFunc, s).OrderByDescending(x => x.calories).ToList();
            }
            return listfinal;
        }
        //public List<Recette> getSingle(Expression<Func<Recette, bool>> myFunc, bool s)
        //{

        //    return reprec.GetAll(myFunc, s).OrderBy(x => x.name).ToList();
        //}
        public int addRec(Recette com)
        {
            return reprec.Add(com);
        }
        public int UpdateRec(Recette com)
        {
            return reprec.Update(com);
        }
        public int deleteRec(Recette com)
        {
            return reprec.Delete(com);
        }
        //public int lastindex(Recette com)
        //{
            
        //}
    }
}
