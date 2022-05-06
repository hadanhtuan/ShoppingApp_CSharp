using Microsoft.Reporting.WinForms;
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
    public partial class Report : Form
    {
        private int index;
        private List<int> id_pd;
        private List<int> qt_pd;
        private String filePath1 = Application.StartupPath + @"\assets\Orders.xlsx";
        private List<Orders> listOrders;

        public Report()
        {
            InitializeComponent();
        }

        public Report(int id)
        {
            InitializeComponent();
            index = id;
        }

        private void Report_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("TotalPrice", typeof(string));

            ReadData();
            StringToArray(listOrders[index]);
            List<OrdProducts> l = new List<OrdProducts>();

            for (int i = 0; i < id_pd.Count; i++)
            {
                foreach (Product pd in ListProduct.Instance.List)
                {
                    if (pd.Id == id_pd[i])
                    {
                        OrdProducts p = new OrdProducts();
                        p.Id = pd.Id;
                        p.Name = pd.Name;
                        p.Quantity = qt_pd[i];
                        p.TotalPrice = pd.Price * p.Quantity;
                        l.Add(p);
                    }
                }
            }
            for (int i = 0; i < l.Count; i++)
            {
                dt.Rows.Add(i.ToString(), l[i].Name, l[i].Quantity.ToString(), l[i].TotalPrice.ToString());
            }

            ReportParameter[] parameter = new ReportParameter[4];
            parameter[0] = new ReportParameter("Id");
            parameter[0].Values.Add(listOrders[index].Id.ToString());
            parameter[1] = new ReportParameter("TotalPrice");
            parameter[1].Values.Add(listOrders[index].Total_price.ToString());
            parameter[2] = new ReportParameter("Address");
            parameter[2].Values.Add(listOrders[index].Address);
            parameter[3] = new ReportParameter("Date");
            parameter[3].Values.Add(listOrders[index].Date);

            this.reportViewer1.LocalReport.SetParameters(parameter);

            var reportDataSource1 = new ReportDataSource("DataSet1", dt);

            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ShoppingApp.Report1.rdlc";

           this.reportViewer1.RefreshReport();
        }

        public void StringToArray(Orders ord)
        {
            id_pd = new List<int>(ord.Id_pd.Split(',').Select(n => Convert.ToInt32(n)).ToArray());
            qt_pd = new List<int>(ord.Quantity_pd.Split(',').Select(n => Convert.ToInt32(n)).ToArray());
        }

        private void ReadData()
        {
            listOrders = new ExcelMapper(filePath1).Fetch<Orders>().ToList();
        }

    }
}
