﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billID, int foodID, int quantity)
        {
            this.ID = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.Quantity = quantity;
        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["idBill"];
            this.FoodID = (int)row["idFood"];
            this.Quantity = (int)row["quantity"];
        }

        private int quantity;

        private int billID;

        private int foodID;

        private int iD;

        public int ID 
        {
            get { return iD; }
            set { iD = value; } 
        }

        public int Quantity 
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int BillID 
        {
            get { return billID; }
            set { billID = value; }
        }
        public int FoodID 
        {
            get { return foodID; }
            set { foodID = value; }
        }
    }
}
