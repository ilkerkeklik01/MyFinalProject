﻿using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
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

        public List<Product> GetAll()
        {
            //business codes
            //is it has authorization?

            return _productDal.GetAll();


        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
            return _productDal.GetAll(p=> p.CategoryId == categoryId);

        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            
            return _productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<= max);

        }
    }
}