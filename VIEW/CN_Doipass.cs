using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.BLL;
namespace QuanLyThuVien.VIEW
{
    public partial class CN_Doipass : Form
    {
        private string taikhoan = null; // tai khoan
        public CN_Doipass(string user = null)
        {
            InitializeComponent();
            taikhoan = user;
        }
        public static void show_Message(string message)
        {
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice(message);
        }
        private void txtMKC_TextChanged(object sender, EventArgs e)
        {
            txtMKC.PasswordChar = '●';
            if (txtMKM.PasswordChar == '●' && txtMKC.PasswordChar == '●' && txtXNMK.PasswordChar == '●')
            {
                btnOK.BackColor = Color.FromArgb(0, 122, 204);
            }
            else
            {
                btnOK.BackColor = Color.DimGray;
            }
        }

        private void txtMKC_Enter(object sender, EventArgs e)
        {
            if (txtMKC.Text == "Nhập vào mật khẩu cũ")
            {
                txtMKC.Text = "";
                txtMKC.ForeColor = Color.Black;
            }
        }

        private void txtMKC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void txtMKM_TextChanged(object sender, EventArgs e)
        {
            txtMKM.PasswordChar = '●';
            if (txtMKM.PasswordChar == '●' && txtMKC.PasswordChar == '●' && txtXNMK.PasswordChar == '●')
            {
                btnOK.BackColor = Color.FromArgb(0, 122, 204);
            }
            else
            {
                btnOK.BackColor = Color.DimGray;
            }
        }

        private void txtMKM_Enter(object sender, EventArgs e)
        {
            if (txtMKC.Text == "Nhập vào mật khẩu mới")
            {
                txtMKC.Text = "";
                txtMKC.ForeColor = Color.Black;
            }
        }
        private void txtMKM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void txtXNMK_TextChanged(object sender, EventArgs e)
        {
            txtXNMK.PasswordChar = '●';
            if (txtMKM.PasswordChar == '●' && txtMKC.PasswordChar == '●' && txtXNMK.PasswordChar == '●')
            {
                btnOK.BackColor = Color.FromArgb(0, 122, 204);
            }
            else
            {
                btnOK.BackColor = Color.DimGray;
            }
        }
        private void txtXNMK_Enter(object sender, EventArgs e)
        {
            if (txtMKC.Text == "Xác nhận mật khẩu mới")
            {
                txtMKC.Text = "";
                txtMKC.ForeColor = Color.Black;
            }
        }
        private void txtXNMK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XuLyMatKhau();
            }
        }
        private void XuLyMatKhau()
        {
            string oPass = txtMKC.Text;
            string nPass = txtMKM.Text;
            string rPass = txtXNMK.Text;
            string manguoidung = BLL_NGUOIDUNG.Instance.findUserByTaikhoan(taikhoan).MANGUOIDUNG;
            string result = BLL_PHANQUYEN.Instance.changePassword(manguoidung, oPass, nPass, rPass);
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice(result);
        }
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            XuLyMatKhau();
        }
    }
}
