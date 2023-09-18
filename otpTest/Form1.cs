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
        public Form1()
        {
            InitializeComponent();
            
        }
        private System.Windows.Forms.Timer aTimer;

        DataAccountContext account = new DataAccountContext();

        DateTime date;
        int otp = 0;//mã otp để gửi cho KH
        int atick=60;//thời gian mã otp có hiệu lực là 60s
        public int randomMaOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);//random cho otp là số có 6 chữ số
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

        public static bool IsValidEmail(string inputEmail)//hàm kiểm tra định dạng mail
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

       
        private bool checkMail(string chkmail)
        {
            List<DataAccount> listaccounts = account.DataAccounts.ToList();

            DataAccount dt = listaccounts.FirstOrDefault(p => p.Email == chkmail);

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
            if (txtMail.Text.Contains("@") && !txtMail.Text.EndsWith(".") && IsValidEmail(txtMail.Text))
            {
                if (checkMail(txtMail.Text) )
                {
                    otp = randomMaOTP();
                    date = DateTime.Now;
                    atick = 60;//khoảng thời gian mã otp hiệu lực là 60s
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

        private void aTimer_Tick(object sender, EventArgs e)// hàm đếm ngược thời gian

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
        int t = 0;//đếm số lần nhập sai otp
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if(otp == 0)//nếu người dùng chưa có mã otp
            {
                MessageBox.Show("Vui lòng chọn gửi mã OTP", "Thông báo");
            }
            else if (t < 3)//nếu người dùng nhập sai =<3 lần
            {
                if ((DateTime.Now - Convert.ToDateTime(date)).TotalSeconds > 60)//nếu thời gian của otp hết hiệu lực
                {
                    MessageBox.Show("Mã OTP đã hết hiệu lực", "Thông báo");
                    t = 0;
                }
                else
                {
                    try//try catch bắt sk nếu otp nhập vào ko phải là số
                    {
                        if (int.Parse(txtOTP.Text) == otp)//nếu otp nhập vào đúng otp đã gửi
                        {
                            List<DataAccount> listaccounts = account.DataAccounts.ToList();

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
                            t++;
                        }
                        
                    }
                    catch
                    {
                        MessageBox.Show("Bạn đã nhập sai", "Thông báo");
                        t++;
                    }
                    
                }
            }
            else//nhập sai quá 3 lần
            {
                MessageBox.Show("Bạn đã nhập sai quá 3 lần. Mã OTP hiện tại hết hiệu lực.", "Thông báo");
                t = 0;
                otp= 0;
                atick = 0;
                aTimer.Stop();
                btnSendOTP.Enabled = true;
                lblTimer.Text = "0s";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTimer.Hide();

        }
    }
}
