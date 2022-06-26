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
namespace QuanLyThuVien.VIEW
{
    public partial class PM_Danhsach : Form
    {
        private string manguoidung;
        public static void showMessage(string message)
        {
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice(message);
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã phiếu mượn";
            dataGridView1.Columns[1].HeaderText = "Mã người dùng";
            dataGridView1.Columns[2].HeaderText = "Mã độc giả";
            dataGridView1.Columns[3].HeaderText = "Ngày mượn";
            dataGridView1.Columns[4].HeaderText = "Ngày trả";
            dataGridView1.Columns[5].HeaderText = "Mã sách";
            dataGridView1.Columns[6].HeaderText = "Số lượng";
        }
        public PM_Danhsach(string MANGUOIDUNG)
        {
            this.manguoidung = MANGUOIDUNG;
            InitializeComponent();
            GUI();
            showDGV();
        }
        public void showDGV()
        {
            dataGridView1.DataSource = BLL_PHIEUMUON.Instance.getAllChiTietPhieuMuon();
            setDGVColumnsHeader();
        }
        public void GUI()
        {
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 1,
                TEXT = "Mã phiếu mượn"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 2,
                TEXT = "Mã người dùng"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 3,
                TEXT = "Mã độc giả"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 4,
                TEXT = "Ngày mượn"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 5,
                TEXT = "Ngày trả"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 6,
                TEXT = "Mã sách"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 7,
                TEXT = "Số lượng"
            });
            cbbThuocTinh.SelectedIndex = 0;
        }
        private void btn_ToanBo_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_PHIEUMUON.Instance.getAllChiTietPhieuMuon();
            setDGVColumnsHeader();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            PM_Them pM_Them = new PM_Them(manguoidung);
            pM_Them.d = new PM_Them.Mydel(showDGV);
            pM_Them.Show();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 1)
            {
                string maphieu = dataGridView1.SelectedRows[0].Cells["MAPHIEUMUON"].Value.ToString();
                string madocgia = dataGridView1.SelectedRows[0].Cells["MADOCGIA"].Value.ToString();
                BLL_PHIEUMUON.Instance.xoaAllChiTietPhieuMuonTheoMaPhieuMuon(maphieu, madocgia);
                BLL_PHIEUMUON.Instance.xoaPhieuMuon(maphieu);
                GUI();
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn dòng để xóa!");
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string maphieumuon = dataGridView1.SelectedRows[0].Cells["MAPHIEUMUON"].Value.ToString();
                PM_Sua pM_Sua = new PM_Sua(manguoidung, maphieumuon);
                pM_Sua.Show();
            }
            else if (dataGridView1.SelectedRows.Count == 0)
            {
                PM_Sua pM_Sua = new PM_Sua(manguoidung);
                pM_Sua.Show();
            }
            else
            {
                //MessageBox.Show("Vui long chon 1 dong!");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn 1 dòng!");
            }
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string maphieu = txtMaPhieuMuon.Text;
            string madocgia = txtMaDocGia.Text;
            string manguoidung = txtMaNguoiDung.Text;
            string masach = txtMaSach.Text;
            List<PM_CHITIET> list = BLL_PHIEUMUON.Instance.timChiTietPhieumuon(maphieu, madocgia, manguoidung, masach);
            dataGridView1.DataSource = list;
            setDGVColumnsHeader();
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            List<PM_CHITIET> list = (List<PM_CHITIET>)dataGridView1.DataSource;
            int value = ((CBB_ITEM)cbbThuocTinh.SelectedItem).VALUE;
            dataGridView1.DataSource = typeof(List<PM_CHITIET>);
            dataGridView1.DataSource = BLL_PHIEUMUON.Instance.sapXepPhieuMuon(list, value);
        }

    }
}
