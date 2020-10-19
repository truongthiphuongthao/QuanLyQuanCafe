using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        public Account(string userName, string displayName, int type, string password = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.Type = type;
            this.Password = password;
        }
        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.DisplayName = row["displayName"].ToString();
            this.Type = (int)row["type"];

            if (row.Table.Columns.Contains("password"))
            {
                this.Password = row["Password"].ToString();
            }
            
            
        }
        private string userName;
        private string displayName;
        private string password;
        private int type;
        [System.ComponentModel.DisplayName("Tên tài khoản")]
        public string UserName 
        {
            get { return userName; }
            set { userName = value; }
        }
        [System.ComponentModel.DisplayName("Tên hiển thị")]
        public string DisplayName 
        {
            get { return displayName; }
            set { displayName = value; }
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DisplayName("Mật khẩu")]
        public string Password 
        {
            get { return password; }
            set { password = value; }
        }
        [System.ComponentModel.DisplayName("Loại tài khoản")]
        public int Type 
        {
            get { return type; }
            set { type = value; }
        }
    }
}

