using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    public class Orders
    {
        private int id;
        private string id_pd;
        private string quantity_pd;
        private int total_price;
        private string date;
        private string address;
        private static Orders instance;

        public int Id { get => id; set => id = value; }
        public string Id_pd { get => id_pd; set => id_pd = value; }
        public string Quantity_pd { get => quantity_pd; set => quantity_pd = value; }
        public int Total_price { get => total_price; set => total_price = value; }
        public string Date { get => date; set => date = value; }
        public string Address { get => address; set => address = value; }
        internal static Orders Instance { get {
                if (instance == null)
                    instance = new Orders();
                return instance; }
                 set => instance = value; }
    }
}
