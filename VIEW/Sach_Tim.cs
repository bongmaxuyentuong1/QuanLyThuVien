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
    public partial class Sach_Tim : Form
    {
        public Sach_Tim()
        {
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_SACH.Instance.getAllSach();
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
        public void GUI()
        {
            // add combobox items 
            List<string> theloais = BLL_SACH.Instance.getAllTheloai();
            List<string> ngonngus = BLL_SACH.Instance.getAllNgonNgu();
            List<string> nxbs = BLL_SACH.Instance.getAllNXB();
            cbbMaTL.Items.Add(new CBB_ITEM() { VALUE = 0, TEXT = "Tất cả" });
            cbbMaNN.Items.Add(new CBB_ITEM() { VALUE = 0, TEXT = "Tất cả" });
            cbbMaNXB.Items.Add(new CBB_ITEM() { VALUE = 0, TEXT = "Tất cả" });
            for (int i = 1; i <= theloais.Count; i++)
            {
                cbbMaTL.Items.Add(new CBB_ITEM() { TEXT = theloais[i - 1], VALUE = i });
            }
            cbbMaTL.SelectedIndex = 0;
            for (int i = 1; i <= ngonngus.Count; i++)
            {
                cbbMaNN.Items.Add(new CBB_ITEM() { TEXT = ngonngus[i - 1], VALUE = i });
            }
            cbbMaNN.SelectedIndex = 0;
            for (int i = 1; i <= nxbs.Count; i++)
            {
                cbbMaNXB.Items.Add(new CBB_ITEM() { TEXT = nxbs[i - 1], VALUE = i });
            }
            cbbMaNXB.SelectedIndex = 0;

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string theloai = cbbMaTL.Text;
            string ngonngu = cbbMaNN.Text;
            string nxb = cbbMaNXB.Text;
            string nhande = txtNhande.Text.ToLower();
            string tacgia = txtTacgia.Text.ToLower();
            int nam;
            bool res = int.TryParse(txtNamXB.Text, out nam);
            if (res)
            {
                dataGridView1.DataSource = BLL_SACH.Instance.timSach("0", nhande, tacgia, nam, nxb, ngonngu, theloai);
                setDGVColumnsHeader();
            }
            else
            {
                dataGridView1.DataSource = BLL_SACH.Instance.timSach("0", nhande, tacgia, nam, nxb, ngonngu, theloai);
                setDGVColumnsHeader();
            }
        }
        private void btnHienthi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string masach = dataGridView1.SelectedRows[0].Cells["MASACH"].Value.ToString();
                Sach sach = new Sach(masach);
                sach.Show();
            }
        }
    }
}
