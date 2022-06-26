using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien.VIEW
{
    public partial class CN_Thongbao : Form
    {
        public CN_Thongbao()
        {
            InitializeComponent();
        }
        public void setNotice(string s)
        {
            lbThongbao.Text = s;
            this.Show();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

