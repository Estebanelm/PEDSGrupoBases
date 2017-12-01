using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DigiTutorService.DataAccessLayer.Repository
{
    public class RepositoryDAL
    {
        DigiTutorDBEntities dbContext;

        public RepositoryDAL()
        {
            dbContext = new DigiTutorDBEntities();
        }
        public List<T> Read<T>() where T : class
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
        public List<T> Read<T>(Expression<Func<T, bool>> predicate) where T:class
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
        public List<T> Read<T, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderingKey) where T: class
        {
                try
                {
                    return dbContext.Set<T>().Where(predicate).OrderByDescending(orderingKey).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
        }
        public  bool Create<T>(T newObject) where T : class
        {
                try
                {
                    dbContext.Set<T>().Add(newObject);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
        }
        public bool Create<T>(List<T> listofObjects) where T : class
        {
                try
                {
                    foreach(T obj in listofObjects)
                    {
                        dbContext.Set<T>().Add(obj);
                    }
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
        }
        public bool Delete<T>(T objectToDelete) where T : class
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
        public int Update<T>(T modifiedObject) where T : class
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