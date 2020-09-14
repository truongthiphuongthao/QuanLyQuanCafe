using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateCheckin, DateTime? dateCheckout, int status)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckin;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime)row["dateCheckin"];
            var dateCheckOutTemp = row["dateCheckOut"];
            if(dateCheckOutTemp.ToString() != "")   
               this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            this.Status = (int)row["status"];
        }
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int iD;
        private int status;
        public int ID 
        {  
            get { return iD; }
            set { iD = value; } 
        }

        public DateTime? DateCheckIn 
        { 
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        public DateTime? DateCheckOut 
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        public int Status 
        {
            get { return status; }
            set { status = value; }
        }
    }
}
