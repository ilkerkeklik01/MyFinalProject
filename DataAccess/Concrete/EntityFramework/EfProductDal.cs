using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet installed in DataAccess project 
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {

            //Garbage collector removes it imidiately when it is done 
            //IDispossable pattern implementation of c#
            using (NorthwindContext northwindContext = new NorthwindContext())
            {
                var addedEntity = northwindContext.Entry(entity);
                addedEntity.State = EntityState.Added;
                northwindContext.SaveChanges();
            }

        }

        public void Delete(Product entity)
        {
            using (NorthwindContext northwindContext = new NorthwindContext())
            {
                var deletedEntity = northwindContext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                northwindContext.SaveChanges();
            }


        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            List<Product> products;
            using(NorthwindContext northwindContext = new NorthwindContext()){

                return filter == null ? northwindContext.Set<Product>().ToList() :
                    northwindContext.Set<Product>().Where(filter).ToList();
            }


        }

        public Product GetValue(Expression<Func<Product, bool>> filter)
        {
            using(NorthwindContext northwindContext=new NorthwindContext())
            {
                return northwindContext.Set<Product>().SingleOrDefault(filter);
            }

        }

        public void Update(Product entity)
        {
            using (NorthwindContext northwindContext = new NorthwindContext())
            {
                var updatedEntity = northwindContext.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                northwindContext.SaveChanges();
            }
        }
    }
}
