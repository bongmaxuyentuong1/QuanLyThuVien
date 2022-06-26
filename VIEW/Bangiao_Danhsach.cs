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
    public partial class Bangiao_Danhsach : Form
    {
        private string manguoidung;
        public Bangiao_Danhsach(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
        }

        public void setColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã bàn giao";
            dataGridView1.Columns[1].HeaderText = "Mã người bàn giao";
            dataGridView1.Columns[2].HeaderText = "Ngày bàn giao";
            dataGridView1.Columns[3].HeaderText = "Mã người xác nhận";
            dataGridView1.Columns[4].HeaderText = "Ngày xác nhận";
            dataGridView1.Columns[5].HeaderText = "Mã sách";
            dataGridView1.Columns[6].HeaderText = "Số lượng";
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Cyan;
        }

        public void GUI()
        {
            dataGridView1.DataSource = BLL_BANGIAO.Instance.getAllBanGiaoChiTiet();
            setColumnsHeader();
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 1,
                TEXT = "Mã bàn giao"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 2,
                TEXT = "Mã người bàn giao"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 3,
                TEXT = "Mã người xác nhận"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 4,
                TEXT = "Ngày bàn giao"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 5,
                TEXT = "Ngày xác nhận"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 6,
                TEXT = "Mã sách"
            });
            cbbThuocTinh.SelectedIndex = 0;
        }

        private void btnToanBo_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_BANGIAO.Instance.getAllBanGiaoChiTiet();
            setColumnsHeader();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Bangiao_Them bangiao_Them = new Bangiao_Them(this.manguoidung);
            bangiao_Them.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string mabangiao = dataGridView1.SelectedRows[0].Cells["MABANGIAO"].Value.ToString();
                Bangiao_Sua bangiao_Sua = new Bangiao_Sua(this.manguoidung, mabangiao);
                bangiao_Sua.Show();
            }
            else
            {
                Bangiao_Sua bangiao_Sua = new Bangiao_Sua(this.manguoidung);
                bangiao_Sua.Show();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<string> list_mabangiao = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    list_mabangiao.Add(row.Cells["MABANGIAO"].Value.ToString());
                }
                list_mabangiao = list_mabangiao.Distinct().ToList();
                BLL_BANGIAO.Instance.xoaBanGiao(list_mabangiao);
                //MessageBox.Show("Xóa bàn giao thành công!");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Xóa bàn giao thành công!");
            }
            else
            {
                //MessageBox.Show("Vui lòng chọn các bàn giao cần xóa");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn các bàn giao cần xóa!");
            }
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            List<BANGIAO_VIEW_CHITIET> list = (List<BANGIAO_VIEW_CHITIET>)dataGridView1.DataSource;
            int value = ((CBB_ITEM)cbbThuocTinh.SelectedItem).VALUE;
            dataGridView1.DataSource = typeof(List<BANGIAO_VIEW_CHITIET>);
            dataGridView1.DataSource = BLL_BANGIAO.Instance.sapXepBanGiao(list, value);
            setColumnsHeader();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string mabangiao = txtMaBanGiao.Text;
            string manguoibangiao = txtMaNguoiBanGiao.Text;
            string manguoixacnhan = txtMaNguoiXacNhan.Text;
            string masach = txtMaSach.Text;
            dataGridView1.DataSource = typeof(List<BANGIAO_VIEW_CHITIET>);
            dataGridView1.DataSource = BLL_BANGIAO.Instance.timBanGiaoChiTiet(mabangiao, manguoibangiao, manguoixacnhan, masach);
            setColumnsHeader();
        }
    }
}
