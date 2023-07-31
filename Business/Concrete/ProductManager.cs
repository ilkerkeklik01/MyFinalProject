using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        ICategoryService _categoryService;
        
        //when the software needs to change the type of ProductDal
        //IT is the easiest way to implement like this
        //just give the parameter that implements IProductDal
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //warning
        [ValidationAspect(typeof(ProductValidator))]

        public IResult Add(Product product)
        {
            //business code
            //validation

            //warning
            //ValidationTool.Validate(new ProductValidator(),product);
            //Eğer mevcut kategori sayısı 15 i geçtiyse sisteme yeni ürün eklenemez!


            IResult result = BusinessRules.Run(CheckIfProductNameNotRepeated(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded()
                );



            if(result != null)
            {
                return result;
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


        [ValidationAspect(typeof(ProductValidator))]

        public IResult Update(Product product)
        {
            BusinessRules.Run(CheckIfProductNameNotRepeated(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId)
                );

            _productDal.Update(product);
            return new SuccessResult(Messages.ProductAddedMessage);
        }


        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            else return new SuccessResult();

        }
        private IResult CheckIfProductNameNotRepeated(string productName)
        {
            var result = _productDal.GetAll(p=>p.ProductName == productName).Any();

            if (result) return new ErrorResult(Messages.ProductNameRepeatedError);
            
            return new SuccessResult();

        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if(result.Data.Count > 15) {
                return new ErrorResult(Messages.CategoryLimitOverflow);
            }
            return new SuccessResult();
        }

    }
}
