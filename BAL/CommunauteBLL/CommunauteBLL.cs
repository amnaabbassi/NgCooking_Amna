using BLL.Repository;
using NgCooking.CrossCutting.Enum;

using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.CommunauteBLL
{
   public class CommunauteBLL
    {
        #region declaration
        Repository<Communaute> repcom;
        #endregion
        public CommunauteBLL()
        {
            repcom = new Repository<Communaute>(DataBaseEnum.NgCookingdev);

        }
        int afficher { get; set; }
        public int Taille { get; set; }
        public List<Communaute> getCom(Expression<Func<Communaute, bool>> myFunc, bool s, string choix , int display = 3)
        {
            afficher = display;
            List<Communaute> listfinal = new List<Communaute>();
            if (choix == "az")
            {
                listfinal= repcom.GetAll(myFunc, s).Take(afficher).ToList();
            }
            else if(choix == "za")
            {
                listfinal= repcom.GetAll(myFunc, s).OrderByDescending(x => x.firstname).Take(afficher).ToList();
            }
            else if(choix == "exp")
            {
                listfinal = repcom.GetAll(myFunc, s).OrderByDescending(x => x.niveau).Take(afficher).ToList();
            }
            else if (choix == "nov")
            {
                listfinal = repcom.GetAll(myFunc, s).OrderBy(x => x.niveau).Take(afficher).ToList();
            }
            else if(choix == "prod")
            {
                listfinal = repcom.GetAll(myFunc, s).OrderByDescending(x => x.Recette.Count()).Take(afficher).ToList();
            }
            else if (choix == "prod_desc")
            {
                listfinal = repcom.GetAll(myFunc, s).OrderBy(x => x.Recette.Count()).Take(afficher).ToList();
            }
            return listfinal;
           
        }
        public int addCom(Communaute com)
        {
            return repcom.Add(com);
        }
        public int UpdateCom(Communaute com)
        {
            return repcom.Update(com);
        }
        public int deleteCom(Communaute com)
        {
            return repcom.Delete(com);
        }
    }
}
