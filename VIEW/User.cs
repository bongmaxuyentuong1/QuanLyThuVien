using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Entity;
using QuanLyThuVien.BLL;
namespace QuanLyThuVien.VIEW
{
    public partial class User : Form
    {
        string manguoidung;
        public User(string MANGUOIDUNG)
        {
            this.manguoidung = MANGUOIDUNG;
            InitializeComponent();
            GUI();
        }
        public void GUI()
        {
            NGUOIDUNG nd = BLL_NGUOIDUNG.Instance.timNguoiDungTheoMaNguoiDung(manguoidung);
            List<string> list = BLL_NGUOIDUNG.Instance.layThongtinNguoiDungTheoNguoiDung(nd);
            lbHvt.Text = list[0];
            lbMaNV.Text = list[1];
            lbDienthoai.Text = list[2];
            lbNS.Text = list[3];
            lbGioitinh.Text = list[4];
            lbEmail.Text = list[5];
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            User_Sua user_Sua = new User_Sua(null, manguoidung);
            user_Sua.d = new User_Sua.Mydel(GUI);
            user_Sua.Show();
        }
    }
}
