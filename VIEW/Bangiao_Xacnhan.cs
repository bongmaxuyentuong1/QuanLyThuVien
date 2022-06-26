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

namespace QuanLyThuVien.VIEW
{
    public partial class Bangiao_Xacnhan : Form
    {
        private string manguoidung;
        private string mabangiao;
        public delegate void Mydel();
        public Mydel d;
        public Bangiao_Xacnhan(string manguoidung, string mabangiao)
        {
            this.manguoidung = manguoidung;
            this.mabangiao = mabangiao;
            InitializeComponent();
            GUI();
        }
        public void GUI()
        {
            lbMaBanGiao.Text = this.mabangiao;
            lbMaNguoiXacNhan.Text = this.manguoidung;
            lbNgayXacNhan.Text = DateTime.Now.ToString();
            dataGridView1.DataSource = BLL_BANGIAO.Instance.getSoLuongTheoMaBanGiao(this.mabangiao);
            setColumnsHeader();
        }
        public void setColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã sách";
            dataGridView1.Columns[1].HeaderText = "Số lượng";
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            List<string> list_mabangiao = new List<string>()
            {
                this.mabangiao
            };
            BLL_BANGIAO.Instance.tuChoiBanGiao(list_mabangiao);
            //MessageBox.Show("Tu choi!");
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice("Từ chối!");
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            List<string> list_mabangiao = new List<string>()
            {
                this.mabangiao
            };
            BLL_BANGIAO.Instance.xacNhanBanGiao(list_mabangiao, this.manguoidung, DateTime.Now);
            //MessageBox.Show("Chap nhan");
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice("Chấp nhận!");
            d();
            this.Close();
        }
    }
}
