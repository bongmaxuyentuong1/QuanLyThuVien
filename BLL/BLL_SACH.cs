using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;
namespace QuanLyThuVien.BLL
{
    internal class BLL_SACH
    {
        private static BLL_SACH _Instance;
        private BLL_SACH()
        {

        }

        public static BLL_SACH Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_SACH();
                }
                return _Instance;
            }
            private set { }
        }
        public List<string> getAllMaTheLoai()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.THELOAIs.Select(p => p.MATHELOAI).ToList();
        }
        public List<string> getAllMaNgonNgu()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.NGONNGUs.Select(p => p.MANGONNGU).ToList();
        }
        public List<string> getAllMaNXB()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.NXBs.Select(p => p.MANXB).ToList();
        }
        public List<string> getAllTheloai()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.THELOAIs.Select(p => p.TENTHELOAI).ToList();
        }
        public List<string> getAllNgonNgu()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.NGONNGUs.Select(p => p.TENNGONNGU).ToList();
        }
        public List<string> getAllNXB()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.NXBs.Select(p => p.TENNXB).ToList();
        }
        public void themNgonNgu(string mangonngu, string tenngonngu)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NGONNGU ngonngumoi = new NGONNGU()
            {
                MANGONNGU = mangonngu,
                TENNGONNGU = tenngonngu
            };
            entities.NGONNGUs.Add(ngonngumoi);
            entities.SaveChanges();
        }
        public void themNXB(string manxb, string tennxb)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NXB nxb = new NXB()
            {
                MANXB = manxb,
                TENNXB = tennxb
            };
            entities.NXBs.Add(nxb);
            entities.SaveChanges();
        }
        public void themTheLoai(string matheloai, string tentheloai)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            THELOAI theloai = new THELOAI()
            {
                MATHELOAI = matheloai,
                TENTHELOAI = tentheloai
            };
            entities.THELOAIs.Add(theloai);
            entities.SaveChanges();
        }
        public SACH findSachByMasach(string masach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            SACH sach = entities.SACHes.Where(p => p.MASACH == masach).FirstOrDefault();
            return sach;
        }
        public SOLUONG_VIEW getSLSACH(SACH sach)
        {
            QuanLyThuVienEntities entity = new QuanLyThuVienEntities();
            SOLUONG_VIEW x = new SOLUONG_VIEW()
            {
                TONGSL = (int)sach.TONGSL,
                SLHIENTAI = (int)sach.SLHIENTAI,
                SLDANGMUON = (int)sach.SLDANGMUON,
                SLTONKHO = (int)sach.SLTONKHO,
            };
            return x;
        }
        public List<string> getInformationSach(SACH sach)
        {
            List<string> list = new List<string>();
            QuanLyThuVienEntities entity = new QuanLyThuVienEntities();

            string nhande = sach.NHANDE.ToString();
            list.Add(nhande);

            string tacgia = sach.TACGIA.ToString();
            list.Add(tacgia);

            string xuatban = sach.NAMXB.Year.ToString() + "," + entity.NXBs.Where(p => p.MANXB == sach.MANXB).FirstOrDefault().TENNXB;
            list.Add(xuatban);

            string theloai = entity.THELOAIs.Where(p => p.MATHELOAI == sach.MATHELOAI).FirstOrDefault().TENTHELOAI;
            list.Add(theloai);

            string ngongu = entity.NGONNGUs.Where(p => p.MANGONNGU == sach.MANGONNGU).FirstOrDefault().TENNGONNGU;
            list.Add(ngongu);

            return list;
        }

        public List<SACH_VIEW> getAllSach()
        {
            List<SACH_VIEW> list = new List<SACH_VIEW>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            list = entities.SACHes.Select(p => new SACH_VIEW()
            {
                MASACH = p.MASACH,
                NHANDE = p.NHANDE,
                TACGIA = p.TACGIA,
                NAMXB = p.NAMXB.Year,
                THELOAI = p.THELOAI.TENTHELOAI,
                NGONNGU = p.NGONNGU.TENNGONNGU,
                NXB = p.NXB.TENNXB,
                TONGSL = (int)p.TONGSL,
                SLDANGMUON = (int)p.SLDANGMUON,
                SLHIENTAI = (int)p.SLHIENTAI,
                SLTONKHO = (int)p.SLTONKHO

            }).ToList();
            return list;
        }

        public List<string> getAllMaSach()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<string> list_masach = new List<string>();
            list_masach = entities.SACHes.Select(p => p.MASACH).ToList();
            return list_masach;
        }

        public void themSach(string masach, string nhande, string matheloai, string manxb, DateTime namxuatban, string mangonngu, string tacgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            SACH sachmoi = new SACH()
            {
                MASACH = masach,
                NHANDE = nhande,
                MATHELOAI = matheloai,
                MANXB = manxb,
                NAMXB = namxuatban,
                MANGONNGU = mangonngu,
                TACGIA = tacgia,
                TONGSL = 0,
                SLDANGMUON = 0,
                SLHIENTAI = 0,
                SLTONKHO = 0
            };
            entities.SACHes.Add(sachmoi);
            entities.SaveChanges();
        }

        public void xoaSach(List<string> list_masach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            foreach (string masach in list_masach)
            {
                SACH sach = entities.SACHes.Where(p => p.MASACH == masach).FirstOrDefault();
                entities.SACHes.Remove(sach);
            }
            entities.SaveChanges();
        }

        public List<SACH_VIEW> sapXepSach(List<SACH_VIEW> list, int value)
        {
            List<SACH_VIEW> list_new = list.OrderBy(o => o.MASACH).ToList();
            if (value == 2)
            {
                list_new = list.OrderBy(o => o.NHANDE).ToList();
            }
            else if (value == 3)
            {
                list_new = list.OrderBy(o => o.TACGIA).ToList();
            }
            else if (value == 4)
            {
                list_new = list.OrderBy(o => o.NAMXB).ToList();
            }
            else if (value == 5)
            {
                list_new = list.OrderBy(o => o.TONGSL).ToList();
            }
            return list_new;
        }

        public List<SACH_VIEW> timSach(string masach, string nhande, string tacgia, int namxuatban, string nhaxuatban, string ngonngu, string theloai)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<SACH_VIEW> list_new = new List<SACH_VIEW>();
            list_new = entities.SACHes.
                Where(p => p.MASACH.Contains(masach)).
                Where(p => p.NHANDE.Contains(nhande)).
                Where(p => p.TACGIA.Contains(tacgia)).
                Where(p => p.NAMXB.Year == namxuatban || namxuatban == 0).
                Where(p => p.NXB.TENNXB == nhaxuatban || nhaxuatban == "Tất cả").
                Where(p => p.NGONNGU.TENNGONNGU == ngonngu || ngonngu == "Tất cả").
                Where(p => p.THELOAI.TENTHELOAI == theloai || theloai == "Tất cả").
                Select(p => new SACH_VIEW()
                {
                    MASACH = p.MASACH,
                    NHANDE = p.NHANDE,
                    TACGIA = p.TACGIA,
                    NAMXB = p.NAMXB.Year,
                    THELOAI = p.THELOAI.TENTHELOAI,
                    NGONNGU = p.NGONNGU.TENNGONNGU,
                    NXB = p.NXB.TENNXB,
                    TONGSL = (int)p.TONGSL,
                    SLDANGMUON = (int)p.SLDANGMUON,
                    SLHIENTAI = (int)p.SLHIENTAI,
                    SLTONKHO = (int)p.SLTONKHO
                }).ToList();
            return list_new;
        }

        public void suaSach(string masach, string nhande, string matheloai, string manxb, DateTime namxuatban, string mangonngu, string tacgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            SACH sach = entities.SACHes.Where(p => p.MASACH == masach).FirstOrDefault();
            if (sach != null)
            {
                sach.NHANDE = nhande;
                sach.MATHELOAI = matheloai;
                sach.MANXB = manxb;
                sach.NAMXB = namxuatban;
                sach.MANGONNGU = mangonngu;
                sach.TACGIA = tacgia;
            }
            entities.SaveChanges();
        }

        public List<CBB_ITEM> GetCBOTL()
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var l1 = from p in db.THELOAIs
                     select new CBB_ITEM
                     {
                         VALUE = Convert.ToInt32(p.MATHELOAI),
                         TEXT = p.TENTHELOAI
                     };
            return l1.ToList();
        }
        public List<CBB_ITEM> GetCBBNN()
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var l1 = from p in db.NGONNGUs
                     select new CBB_ITEM
                     {
                         VALUE = Convert.ToInt32(p.MANGONNGU),
                         TEXT = p.TENNGONNGU
                     };
            return l1.ToList();
        }
        public List<CBB_ITEM> GetCBONXB()
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var l1 = from p in db.NXBs
                     select new CBB_ITEM
                     {
                         VALUE = Convert.ToInt32(p.MANXB),
                         TEXT = p.TENNXB
                     };
            return l1.ToList();
        }
        public List<SACH_VIEW> getSach()
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var l1 = from Sach in db.SACHes
                     join tl in db.THELOAIs
                     on Sach.MATHELOAI equals tl.MATHELOAI
                     join nxb in db.NXBs
                     on Sach.MANXB equals nxb.MANXB
                     join nn in db.NGONNGUs
                     on Sach.MANGONNGU equals nn.MANGONNGU
                     select new SACH_VIEW
                     {
                         MASACH = Sach.MASACH,
                         NHANDE = Sach.NHANDE,
                         THELOAI = tl.TENTHELOAI,
                         TACGIA = Sach.TACGIA,
                         NXB = nxb.TENNXB,
                         NGONNGU = nn.TENNGONNGU,
                         NAMXB = Sach.NAMXB.Year,
                         SLHIENTAI = Sach.SLHIENTAI,
                         SLDANGMUON = Sach.SLDANGMUON,
                         TONGSL = Sach.TONGSL,
                         SLTONKHO = Sach.SLTONKHO,
                     };
            return l1.ToList();
        }
        public List<SACH_VIEW> getSachHet(int a)
        {
            List<SACH_VIEW> data = new List<SACH_VIEW>();
            foreach (SACH_VIEW s in getSach())
            {
                if (s.SLHIENTAI <= a && s.SLHIENTAI > 0 || a == 0 && s.SLHIENTAI == 0)
                {
                    data.Add(s);
                }
            }
            return data;
        }
        public List<SACH_VIEW> getSachDuocMuonNhieu()
        {
            List<SACH_VIEW> data = new List<SACH_VIEW>();
            foreach (SACH_VIEW s in getSach())
            {
                if (s.SLDANGMUON >= 3)
                {
                    data.Add(s);
                }
            }
            return data;
        }
    }
}
