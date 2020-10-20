using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;
using Menu = QuanLyQuanCafe.DTO.Menu;

namespace QuanLyQuanCafe
{
    public partial class TableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount 
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public TableManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();
            LoadComboboxTable(cbSwitchTable);
        }
        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
            
        }


        public void LoadCategory() 
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();

            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";

            cbCategory.SelectedIndex = -1;

        }
        void LoadFoodListByCategoryID(int id) 
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        public void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };

                btn.Text = item.Name + Environment.NewLine + item.Status;

                btn.Click += btn_Click;

                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flpTable.Controls.Add(btn);

            }

        }
      
      
        
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach(Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());

                lsvItem.SubItems.Add(item.Quantity.ToString());

                lsvItem.SubItems.Add(item.Price.ToString());

                lsvItem.SubItems.Add(item.TotalPrice.ToString());

                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);

            }
           CultureInfo culture = new CultureInfo("vi-VN");
           //Thread.CurrentThread.CurrentCulture = culture;
           txbTotalPrice.Text = totalPrice.ToString("c", culture);
           

        }

        public void LoadTableOnComboBox()
        {
            LoadComboboxTable(cbSwitchTable);
        }
        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
            cb.Update();
        }
        #endregion

        #region Events
        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheckOut_Click(this, new EventArgs());

        }

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;

            lsvBill.Tag = (sender as Button).Tag;

            ShowBill(tableID);
        }
        private void TableManager_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nmFoodCount_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountProfile f = new AccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();

        }

        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtgvBill1 f = new dtgvBill1();
            f.loginAccount = LoginAccount;
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.tableManagerForm = this;
            
            f.ShowDialog();
            
        }
        void f_UpdateFood(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem == null)
                return;
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);

        }

        void f_DeleteFood(object sender, EventArgs e)
        {
            if (cbCategory.SelectedItem == null)
                return;
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        void f_InsertFood(object sender, EventArgs e)
        {
            Console.WriteLine(cbCategory);

            if (cbCategory.SelectedItem == null)
                return;
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if(cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            if (selected == null)
                return;
            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if(table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            int foodID = (cbFood.SelectedItem as Food).ID;
           
            int quantity = (int)nmFoodCount.Value;

            if(idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, quantity);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, quantity);
            }

            ShowBill(table.ID);
            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int discount = (int)nmDisCount.Value;
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if(idBill != -1)
            {
                if(MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\n Tổng tiền - (Tổng tiền) x Giảm giá\n => {1} - ({1} / 100) x {2} = {3}",table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float) finalTotalPrice);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
        }


        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
      
            int id1 = (lsvBill.Tag as Table).ID;

            int id2 = (cbSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);

                LoadTable();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cbSwitchTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    #endregion
}
