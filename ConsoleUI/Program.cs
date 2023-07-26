using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI
{

    //SOLID
    //Open Closed Principle
    internal class Program
    {
        static void Main(string[] args)
        {

            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var item in productManager.GetByUnitPrice(50,100))
            {
                Console.WriteLine(item.ProductName +" " +item.UnitPrice);
            }



        }
    }
}