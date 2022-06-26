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
    public partial class Docgia_Tim : Form
    {
        private string manguoidung = null;
        public Docgia_Tim(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            dataGridView1.DataSource = BLL_DOCGIA.Instance.getDocGia();
            setDGVColumnsHeader();
            GUI();
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
            dataGridViewSinhvien.Columns[0].HeaderText = "Mã độc giả";
            dataGridViewSinhvien.Columns[1].HeaderText = "Họ tên";
            dataGridViewSinhvien.Columns[2].HeaderText = "Địa chỉ";
            dataGridViewSinhvien.Columns[3].HeaderText = "Số sách mượn";
            dataGridViewSinhvien.Columns[4].HeaderText = "Mã sinh viên";
            dataGridViewSinhvien.Columns[5].HeaderText = "Lớp sinh hoạt";
        }
        public void setDGVGVColumnsHeader()
        {
            dataGridViewGiangvien.Columns[0].HeaderText = "Mã độc giả";
            dataGridViewGiangvien.Columns[1].HeaderText = "Họ tên";
            dataGridViewGiangvien.Columns[2].HeaderText = "Địa chỉ";
            dataGridViewGiangvien.Columns[3].HeaderText = "Số sách mượn";
            dataGridViewGiangvien.Columns[4].HeaderText = "Học vị";
            dataGridViewGiangvien.Columns[5].HeaderText = "Khoa";
        }
        public void GUI()
        {
            List<string> list_lopsh = BLL_DOCGIA.Instance.getAllLopSH();
            cbbLSH.Items.Add(new CBB_ITEM()
            {
                TEXT = "Tất cả",
                VALUE = 0
            });
            for (int i = 1; i <= list_lopsh.Count; i++)
            {
                cbbLSH.Items.Add(new CBB_ITEM()
                {
                    TEXT = list_lopsh[i - 1],
                    VALUE = i
                });
            }
            cbbLSH.SelectedIndex = 0;

            List<string> list_khoa = BLL_DOCGIA.Instance.getAllKhoa();
            cbbKhoa.Items.Add(new CBB_ITEM()
            {
                TEXT = "Tất cả",
                VALUE = 0
            });
            for (int i = 1; i <= list_khoa.Count; i++)
            {
                cbbKhoa.Items.Add(new CBB_ITEM()
                {
                    TEXT = list_khoa[i - 1],
                    VALUE = i
                });
            }
            cbbKhoa.SelectedIndex = 0;

            txtHocVi.Enabled = false;
            txtMSSV.Enabled = false;
            cbbKhoa.Enabled = false;
            cbbLSH.Enabled = false;

            rbSV.Checked = true;
        }
        public void showDGV(List<DOCGIA_VIEW_SINHVIEN> list_sv, List<DOCGIA_VIEW_GIANGVIEN> list_gv)
        {
            if (list_sv != null)
            {
                dataGridViewSinhvien.DataSource = list_sv;
                dataGridView1.Visible = false;
                dataGridViewGiangvien.Visible = false;
                dataGridViewSinhvien.Visible = true;
                setDGVSVColumnsHeader();
            }
            if (list_gv != null)
            {
                dataGridView1.Visible = false;
                dataGridViewSinhvien.Visible = false;
                dataGridViewGiangvien.Visible = true;
                dataGridViewGiangvien.DataSource = list_gv;
                setDGVGVColumnsHeader();
            }
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string hoten = txtHvt.Text;
            string diachi = txtDiachi.Text;
            string madocgia = txtMadocgia.Text;
            bool sinhvien = rbSV.Checked;
            string mssv = txtMSSV.Text;
            string hocvi = txtHocVi.Text;

            string malopsh = ((CBB_ITEM)cbbLSH.SelectedItem).VALUE.ToString();
            for (int i = 1; i <= 3 - malopsh.Length + 1; i++)
            {
                malopsh = "0" + malopsh;
            }
            string makhoa = ((CBB_ITEM)cbbKhoa.SelectedItem).VALUE.ToString();
            for (int i = 1; i <= 3 - makhoa.Length + 1; i++)
            {
                makhoa = "0" + makhoa;
            }
            List<DOCGIA_VIEW_SINHVIEN> list_sinhvien = BLL_DOCGIA.Instance.timDocGiaSinhVien(
                hoten, diachi, madocgia, sinhvien, mssv, malopsh, hocvi, makhoa);
            List<DOCGIA_VIEW_GIANGVIEN> list_giangvien = BLL_DOCGIA.Instance.timDocGiaGiangVien(
                hoten, diachi, madocgia, sinhvien, mssv, malopsh, hocvi, makhoa);
            showDGV(list_sinhvien, list_giangvien);
        }

        private void rbSV_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSV.Checked)
            {
                txtMSSV.Enabled = true;
                cbbLSH.Enabled = true;
            }
            else
            {
                txtMSSV.Enabled = false;
                cbbLSH.Enabled = false;
            }
        }

        private void rbGV_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGV.Checked)
            {
                txtHocVi.Enabled = true;
                cbbKhoa.Enabled = true;
            }
            else
            {
                txtHocVi.Enabled = false;
                cbbKhoa.Enabled = false;
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (rbSV.Checked == true && dataGridViewSinhvien.SelectedRows.Count == 1)
            {
                string madocgia = dataGridViewSinhvien.SelectedRows[0].Cells["MADOCGIA"].Value.ToString();
                Docgia docgia = new Docgia(madocgia);
                docgia.Show();
            }
            if (rbSV.Checked == false && dataGridViewGiangvien.SelectedRows.Count == 1)
            {
                string madocgia = dataGridViewGiangvien.SelectedRows[0].Cells["MADOCGIA"].Value.ToString();
                Docgia docgia = new Docgia(madocgia);
                docgia.Show();
            }
            if (rbSV.Checked == true && dataGridView1.SelectedRows.Count == 1)
            {
                string madocgia = dataGridView1.SelectedRows[0].Cells["MADOCGIA"].Value.ToString();
                Docgia docgia = new Docgia(madocgia);
                docgia.Show();
            }
        }
    }
}
