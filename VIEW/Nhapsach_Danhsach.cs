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
    public partial class Nhapsach_Danhsach : Form
    {
        private string manguoidung { get; set; }
        public Nhapsach_Danhsach(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã nhập sách";
            dataGridView1.Columns[1].HeaderText = "Mã người dùng";
            dataGridView1.Columns[2].HeaderText = "Ngày nhập";
            dataGridView1.Columns[3].HeaderText = "Mã sách";
            dataGridView1.Columns[4].HeaderText = "Số lượng";
        }
        public void GUI()
        {
            dataGridView1.DataSource = BLL_NHAPSACH.Instance.getAllChiTietNhapSach();
            setDGVColumnsHeader();
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 1,
                TEXT = "Mã nhập"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 2,
                TEXT = "Mã người nhập"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 3,
                TEXT = "Ngày nhập"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 4,
                TEXT = "Mã sách"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 5,
                TEXT = "Số lượng"
            });
        }



        private void btnTim_Click(object sender, EventArgs e)
        {
            string manhapsach = txtManhapsach.Text;
            string manguoidung = txtManguoidung.Text;
            string masach = txtMasach.Text;
            List<NHAPSACH_CHITIET> list = BLL_NHAPSACH.Instance.timChiTietNhapSach(manhapsach, manguoidung, masach);
            dataGridView1.DataSource = list;
            setDGVColumnsHeader();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string manhapsach = dataGridView1.SelectedRows[0].Cells["MANHAPSACH"].Value.ToString();
                BLL_NHAPSACH.Instance.xoaDongChiTietNhapSach(manhapsach);
                BLL_NHAPSACH.Instance.xoaNhapSach(manhapsach);
                GUI();
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn dòng để xóa!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string manhapsach = dataGridView1.SelectedRows[0].Cells["MANHAPSACH"].Value.ToString();
                Nhapsach_Sua nhapsach_Sua = new Nhapsach_Sua(this.manguoidung, manhapsach);
                nhapsach_Sua.Show();
            }
            else
            {
                Nhapsach_Sua nhapsach_sua = new Nhapsach_Sua(this.manguoidung);
                nhapsach_sua.Show();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Nhapsach_Them nhapsach_Them = new Nhapsach_Them(this.manguoidung);
            nhapsach_Them.Show();
        }

        private void btnToanbo_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_NHAPSACH.Instance.getAllChiTietNhapSach();
            setDGVColumnsHeader();
        }


        private void btnSapXep_Click(object sender, EventArgs e)
        {
            List<NHAPSACH_CHITIET> list = (List<NHAPSACH_CHITIET>)dataGridView1.DataSource;
            int value = ((CBB_ITEM)cbbThuocTinh.SelectedItem).VALUE;
            dataGridView1.DataSource = typeof(List<NHAPSACH_CHITIET>);
            dataGridView1.DataSource = BLL_NHAPSACH.Instance.sapXepNhapSach(list, value);
            setDGVColumnsHeader();
        }
    }
}
