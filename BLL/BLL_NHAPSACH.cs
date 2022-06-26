using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;
using QuanLyThuVien.VIEW;

namespace QuanLyThuVien.BLL
{
    public class BLL_NHAPSACH
    {
        private static BLL_NHAPSACH _Instance;
        private BLL_NHAPSACH()
        {

        }

        public static BLL_NHAPSACH Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_NHAPSACH();
                }
                return _Instance;
            }
            private set { }
        }
        public List<NHAPSACH_CHITIET> getAllChiTietNhapSach()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<NHAPSACH_CHITIET> list = (from nhapsach in entities.NHAPSACHes
                                           join chitiet in entities.CHITIETNHAPSACHes
                                           on nhapsach.MANHAPSACH equals chitiet.MANHAPSACH
                                           select new NHAPSACH_CHITIET()
                                           {
                                               MANHAPSACH = chitiet.MANHAPSACH,
                                               MANGUOIDUNG = nhapsach.MANGUOIDUNG,
                                               NGAYNHAP = nhapsach.NGAYNHAP,
                                               MASACH = chitiet.MASACH,
                                               SOLUONG = chitiet.SOLUONG
                                           }).ToList();
            return list;
        }

        public List<NHAPSACH_CHITIET> timChiTietNhapSach(string manhapsach, string manguoidung, string masach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<NHAPSACH_CHITIET> list = (from nhapsach in entities.NHAPSACHes
                                           join chitiet in entities.CHITIETNHAPSACHes
                                           on nhapsach.MANHAPSACH equals chitiet.MANHAPSACH
                                           where nhapsach.MANGUOIDUNG.Contains(manguoidung)
                                           where nhapsach.MANHAPSACH.Contains(manhapsach)
                                           where chitiet.MASACH.Contains(masach)
                                           select new NHAPSACH_CHITIET()
                                           {
                                               MANHAPSACH = chitiet.MANHAPSACH,
                                               MANGUOIDUNG = nhapsach.MANGUOIDUNG,
                                               NGAYNHAP = nhapsach.NGAYNHAP,
                                               MASACH = chitiet.MASACH,
                                               SOLUONG = chitiet.SOLUONG
                                           }).ToList();
            return list;
        }

        public List<string> getAllMaNhapSach()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<string> list = entities.NHAPSACHes.Select(p => p.MANHAPSACH).ToList();
            return list;
        }

        public List<NHAPSACH_VIEW_SOLUONG> themSoLuongVaoDGV(string masach, int soluong, List<NHAPSACH_VIEW_SOLUONG> list)
        {
            if (soluong == 0)
            {
                return list;
            }
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            SACH sach = BLL_SACH.Instance.findSachByMasach(masach);
            if (sach == null)
            {
                Nhapsach_Them.showMessageBoxAddNewSach();
            }
            else
            {
                if (list == null)
                {
                    list = new List<NHAPSACH_VIEW_SOLUONG>();
                }
                bool inDGV = false;
                foreach (NHAPSACH_VIEW_SOLUONG v in list)
                {
                    if (v.MASACH == masach)
                    {
                        inDGV = true;
                        break;
                    }
                }
                if (!inDGV)
                {
                    NHAPSACH_VIEW_SOLUONG new_soluong = new NHAPSACH_VIEW_SOLUONG()
                    {
                        MASACH = masach,
                        SOLUONG = soluong,
                    };
                    list.Add(new_soluong);
                }
                else
                {
                    Nhapsach_Them.showMessageBox("Mã sách đã tồn tại vui lòng kiểm tra lại!");
                }
            }
            return list;
        }
        public List<NHAPSACH_VIEW_SOLUONG> xoaDongDGV(string masach, List<NHAPSACH_VIEW_SOLUONG> list)
        {
            foreach (NHAPSACH_VIEW_SOLUONG v in list)
            {
                if (v.MASACH == masach)
                {
                    list.Remove(v);
                    break;
                }
            }
            if (list.Count == 0) list = null;
            return list;
        }

        public void themDongNhapSach(string manhapsach, DateTime ngaynhap, string manguoidung)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NHAPSACH nhapsach = new NHAPSACH()
            {
                MANHAPSACH = manhapsach,
                NGAYNHAP = ngaynhap,
                MANGUOIDUNG = manguoidung
            };
            entities.NHAPSACHes.Add(nhapsach);
            entities.SaveChanges();
        }

        public void themDongChiTietNhapSach(string manhapsach, List<NHAPSACH_VIEW_SOLUONG> list)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            foreach (NHAPSACH_VIEW_SOLUONG v in list)
            {
                CHITIETNHAPSACH chitietnhapsach = new CHITIETNHAPSACH()
                {
                    MANHAPSACH = manhapsach,
                    MASACH = v.MASACH,
                    SOLUONG = v.SOLUONG,
                };
                entities.CHITIETNHAPSACHes.Add(chitietnhapsach);
                SACH sach = entities.SACHes.Where(p => p.MASACH == v.MASACH).FirstOrDefault();
                sach.SLTONKHO += v.SOLUONG;
            }
            entities.SaveChanges();
        }

        public void themNhapSach(string manhapsach, DateTime ngaynhap, string manguoidung, List<NHAPSACH_VIEW_SOLUONG> list)
        {
            themDongNhapSach(manhapsach, ngaynhap, manguoidung);
            themDongChiTietNhapSach(manhapsach, list);
        }

        public List<NHAPSACH_VIEW> getAllNhapSach()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<NHAPSACH_VIEW> list = entities.NHAPSACHes.Select(p => new NHAPSACH_VIEW()
            {
                MANHAPSACH = p.MANHAPSACH,
                MANGUOIDUNG = p.MANGUOIDUNG,
                NGAYNHAP = p.NGAYNHAP,
            }).ToList();
            return list;
        }

        public NHAPSACH timNhapSachTheoMaNhapSach(string manhapsach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.NHAPSACHes.Where(p => p.MANHAPSACH == manhapsach).FirstOrDefault();
        }

        public List<NHAPSACH_VIEW_SOLUONG> timChiTietNhapSachSoLuongTheoMaNhapSach(string manhapsach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<NHAPSACH_VIEW_SOLUONG> list = entities.CHITIETNHAPSACHes.Where((p) => p.MANHAPSACH == manhapsach).Select(p => new NHAPSACH_VIEW_SOLUONG()
            {
                MASACH = p.MASACH,
                SOLUONG = p.SOLUONG,
            }).ToList();
            return list;
        }

        public void xoaDongChiTietNhapSach(string manhapsach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<CHITIETNHAPSACH> list = entities.CHITIETNHAPSACHes.Where(p => p.MANHAPSACH == manhapsach).ToList();
            foreach (CHITIETNHAPSACH ch in list)
            {
                entities.CHITIETNHAPSACHes.Remove(ch);

                SACH sach = entities.SACHes.Where(p => p.MASACH == ch.MASACH).FirstOrDefault();
                sach.SLTONKHO -= ch.SOLUONG;
            }
            entities.SaveChanges();
        }

        public void suaChiTietNhapSach(string manhapsach, List<NHAPSACH_VIEW_SOLUONG> list)
        {
            xoaDongChiTietNhapSach(manhapsach);
            themDongChiTietNhapSach(manhapsach, list);
        }

        public void xoaNhapSach(string manhapsach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NHAPSACH nhapsach = entities.NHAPSACHes.Where(p => p.MANHAPSACH == manhapsach).FirstOrDefault();
            if (nhapsach != null)
            {
                entities.NHAPSACHes.Remove(nhapsach);
            }
            entities.SaveChanges();
        }
        public List<NHAPSACH_CHITIET> sapXepNhapSach(List<NHAPSACH_CHITIET> list, int value)
        {
            List<NHAPSACH_CHITIET> list_new = list.OrderBy(o => o.MANHAPSACH).ToList();
            if (value == 2)
            {
                list_new = list.OrderBy(o => o.MANGUOIDUNG).ToList();
            }
            else if (value == 3)
            {
                list_new = list.OrderBy(o => o.NGAYNHAP).ToList();
            }
            else if (value == 4)
            {
                list_new = list.OrderBy(o => o.MASACH).ToList();
            }
            else if (value == 5)
            {
                list_new = list.OrderBy(o => o.SOLUONG).ToList();
            }
            return list_new;
        }
    }
}
