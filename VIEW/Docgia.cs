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
    public partial class Docgia : Form
    {
        public string madocgia;
        public string textChange;
        public Docgia(string MADOCGIA)
        {
            this.madocgia = MADOCGIA;
            InitializeComponent();
            GUI();
        }

        public void GUI()
        {
            DOCGIA docgia = BLL_DOCGIA.Instance.timDocGiaTheoMaDocGia(madocgia);
            if (BLL_DOCGIA.Instance.kiemtraSVhayGV(docgia))
            {
                List<string> list = BLL_DOCGIA.Instance.getThongtinSinhVien(docgia);
                lbHvt.Text = list[0];
                lbMaso.Text = list[1];
                lbDiachi.Text = list[2];
                lbSachmuon.Text = list[3];
                lbMaSinhVien.Text = list[4];
                lbLSH.Text = list[5];
                lbHocvi.Visible = false;
                lbKhoa.Visible = false;
                label1.Visible = false;
                label9.Visible = false;
            }
            else
            {
                List<string> list = BLL_DOCGIA.Instance.getThongtinGiangVien(docgia);
                lbHvt.Text = list[0];
                lbMaso.Text = list[1];
                lbDiachi.Text = list[2];
                lbSachmuon.Text = list[3];
                lbKhoa.Text = list[4];
                lbHocvi.Text = list[5];
                lbLSH.Visible = false;
                lbMaSinhVien.Visible = false;
                label7.Visible = false;
                label5.Visible = false;
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            Docgia_Sua docgia_sua = new Docgia_Sua(madocgia);
            docgia_sua.d = new Docgia_Sua.Mydel(GUI);
            docgia_sua.Show();
        }
    }
}
