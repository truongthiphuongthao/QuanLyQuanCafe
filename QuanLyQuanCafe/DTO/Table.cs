using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Table
    {
        public Table(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }
        private string status;
        private string name;
        private int iD;

        [System.ComponentModel.DisplayName("ID")]
        public int ID 
        {
            get { return iD; }
            set { iD = value; } 
        }
        [System.ComponentModel.DisplayName("Tên bàn")]
        public string Name 
        { 
            get { return name;  } 
            set { name = value;  } 
        }
        [System.ComponentModel.DisplayName("Trạng thái")]
        public string Status 
        { 
            get { return status;  }
            set { status = value;  }
        }
    }
}
