using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QRCoder;
using BarcodeLib;

namespace ShoppingApp
{
    public partial class Detail : Form
    {
        private int index = -1;
        public bool isExit = true;
        public event EventHandler<EventArgs> ExitShow;

        public Detail()
        {
            InitializeComponent();
        }

        public Detail(int index)
        {
            this.index = index;
            InitializeComponent();
        }

        private void Detail_Load(object sender, EventArgs e)
        {
            Product pd = ListProduct.Instance.List[index];

            lblName.Text = pd.Name;
            lblPrice.Text = "Gia: "+pd.Price;
            lblQuantity.Text = "So luong con lai: "+pd.Quantity.ToString();
            lblDes.Text = pd.Description;
            Image img = Image.FromFile(Application.StartupPath + pd.Img_url);
            ptb1.Image = img;
            ptb1.SizeMode = PictureBoxSizeMode.StretchImage;
            
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCode qrCode = new QRCode(qrCodeGenerator.CreateQrCode(pd.Id.ToString(), QRCodeGenerator.ECCLevel.Q));
            picQR.Image = qrCode.GetGraphic(2, Color.Black, Color.White, false);
            qrCodeGenerator.Dispose();
            qrCode.Dispose();


        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            int value1 = ListProduct.Instance.List[index].Quantity;
            int value2 = Int32.Parse(txtQuantity.Text);

            if (value1 == value2)
                return;
            txtQuantity.Text = (Int32.Parse(txtQuantity.Text)+1).ToString();  
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            int value = Int32.Parse(txtQuantity.Text);
            if (value <= 1)
                return;
            txtQuantity.Text = (Int32.Parse(txtQuantity.Text) - 1).ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitShow(this, new EventArgs());//event cũng giống như tham chiếu đến 1 hàm
        }

        private void Detail_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cart fc = new Cart(index, Int32.Parse(txtQuantity.Text));
            fc.Show();
            ListProduct.Instance.List[index].Quantity -= Int32.Parse(txtQuantity.Text);
        }
    }
}
