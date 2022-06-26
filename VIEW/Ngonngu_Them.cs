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
    public partial class Ngonngu_Them : Form
    {
        public delegate void reload();
        public reload _reload;
        public Ngonngu_Them()
        {
            InitializeComponent();
            GUI();
        }
        public void GUI()
        {
            string mangonngumoi = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_SACH.Instance.getAllMaNgonNgu()));
            lbMaNgonNguMoi.Text = mangonngumoi;

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {

            string mangonngu = lbMaNgonNguMoi.Text;
            string tenngongu = txtTenNgonNgu.Text;
            if(tenngongu == null || tenngongu.Length == 0)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Nhap ten ngon ngu");
            }
            else
            {
                BLL_SACH.Instance.themNgonNgu(mangonngu, tenngongu);
            }
            _reload();
            GUI();
        }
    }
}
