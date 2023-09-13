using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using System.Drawing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using ZXing.QrCode;

namespace otpTest
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Hide();
            frm1.ShowDialog();
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png, *.jpg, *.bmp)|*.png;*.jpg;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Đọc hình ảnh từ tệp
                var image = Image.FromFile(openFileDialog.FileName);

                // Hiển thị hình ảnh trong picturebox
                picQRUpload.Image = image;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var image = picQRUpload.Image;
            var bitmap = new Bitmap(image);
            // Đọc QR code

            var barcodeReader = new BarcodeReader();
            var text = barcodeReader.Decode(bitmap);
            

            if (text.ToString() == "2World Xin Chào!") //Thay bằng việc truy vấn xem chuỗi có tồn tại trong CSDL chưa

            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo");
                FormIn4 frm2 = new FormIn4();
                this.Hide();
                frm2.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Mã QR không tồn tại trong hệ thống");
            }

            
        }

        private void FormLogin_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}
