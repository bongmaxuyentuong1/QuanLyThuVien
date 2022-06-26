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
    public partial class Sach : Form
    {
        public string masach;
        public string textChange;
        public SACH sach;
        public Sach(string MASACH)
        {
            this.masach = MASACH;
            InitializeComponent();
            GUI();
        }
        public void GUI()
        {
            sach = BLL_SACH.Instance.findSachByMasach(masach);
            List<string> list = BLL_SACH.Instance.getInformationSach(sach);
            lbNhande.Text = list[0];
            lbTacgia.Text = list[1];
            lbNXB.Text = list[2];
            lbTheloai.Text = list[3];
            lbNN.Text = list[4];
            dataGridView1.DataSource = new List<SOLUONG_VIEW>() { BLL_SACH.Instance.getSLSACH(sach) };
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            Sach_Sua sach_Sua = new Sach_Sua(masach);
            sach_Sua.d = new Sach_Sua.Mydel(GUI);
            sach_Sua.Show();
        }
    }
}
