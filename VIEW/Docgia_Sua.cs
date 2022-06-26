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
    public partial class Docgia_Sua : Form
    {
        private string madocgia;
        public delegate void Mydel();
        public Mydel d;
        public Docgia_Sua(string MADOCGIA)
        {
            this.madocgia = MADOCGIA;
            InitializeComponent();
            GUI();
            loadForm();
        }
        public void loadForm()
        {

            DOCGIA docgia = BLL_DOCGIA.Instance.timDocGiaTheoMaDocGia(madocgia);
            if (BLL_DOCGIA.Instance.kiemtraSVhayGV(docgia))
            {
                dataGridViewSinhvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaSinhVien();
                dataGridViewGiangvien.Visible = false;
                setDGVSVColumnsHeader();
            }
            else
            {
                dataGridViewGiangvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaGiangVien();
                dataGridViewSinhvien.Visible = false;
                setDGVGVColumnsHeader();
            }
        }
        public void GUI()
        {
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
            DOCGIA docgia = BLL_DOCGIA.Instance.timDocGiaTheoMaDocGia(madocgia);

            lbMaDocGia.Text = docgia.MADOCGIA;

            txtDiachi.Text = docgia.DIACHI;
            txtHvt.Text = docgia.HOTEN;
            SINHVIEN sv = BLL_DOCGIA.Instance.timSinhVienTheoMaDocGia(madocgia);
            if (sv != null)
            {
                rbSV.Enabled = false;
                rbGV.Enabled = false;
                rbSV.Checked = true;
                rbGV.Checked = false;

                txtMSSV.Text = sv.MASINHVIEN;
                cbbLSH.SelectedIndex = Int32.Parse(sv.MALOPSH) - 1;

                txtHocVi.Enabled = false;
                cbbKhoa.Enabled = false;
            }
            else
            {
                GIANGVIEN gv = BLL_DOCGIA.Instance.timGiangVienTheoMaDocGia(madocgia);
                rbSV.Checked = false;
                rbGV.Checked = true;
                rbSV.Enabled = false;
                rbGV.Enabled = false;

                txtHocVi.Text = gv.HOCVI;
                cbbKhoa.SelectedIndex = Int32.Parse(gv.MAKHOA) - 1;

                txtMSSV.Enabled = false;
                cbbLSH.Enabled = false;
            }
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
            dataGridViewSinhvien.Columns[0].HeaderText = "Mã độc giả";
            dataGridViewSinhvien.Columns[1].HeaderText = "Họ tên";
            dataGridViewSinhvien.Columns[2].HeaderText = "Địa chỉ";
            dataGridViewSinhvien.Columns[3].HeaderText = "Số sách mượn";
            dataGridViewSinhvien.Columns[4].HeaderText = "Học vị";
            dataGridViewSinhvien.Columns[5].HeaderText = "Khoa";
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
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
            string res = BLL_DOCGIA.Instance.suaDocGia(madocgia, hoten, diachi, sinhvien, mssv, malopsh, hocvi, makhoa);
            loadForm();
            if (d != null)
            {
                d();
            }
            //CN_Thongbao f = new CN_Thongbao();
            //f.setNotice(res);
        }
    }
}
