using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    internal class ListProduct
    {
        List<Product> list;
        private static ListProduct instance = new ListProduct();

        internal List<Product> List { get => list; set => list = value; }
        internal static ListProduct Instance { get => instance; set => instance = value; }
    }
}
