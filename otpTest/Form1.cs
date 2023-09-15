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

namespace otpTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        DateTime date;
        int otp = 0;
        
        public int randomMaOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp;
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
            SmtpServer.Credentials = new System.Net.NetworkCredential("phat14072009@gmail.com", "lregnmmorismzxvt");

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

        public bool VerifyEmail(string emailVerify)
        {
            
            using (WebClient webclient = new WebClient())
            {
                string url = "http://verify-email.org/ ";
                NameValueCollection formData = new NameValueCollection();
                formData["check"] = emailVerify;
                byte[] responseBytes = webclient.UploadValues(url, "POST", formData);
                string response = Encoding.ASCII.GetString(responseBytes);
                if (response.Contains("Result : OK"))
                {
                    return true;
                }
                return false;
            }
            
            

        }
        public static bool IsValidEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        private bool checkMail(string chkmail)
        {
            
            if (IsValidEmail(chkmail))
                if (VerifyEmail(chkmail))
                    return true;
                else
                {

                    MessageBox.Show("Mail không tồn tại");
                    return false;
                }
            else
            {
                
                return false;
            }
            
        }

        private void btnGuiMaOTP_Click(object sender, EventArgs e)
        {
            if (txtMail.Text.Contains("@") && !txtMail.Text.EndsWith(".") && IsValidEmail(txtMail.Text))
            {
                otp = randomMaOTP();
                date = DateTime.Now;
                GuiMaOTP("phat14072009@gmail.com", txtMail.Text.Trim(), otp);

                
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng mail");
            }
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
                }
                else
                {
                    if (int.Parse(txtOTP.Text) == otp)
                    {
                        MessageBox.Show("Xác nhận thành công", "Thông báo");

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
            }

        }
    }
}
