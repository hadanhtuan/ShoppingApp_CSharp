using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ganss.Excel;

namespace ShoppingApp
{
    public partial class Cart : Form
    {
        private int index;
        private List<int> id_pd;
        private List<int> qt_pd;
        private String filePath1 = Application.StartupPath + @"\assets\Orders.xlsx";
        private List<Orders> listOrders;
        public Cart()
        {
            InitializeComponent();
            ReadData();
            StringToArray(Orders.Instance);
            load_dgv();

        }

        public Cart(int index)
        {
            InitializeComponent();
            ReadData();
            StringToArray(listOrders[index]);
            load_dgv();
            btnConfirm.Visible = false;
            btnReport.Visible = true;
            this.index = index;
            txtAddress.Text = listOrders[index].Address;    
        }

        public Cart(int id, int quantity)
        {
            InitializeComponent();

            if (Orders.Instance.Id_pd == null)
            {
                Orders.Instance.Id_pd = id.ToString();
                Orders.Instance.Quantity_pd = quantity.ToString();
                StringToArray(Orders.Instance);
                load_dgv();
                ReadData();
                return;
            }
            StringToArray(Orders.Instance);

            bool check = false;
            for (int i = 0; i < id_pd.Count; i++)
            {
                if (id_pd[i] == id)
                {
                    check = true;
                    qt_pd[i] += quantity;
                }
            }
            if (!check)
            {
                id_pd.Add(id);
                qt_pd.Add(quantity);
            }
            ArrayToString();

            load_dgv();
            ReadData();
        }

        public void StringToArray(Orders ord)
        {
            id_pd = new List<int>(ord.Id_pd.Split(',').Select(n => Convert.ToInt32(n)).ToArray());
            qt_pd = new List<int>(ord.Quantity_pd.Split(',').Select(n => Convert.ToInt32(n)).ToArray());
        }

        public void ArrayToString()
        {
            Orders.Instance.Id_pd = "";
            foreach (int id in id_pd)
                Orders.Instance.Id_pd += id.ToString() + ",";
            Orders.Instance.Id_pd = Orders.Instance.Id_pd.Substring(0, Orders.Instance.Id_pd.Length - 1);

            Orders.Instance.Quantity_pd = "";
            foreach (int qt in qt_pd)
                Orders.Instance.Quantity_pd += qt.ToString() + ",";
            Orders.Instance.Quantity_pd = Orders.Instance.Quantity_pd.Substring(0, Orders.Instance.Quantity_pd.Length - 1);
        }

        private void ReadData()
        {
            listOrders = new ExcelMapper(filePath1).Fetch<Orders>().ToList();
        }
        void WriteData()
        {
            var execl1 = new ExcelMapper();
            execl1.Save(filePath1, listOrders);

        }

        void createColumnFormDataGridView()
        {
            var colName = new DataGridViewTextBoxColumn();
            var colQuantity = new DataGridViewTextBoxColumn();
            var coltotalPrice = new DataGridViewTextBoxColumn();

            colName.HeaderText = "Tên sản phẩm";
            colQuantity.HeaderText = "Số lượng";
            coltotalPrice.HeaderText = "Giá tiền";

            colName.Width = 180;
            colQuantity.Width = 180;
            coltotalPrice.Width = 180;

            colName.DataPropertyName = "Name";
            colQuantity.DataPropertyName = "Quantity";
            coltotalPrice.DataPropertyName = "TotalPrice";


            dgv1.Columns.AddRange(new DataGridViewColumn[] { colName, colQuantity, coltotalPrice });

        }

        private void load_dgv()
        {
            dgv1.DataSource = null;
            createColumnFormDataGridView();
            List<OrdProducts> l = new List<OrdProducts>();
            int tp = 0;
            for(int i=0; i<id_pd.Count; i++)
            {
                foreach(Product pd in ListProduct.Instance.List)
                {
                    if(pd.Id== id_pd[i])
                    {
                        OrdProducts p = new OrdProducts();
                        p.Id = pd.Id;
                        p.Name = pd.Name;
                        p.Quantity = qt_pd[i];
                        p.TotalPrice = pd.Price * p.Quantity;
                        tp += p.TotalPrice;
                        l.Add(p);
                    }
                   }
            }
            lblTP.Text = tp.ToString();
            for (int i = 0; i < l.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgv1.Rows[0].Clone();
                row.Cells[0].Value = l[i].Name;
                row.Cells[1].Value = l[i].Quantity.ToString();
                row.Cells[2].Value = l[i].TotalPrice.ToString();
                dgv1.Rows.Add(row);
            }
            dgv1.Refresh();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xac nhan thanh toan?", "Thong bao", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            Orders.Instance.Address = txtAddress.Text;
            Orders.Instance.Total_price = Int32.Parse(lblTP.Text);
            Orders.Instance.Date = DateTime.Now.ToShortDateString();
            Orders.Instance.Id = listOrders.Count;
            listOrders.Add(Orders.Instance);
            Orders.Instance = null;
            WriteData();
            MessageBox.Show("Mua hang thanh cong", "Thong bao", MessageBoxButtons.OK);
            List<OrdProducts> l = new List<OrdProducts>();
            dgv1.DataSource = null;
            lblTP.Text = "";
            dgv1.Refresh();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report rp  = new Report(index);
            rp.Show();
        }
    }
}
