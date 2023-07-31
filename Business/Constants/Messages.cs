using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAddedMessage = "Product added!";
        public static string ProductNameInvalid = "Invalid product name!";
        public static string MaintenanceTime = "The system is in maintenance!";
        public static string ProductsListed = "Products are listed!";
        public static string ProductCountOfCategoryError="There can be at most 10 products in each category!";
        internal static string ProductNameRepeatedError= "A product with the same name already exists!";
        internal static string CategoryLimitOverflow="Category limit exceeded!";
    }
}
