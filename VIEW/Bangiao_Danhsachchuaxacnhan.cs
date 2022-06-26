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
    public partial class Bangiao_Danhsachchuaxacnhan : Form
    {
        private string manguoidung;
        public Bangiao_Danhsachchuaxacnhan(string manguoidung)
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
        }
        public void GUI()
        {
            dataGridView1.DataSource = BLL_BANGIAO.Instance.getBanGiaoChuaXacNhan(this.manguoidung);
            setColumnsHeader();
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<string> list_mabangiao = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    list_mabangiao.Add(row.Cells["MABANGIAO"].Value.ToString());
                }
                BLL_BANGIAO.Instance.tuChoiBanGiao(list_mabangiao);
                GUI();
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn các bàn giao cần từ chối!");
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string mabangiao = dataGridView1.SelectedRows[0].Cells["MABANGIAO"].Value.ToString();
                Bangiao_Xacnhan bangiao_Xacnhan = new Bangiao_Xacnhan(this.manguoidung, mabangiao);
                bangiao_Xacnhan.d = new Bangiao_Xacnhan.Mydel(GUI);
                bangiao_Xacnhan.Show();
            }
            else
            {
                //MessageBox.Show("Vui long chon 1 dong");
                
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn 1 dòng!");
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DateTime ngayxacnhan = DateTime.Now;
                List<string> list_mabangiao = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    list_mabangiao.Add(row.Cells["MABANGIAO"].Value.ToString());
                }
                BLL_BANGIAO.Instance.xacNhanBanGiao(list_mabangiao, manguoidung, ngayxacnhan);
                GUI();
            }

        }
    }
}
