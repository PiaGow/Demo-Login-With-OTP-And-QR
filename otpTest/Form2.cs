using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace otpTest
{
    public partial class FormIn4 : Form
    {
        Model1 account = new Model1();

        private string uid;
        public string GetUid
        {
            get { return uid; }
            set { uid = value; }
        }
        public FormIn4()
        {
            InitializeComponent();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var image = picUserQR.Image;

            // Mở hộp thoại lưu tệp
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png, *.jpg, *.bmp)|*.png;*.jpg;*.bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lưu hình ảnh
                image.Save(saveFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLogin frm3 = new FormLogin();
            this.Hide();
            frm3.ShowDialog();
            this.Close();
        }

        private void FormIn4_Load(object sender, EventArgs e)
        {


            List<DataAccount> listaccounts = account.DataAccounts.ToList();

            DataAccount dt = listaccounts.FirstOrDefault(p => p.UID.ToString().Trim() == uid.ToString().Trim());//Tìm người dùng theo uid


            labName.Text = dt.TenNguoiDung.ToString();
            txtName.Text = dt.TenNguoiDung.ToString();
            txtName.Text = dt.TenNguoiDung.ToString();
            txtEmail.Text = dt.Email.ToString();

            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = 253, Height = 250, Margin = 0, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap = barcodeWriter.Write(uid.ToString());//Lấy dữ liệu  = UID bên SQL sang
            Bitmap logo = new Bitmap($"{Application.StartupPath}/logo.png");
            Bitmap resize_logo = new Bitmap(logo, new Size(100, 100));
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(resize_logo, new Point((bitmap.Width - (resize_logo.Width)) / 2, (bitmap.Height - (resize_logo.Height)) / 2));
            picUserQR.Image = bitmap;
        }
    }
}
