using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otpTest
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png, *.jpg, *.bmp)|*.png;*.jpg;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Đọc hình ảnh từ tệp
                var image = Image.FromFile(openFileDialog.FileName);

                // Hiển thị hình ảnh trong picturebox
                pictureBox1.Image = image;
            }
        }
    }
}
