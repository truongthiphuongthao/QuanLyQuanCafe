using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class dtgvBill : Form
    {
        public dtgvBill()
        {
            InitializeComponent();
            LoadAccountList();
        }
        // kết nối database 
        void LoadAccountList()
        {
            string connectionSTR = "Data Source = (local); Initial Catalog = QuanLyQuanCafe; Integrated Security = True";

            SqlConnection connection = new SqlConnection(connectionSTR);

            string query = "SELECT * FROM dbo.Account";

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);

            connection.Close();

            dtgvAccount.DataSource = data;


        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tpAccount_Click(object sender, EventArgs e)
        {

        }
    }
}
