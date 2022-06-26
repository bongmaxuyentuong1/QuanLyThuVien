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
    public partial class Sach_Them : Form
    {
        private string manguoidung = null;
        public delegate void Mydel();
        public Mydel d = null;
        public Sach_Them(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_SACH.Instance.getAllSach();
            setDGVColumnsHeader();
        }
        public Sach_Them()
        {
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_SACH.Instance.getAllSach();
            setDGVColumnsHeader();
        }
        public void GUI()
        {
            cbbNgonNgu.Items.Clear();
            List<string> nns = BLL_SACH.Instance.getAllNgonNgu();
            for (int i = 1; i <= nns.Count; i++)
            {
                cbbNgonNgu.Items.Add(new CBB_ITEM() { TEXT = nns[i - 1], VALUE = i });
            }
            cbbNgonNgu.SelectedIndex = 0;

            cbbNXB.Items.Clear();
            List<string> nxbs = BLL_SACH.Instance.getAllNXB();
            for (int i = 1; i <= nxbs.Count; i++)
            {
                cbbNXB.Items.Add(new CBB_ITEM() { TEXT = nxbs[i - 1], VALUE = i });
            }
            cbbNXB.SelectedIndex = 0;

            cbbTheLoai.Items.Clear();
            List<string> theloais = BLL_SACH.Instance.getAllTheloai();
            for (int i = 1; i <= theloais.Count; i++)
            {
                cbbTheLoai.Items.Add(new CBB_ITEM() { TEXT = theloais[i - 1], VALUE = i });
            }
            cbbTheLoai.SelectedIndex = 0;
            lbMaSach.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_SACH.Instance.getAllMaSach()));
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
        private void btnThem_Click(object sender, EventArgs e)
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
            try
            {
                BLL_SACH.Instance.themSach(masach, nhande, matheloai, manxb, namxuatban, mangonngu, tacgia);
                dataGridView1.DataSource = BLL_SACH.Instance.getAllSach();
                if (manguoidung != null && d != null)
                    d();
                reLoad();
            }
            catch (Exception ex)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Kiểm tra đầu vào!");
            }
        }
        public void reLoad()
        {
            cbbTheLoai.SelectedIndex = 0;
            cbbNgonNgu.SelectedIndex = 0;
            cbbNXB.SelectedIndex = 0;
            txtNamXB.Text = "";
            txtNhande.Text = "";
            txtTacgia.Text = "";
            lbMaSach.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_SACH.Instance.getAllMaSach()));
        }

        private void btnThemTheLoai_Click(object sender, EventArgs e)
        {
            Theloai_Them f = new Theloai_Them();
            f._reload = GUI;
            f.Show();
        }

        private void btnThemNgonNgu_Click(object sender, EventArgs e)
        {
            Ngonngu_Them f = new Ngonngu_Them();
            f._reload = GUI;

            f.Show();
        }

        private void btnThemNXB_Click(object sender, EventArgs e)
        {
            NXB_Them f = new NXB_Them();
            f._reload = GUI;
            f.Show();
        }
    }
}
