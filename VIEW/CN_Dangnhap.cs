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
using QuanLyThuVien.Entity;
namespace QuanLyThuVien.VIEW
{
    public partial class CN_Dangnhap : Form
    {
        public CN_Dangnhap()
        {
            InitializeComponent();
        }
        private void txtTenDangNhap_Enter(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "Nhập vào tên đăng nhập")
                txtTenDangNhap.Text = "";
            txtTenDangNhap.ForeColor = Color.Black;
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //XuLyDangNhap();
            BLL_PHANQUYEN.Instance.dencentralize(txtTenDangNhap.Text, txtMatKhau.Text);
        }
        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //XuLyDangNhap();
                BLL_PHANQUYEN.Instance.dencentralize(txtTenDangNhap.Text, txtMatKhau.Text);
            }
        }
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (btnShowPass.ImageIndex == 0)
            {
                btnShowPass.ImageIndex = 1;
                txtMatKhau.Focus();
            }
            else
            {
                btnShowPass.ImageIndex = 0;
                txtMatKhau.Focus();
            }
            if (txtMatKhau.PasswordChar == '●')
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhau.PasswordChar = '●';
            }
        }

        private void btnShowPass_MouseHover(object sender, EventArgs e)
        {
            if (btnShowPass.ImageIndex == 0)
                toolTip1.Show("Ẩn mật khẩu", btnShowPass);
            else
                toolTip1.Show("Hiện mật khẩu", btnShowPass);
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text != "" && txtMatKhau.PasswordChar == '●')
            {
                btnDangNhap.BackColor = Color.FromArgb(0, 122, 204);
            }
            else
            {
                btnDangNhap.BackColor = Color.DimGray;
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '●';
            if (txtTenDangNhap.Text != "" && txtMatKhau.PasswordChar == '●')
            {
                btnDangNhap.BackColor = Color.FromArgb(0, 122, 204);
            }
            else
            {
                btnDangNhap.BackColor = Color.DimGray;
            }
        }
        private void txtMatKhau_Enter(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "Nhập vào mật khẩu")
            {
                txtMatKhau.Text = "";
                txtMatKhau.ForeColor = Color.Black;
            }
        }

        private void CN_Dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát phiên đăng nhập?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}

