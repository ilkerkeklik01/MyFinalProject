using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    // new() means It can be newable :) so T cannot be IEntity interface
    //Can be a normal class implements IEntity
    public interface IEntityRepository<T>where T : IEntity, new()

    {
                                                //no need to give filter if you want
                                                //delegate
        List<T> GetAll(Expression<Func<T,bool>> filter =null);
        T GetValue(Expression<Func<T, bool>> filter); 
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        //List<T> GetAllByCategory(int categoryId);



    }
}
