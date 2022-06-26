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
    public partial class Sach_Sua : Form
    {
        private string manguoidung;
        private string masach;
        private SACH sach;
        public delegate void Mydel();
        public Mydel d;
        public Sach_Sua(string manguoidung, string masach)
        {
            this.manguoidung = manguoidung;
            this.masach = masach;
            this.sach = BLL_SACH.Instance.findSachByMasach(masach);
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_SACH.Instance.getAllSach();
            setDGVColumnsHeader();
        }
        public Sach_Sua(string masach)
        {
            this.manguoidung = null;
            this.masach = masach;
            this.sach = BLL_SACH.Instance.findSachByMasach(masach);
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_SACH.Instance.getAllSach();
            setDGVColumnsHeader();

        }
        public void GUI()
        {
            lbMaSach.Text = this.masach;
            txtNhande.Text = sach.NHANDE.ToString();
            txtNamXB.Text = sach.NAMXB.Year.ToString();
            txtTacgia.Text = sach.TACGIA.ToString();
            List<string> theloais = BLL_SACH.Instance.getAllTheloai();
            for (int i = 1; i <= theloais.Count; i++)
            {
                cbbTheLoai.Items.Add(new CBB_ITEM()
                {
                    TEXT = theloais[i - 1],
                    VALUE = i
                });
            }
            cbbTheLoai.Text = sach.THELOAI.TENTHELOAI.ToString();

            List<string> nns = BLL_SACH.Instance.getAllNgonNgu();
            for (int i = 1; i <= nns.Count; i++)
            {
                cbbNgonNgu.Items.Add(new CBB_ITEM()
                {
                    TEXT = nns[i - 1],
                    VALUE = i
                });
            }
            cbbNgonNgu.Text = sach.NGONNGU.TENNGONNGU.ToString();

            List<string> nxbs = BLL_SACH.Instance.getAllNXB();
            for (int i = 1; i <= nxbs.Count; i++)
            {
                cbbNXB.Items.Add(new CBB_ITEM()
                {
                    TEXT = nxbs[i - 1],
                    VALUE = i
                });
            }
            cbbNXB.Text = sach.NXB.TENNXB.ToString();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã sách";
            dataGridView1.Columns[1].HeaderText = "Nhan đề";
            dataGridView1.Columns[2].HeaderText = "Thể loại";
            dataGridView1.Columns[3].HeaderText = "Nhà xuất bản";
            dataGridView1.Columns[4].HeaderText = "Năm xuất bản";
            dataGridView1.Columns[5].HeaderText = "Ngôn ngữ";
            dataGridView1.Columns[6].HeaderText = "Tác giả";
            dataGridView1.Columns[7].HeaderText = "Tổng số lượng";
            dataGridView1.Columns[8].HeaderText = "Số lượng đang mượn";
            dataGridView1.Columns[9].HeaderText = "Số lượng hiện tại";
            dataGridView1.Columns[10].HeaderText = "Số lượng tồn kho";
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string masach = lbMaSach.Text;
            string nhande = txtNhande.Text;
            string tacgia = txtTacgia.Text;
            DateTime namxuatban = new DateTime(Convert.ToInt32(txtNamXB.Text), 1, 1);
            string mangonngu = ((CBB_ITEM)cbbNgonNgu.SelectedItem).VALUE.ToString();
            for (int i = 1; i <= 3 - mangonngu.Length + 1; i++)
            {
                mangonngu = "0" + mangonngu;
            }
            string matheloai = ((CBB_ITEM)cbbTheLoai.SelectedItem).VALUE.ToString();
            for (int i = 1; i <= 3 - matheloai.Length + 1; i++)
            {
                matheloai = "0" + matheloai;
            }
            string manxb = ((CBB_ITEM)cbbNXB.SelectedItem).VALUE.ToString();
            for (int i = 1; i <= 3 - manxb.Length + 1; i++)
            {
                manxb = "0" + manxb;
            }
            BLL_SACH.Instance.suaSach(masach, nhande, matheloai, manxb, namxuatban, mangonngu, tacgia);
            dataGridView1.DataSource = BLL_SACH.Instance.getAllSach();
            if (d != null)
            {
                //MessageBox.Show("cogoi");
                d();
            }
        }
    }
}
