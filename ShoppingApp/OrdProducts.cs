using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    internal class OrdProducts
    {
        private int id;
        private string name;
        private int totalPrice;
        private int quantity;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
