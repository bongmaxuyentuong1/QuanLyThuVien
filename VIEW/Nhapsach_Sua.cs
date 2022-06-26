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
    public partial class Nhapsach_Sua : Form
    {
        private string manguoidung { get; set; }
        private string manhapsach { get; set; }
        private NHAPSACH nhapsach { get; set; }
        public Nhapsach_Sua(string manguoidung)
        {
            this.manguoidung = manguoidung;
            this.manhapsach = null;
            InitializeComponent();
            GUI();
            unableGroup2();
        }
        public void setDGV1ColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã nhập sách";
            dataGridView1.Columns[1].HeaderText = "Ngày nhập";
            dataGridView1.Columns[2].HeaderText = "Mã người dùng";
        }
        public void setDGV2ColumnsHeader()
        {
            dataGridView2.Columns[0].HeaderText = "Mã sách";
            dataGridView2.Columns[1].HeaderText = "Số lượng";
        }
        public Nhapsach_Sua(string manguoidung, string manhapsach)
        {
            this.manguoidung = manguoidung;
            this.manhapsach = manhapsach;
            InitializeComponent();
            GUI();
            enableGroup2();
        }

        public void GUI()
        {
            dataGridView1.DataSource = BLL_NHAPSACH.Instance.getAllNhapSach();
            setDGV1ColumnsHeader();
        }

        public void enableGroup2()
        {
            panel6.Enabled = true;
            label1.Enabled = true;
            this.nhapsach = BLL_NHAPSACH.Instance.timNhapSachTheoMaNhapSach(this.manhapsach);
            List<NHAPSACH_VIEW_SOLUONG> list_soluong = BLL_NHAPSACH.Instance.timChiTietNhapSachSoLuongTheoMaNhapSach(this.manhapsach);
            dataGridView2.DataSource = typeof(List<NHAPSACH_VIEW_SOLUONG>);
            dataGridView2.DataSource = list_soluong;
            setDGV2ColumnsHeader();
            lbNgaynhapsach.Text = nhapsach.NGAYNHAP.ToString();
            lbManguoidung.Text = nhapsach.MANGUOIDUNG;
            lbManhapsach.Text = nhapsach.MANHAPSACH;
        }

        public void unableGroup2()
        {
            dataGridView2.DataSource = typeof(List<NHAPSACH_VIEW_SOLUONG>);
            dataGridView2.DataSource = null;
            lbManguoidung.Text = null;
            lbManhapsach.Text = null;
            lbNgaynhapsach.Text = null;
            txtMasach.Text = null;
            txtSoluong.Text = null; 
            panel6.Enabled = false;
            label1.Enabled = false;

        }
        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                this.manhapsach = dataGridView1.SelectedRows[0].Cells["MANHAPSACH"].Value.ToString();
                enableGroup2();
            }
            else
            {
                //MessageBox.Show("Vui lòng chỉ chọn 1 dòng!");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chỉ chọn 1 dòng!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                string masach = dataGridView2.SelectedRows[0].Cells["MASACH"].Value.ToString();
                List<NHAPSACH_VIEW_SOLUONG> new_list = BLL_NHAPSACH.Instance.xoaDongDGV(masach, (List<NHAPSACH_VIEW_SOLUONG>)dataGridView2.DataSource);
                dataGridView2.DataSource = typeof(List<NHAPSACH_VIEW_SOLUONG>);
                dataGridView2.DataSource = new_list;
                setDGV2ColumnsHeader();
            }
            else
            {
                //MessageBox.Show("Vui lòng chọn 1 dòng trong bảng dữ liệu bên cạnh");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn 1 dòng trong bảng dữ liệu bên cạnh!");
            }
        }

        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            if (dataGridView2 == null || dataGridView2.Rows.Count == 0)
            {
                //MessageBox.Show("Vui lòng thêm sách trước!");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng thêm sách trước!");
            }
            else
            {
                List<NHAPSACH_VIEW_SOLUONG> list = (List<NHAPSACH_VIEW_SOLUONG>)dataGridView2.DataSource;
                BLL_NHAPSACH.Instance.suaChiTietNhapSach(this.manhapsach, list);
                unableGroup2();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string masach = txtMasach.Text;
            int soluong = Int32.Parse(txtSoluong.Text);
            List<NHAPSACH_VIEW_SOLUONG> new_list = BLL_NHAPSACH.Instance.themSoLuongVaoDGV(masach, soluong, (List<NHAPSACH_VIEW_SOLUONG>)dataGridView2.DataSource);
            dataGridView2.DataSource = typeof(List<NHAPSACH_VIEW_SOLUONG>);
            dataGridView2.DataSource = new_list;
            setDGV2ColumnsHeader();
        }
    }
}
