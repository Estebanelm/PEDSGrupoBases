using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DigiTutorService.DataAccessLayer.Repository
{
    public static class RepositoryDAL
    {
        public static List<T> Read<T>() where T : class
        {
            using (DigiTutorDBEntities dbContext = new DigiTutorDBEntities())
            {
                try
                {
                    return dbContext.Set<T>().ToList();
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
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
        public static List<T> Read<T, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderingKey) where T: class
        {
            using (DigiTutorDBEntities dbContext = new DigiTutorDBEntities())
            {
                try
                {
                    return dbContext.Set<T>().Where(predicate).OrderBy(orderingKey).ToList();
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
        public static bool Create<T>(T newObject) where T : class
        {
            using (DigiTutorDBEntities dbContext = new DigiTutorDBEntities())
            {
                try
                {
                    dbContext.Set<T>().Add(newObject);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return true;
                }
                    
            }
        }
        public static bool Delete<T>(T objectToDelete) where T : class
        {
            using (DigiTutorDBEntities dbContext = new DigiTutorDBEntities())
            {
                try
                {
                    dbContext.Set<T>().Attach(objectToDelete);
                    dbContext.Set<T>().Remove(objectToDelete);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
        public static int Update<T>(T modifiedObject) where T : class
        {
            using (DigiTutorDBEntities dbContext = new DigiTutorDBEntities())
            {
                try
                {
                    var entry = dbContext.Entry(modifiedObject);
                    dbContext.Set<T>().Attach(modifiedObject);
                    entry.State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }

            }
        }

    }
}