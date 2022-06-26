using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.VIEW;
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;

namespace QuanLyThuVien.BLL
{
    internal class BLL_PHANQUYEN
    {
        private static BLL_PHANQUYEN _Instance;
        private BLL_PHANQUYEN()
        {

        }

        public static BLL_PHANQUYEN Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_PHANQUYEN();
                }
                return _Instance;
            }
            private set { }
        }
        public NGUOIDUNG checkValidPassword(string user, string password)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            foreach (NGUOIDUNG u in entities.NGUOIDUNGs)
            {
                if (u.TAIKHOAN == user && u.MATKHAU == password)
                {
                    return u;
                }
            }
            return null;
        }

        public string checkRole(NGUOIDUNG user)
        {
            return user.MANHIEMVU;
        }

        public string changePassword(string manguoidung, string oPass, string nPass, string rPass)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NGUOIDUNG user = entities.NGUOIDUNGs.Find(manguoidung);
            string result;
            if (user.MATKHAU == oPass)
            {
                if (nPass == rPass)
                {
                    user.MATKHAU = nPass;
                    result = "Doi mat khau thanh cong";
                    entities.SaveChanges();
                }
                else
                {
                    result = "Mat khau moi nhap lai khong trung khop";
                }
            }
            else
            {
                result = "Mat khau cu khong dung";
            }
            return result;
        }
        public void dencentralize(string user, string password)
        {
            NGUOIDUNG taikhoan = checkValidPassword(user, password);
            if (taikhoan != null)
            {
                string role = BLL_PHANQUYEN.Instance.checkRole(taikhoan);
                if (role == "001") // thu thu
                {
                    string s = "Thủ thư";
                    Program.callAdminForm(user, s);
                }
                else if (role == "002") // thu kho
                {
                    string s = "Thủ kho";
                    Program.callThuThuForm(user, s);
                }
                else
                {
                    // ----------check here please----------
                    CN_Thongbao f = new CN_Thongbao();
                    f.setNotice("Nhiệm vụ không tồn tại!\nVui lòng nhập lại...");
                }
            }
            else
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Tên đăng nhập hoặc mật khẩu không đúng!\nVui lòng nhập lại...");
            }
        }
    }
}
