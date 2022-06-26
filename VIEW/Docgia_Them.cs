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
    public partial class Docgia_Them : Form
    {
        private string manguoidung = null;
        public delegate void Mydel();
        public Mydel d = null;
        public Docgia_Them()
        {
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_DOCGIA.Instance.getDocGia();
            setDGVColumnsHeader();
        }
        public Docgia_Them(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_DOCGIA.Instance.getDocGia();
            setDGVColumnsHeader();
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
            dataGridView1.Visible = true;
            dataGridViewSinhvien.Visible = false;
            dataGridViewGiangvien.Visible = false;
            List<string> list_lopsh = BLL_DOCGIA.Instance.getAllLopSH();
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

            lbMaDocGia.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_DOCGIA.Instance.getAllMaDocGia()));
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string madocgia = lbMaDocGia.Text;
            string hoten = txtHvt.Text;
            string diachi = txtDiachi.Text;
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
            string message = BLL_DOCGIA.Instance.themDocGia(madocgia, hoten, diachi, sinhvien, mssv, malopsh, hocvi, makhoa);
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice(message);
            if (sinhvien)
            {
                dataGridView1.Visible = false;
                dataGridViewSinhvien.Visible = true;
                dataGridViewGiangvien.Visible = false;
                dataGridViewSinhvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaSinhVien();
                setDGVSVColumnsHeader();
                if (manguoidung != null && d != null)
                    d();
                reLoad();
            }
            else
            {
                dataGridView1.Visible = false;
                dataGridViewSinhvien.Visible = false;
                dataGridViewGiangvien.Visible = true;
                dataGridViewGiangvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaGiangVien();
                setDGVGVColumnsHeader();
                if (manguoidung != null && d != null)
                    d();
                reLoad();
            }
        }

        private void rbSV_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSV.Checked)
            {
                txtMSSV.Enabled = true;
                cbbLSH.Enabled = true;
                txtHocVi.Enabled = false;
                cbbKhoa.Enabled = false;
            }
            else
            {
                txtMSSV.Enabled = true;
                cbbLSH.Enabled = true;
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
                txtMSSV.Enabled = false;
                cbbLSH.Enabled = false;
            }
            else
            {
                txtHocVi.Enabled = false;
                cbbKhoa.Enabled = false;
                txtMSSV.Enabled = true;
                cbbLSH.Enabled = true;
            }
        }
        public void reLoad()
        {
            cbbKhoa.SelectedIndex = 0;
            cbbLSH.SelectedIndex = 0;
            txtDiachi.Text = "";
            txtHocVi.Text = "";
            txtHvt.Text = "";
            txtMSSV.Text = "";
            lbMaDocGia.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_DOCGIA.Instance.getAllMaDocGia()));
        }
    }
}
