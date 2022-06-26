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
using QuanLyThuVien.VIEW;
using QuanLyThuVien.Entity;
namespace QuanLyThuVien.VIEW
{
    public partial class Bangiao_Sua : Form
    {
        private string manguoidung;
        private string mabangiao = null;
        private BANGIAO bangiao = null;
        public Bangiao_Sua(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
            unableGroup2();
        }
        public Bangiao_Sua(string manguoidung, string mabangiao)
        {
            this.manguoidung = manguoidung;
            this.mabangiao = mabangiao;
            InitializeComponent();
            GUI();
            enableGroup2();
        }
        public void setDGV1ColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã bàn giao";
            dataGridView1.Columns[1].HeaderText = "Mã người bàn giao";
            dataGridView1.Columns[2].HeaderText = "Ngày bàn giao";
            dataGridView1.Columns[3].HeaderText = "Mã người xác nhận";
            dataGridView1.Columns[4].HeaderText = "Ngày xác nhận";
        }
        public void setDGV2ColumnsHeader()
        {
            dataGridView2.Columns[0].HeaderText = "Mã sách";
            dataGridView2.Columns[1].HeaderText = "Số lượng";
        }
        public void GUI()
        {
            dataGridView1.DataSource = BLL_BANGIAO.Instance.getAllBanGiao();
            setDGV1ColumnsHeader();
        }
        public void enableGroup2()
        {
            this.bangiao = BLL_BANGIAO.Instance.getBanGiaoTheoMaBanGiao(this.mabangiao);
            lbMaBanGiao.Text = this.bangiao.MABANGIAO;
            lbMaNguoiBanGiao.Text = this.bangiao.MANGUOIBANGIAO;
            lbNgayBanGiao.Text = this.bangiao.NGAYBANGIAO.ToString();
            dataGridView2.DataSource = BLL_BANGIAO.Instance.getSoLuongTheoMaBanGiao(this.mabangiao);
            setDGV2ColumnsHeader();

            txtMaSach.Enabled = true;
            txtSoLuong.Enabled = true;
            btnThem.Enabled = true;
            btnXacNhan.Enabled = true;
            btnXoa.Enabled = true;
        }
        public void unableGroup2()
        {
            lbMaBanGiao.Text = null;
            lbMaNguoiBanGiao.Text = null;
            lbNgayBanGiao.Text = null;

            txtMaSach.Enabled = false;
            txtMaSach.Text = null;
            txtSoLuong.Text = null;
            txtSoLuong.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnXacNhan.Enabled = false;
            dataGridView2.DataSource = typeof(List<BANGIAO_VIEW_SOLUONG>);
            dataGridView2.DataSource = new List<BANGIAO_VIEW_SOLUONG>();
            setDGV2ColumnsHeader();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                this.mabangiao = dataGridView1.SelectedRows[0].Cells["MABANGIAO"].Value.ToString();
                enableGroup2();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trong bảng dữ liệu");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                string masach = dataGridView2.SelectedRows[0].Cells["MASACH"].Value.ToString();
                List<BANGIAO_VIEW_SOLUONG> list = (List<BANGIAO_VIEW_SOLUONG>)dataGridView2.DataSource;
                dataGridView2.DataSource = typeof(List<BANGIAO_VIEW_SOLUONG>);
                dataGridView2.DataSource = BLL_BANGIAO.Instance.xoaDongDGV(list, masach);
                setDGV2ColumnsHeader();
            }
            else
            {
                MessageBox.Show("Vui lòng chỉ chọn 1 dòng");
            }

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string mabangiao = lbMaBanGiao.Text;
            List<BANGIAO_VIEW_SOLUONG> list = (List<BANGIAO_VIEW_SOLUONG>)dataGridView2.DataSource;
            BLL_BANGIAO.Instance.chinhSuaBanGiao(mabangiao, list);
            GUI();
            MessageBox.Show("Chỉnh sửa bàn giao thành công!");
        }
    }
}
