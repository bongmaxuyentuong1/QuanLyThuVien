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
    public partial class Thongke_Sachhet : Form
    {
        public string manguoidung;

        public Thongke_Sachhet(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            showSachHet();
        }
        private void showSachHet()
        {
            NGUOIDUNG nd = BLL_NGUOIDUNG.Instance.timNguoiDungTheoMaNguoiDung(this.manguoidung);
            string role = nd.MANHIEMVU;
            if(role == "001")
            {
                dataGridView1.DataSource = BLL_SACH.Instance.getSachHetAdmin();
            }
            else
            {
                dataGridView1.DataSource = BLL_SACH.Instance.getSachHetThuthu();
            }
        }
    }
}
