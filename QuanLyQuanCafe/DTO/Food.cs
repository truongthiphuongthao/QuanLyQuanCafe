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
                this.CategoryID = -1; 
            }
            else
            {
                this.CategoryID = (int)row["idCategory"];
               
            }
            
            if (row.Table.Columns.Contains("categoryName"))
            {
                this.CategoryName = row["categoryName"].ToString();
            }
            
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }

        private int iD;
        private string name;
        private int categoryID;
        private float price;

        private string categoryName;

        [System.ComponentModel.DisplayName("ID")]
        public int ID 
        {
            get { return iD; }
            set { iD = value; }
        }

        [System.ComponentModel.DisplayName("Tên món")]
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }


        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DisplayName("Phân loại")]
        public int CategoryID 
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        [System.ComponentModel.DisplayName("Giá")]
        public float Price {
            get { return price; }
            set { price = value; }
        }

        [System.ComponentModel.DisplayName("Thuộc loại")]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
    }
}
