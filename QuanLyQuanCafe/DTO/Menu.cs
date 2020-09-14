using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Menu
    {
        public Menu(string foodName, int quantity, int price, float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.Quantity = quantity;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Menu(DataRow row)
        {
            this.FoodName = row["Name"].ToString();
            this.Quantity = (int) row["quantity"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }
        private string foodName;

        private int quantity;

        private float price;

        private float totalPrice;
        public string FoodName 
        { 
            get { return foodName; }
            set { foodName = value; }
        }
        public int Quantity 
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public float Price 
        {
            get { return price; }
            set { price = value; }
        }

        public float TotalPrice 
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
    }
}
