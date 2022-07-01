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
    public partial class Thongke_Docgiamuonnhieu : Form
    {
        public Thongke_Docgiamuonnhieu()
        {
            InitializeComponent();
            ShowDocGiaMuonNhieu();
            setDGVColumnsHeader();
        }
        private void ShowDocGiaMuonNhieu()
        {
            List<DOCGIA> data = BLL_DOCGIA.Instance.getDocGiaMuonNhieu();
            dataGridView1.DataSource = data;
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã độc giả";
            dataGridView1.Columns[1].HeaderText = "Họ tên";
            dataGridView1.Columns[2].HeaderText = "Địa chỉ";
            dataGridView1.Columns[3].HeaderText = "Số sách mượn";
            dataGridView1.Columns[4].Visible = false;
        }

        public void setDGVSVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã độc giả";
            dataGridView1.Columns[1].HeaderText = "Họ tên";
            dataGridView1.Columns[2].HeaderText = "Địa chỉ";
            dataGridView1.Columns[3].HeaderText = "Số sách mượn";
            dataGridView1.Columns[4].HeaderText = "Mã sinh viên";
            dataGridView1.Columns[5].HeaderText = "Lớp sinh hoạt";
        }
        public void setDGVGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã độc giả";
            dataGridView1.Columns[1].HeaderText = "Họ tên";
            dataGridView1.Columns[2].HeaderText = "Địa chỉ";
            dataGridView1.Columns[3].HeaderText = "Số sách mượn";
            dataGridView1.Columns[4].HeaderText = "Học vị";
            dataGridView1.Columns[5].HeaderText = "Khoa";
        }
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string MaDocGia = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if (BLL_DOCGIA.Instance.getChiTietSinhVien(MaDocGia).Count == 0)
                {
                    dataGridView1.DataSource = BLL_DOCGIA.Instance.getChiTietGiangVien(MaDocGia);
                    setDGVGVColumnsHeader();
                }
                else
                {
                    dataGridView1.DataSource = BLL_DOCGIA.Instance.getChiTietSinhVien(MaDocGia);
                    setDGVSVColumnsHeader();
                }
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Chọn một đối tượng để xem thông tin");
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            ShowDocGiaMuonNhieu();
            setDGVColumnsHeader();
        }
    }
}
