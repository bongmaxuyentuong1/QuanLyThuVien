using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.DTO;
using QuanLyThuVien.BLL;

namespace QuanLyThuVien.VIEW
{
    public partial class NXB_Them : Form
    {
        public delegate void reload();
        public reload _reload;
        public NXB_Them()
        {
            InitializeComponent();
            GUI();
        }

        public void GUI()
        {
            string manxbmoi = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_SACH.Instance.getAllMaNXB()));
            lbMaNXBMoi.Text = manxbmoi;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {

            string manxb = lbMaNXBMoi.Text;
            string tennxb = txtTenNXBMoi.Text;
            if (tennxb == null || tennxb.Length == 0)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Nhap ten ngon ngu");
            }
            else
            {
                BLL_SACH.Instance.themNXB(manxb, tennxb);
            }
            //
            _reload();
            GUI();
            this.Close();
        }
    }
}
