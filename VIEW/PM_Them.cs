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
    public partial class PM_Them : Form
    {
        private string manguoidung;

        public delegate void Mydel();
        public Mydel d = null;
        public PM_Them(string MANGUOIDUNG)
        {
            this.manguoidung = MANGUOIDUNG;
            InitializeComponent();
            GUI();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã sách";
            dataGridView1.Columns[1].HeaderText = "Số lượng";
        }
        public void GUI()
        {
            // Tao tu dong ma phieu muon, ma nguoi dung, ngay muon, ngay tra = ngay muon + 180
            string maphieu_moi = BLL_ID.Instance.convertInt2Ma(
            BLL_ID.Instance.kiemTraViTriCoTheThem(
            BLL_PHIEUMUON.Instance.getAllMaPhieuMuon()));
            lbMaPhieuMuon.Text = maphieu_moi;
            lbMaNguoiDung.Text = this.manguoidung;
            lbNgayMuon.Text = DateTime.Now.ToShortDateString();
            dateTimePicker1.Value = DateTime.Now.AddDays(180);

            txtSoLuong.Enabled = false;
            txtMaSach.Enabled = false;
            btn_Them.Enabled = false;
            btn_XacNhan.Enabled = false;
            btn_Xoa.Enabled = false;
        }

        private void btn_KiemTra_Click(object sender, EventArgs e)
        {
            bool res1 = BLL_PHIEUMUON.Instance.kiemTraMaDocGia(txtMaDocGia.Text);
            bool res2 = BLL_PHIEUMUON.Instance.kiemTraNgayMuonNgayTra(Convert.ToDateTime(lbNgayMuon.Text), dateTimePicker1.Value);
            if (!res1)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Độc giả không đáp ứng yêu cầu mượn sách!\nTạo mới hoặc trả sách trước khi mượn thêm");
            }
            else if (!res2)
            {
                
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Ngày trả không hợp lệ!");
                
            }
            else
            {
                txtMaDocGia.Enabled = false;
                dateTimePicker1.Enabled = false;

                txtSoLuong.Enabled = true;
                txtMaSach.Enabled = true;
                btn_Them.Enabled = true;
                btn_XacNhan.Enabled = true;
                btn_Xoa.Enabled = true;
            }
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            if (dataGridView1 == null || dataGridView1.Rows.Count == 0)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng thêm sách trước!");
            }
            else
            {
                string maphieumuon = lbMaPhieuMuon.Text;
                string madocgia = txtMaDocGia.Text;
                DateTime ngaymuon = Convert.ToDateTime(lbNgayMuon.Text);
                DateTime ngaytra = dateTimePicker1.Value;
                List<PM_VIEW_SOLUONG> list = (List<PM_VIEW_SOLUONG>)dataGridView1.DataSource;
                string res = BLL_PHIEUMUON.Instance.themPhieuMuon(maphieumuon, madocgia, ngaymuon, ngaytra, this.manguoidung, list);
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice(res);
                if (d != null)
                    d();
                reset();
                lbMaPhieuMuon.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_PHIEUMUON.Instance.getAllMaPhieuMuon()));
            }
        }
        public void reset()
        {
            txtMaDocGia.Enabled = true;
            dateTimePicker1.Enabled = true;
            dataGridView1.DataSource = null;
            txtMaSach.Text = "";
            txtSoLuong.Text = "";
            txtSoLuong.Enabled = false;
            txtMaSach.Enabled = false;
            btn_Them.Enabled = false;
            btn_XacNhan.Enabled = false;
            btn_Xoa.Enabled = false;
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            string masach = txtMaSach.Text;
            int soluong = Int32.Parse(txtSoLuong.Text);
            List<PM_VIEW_SOLUONG> list_new = BLL_PHIEUMUON.Instance.themVaoDGVSoLuong(txtMaDocGia.Text, masach, soluong, (List<PM_VIEW_SOLUONG>)dataGridView1.DataSource);
            dataGridView1.DataSource = typeof(List<PM_VIEW_SOLUONG>);
            dataGridView1.DataSource = list_new;
            setDGVColumnsHeader();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string id = dataGridView1.SelectedRows[0].Cells["MASACH"].Value.ToString();
                List<PM_VIEW_SOLUONG> list_new = BLL_PHIEUMUON.Instance.xoaDongDGVSoLuong((List<PM_VIEW_SOLUONG>)dataGridView1.DataSource, id);
                //List<PM_VIEW_SOLUONG> list_new = BLL_PHIEUMUON.Instance.xoaDongDGVSoLuong((List<PM_VIEW_SOLUONG>)dataGridView1.DataSource, id, txtMaDocGia.Text);
                dataGridView1.DataSource = typeof(List<PM_VIEW_SOLUONG>);
                dataGridView1.DataSource = list_new;
                setDGVColumnsHeader();

            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn dòng để xóa!");
            }
        }
        private void btn_Nhaplai_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
