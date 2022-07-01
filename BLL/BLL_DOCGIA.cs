using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;
using System.Windows.Forms;
namespace QuanLyThuVien.BLL
{
    internal class BLL_DOCGIA
    {
        private static BLL_DOCGIA _Instance;
        private BLL_DOCGIA()
        {

        }
        public static BLL_DOCGIA Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_DOCGIA();
                }
                return _Instance;
            }
            private set { }
        }
        public List<DOCGIA_VIEW_SINHVIEN> getAllDocGiaSinhVien()
        {
            List<DOCGIA_VIEW_SINHVIEN> list_docgia_sinhvien = new List<DOCGIA_VIEW_SINHVIEN>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            var list = from docgia in entities.DOCGIAs
                       join sinhvien in entities.SINHVIENs
                       on docgia.MADOCGIA equals sinhvien.MADOCGIA
                       select new DOCGIA_VIEW_SINHVIEN()
                       {
                           MADOCGIA = docgia.MADOCGIA,
                           HOTEN = docgia.HOTEN,
                           DIACHI = docgia.DIACHI,
                           SOSACHMUON = (int)docgia.SOSACHMUON,
                           MASINHVIEN = sinhvien.MASINHVIEN,
                           LOPSH = sinhvien.LOPSH.TENLOPSH
                       };
            list_docgia_sinhvien = list.ToList();
            return list_docgia_sinhvien;
        }
        public List<DOCGIA_VIEW_GIANGVIEN> getAllDocGiaGiangVien()
        {
            List<DOCGIA_VIEW_GIANGVIEN> list_docgia_giangvien = new List<DOCGIA_VIEW_GIANGVIEN>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            var list = from docgia in entities.DOCGIAs
                       join giangvien in entities.GIANGVIENs
                       on docgia.MADOCGIA equals giangvien.MADOCGIA
                       select new DOCGIA_VIEW_GIANGVIEN()
                       {
                           MADOCGIA = docgia.MADOCGIA,
                           HOTEN = docgia.HOTEN,
                           DIACHI = docgia.DIACHI,
                           SOSACHMUON = (int)docgia.SOSACHMUON,
                           HOCVI = giangvien.HOCVI,
                           KHOA = giangvien.KHOA.TENKHOA
                       };
            list_docgia_giangvien = list.ToList();
            return list_docgia_giangvien;
        }

        public bool checkValid(string hoten, string diachi, bool sinhvien,
            string mssv, string malopsh, string hocvi, string makhoa) // true: valid
        {
            bool res = true;
            if (hoten.Length >= 50 || diachi.Length >= 50)
            {
                res = false;
            }
            if (sinhvien)
            {
                if (mssv.Length >= 10 || checkMSSV(mssv))
                {
                    res = false;
                }
            }
            else
            {
                if (hocvi.Length >= 50)
                {
                    res = false;
                }
            }
            return res;
        }

        public bool checkMSSV(string masinhvien) // true : ton tai trong database
        {
            bool res = false;
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            var sinhvien = entities.SINHVIENs.Where(p => p.MASINHVIEN == masinhvien).FirstOrDefault();
            if (sinhvien != null)
            {
                res = true;
            }
            return res;
        }
        public List<string> getAllMaDocGia()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<string> list_id = entities.DOCGIAs.Select(p => p.MADOCGIA).ToList();
            return list_id;
        }
        public string themDocGia(string madocgia, string hoten, string diachi, bool sinhvien,
            string mssv, string malopsh, string hocvi, string makhoa)
        {
            bool validAdd = checkValid(hoten, diachi, sinhvien, mssv, malopsh, hocvi, makhoa);
            if (validAdd)
            {
                QuanLyThuVienEntities entities = new QuanLyThuVienEntities();

                entities.DOCGIAs.Add(new DOCGIA()
                {
                    MADOCGIA = madocgia,
                    HOTEN = hoten,
                    DIACHI = diachi,
                    SOSACHMUON = 0
                });

                if (sinhvien)
                {
                    entities.SINHVIENs.Add(new SINHVIEN()
                    {
                        MADOCGIA = madocgia,
                        MASINHVIEN = mssv,
                        MALOPSH = malopsh
                    });
                }
                else
                {
                    entities.GIANGVIENs.Add(new GIANGVIEN()
                    {
                        MADOCGIA = madocgia,
                        HOCVI = hocvi,
                        MAKHOA = makhoa
                    });
                }
                entities.SaveChanges();
                return "Thêm dữ liệu thành công!";
            }
            else
            {
                return "Đầu vào không phù hợp, vui lòng kiểm tra lại!";
            }
        }

        public List<string> getAllLopSH()
        {
            List<string> list_lopsh = new List<string>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            list_lopsh = entities.LOPSHes.Select(e => e.TENLOPSH).ToList();
            return list_lopsh;
        }

        public List<string> getAllKhoa()
        {
            List<string> list_khoa = new List<string>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            list_khoa = entities.KHOAs.Select(e => e.TENKHOA).ToList();
            return list_khoa;
        }

        public List<DOCGIA_VIEW_SINHVIEN> timDocGiaSinhVien(string hoten, string diachi, string madocgia,
            bool sinhvien, string mssv, string malopsh, string hocvi, string makhoa)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            if (sinhvien)
            {
                List<DOCGIA_VIEW_SINHVIEN> list = (from docgia in entities.DOCGIAs
                                                   join sv in entities.SINHVIENs
                                                   on docgia.MADOCGIA equals sv.MADOCGIA
                                                   where docgia.HOTEN.Contains(hoten)
                                                   where docgia.DIACHI.Contains(diachi)
                                                   where docgia.MADOCGIA.Contains(madocgia)
                                                   where (sv.MALOPSH == malopsh || malopsh == "000")
                                                   where sv.MASINHVIEN.Contains(mssv)
                                                   select new DOCGIA_VIEW_SINHVIEN()
                                                   {
                                                       MADOCGIA = docgia.MADOCGIA,
                                                       HOTEN = docgia.HOTEN,
                                                       DIACHI = docgia.DIACHI,
                                                       SOSACHMUON = (int)docgia.SOSACHMUON,
                                                       MASINHVIEN = sv.MASINHVIEN,
                                                       LOPSH = sv.LOPSH.TENLOPSH
                                                   }).ToList();
                return list;
            }
            return null;
        }
        public List<DOCGIA_VIEW_GIANGVIEN> timDocGiaGiangVien(string hoten, string diachi, string madocgia,
            bool sinhvien, string mssv, string malopsh, string hocvi, string makhoa)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            if (!sinhvien)
            {
                List<DOCGIA_VIEW_GIANGVIEN> list = (from docgia in entities.DOCGIAs
                                                    join gv in entities.GIANGVIENs
                                                    on docgia.MADOCGIA equals gv.MADOCGIA
                                                    where docgia.HOTEN.Contains(hoten)
                                                    where docgia.DIACHI.Contains(diachi)
                                                    where docgia.MADOCGIA.Contains(madocgia)
                                                    where gv.HOCVI.Contains(hocvi)
                                                    where (gv.MAKHOA == makhoa || makhoa == "000")
                                                    select new DOCGIA_VIEW_GIANGVIEN()
                                                    {
                                                        MADOCGIA = docgia.MADOCGIA,
                                                        HOTEN = docgia.HOTEN,
                                                        DIACHI = docgia.DIACHI,
                                                        SOSACHMUON = (int)docgia.SOSACHMUON,
                                                        HOCVI = gv.HOCVI,
                                                        KHOA = gv.KHOA.TENKHOA
                                                    }).ToList();
                return list;
            }
            return null;
        }

        public string xoaDanhSachDocGiaGiangVien(List<string> list_id_giangvien)
        {
            try
            {
                foreach (string id_giangvien in list_id_giangvien)
                {
                    xoaDocGiaGiangVien(id_giangvien);
                }
                return "Xoá thành công!";
            }
            catch (Exception ex)
            {
                return "Xuất hiện lỗi!";
            }
        }

        public void xoaDocGiaGiangVien(string id_giangvien)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            GIANGVIEN giangvien = entities.GIANGVIENs.Where(p => p.MADOCGIA == id_giangvien).FirstOrDefault();
            entities.GIANGVIENs.Remove(giangvien); ;
            entities.SaveChanges();

            DOCGIA docgia = entities.DOCGIAs.Where(p => p.MADOCGIA == id_giangvien).FirstOrDefault();
            entities.DOCGIAs.Remove(docgia);
            entities.SaveChanges();
        }
        public string xoaDanhSachDocGiaSinhVien(List<string> list_id_sinhvien)
        {
            try
            {
                foreach (string id_sinhvien in list_id_sinhvien)
                {
                    xoaDocGiaSinhVien(id_sinhvien);
                }
                return "Xoá thành công!";
            }
            catch (Exception ex)
            {
                return "Xuất hiện lỗi!";
            }
        }
        public void xoaDocGiaSinhVien(string id_sinhvien)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            SINHVIEN sinhvien = entities.SINHVIENs.Where(p => p.MADOCGIA == id_sinhvien).FirstOrDefault();
            entities.SINHVIENs.Remove(sinhvien); ;
            entities.SaveChanges();

            DOCGIA docgia = entities.DOCGIAs.Where(p => p.MADOCGIA == id_sinhvien).FirstOrDefault();
            entities.DOCGIAs.Remove(docgia);
            entities.SaveChanges();
        }

        public DOCGIA timDocGiaTheoMaDocGia(string madocgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.DOCGIAs.Where(p => p.MADOCGIA == madocgia).FirstOrDefault();
        }

        public SINHVIEN timSinhVienTheoMaDocGia(string madocgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.SINHVIENs.Where(p => p.MADOCGIA == madocgia).FirstOrDefault();
        }
        public GIANGVIEN timGiangVienTheoMaDocGia(string madocgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.GIANGVIENs.Where(p => p.MADOCGIA == madocgia).FirstOrDefault();
        }

        public string suaDocGia(string madocgia, string hoten, string diachi, bool sinhvien,
            string mssv, string malopsh, string hocvi, string makhoa)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            DOCGIA docgia = entities.DOCGIAs.Where(p => p.MADOCGIA == madocgia).FirstOrDefault();
            SINHVIEN sv = entities.SINHVIENs.Where((p) => p.MADOCGIA == madocgia).FirstOrDefault();
            GIANGVIEN gv = entities.GIANGVIENs.Where((p) => p.MADOCGIA == madocgia).FirstOrDefault();
            try
            {
                docgia.HOTEN = hoten;
                docgia.DIACHI = diachi;

                if (sinhvien)
                {
                    sv.MASINHVIEN = mssv;
                    sv.MALOPSH = malopsh;
                }
                else
                {
                    gv.MAKHOA = makhoa;
                    gv.HOCVI = hocvi;
                }
                entities.SaveChanges();
                return "Chỉnh sửa thành công!";
            }
            catch (Exception ex)
            {
                return "Thông tin chỉnh sửa không hợp lệ, Vui lòng kiểm tra lại!";
            }
        }
        public List<DOCGIA_VIEW_SINHVIEN> sapXepSinhVien(List<DOCGIA_VIEW_SINHVIEN> list, int value)
        {
            List<DOCGIA_VIEW_SINHVIEN> list_new = list.OrderBy(o => o.MADOCGIA).ToList();
            if (value == 2)
            {
                list_new = list.OrderBy(o => o.HOTEN).ToList();
            }
            else if (value == 3)
            {
                list_new = list.OrderBy(o => o.SOSACHMUON).ToList();
            }
            else if (value == 4)
            {
                list_new = list.OrderBy(o => o.MASINHVIEN).ToList();
            }
            return list_new;
        }
        public List<DOCGIA_VIEW_GIANGVIEN> sapXepGiangVien(List<DOCGIA_VIEW_GIANGVIEN> list, int value)
        {
            List<DOCGIA_VIEW_GIANGVIEN> list_new = list.OrderBy(o => o.MADOCGIA).ToList();
            if (value == 2)
            {
                list_new = list.OrderBy(o => o.HOTEN).ToList();
            }
            else if (value == 3)
            {
                list_new = list.OrderBy(o => o.SOSACHMUON).ToList();
            }
            return list_new;
        }
        public bool kiemtraSVhayGV(DOCGIA d)//true - la SV
        {
            bool kt = false;
            List<DOCGIA_VIEW_SINHVIEN> list = getAllDocGiaSinhVien();
            foreach (DOCGIA_VIEW_SINHVIEN dg in list)
            {
                if (d.MADOCGIA == dg.MADOCGIA) return true;
            }
            return kt;
        }
        public List<string> getThongtinSinhVien(DOCGIA docgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<string> list = new List<string>();
            string malsh = entities.SINHVIENs.Where(p => p.MADOCGIA == docgia.MADOCGIA).FirstOrDefault().MALOPSH;
            string masinhvien = entities.SINHVIENs.Where(p => p.MADOCGIA == docgia.MADOCGIA).FirstOrDefault().MASINHVIEN;
            string tenlopsh = entities.LOPSHes.Where(p => p.MALOPSH == malsh).FirstOrDefault().TENLOPSH;
            list.Add(docgia.HOTEN);
            list.Add(docgia.MADOCGIA);
            list.Add(docgia.DIACHI);
            list.Add(docgia.SOSACHMUON.ToString());
            list.Add(masinhvien);
            list.Add(tenlopsh);
            return list;
        }
        public List<string> getThongtinGiangVien(DOCGIA docgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<string> list = new List<string>();
            string makhoa = entities.GIANGVIENs.Where(p => p.MADOCGIA == docgia.MADOCGIA).FirstOrDefault().MAKHOA;
            string tenkhoa = entities.KHOAs.Where(p => p.MAKHOA == makhoa).FirstOrDefault().TENKHOA;
            string hocvi = entities.GIANGVIENs.Where(p => p.MADOCGIA == docgia.MADOCGIA).FirstOrDefault().HOCVI;
            list.Add(docgia.HOTEN);
            list.Add(docgia.MADOCGIA);
            list.Add(docgia.DIACHI);
            list.Add(docgia.SOSACHMUON.ToString());
            list.Add(tenkhoa);
            list.Add(hocvi);
            return list;
        }
        public List<DOCGIA> getDocGia()
        {

            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.DOCGIAs.ToList();
        }
        public List<DOCGIA_VIEW_SINHVIEN> getChiTietSinhVien(string MaDocGia)
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var l1 = from dg in db.DOCGIAs
                     join sv in db.SINHVIENs
                     on dg.MADOCGIA equals sv.MADOCGIA
                     join lsh in db.LOPSHes
                     on sv.MALOPSH equals lsh.MALOPSH
                     where dg.MADOCGIA == MaDocGia
                     select new DOCGIA_VIEW_SINHVIEN
                     {
                         MADOCGIA = dg.MADOCGIA,
                         HOTEN = dg.HOTEN,
                         DIACHI = dg.DIACHI,
                         LOPSH = lsh.TENLOPSH,
                         MASINHVIEN = sv.MASINHVIEN,
                         SOSACHMUON = dg.SOSACHMUON,
                     };
            return l1.ToList();
        }
        public List<DOCGIA_VIEW_GIANGVIEN> getChiTietGiangVien(string MaDocGia)
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var l1 = from dg in db.DOCGIAs
                     join gv in db.GIANGVIENs
                     on dg.MADOCGIA equals gv.MADOCGIA
                     join khoa in db.KHOAs
                     on gv.MAKHOA equals khoa.MAKHOA
                     where dg.MADOCGIA == MaDocGia
                     select new DOCGIA_VIEW_GIANGVIEN
                     {
                         MADOCGIA = dg.MADOCGIA,
                         HOTEN = dg.HOTEN,
                         DIACHI = dg.DIACHI,
                         KHOA = khoa.TENKHOA,
                         HOCVI = gv.HOCVI,
                         SOSACHMUON = dg.SOSACHMUON
                     };
            return l1.ToList();
        }
        public List<DOCGIA> getDocGiaMuonNhieu()
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            List<DOCGIA> l1 = (from dg in db.DOCGIAs
                            where dg.SOSACHMUON >= 10
                            select dg).ToList();
            return l1;
        }
        public int TinhNgayTre(DateTime NgayMuon)
        {
            TimeSpan Time = DateTime.Now - NgayMuon;
            return Time.Days;
        }
        public List<DOCGIA_VIPHAM> getDocGiaVipham()
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var l1 = from dg in db.DOCGIAs
                     join pm in db.PHIEUMUONs
                     on dg.MADOCGIA equals pm.MADOCGIA
                     join ctpm in db.CHITIETPHIEUMUONs
                     on pm.MAPHIEU equals ctpm.MAPHIEU
                     select new DOCGIA_VIPHAM
                     {
                         Ma_Doc_Gia = dg.MADOCGIA,
                         Ho_Ten = dg.HOTEN,
                         Dia_Chi = dg.DIACHI,
                         Ma_Phieu = pm.MAPHIEU,
                         Ma_Sach = ctpm.MASACH,
                         Ngay_Muon = pm.NGAYMUON,
                         Ngay_Tra = pm.NGAYTRA,
                         Noi_Dung_Vi_Pham = ""
                     };
            List<DOCGIA_VIPHAM> data = l1.ToList();
            List<DOCGIA_VIPHAM> dataResult = new List<DOCGIA_VIPHAM>();
            foreach (DOCGIA_VIPHAM d in data) 
            {
                if (DateTime.Compare(d.Ngay_Tra, DateTime.Now) < 0 && TinhNgayTre(d.Ngay_Tra) != 0)
                {
                    d.Noi_Dung_Vi_Pham = "Qua han " + TinhNgayTre(d.Ngay_Tra).ToString() + " ngay";
                    dataResult.Add(d);
                }
            }
            return dataResult;
        }
    }
}
