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
    public partial class PM_Sua : Form
    {
        private string manguoidung;
        private string maphieumuon;
        private PHIEUMUON phieumuon;
        public PM_Sua(string manguoidung)
        {
            InitializeComponent();
            this.manguoidung = manguoidung;
            GUI();
            unableGroup2();
            unableGroup3();
        }
        public PM_Sua(string manguoidung, string maphieumuon)
        {
            InitializeComponent();
            this.maphieumuon = maphieumuon;
            this.manguoidung = manguoidung;
            this.phieumuon = BLL_PHIEUMUON.Instance.timPhieuMuonTheoMaPhieu(this.maphieumuon);
            GUI();
            enableGroup2();
            unableGroup3();
        }
        public void setDGV1ColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã phiếu mượn";
            dataGridView1.Columns[1].HeaderText = "Mã người dùng";
            dataGridView1.Columns[2].HeaderText = "Mã độc giả";
            dataGridView1.Columns[3].HeaderText = "Ngày mượn";
            dataGridView1.Columns[4].HeaderText = "Ngày trả";
        }
        public void setDGV2ColumnsHeader()
        {
            dataGridView2.Columns[0].HeaderText = "Mã sách";
            dataGridView2.Columns[1].HeaderText = "Số lượng";
        }
        public void GUI()
        {
            dataGridView1.DataSource = BLL_PHIEUMUON.Instance.getAllPhieuMuon();
            setDGV1ColumnsHeader();
        }
        public void enableGroup2()
        {
            lbMaPhieuMuon.Text = this.phieumuon.MAPHIEU;
            lbMaNguoiDung.Text = this.phieumuon.MANGUOIDUNG;
            txtMaDocGia.Enabled = true;
            txtMaDocGia.Text = this.phieumuon.MADOCGIA;
            dateTimePicker1.Enabled = true;
            dateTimePicker1.Value = this.phieumuon.NGAYTRA;
            btn_XacNhan1.Enabled = true;
        }
        public void unableGroup2()
        {
            lbMaNguoiDung.Text = "";
            lbMaPhieuMuon.Text = "";
            txtMaDocGia.Enabled = false;
            dateTimePicker1.Enabled = false;
            btn_XacNhan1.Enabled = false;
        }
        public void unableGroup3()
        {
            txtMaSach.Enabled = false;
            txtMaSach.Text = null;
            txtSoLuong.Enabled = false;
            txtSoLuong.Text = null;
            btn_XacNhan2.Enabled = false;
            btn_Sua.Enabled = false;
            btn_Xoa.Enabled = false;
            dataGridView2.DataSource = null;
        }
        public void enableGroup3()
        {
            List<PM_VIEW_SOLUONG> list_soluong = BLL_PHIEUMUON.Instance.getAllPMSoLuongTheoMaPhieu(this.maphieumuon);
            dataGridView2.DataSource = typeof(List<PM_VIEW_SOLUONG>);
            dataGridView2.DataSource = list_soluong;
            setDGV2ColumnsHeader();

            txtMaSach.Enabled = true;
            txtSoLuong.Enabled = true;
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
            btn_XacNhan2.Enabled = true;
        }
        private void btn_Chon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                this.maphieumuon = dataGridView1.SelectedRows[0].Cells["MAPHIEUMUON"].Value.ToString();
                this.phieumuon = BLL_PHIEUMUON.Instance.timPhieuMuonTheoMaPhieu(this.maphieumuon);
                enableGroup2();
                unableGroup3();
            }
        }

        private void btn_XacNhan1_Click(object sender, EventArgs e)
        {
            bool res1 = BLL_PHIEUMUON.Instance.kiemTraMaDocGia(txtMaDocGia.Text);
            bool res2 = BLL_PHIEUMUON.Instance.kiemTraNgayMuonNgayTra(this.phieumuon.NGAYMUON, dateTimePicker1.Value);
            if (!res1)
            {
                //MessageBox.Show("Độc giả không đáp ứng yêu cầu mượn sách!\nTạo mới hoặc trả sách trước khi mượn thêm");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Độc giả không đáp ứng yêu cầu mượn sách!\nTạo mới hoặc trả sách trước khi mượn thêm!");
            }
            else if (!res2)
            {
                //MessageBox.Show("Ngày trả không hợp lệ");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Ngày trả không hợp lệ!");
            }
            else
            {
                unableGroup2();
                enableGroup3();
            }
        }
        private void btn_XacNhan2_Click(object sender, EventArgs e)
        {
            if (dataGridView2 == null || dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm sách trước!");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng thêm sách trước!");
            }
            else
            {
                string madocgia = txtMaDocGia.Text;
                DateTime ngaytra = dateTimePicker1.Value;
                List<PM_VIEW_SOLUONG> list = (List<PM_VIEW_SOLUONG>)dataGridView2.DataSource;
                BLL_PHIEUMUON.Instance.suaPhieuMuon(this.maphieumuon, madocgia, phieumuon.NGAYMUON, ngaytra, phieumuon.MANGUOIDUNG, list);
                unableGroup3();
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string masach = txtMaSach.Text;
            int soluong = Int32.Parse(txtSoLuong.Text);
            List<PM_VIEW_SOLUONG> list_new = BLL_PHIEUMUON.Instance.themVaoDGVSoLuong(txtMaDocGia.Text, masach, soluong, (List<PM_VIEW_SOLUONG>)dataGridView2.DataSource);
            dataGridView2.DataSource = typeof(List<PM_VIEW_SOLUONG>);
            dataGridView2.DataSource = list_new;
            setDGV2ColumnsHeader();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                string id = dataGridView2.SelectedRows[0].Cells["MASACH"].Value.ToString();
                List<PM_VIEW_SOLUONG> list_new = BLL_PHIEUMUON.Instance.xoaDongDGVSoLuong((List<PM_VIEW_SOLUONG>)dataGridView2.DataSource, id);
                dataGridView2.DataSource = typeof(List<PM_VIEW_SOLUONG>);
                dataGridView2.DataSource = list_new;
                setDGV2ColumnsHeader();
            }
        }
    }
}
