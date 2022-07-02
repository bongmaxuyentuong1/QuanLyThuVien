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
    public partial class Nhapsach_Them : Form
    {
        private string manguoidung { get; set; }
        public delegate void Mydel();
        public Mydel d = null;
        public Nhapsach_Them(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã sách";
            dataGridView1.Columns[1].HeaderText = "Số lượng";
        }
        public void GUI()
        {
            //  tao tu dong manhapsach
            List<string> list_manhapsach = BLL_NHAPSACH.Instance.getAllMaNhapSach();
            lbManhapsach.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(list_manhapsach));
            // tao tu dong ngaynhapsach
            lbNgaynhapsach.Text = DateTime.Now.ToString();
            // tao tu dong manguoidung
            lbManguoidung.Text = this.manguoidung;
        }

        public static void showMessageBoxAddNewSach()
        {
            string message = "Sách chưa tồn tại trong dữ liệu, vui lòng thêm sách mới!";
            string title = "Bổ sung sách";
            MessageBoxButtons yesno = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, yesno);
            if (result == DialogResult.Yes)
            {
                Sach_Them sach_Them = new Sach_Them();
                sach_Them.Show();
            }
        }

        public static void showMessageBox(string message)
        {
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice(message);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string masach = txtMasach.Text;
            int soluong = Int32.Parse(txtSoluong.Text);
            List<NHAPSACH_VIEW_SOLUONG> new_list = BLL_NHAPSACH.Instance.themSoLuongVaoDGV(masach, soluong, (List<NHAPSACH_VIEW_SOLUONG>)dataGridView1.DataSource);
            dataGridView1.DataSource = typeof(List<NHAPSACH_VIEW_SOLUONG>);
            dataGridView1.DataSource = new_list;
            setDGVColumnsHeader();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string masach = dataGridView1.SelectedRows[0].Cells["MASACH"].Value.ToString();
                List<NHAPSACH_VIEW_SOLUONG> new_list = BLL_NHAPSACH.Instance.xoaDongDGV(masach, (List<NHAPSACH_VIEW_SOLUONG>)dataGridView1.DataSource);
                dataGridView1.DataSource = typeof(List<NHAPSACH_VIEW_SOLUONG>);
                dataGridView1.DataSource = new_list;
                setDGVColumnsHeader();
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
            if (dataGridView1 == null || dataGridView1.Rows.Count == 0)
            {
                //MessageBox.Show("Vui lòng thêm sách trước!");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng thêm sách trước!");
            }
            else
            {
                string manhapsach = lbManhapsach.Text;
                string manguoidung = this.manguoidung;
                DateTime ngaynhap = Convert.ToDateTime(lbNgaynhapsach.Text);
                List<NHAPSACH_VIEW_SOLUONG> list = (List<NHAPSACH_VIEW_SOLUONG>)dataGridView1.DataSource;
                BLL_NHAPSACH.Instance.themNhapSach(manhapsach, ngaynhap, manguoidung, list);
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Thêm thông tin nhập sách thành công!");
                if (d != null)
                {
                    d();
                }
                dataGridView1.DataSource = null;
                txtMasach.Text = "";
                txtSoluong.Text = "";
                GUI();
            }
        }

        private void txtMasach_TabIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
