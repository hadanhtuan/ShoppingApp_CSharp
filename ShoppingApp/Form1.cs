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

using QRCoder;
using BarcodeLib;

using ZXing;

using AForge.Video;
using AForge.Video.DirectShow;

namespace ShoppingApp
{
    public partial class Form1 : Form
    {
        private String filePath1 = Application.StartupPath + @"\assets\Product.xlsx";
        ImageList imgListLarge;

        FilterInfoCollection f;
        VideoCaptureDevice v = new VideoCaptureDevice();
        BarcodeLib.Barcode code128;
        bool isScan = true;
        List<Product> seen = new List<Product>();

        public Form1()
        {
            InitializeComponent();
        }

        private void ReadData()
        {
            ListProduct.Instance.List = new ExcelMapper(filePath1).Fetch<Product>().ToList();
        }
        void WriteData()
        {
            var execl1 = new ExcelMapper();
            execl1.Save(filePath1, ListProduct.Instance.List);

        }

        void LoadImageList()
        {
            imgListLarge = new ImageList() { ImageSize = new Size(150, 200) };
            foreach (var product in ListProduct.Instance.List)
            {
                imgListLarge.Images.Add(new Bitmap(Application.StartupPath + product.Img_url));
            }
        }

        void LoadListView(List<Product> list)
        {
            lsv1.Items.Clear();
            lsv1.BorderStyle = BorderStyle.None;
            LoadImageList();
            lsv1.LargeImageList = imgListLarge;

            foreach (var product in list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = product.Name+"\n"+product.Price.ToString()+"d";
                item.ImageIndex = product.Id;
                lsv1.Items.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadData();
            LoadListView(ListProduct.Instance.List);

            code128 = new Barcode();

            f = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo item in f)
                cboDevice.Items.Add(item.Name);
            cboDevice.SelectedIndex = 0;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            clearCategoryColor();
            string key = txtSearch.Text;
            List<Product> list = new List<Product>();
            foreach (var item in ListProduct.Instance.List)
            {
                if (item.Name.ToLower().Contains(key.ToLower()))
                    list.Add(item);
            }

            LoadListView(list);
        }

        private void clearSearch()
        {
            txtSearch.Text = "";
        }
        private void Categories_Click(object sender, EventArgs e)
        {
            clearSearch();
            clearCategoryColor();
            Label label = sender as Label;

            label.ForeColor = Color.Black;
            string category = label.Text;
            List<Product> list = new List<Product>();

            foreach (var item in ListProduct.Instance.List)
            {
                if (item.Category.ToLower() == category.ToLower())
                    list.Add(item);
            }

            LoadListView(list);
        }

        void clearCategoryColor()
        {
            foreach (var item in pnlCategories.Controls)
            {
                var item1 = item as Label;
                item1.ForeColor = Color.White;
            }
        }

        private void lsv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lsv = sender as ListView;

            foreach (ListViewItem item in lsv.SelectedItems)
            {
                Detail form = new Detail(item.ImageIndex);
                this.Hide();
                form.Show();
                form.ExitShow += Form_ExitShow;

                seen.Add(ListProduct.Instance.List[item.ImageIndex]);

            }

        }


        private void Form_ExitShow(object sender, EventArgs e)
        {
            (sender as Detail).isExit = false;
            (sender as Detail).Close();
            this.Show();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            if (Orders.Instance.Id_pd == null)
            {
                MessageBox.Show("Không có sản phẩm trong giỏ hàng", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            Cart fc = new Cart();
            fc.Show();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            History ht = new History();
            ht.Show();
        }
        //-----qrcode scan--------
        private void btnStart_Click(object sender, EventArgs e)
        {
            isScan = !isScan;
            if (!isScan)
            {
                lsv1.Visible = false;
                p1.Visible = true;
                cboDevice.Visible = true;
                v = new VideoCaptureDevice(f[cboDevice.SelectedIndex].MonikerString);
                v.NewFrame += V_NewFrame;
                v.Start();
                timer1.Start();
            }
            else
            {
                lsv1.Visible = true;
                p1.Visible = false;
                cboDevice.Visible = false;
            }
        }

        private void V_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            p1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (v.IsRunning)
                v.Stop();
            WriteData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (p1.Image != null)
            {
                BarcodeReader br = new BarcodeReader();
                Result r = br.Decode((Bitmap)p1.Image);
                if (r != null)
                {
                    v.Stop();

                    Detail form = new Detail(Int32.Parse(r.ToString()));
                    this.Hide();
                    form.Show();
                    form.ExitShow += Form_ExitShow;
                    timer1.Stop();
                    //if (v.IsRunning)
                    //{
                    //    v.Stop();
                    //}
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadListView(ListProduct.Instance.List);
            clearCategoryColor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Seen s = new Seen(seen);
            s.Show();
        }
    }
  
}
