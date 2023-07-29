using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        //Connect with abstract class
        IProductDal _productDal;
        
        //when the software needs to change the type of ProductDal
        //IT is the easiest way to implement like this
        //just give the parameter that implements IProductDal
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {

            if (product.ProductName.Length < 2)
            {
                //magic strings 
                return new ErrorResult(Messages.ProductNameInvalid);
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAddedMessage);

        }

        public IDataResult<List<Product>> GetAll()
        {
            //business codes
            //is it has authorization?

            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }


            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);


        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.CategoryId == categoryId));

        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));

        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<= max));

        }

        public IDataResult<List<ProductDetailDTO>> GetProductDetails()
        {
            //if (DateTime.Now.Hour == 19)
            //{
            //    return new ErrorDataResult<List<ProductDetailDTO>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<ProductDetailDTO>>(_productDal.GetProductDetails());
        }

    }
}
