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
    public partial class Docgia_Danhsach : Form
    {
        private string manguoidung { get; set; }
        public Docgia_Danhsach(string manguoidung)
        {
            InitializeComponent();
            this.manguoidung = manguoidung;
            setCBBThuocTinh();
            GUI();
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

        public void ShowDGV(List<DOCGIA_VIEW_SINHVIEN> list_sinhvien, List<DOCGIA_VIEW_GIANGVIEN> list_giangvien)
        {
            if (list_sinhvien != null)
            {
                dataGridViewSinhvien.DataSource = list_sinhvien;
                setDGVSVColumnsHeader();
            }
            if (list_giangvien != null)
            {
                dataGridViewGiangvien.DataSource = list_giangvien;
                setDGVGVColumnsHeader();
            }

        }
        public void ShowDGV()
        {
            dataGridViewSinhvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaSinhVien();
            setDGVSVColumnsHeader();
            dataGridViewGiangvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaGiangVien();
            setDGVGVColumnsHeader();
        }
        public void setCBBThuocTinh()
        {
            cbbThuocTinhSV.Items.Add(new CBB_ITEM()
            {
                VALUE = 1,
                TEXT = "Mã độc giả"
            });
            cbbThuocTinhSV.Items.Add(new CBB_ITEM()
            {
                VALUE = 2,
                TEXT = "Họ và tên"
            });
            cbbThuocTinhSV.Items.Add(new CBB_ITEM()
            {
                VALUE = 3,
                TEXT = "Số sách mượn"
            });
            cbbThuocTinhSV.Items.Add(new CBB_ITEM()
            {
                VALUE = 4,
                TEXT = "Mã sinh viên"
            });
            cbbThuocTinhSV.SelectedIndex = 0;

            cbbThuocTinhGV.Items.Add(new CBB_ITEM()
            {
                VALUE = 1,
                TEXT = "Mã độc giả"
            });
            cbbThuocTinhGV.Items.Add(new CBB_ITEM()
            {
                VALUE = 2,
                TEXT = "Họ và tên"
            });
            cbbThuocTinhGV.Items.Add(new CBB_ITEM()
            {
                VALUE = 3,
                TEXT = "Số sách mượn"
            });
            cbbThuocTinhGV.SelectedIndex = 0;
        }
        public void GUI()
        {
            dataGridViewSinhvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaSinhVien();
            setDGVSVColumnsHeader();
            dataGridViewGiangvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaGiangVien();
            setDGVGVColumnsHeader();
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

        private void btn_ToanBo_Click(object sender, EventArgs e)
        {
            dataGridViewSinhvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaSinhVien();
            setDGVSVColumnsHeader();
            dataGridViewGiangvien.DataSource = BLL_DOCGIA.Instance.getAllDocGiaGiangVien();
            setDGVGVColumnsHeader();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            Docgia_Them docgia_Them = new Docgia_Them(this.manguoidung);
            docgia_Them.d = new Docgia_Them.Mydel(ShowDGV);
            docgia_Them.Show();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewGiangvien.SelectedRows.Count > 0)
            {
                List<string> list_id_giaovien = new List<string>();
                foreach (DataGridViewRow dr in dataGridViewGiangvien.SelectedRows)
                {
                    list_id_giaovien.Add(dr.Cells["MADOCGIA"].Value.ToString());
                }
                BLL_DOCGIA.Instance.xoaDanhSachDocGiaGiangVien(list_id_giaovien);
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn dòng để xóa!");
            }

            if (dataGridViewSinhvien.SelectedRows.Count > 0)
            {
                List<string> list_id_sinhvien = new List<string>();
                foreach (DataGridViewRow dr in dataGridViewSinhvien.SelectedRows)
                {
                    list_id_sinhvien.Add(dr.Cells["MADOCGIA"].Value.ToString());
                }
                BLL_DOCGIA.Instance.xoaDanhSachDocGiaSinhVien(list_id_sinhvien);
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn dòng để xóa!");
            }
            GUI();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (dataGridViewGiangvien.SelectedRows.Count == 1)
            {
                string id_giangvien = dataGridViewGiangvien.SelectedRows[0].Cells["MADOCGIA"].Value.ToString();
                Docgia_Sua docgia_Sua = new Docgia_Sua(id_giangvien);
                docgia_Sua.d = new Docgia_Sua.Mydel(ShowDGV);
                docgia_Sua.Show();
            }else if (dataGridViewSinhvien.SelectedRows.Count == 1)
            {
                string id_sinhvien = dataGridViewSinhvien.SelectedRows[0].Cells["MADOCGIA"].Value.ToString();
                Docgia_Sua docgia_Sua = new Docgia_Sua(id_sinhvien);
                docgia_Sua.d = new Docgia_Sua.Mydel(ShowDGV);
                docgia_Sua.Show();
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn 1 dòng để sửa!");
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
            ShowDGV(list_sinhvien,list_giangvien);
        }

        private void btnSapXepSV_Click(object sender, EventArgs e)
        {
            List<DOCGIA_VIEW_SINHVIEN> list_sv = (List<DOCGIA_VIEW_SINHVIEN>)dataGridViewSinhvien.DataSource;
            int value = ((CBB_ITEM)cbbThuocTinhSV.SelectedItem).VALUE;
            dataGridViewSinhvien.DataSource = typeof(List<DOCGIA_VIEW_SINHVIEN>);
            dataGridViewSinhvien.DataSource = BLL_DOCGIA.Instance.sapXepSinhVien(list_sv, value);
            setDGVSVColumnsHeader();
        }

        private void btnSapXepGV_Click(object sender, EventArgs e)
        {
            List<DOCGIA_VIEW_GIANGVIEN> list_gv = (List<DOCGIA_VIEW_GIANGVIEN>)dataGridViewGiangvien.DataSource;
            int value = ((CBB_ITEM)cbbThuocTinhGV.SelectedItem).VALUE;
            dataGridViewGiangvien.DataSource = typeof(List<DOCGIA_VIEW_GIANGVIEN>);
            dataGridViewGiangvien.DataSource = BLL_DOCGIA.Instance.sapXepGiangVien(list_gv, value);
            setDGVGVColumnsHeader();
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
    }
}
