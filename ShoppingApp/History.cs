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
    public partial class History : Form
    {
        private String filePath1 = Application.StartupPath + @"\assets\Orders.xlsx";
        private List<Orders> listOrders;

        public History()
        {    
            InitializeComponent();
            ReadData();
            loadListPhoneBook();
        }

        private void dgvHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            Cart c = new Cart(index);
            c.Show();
        }

        private void ReadData()
        {
            listOrders = new ExcelMapper(filePath1).Fetch<Orders>().ToList();
        }

        void createColumnFormDataGridView()
        {
            var colId = new DataGridViewTextBoxColumn();
            var colTotalPrice = new DataGridViewTextBoxColumn();
            var colDate = new DataGridViewTextBoxColumn();

            colId.HeaderText = "Mã hóa đơn";
            colTotalPrice.HeaderText = "Thành tiền";
            colDate.HeaderText = "Ngày hóa đơn";

            colId.Width = 180;
            colTotalPrice.Width = 180;
            colDate.Width = 180;

            colId.DataPropertyName = "Id";
            colTotalPrice.DataPropertyName = "TotalPrice";
            colDate.DataPropertyName = "Date";

            dgvHistory.Columns.AddRange(new DataGridViewColumn[] { colId, colTotalPrice, colDate });
        }
 
        void loadListPhoneBook()
        {
            dgvHistory.DataSource = null;
            createColumnFormDataGridView();
            //dgvHistory.DataSource = listOrders;
            for(int i=0; i< listOrders.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgvHistory.Rows[0].Clone();
                row.Cells[0].Value = listOrders[i].Id.ToString();
                row.Cells[1].Value = listOrders[i].Total_price.ToString();
                row.Cells[2].Value = listOrders[i].Date;
                dgvHistory.Rows.Add(row);
            }
        }
    }
}
