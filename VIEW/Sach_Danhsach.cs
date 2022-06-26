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
    public partial class Sach_Danhsach : Form
    {
        private string manguoidung = null;
        public Sach_Danhsach(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
        }
        public void ShowDGV()
        {
            dataGridView.DataSource = BLL_SACH.Instance.getAllSach();
            setDGVColumnsHeader();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView.Columns[0].HeaderText = "Mã sách";
            dataGridView.Columns[1].HeaderText = "Nhan đề";
            dataGridView.Columns[2].HeaderText = "Thể loại";
            dataGridView.Columns[3].HeaderText = "Nhà xuất bản";
            dataGridView.Columns[4].HeaderText = "Năm xuất bản";
            dataGridView.Columns[5].HeaderText = "Ngôn ngữ";
            dataGridView.Columns[6].HeaderText = "Tác giả";
            dataGridView.Columns[7].HeaderText = "Tổng số lượng";
            dataGridView.Columns[8].HeaderText = "Số lượng đang mượn";
            dataGridView.Columns[9].HeaderText = "Số lượng hiện tại";
            dataGridView.Columns[10].HeaderText = "Số lượng tồn kho";
        }
        public void GUI()
        {
            dataGridView.DataSource = BLL_SACH.Instance.getAllSach();
            setDGVColumnsHeader();

            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 1,
                TEXT = "Mã sách"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 2,
                TEXT = "Nhan đề"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 3,
                TEXT = "Tác giả"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 4,
                TEXT = "Năm xuất bản"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 5,
                TEXT = "Tổng số lượng"
            });
            cbbThuocTinh.SelectedIndex = 0;

            cbbTheLoai.Items.Add(new CBB_ITEM()
            {
                VALUE = 0,
                TEXT = "Tất cả"
            });
            List<string> theloais = BLL_SACH.Instance.getAllTheloai();
            for (int i = 1; i <= theloais.Count; i++)
            {
                cbbTheLoai.Items.Add(new CBB_ITEM()
                {
                    TEXT = theloais[i - 1],
                    VALUE = i
                });
            }
            cbbTheLoai.SelectedIndex = 0;

            cbbNgonNgu.Items.Add(new CBB_ITEM()
            {
                VALUE = 0,
                TEXT = "Tất cả"
            });
            List<string> nns = BLL_SACH.Instance.getAllNgonNgu();
            for (int i = 1; i <= nns.Count; i++)
            {
                cbbNgonNgu.Items.Add(new CBB_ITEM()
                {
                    TEXT = nns[i - 1],
                    VALUE = i
                });
            }
            cbbNgonNgu.SelectedIndex = 0;

            cbbNhaXB.Items.Add(new CBB_ITEM()
            {
                VALUE = 0,
                TEXT = "Tất cả"
            });
            List<string> nxbs = BLL_SACH.Instance.getAllNXB();
            for (int i = 1; i <= nxbs.Count; i++)
            {
                cbbNhaXB.Items.Add(new CBB_ITEM()
                {
                    TEXT = nxbs[i - 1],
                    VALUE = i
                });
            }
            cbbNhaXB.SelectedIndex = 0;
        }

        private void btnToanBo_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = BLL_SACH.Instance.getAllSach();
            setDGVColumnsHeader();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Sach_Them sach_Them = new Sach_Them(this.manguoidung);
            sach_Them.d = new Sach_Them.Mydel(ShowDGV);
            sach_Them.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                string masach = dataGridView.SelectedRows[0].Cells["MASACH"].Value.ToString();
                Sach_Sua sach_Sua = new Sach_Sua(manguoidung, masach);
                sach_Sua.d = new Sach_Sua.Mydel(ShowDGV);
                sach_Sua.Show();
            }
            else
            {
                {
                    //MessageBox.Show("Chon dong de xoa");
                    CN_Thongbao f = new CN_Thongbao();
                    f.setNotice("Vui lòng chọn 1 dòng để sửa!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                List<string> list_masach = new List<string>();
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    list_masach.Add(row.Cells["MASACH"].Value.ToString());
                }
                BLL_SACH.Instance.xoaSach(list_masach);
                dataGridView.DataSource = BLL_SACH.Instance.getAllSach();
                setDGVColumnsHeader();
            }else
            {
                {
                    //MessageBox.Show("Chon dong de xoa");
                    CN_Thongbao f = new CN_Thongbao();
                    f.setNotice("Vui lòng chọn dòng để xóa!");
                }
            }
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            int value = ((CBB_ITEM)cbbThuocTinh.SelectedItem).VALUE;
            List<SACH_VIEW> list = (List<SACH_VIEW>)dataGridView.DataSource;
            dataGridView.DataSource = typeof(List<SACH_VIEW>);
            dataGridView.DataSource = BLL_SACH.Instance.sapXepSach(list, value);
            setDGVColumnsHeader();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string masach = txtMaSach.Text;
            string nhande = txtNhanDe.Text;
            string tacgia = txtTacGia.Text;
            int namxuatban = txtNamXB.Text == null || txtNamXB.Text == "" ? 0 : Convert.ToInt32(txtNamXB.Text);
            string nhaxuatban = cbbNhaXB.Text;
            string ngonngu = cbbNgonNgu.Text;
            string theloai = cbbTheLoai.Text;

            dataGridView.DataSource = typeof(List<SACH_VIEW>);
            dataGridView.DataSource = BLL_SACH.Instance.timSach(masach, nhande, tacgia, namxuatban, nhaxuatban, ngonngu, theloai);
            setDGVColumnsHeader();
        }
    }
}
