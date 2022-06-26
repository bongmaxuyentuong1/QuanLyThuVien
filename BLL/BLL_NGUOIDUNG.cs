using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;
namespace QuanLyThuVien.BLL
{
    internal class BLL_NGUOIDUNG
    {
        private static BLL_NGUOIDUNG _Instance;
        private BLL_NGUOIDUNG()
        {

        }

        public static BLL_NGUOIDUNG Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_NGUOIDUNG();
                }
                return _Instance;
            }
            private set { }
        }
        public List<string> getAllMaNguoiDung()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<string> list_id = entities.NGUOIDUNGs.Select(p => p.MANGUOIDUNG).ToList();
            list_id.Sort();
            return list_id;
        }
        public NGUOIDUNG_VIEW convertNGUOIDUNG2NGUOIDUNG_VIEW(NGUOIDUNG user)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return new NGUOIDUNG_VIEW()
            {
                MANGUOIDUNG = user.MANGUOIDUNG,
                HOTEN = user.HOTEN,
                NAMSINH = user.NAMSINH,
                GIOITINH = user.GIOITINH,
                DIENTHOAI = user.DIENTHOAI,
                EMAIL = user.EMAIL,
                TAIKHOAN = user.TAIKHOAN,
                TENNHIEMVU = entities.NHIEMVUs.Find(user.MANHIEMVU).TENNHIEMVU
            };
        }

        public List<NGUOIDUNG_VIEW> getAllUserViews()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();

            List<NGUOIDUNG> users = (from user in entities.NGUOIDUNGs select user).ToList();
            List<NGUOIDUNG_VIEW> result = new List<NGUOIDUNG_VIEW>();
            foreach (NGUOIDUNG user in users)
            {
                result.Add(convertNGUOIDUNG2NGUOIDUNG_VIEW(user));
            }
            return result;
        }

        public void themNguoiDung(string manguoidung, string hoten, DateTime namsinh, bool gender, string dt, string email, string user, string pass, string manhiemvu)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NGUOIDUNG newUser = new NGUOIDUNG()
            {
                MANGUOIDUNG = manguoidung,
                HOTEN = hoten,
                NAMSINH = namsinh.Date,
                GIOITINH = gender,
                DIENTHOAI = dt,
                EMAIL = email,
                TAIKHOAN = user,
                MATKHAU = pass,
                MANHIEMVU = manhiemvu
            };
            entities.NGUOIDUNGs.Add(newUser);
            entities.SaveChanges();
        }
        public NGUOIDUNG findUserByTaikhoan(string taikhoan)
        {
            NGUOIDUNG nguoidung = null;
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            nguoidung = entities.NGUOIDUNGs.Select(p => p).Where(p => p.TAIKHOAN == taikhoan).FirstOrDefault();
            return nguoidung;
        }
        public void xoaNguoiDung(List<string> list_manguoidung)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            foreach (string manguoidung in list_manguoidung)
            {
                NGUOIDUNG nguoidung = entities.NGUOIDUNGs.Where(p => p.MANGUOIDUNG == manguoidung).FirstOrDefault();
                entities.NGUOIDUNGs.Remove(nguoidung);
            }
            entities.SaveChanges();
        }

        public string getMaNguoiDungTheoTaiKhoan(string taikhoan)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NGUOIDUNG nguoidung = entities.NGUOIDUNGs.Where(p => p.TAIKHOAN == taikhoan).FirstOrDefault();
            if (nguoidung != null)
            {
                return nguoidung.MANGUOIDUNG;
            }
            return "";
        }

        public List<NGUOIDUNG_VIEW> sapXepNguoiDung(List<NGUOIDUNG_VIEW> list, int value)
        {
            List<NGUOIDUNG_VIEW> list_new = list.OrderBy(o => o.MANGUOIDUNG).ToList();
            if (value == 2)
            {
                list_new = list.OrderBy(o => o.HOTEN).ToList();
            }
            else if (value == 3)
            {
                list_new = list.OrderBy(o => o.NAMSINH).ToList();
            }
            return list_new;
        }

        public List<NGUOIDUNG_VIEW> timNguoiDung(string manguoidung, string hoten, string taikhoan, int nhiemvu, int namsinh, string dienthoai, string email)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            string manhiemvu = nhiemvu == 0 ? "" : nhiemvu == 1 ? "001":"002";
            List<NGUOIDUNG_VIEW> list = new List<NGUOIDUNG_VIEW>();
            list = entities.NGUOIDUNGs.
                Where(p => p.MANGUOIDUNG.Contains(manguoidung)).
                Where(p => p.HOTEN.Contains(hoten)).
                Where(p => p.TAIKHOAN.Contains(taikhoan)).
                Where(p => p.NAMSINH.Year == namsinh || namsinh == 0).
                Where(p => p.MANHIEMVU.Contains(manhiemvu)).
                Where(p => p.DIENTHOAI.Contains(dienthoai)).
                Where(p => p.EMAIL.Contains(email)).
                Select(p => new NGUOIDUNG_VIEW()
                {
                    MANGUOIDUNG = p.MANGUOIDUNG,
                    HOTEN = p.HOTEN,
                    NAMSINH = p.NAMSINH,
                    GIOITINH = p.GIOITINH,
                    DIENTHOAI = p.DIENTHOAI,
                    EMAIL = p.EMAIL,
                    TAIKHOAN = p.TAIKHOAN,
//                    TENNHIEMVU = entities.NHIEMVUs.Where(p1 => p1.MANHIEMVU == manhiemvu).FirstOrDefault().TENNHIEMVU
                    TENNHIEMVU = p.NHIEMVU.TENNHIEMVU,
                }).ToList();
            return list;
        }

        public NGUOIDUNG timNguoiDungTheoMaNguoiDung(string manguoidung)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.NGUOIDUNGs.Where(p => p.MANGUOIDUNG == manguoidung).FirstOrDefault();
        }

        public void suaNguoiDung(string manguoidung, string hoten, string taikhoan, string dienthoai, string email, DateTime namsinh, bool gioitinh, string manhiemvu)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NGUOIDUNG nguoidung = entities.NGUOIDUNGs.Where(p => p.MANGUOIDUNG == manguoidung).FirstOrDefault();
            nguoidung.HOTEN = hoten;
            nguoidung.TAIKHOAN = taikhoan;
            nguoidung.DIENTHOAI = dienthoai;
            nguoidung.EMAIL = email;
            nguoidung.NAMSINH = namsinh.Date;
            nguoidung.MANHIEMVU = manhiemvu;
            nguoidung.GIOITINH = gioitinh;
            entities.SaveChanges();
        }
        public List<string> layThongtinNguoiDungTheoNguoiDung(NGUOIDUNG nguoidung)
        {
            List<string> list = new List<string>();

            string hoten = nguoidung.HOTEN.ToString();
            list.Add(hoten);
            string manv = nguoidung.MANHIEMVU.ToString();
            list.Add(manv);
            string dienthoai = nguoidung.DIENTHOAI.ToString();
            list.Add(dienthoai);
            string namsinh = nguoidung.NAMSINH.ToShortDateString();
            list.Add(namsinh);
            string gioitinh = (nguoidung.GIOITINH)?"Nam":"Nữ";
            list.Add(gioitinh);
            string email = nguoidung.EMAIL.ToString();
            list.Add(email);
            return list;
        }
    }
}
