using QuanLyQuanCafe.DTO;
using System;
using System.Data;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO()
        {

        }
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("select UserName, DisplayName, Type from dbo.Account");
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[]{userName, displayName, pass, newPass});

            return result > 0;
        }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord ";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[]{userName, passWord});

            return result.Rows.Count > 0;

        }
        
        public Account GetAccountByUserName(string userName )
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.Account where UserName = '" + userName + "'");
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool InsertAccount(string userName, string displayName, int type)
        {
            string query = string.Format("INSERT dbo.Account( UserName, DisplayName, Type) VALUES (N'{0}', N'{1}', {2})", userName, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount(string userName, string displayName, int type)
        {
            string query = string.Format("update dbo.Account set DisplayName = N'{1}', Type = {2} where UserName = N'{0}'", userName, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;

        }
        public bool DeleteAccount(string userName)
        {

            string query = string.Format("Delete Account where UserName = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;

        }

        public bool ResetPassword(string userName)
        {
            string query = string.Format("Update Account set Password = N'0' where UserName = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
