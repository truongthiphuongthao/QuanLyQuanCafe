using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
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
    public partial class dtgvBill1 : Form
    {
        BindingSource foodList = new BindingSource();
        public dtgvBill1()
        {
            InitializeComponent();
            Load();
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

        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #region methods
        void Load()
        {
            dtgvFood.DataSource = foodList;
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBinding();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);

        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut )
        {
           dtgvBill.DataSource =  BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name"));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id"));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price"));

        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        #endregion
        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if(dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
               // if (Int32.TryParse(txbCategoryID.Text, out id))
               // {
                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                int index = -1;
                int i = 0;
                foreach(Category item in cbFoodCategory.Items)
                {
                    if(item.ID == category.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                cbFoodCategory.SelectedIndex = index;
               // }
            }
        }
    }
}
