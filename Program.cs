using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.BLL;
using QuanLyThuVien.VIEW;

namespace QuanLyThuVien
{
    internal static class Program
    {
        static CN_Dangnhap cN_Dangnhap;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            cN_Dangnhap = new CN_Dangnhap();
            Application.Run(cN_Dangnhap);
            //Application.Run(new Form1());
        }
        public static void exitProgram()
        {
            Application.Exit();
        }

        public static void callThuThuForm(string admin, string nhiemvu)
        {
            cN_Dangnhap.Hide();
            Form_Thuthu form_Thuthu = new Form_Thuthu(admin, nhiemvu);
            form_Thuthu.ShowDialog();
            cN_Dangnhap.Close();
        }
        public static void callAdminForm(string admin, string nhiemvu)
        {
            cN_Dangnhap.Hide();
            Form_Admin form_Admin = new Form_Admin(admin, nhiemvu);
            form_Admin.ShowDialog();
            cN_Dangnhap.Close();
        }

        public static void reLogin()
        {
            cN_Dangnhap = new CN_Dangnhap();
            cN_Dangnhap.ShowDialog();
        }
    }
}
