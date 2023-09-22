using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace otpTest
{
    public partial class Register : Form
    {
        public static Register instance = new Register();
        public string mail;
        public Register()
        {
            InitializeComponent();
            instance = this;
        }
        Model1 account = new Model1();

        private void Register_Load(object sender, EventArgs e)
        {

        }
        public bool IsValidEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
            {

                return (false);
            }

        }
        public bool checkMail(string chkmail)
        {
            List<DataAccount> listaccounts = account.DataAccounts.ToList();

            DataAccount dt = listaccounts.FirstOrDefault(p => p.Email == chkmail);


            //if (VerifyEmail(chkmail))
            //    return true;
            if (dt != null)
            {
                return true;
            }
            else
            {

                return false;
            }

        }
        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            FormLogin.instance.check = -1;
            if (txtMailUser.Text != string.Empty && txtPassword.Text != string.Empty && txtNhapLaiMatKhau.Text != string.Empty && txtTenNguoiDung.Text != string.Empty)
            {
                if (txtPassword.Text == txtNhapLaiMatKhau.Text && IsValidEmail(txtMailUser.Text) && txtMailUser.Text.Contains("@") && !txtMailUser.Text.EndsWith("."))
                {
                    if (!checkMail(txtMailUser.Text))
                    {
                        mail = txtMailUser.Text;
                        this.Close();
                        this.Hide();
                        Form1 frm1 = new Form1();
                        frm1.Mk=txtPassword.Text;
                        frm1.Ten=txtTenNguoiDung.Text;
                        frm1.ShowDialog();
                        frm1.Show();

                    }
                    else
                        MessageBox.Show("Mail nay da duoc su dung!");
                }
                else
                    MessageBox.Show("Email hoac mat khau sai dinh dang!");
            }
            else
                MessageBox.Show("Vui long nhap day du thong tin");

        }
    }
}
