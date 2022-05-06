using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingApp
{
    public partial class Seen : Form
    {
        public Seen()
        {
            InitializeComponent();
        }

        public Seen(List<Product> list)
        {
            InitializeComponent();
            this.list = list;
        }

        List<Product> list;
        void createColumnFormDataGridView()
        {
            var colId = new DataGridViewTextBoxColumn();
            var colName = new DataGridViewTextBoxColumn();
            var colCategory = new DataGridViewTextBoxColumn();

            colId.HeaderText = "Mã sản phẩm";
            colName.HeaderText = "Tên sản phẩm";
            colCategory.HeaderText = "Thể loại";

            colId.Width = 180;
            colName.Width = 180;
            colCategory.Width = 180;

            colId.DataPropertyName = "Id";
            colName.DataPropertyName = "Name";
            colCategory.DataPropertyName = "Category";

            dgvseen.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colCategory });
        }

        private void load_dgv()
        {
            createColumnFormDataGridView();
            
            for (int i = 0; i < list.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgvseen.Rows[0].Clone();
                row.Cells[0].Value = list[i].Id.ToString();
                row.Cells[1].Value = list[i].Name;
                row.Cells[2].Value = list[i].Category;
                dgvseen.Rows.Add(row);
            }
            dgvseen.Refresh();
        }

        private void Seen_Load(object sender, EventArgs e)
        {
            load_dgv();
        }
    }
}
