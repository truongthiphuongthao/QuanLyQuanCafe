using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {   public Food(int id, string name, int categoryID, float price)
        {
            this.ID = id;
            this.Name = name;
            this.categoryID = categoryID;
            this.price = price;
        }
        public Food(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            if (row["idCategory"] == DBNull.Value)
            {
                this.categoryID = -1; 
            }
            else
            {
                this.categoryID = (int)row["idCategory"];
            }
            
            
            this.price = (float)Convert.ToDouble(row["price"].ToString());
        }
        private int iD;
        private string name;
        private int categoryID;
        private float price;

        public int ID 
        {
            get { return iD; }
            set { iD = value; }
        }
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public int CategoryID 
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public float Price {
            get { return price; }
            set { price = value; }
        }
    }
}
