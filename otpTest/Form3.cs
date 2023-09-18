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
using otpTest.Models;
using System.Security.Cryptography;

namespace otpTest
{
    public partial class FormLogin : Form
    {
        DataAccountContext account = new DataAccountContext();

        
        public FormLogin()
        {
            InitializeComponent();
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

        

       
        private void btnQuickLogin_Click(object sender, EventArgs e)
        {

            var image = picQRUpload.Image;
            if(image== null)
            {
                MessageBox.Show("Hãy tải ảnh QR để đăng nhập","Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                var bitmap = new Bitmap(image);
                var barcodeReader = new BarcodeReader();

                // Đọc QR code
                if (barcodeReader.Decode(bitmap) != null)
                {
                    var text = barcodeReader.Decode(bitmap).ToString();
                    List<DataAccount> listaccounts = account.DataAccounts.ToList();

                    DataAccount dt = listaccounts.FirstOrDefault(p => p.UID.ToString() == text.ToString());//tìm người dùng theo uid

                    if (dt != null) // truy vấn xem người dùng có tồn tại trong CSDL chưa
                    {

                        MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK);
                        FormIn4 frm2 = new FormIn4();
                        frm2.GetUid = dt.UID.ToString();
                        this.Hide();
                        frm2.ShowDialog();
                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("Mã QR không tồn tại trong hệ thống", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng tải đúng ảnh mã qr");
                }
            }    

            


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtMailUser.Text=="" || txtPassword.Text == "")
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin đăng nhập", "Thông báo");
            }
            else
            {
                string taiKhoan = txtMailUser.Text.Trim();
                string matKhau = txtPassword.Text.Trim();


                List<DataAccount> listaccounts = account.DataAccounts.ToList();

                DataAccount dt = listaccounts.FirstOrDefault(p => p.Email == taiKhoan);//Tìm người dùng theo email
                if (dt != null)// truy vấn xem người dùng có tồn tại trong CSDL chưa
                {
                    if(dt.MatKhau.Trim() == matKhau)//Kiểm tra mật khẩu của người dùng nhập vào
                    {
                        string uid=dt.UID.ToString(); 
                        MessageBox.Show("Đăng nhập thành công ", "Thông báo",MessageBoxButtons.OK);
                        FormIn4 frm2 = new FormIn4();
                        frm2.GetUid = uid;


                        this.Hide();
                        frm2.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã nhập sai mật khẩu", "Thông báo",MessageBoxButtons.OK);
                    }    
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại","Thông báo", MessageBoxButtons.OK);
                }
            }    
        }
    }
}
