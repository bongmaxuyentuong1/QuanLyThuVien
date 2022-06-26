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
namespace QuanLyThuVien.VIEW
{
    public partial class Thongke_Sachmuonnhieu : Form
    {
        public Thongke_Sachmuonnhieu()
        {
            InitializeComponent();
            showSachDuocMuonNhieu();
        }
        private void showSachDuocMuonNhieu()
        {
            dataGridView1.DataSource = BLL_SACH.Instance.getSachDuocMuonNhieu();
        }
    }
}
