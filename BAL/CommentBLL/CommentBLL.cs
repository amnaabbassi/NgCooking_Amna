using BLL.Repository;
using NgCooking.CrossCutting.Enum;

using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.CommentBLL
{
    public class CommentBLL
    {
        #region declaration
        Repository<Comment> repcom;
        #endregion
        public CommentBLL()
        {
            repcom = new Repository<Comment>(DataBaseEnum.NgCookingdev);

        }
        public List<Comment> getComm(Expression<Func<Comment, bool>> myFunc, bool s)
        {
            return repcom.GetAll(myFunc, s).ToList();
        }
        public int addComm(Comment com)
        {
            return repcom.Add(com);
        }
        public int UpdateComm(Comment com)
        {
            return repcom.Update(com);
        }
        public int deleteComm(Comment com)
        {
            return repcom.Delete(com);
        }
    }
}
