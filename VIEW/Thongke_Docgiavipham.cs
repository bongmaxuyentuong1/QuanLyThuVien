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
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;
namespace QuanLyThuVien.VIEW
{
    public partial class Thongke_Docgiavipham : Form
    {
        public Thongke_Docgiavipham()
        {
            InitializeComponent();
            ShowDocGiaVipham();
        }
        private void ShowDocGiaVipham()
        {
            dataGridView1.DataSource = BLL_DOCGIA.Instance.getDocGiaVipham();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            ShowDocGiaVipham();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string MaDocGia = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if (BLL_DOCGIA.Instance.getChiTietSinhVien(MaDocGia).Count == 0)
                {
                    dataGridView1.DataSource = BLL_DOCGIA.Instance.getChiTietGiangVien(MaDocGia);
                }
                else
                {
                    dataGridView1.DataSource = BLL_DOCGIA.Instance.getChiTietSinhVien(MaDocGia);
                }
            }
            else
            {
                //MessageBox.Show("Chon mot doi tuong de xem thong tin");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn một đối tượng để xem thông tin!");
            }
        }
    }
}
