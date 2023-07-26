using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{    
    //Data Access Layer Interface of Product
    public interface IProductDal:IEntityRepository<Product>
    {

        
    }


}
