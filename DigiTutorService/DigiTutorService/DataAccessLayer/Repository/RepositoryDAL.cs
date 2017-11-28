using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DigiTutorService.DataAccessLayer.Repository
{
    public static class RepositoryDAL
    {
        public static List<T> Read<T>(Expression<Func<T, bool>> predicate) where T:class
        {
            using (DigiTutorDBEntities dbContext = new DigiTutorDBEntities())
            {
                try
                {
                    return dbContext.Set<T>().Where(predicate).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
        public static int Create<T>(T newObject) where T : class
        {
            using (DigiTutorDBEntities dbContext = new DigiTutorDBEntities())
            {
                try
                {
                    dbContext.Set<T>().Add(newObject);
                    dbContext.SaveChanges();
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
                    
            }
        }
        public static int Delete(Expression<Func<Object, bool>> predicate)
        {
            return 0;
        }
        public static int Update(Object TObject)
        {
            return 0;
        }

    }
}