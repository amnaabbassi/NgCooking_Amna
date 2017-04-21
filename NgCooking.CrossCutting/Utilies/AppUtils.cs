using NgCooking.CrossCutting.Config;
using NgCooking.CrossCutting.Enum;

using NgCooking.DAL.Entities;

namespace NgCooking.CrossCutting.Utilies
{
    public class AppUtils
    {
        public static ngCooking_AmnaEntities GetNewDbContext(DataBaseEnum database)
        {
            switch (database)
            {
                case DataBaseEnum.NgCookingdev:
                    return new ngCooking_AmnaEntities(AppConfig.DbConnectionStringNgCooking);

                default:
                    return new ngCooking_AmnaEntities(AppConfig.DbConnectionStringNgCooking);
                    //return null;
            }
        }
    }
}
