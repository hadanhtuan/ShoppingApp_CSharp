using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    public class Product
    {
        private int id;
        private string name;
        private string category;
        private int price;
        private int quantity;
        private String img_url;
        private string description;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Img_url { get => img_url; set => img_url = value; }
        public string Description { get => description; set => description = value; }
        public string Category { get => category; set => category = value; }
    }
}
