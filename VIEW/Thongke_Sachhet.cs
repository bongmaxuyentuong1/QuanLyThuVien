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
    public partial class Thongke_Sachhet : Form
    {
        public Thongke_Sachhet(int a)
        {
            InitializeComponent();
            showSachHet(a);
        }
        private void showSachHet(int a)
        {
            dataGridView1.DataSource = BLL_SACH.Instance.getSachHet(a);
        }
    }
}
