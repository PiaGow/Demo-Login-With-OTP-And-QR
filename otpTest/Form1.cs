using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using System.Text.RegularExpressions;
using otpTest.Models;
using System.Security.Cryptography;
using System.Reflection.Emit;
using System.Reflection;

namespace otpTest
{
    
    public partial class Form1 : Form
    {
        public static Form1 instance;
        private string mk;
        private string ten;
        public Form1()
        {
            InitializeComponent();
            
        }
        private System.Windows.Forms.Timer aTimer;

        DataAccountContext account = new DataAccountContext();

        DateTime date;
        int otp = 0;
        int atick=60;
        public int randomMaOTP()
        {
            Random random = new Random();
            int rdotp = random.Next(100000, 999999);
            return rdotp;
        }


        public void GuiMaOTP(string nguoiGui, string nguoiNhan,int ma)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(nguoiGui);
            mail.To.Add(nguoiNhan);
            mail.Subject = "Test Mail_send otp ";
            mail.Body = ma.ToString();

            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new System.Net.NetworkCredential("2worldteamsayshi@gmail.com", "qzvrfofkyjxikuzg");

            try
            {
                SmtpServer.Send(mail);
                MessageBox.Show("Gửi mã thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "email",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        //public bool VerifyEmail(string emailVerify)
        //{
            
        //    using (WebClient webclient = new WebClient())
        //    {
        //        string url = "http://verify-email.org/ ";
        //        NameValueCollection formData = new NameValueCollection();
        //        formData["check"] = emailVerify;
        //        byte[] responseBytes = webclient.UploadValues(url, "POST", formData);
        //        string response = Encoding.ASCII.GetString(responseBytes);
        //        if (response.Contains("Result : OK"))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}
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
                if(dt!=null)
                {
                    return true;
                }
                else
                { 
                    
                    return false;
                }
           
        }

        private void btnGuiMaOTP_Click(object sender, EventArgs e)
        {
            if (FormLogin.instance.check == 1)
            {

                if (txtMail.Text.Contains("@") && !txtMail.Text.EndsWith(".") && IsValidEmail(txtMail.Text))
                {
                    if (checkMail(txtMail.Text))
                    {
                        otp = randomMaOTP();
                        date = DateTime.Now;
                        aTimer = new System.Windows.Forms.Timer(); //Khởi tạo đối tượng Timer mới
                        lblTimer.Show();//hiển thi lbl chứa thời gian
                        btnSendOTP.Enabled = false;//tắt chức năng của nút gửi mã OTP
                        aTimer.Tick += new EventHandler(aTimer_Tick); //Tạo sự kiện aTimer_Tick
                        aTimer.Interval = 1000; // thời gian ngắt quãng của Timer là 1 giây
                        aTimer.Start(); //Bắt đầu khởi động Timer
                        lblTimer.Text = atick.ToString(); //Hiển thị biến counter ra Label1
                        GuiMaOTP("2worldteamsayshi@gmail.com", txtMail.Text.Trim(), otp);
                    }
                    else
                    {
                        MessageBox.Show("Mail không tồn tại trong hệ thống");
                    }

                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng mail");
                }
            }
            else 
            {
                
                otp = randomMaOTP();
                date = DateTime.Now;
                aTimer = new System.Windows.Forms.Timer(); //Khởi tạo đối tượng Timer mới
                lblTimer.Show();//hiển thi lbl chứa thời gian
                btnSendOTP.Enabled = false;//tắt chức năng của nút gửi mã OTP
                aTimer.Tick += new EventHandler(aTimer_Tick); //Tạo sự kiện aTimer_Tick
                aTimer.Interval = 1000; // thời gian ngắt quãng của Timer là 1 giây
                aTimer.Start(); //Bắt đầu khởi động Timer
                lblTimer.Text = atick.ToString(); //Hiển thị biến counter ra Label1
                GuiMaOTP("2worldteamsayshi@gmail.com", Register.instance.mail.Trim(), otp);
            }
        }

        private void aTimer_Tick(object sender, EventArgs e)

        {

            atick--;
            if (atick == 0)
            {
                btnSendOTP.Enabled = true;
                aTimer.Stop();
            }

            lblTimer.Text = atick.ToString()+"s";

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            FormLogin frm3 = new FormLogin();
            this.Hide();
            frm3.ShowDialog();
            this.Close();
        }
        int t = 0;
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if(otp == 0)
            {
                MessageBox.Show("Vui lòng chọn gửi mã OTP", "Thông báo");
            }
            else if (t < 3)
            {
                if ((DateTime.Now - Convert.ToDateTime(date)).TotalSeconds > 60)
                {
                    MessageBox.Show("Mã OTP đã hết hiệu lực", "Thông báo");
                    t = 0;
                    atick = 60;
                }
                else
                {
                    if (int.Parse(txtOTP.Text) == otp)
                    {
                        List<DataAccount> listaccounts = account.DataAccounts.ToList();
                        DataAccount acc = new DataAccount();
                        acc.UID = randomMaOTP().ToString();
                        acc.Email =txtMail.Text;
                        acc.TenNguoiDung = ten;
                        acc.MatKhau = mk;
                        listaccounts.Add(acc);
                        DataAccount dt = listaccounts.FirstOrDefault(p => p.Email == txtMail.Text);

                        MessageBox.Show("Xác nhận thành công", "Thông báo");
                        FormIn4 frm = new FormIn4();
                        frm.GetUid = dt.UID.Trim();
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã nhập sai", "Thông báo");
                    }
                    t++;
                }
            }
            else
            {
                MessageBox.Show("Bạn đã nhập sai quá 3 lần. Mã OTP hiện tại hết hiệu lực.", "Thông báo");
                t = 0;
                otp= 0;
                atick = 60;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTimer.Hide();
            if(FormLogin.instance.check == -1)
            {
                lblNhapMail.Visible = false;
                txtMail.Visible = false;
            }

        }
    }
}
