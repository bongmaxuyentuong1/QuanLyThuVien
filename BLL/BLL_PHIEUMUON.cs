using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.VIEW;
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;
using System.Windows.Forms;
namespace QuanLyThuVien.BLL
{
    public class BLL_PHIEUMUON
    {
        private static BLL_PHIEUMUON _Instance;
        private BLL_PHIEUMUON()
        {

        }

        public static BLL_PHIEUMUON Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_PHIEUMUON();
                }
                return _Instance;
            }
            private set { }
        }
        public List<PM_CHITIET> getAllChiTietPhieuMuon()
        {
            List<PM_CHITIET> list_chitiet = new List<PM_CHITIET>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            list_chitiet = (from phieumuon in entities.PHIEUMUONs
                            join chitiet in entities.CHITIETPHIEUMUONs
                            on phieumuon.MAPHIEU equals chitiet.MAPHIEU
                            select new PM_CHITIET()
                            {
                                MAPHIEUMUON = phieumuon.MAPHIEU,
                                MADOCGIA = phieumuon.MADOCGIA,
                                NGAYMUON = phieumuon.NGAYMUON,
                                NGAYTRA = phieumuon.NGAYTRA,
                                MANGUOIDUNG = phieumuon.MANGUOIDUNG,
                                MASACH = chitiet.MASACH,
                                SOLUONG = chitiet.SOLUONG
                            }).ToList();
            return list_chitiet;
        }

        public List<string> getAllMaPhieuMuon()
        {
            List<string> list = new List<string>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            list = entities.PHIEUMUONs.Select(p => p.MAPHIEU).ToList();
            return list;
        }

        public bool kiemTraMaDocGia(string madocgia)
        {
            bool res = true;
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            DOCGIA docgia = entities.DOCGIAs.Where(p => p.MADOCGIA == madocgia).FirstOrDefault();
            //MessageBox.Show(docgia.SOSACHMUON.ToString());
            if (docgia == null || docgia.SOSACHMUON > 20)
            {
                res = false;
            }
            return res;
        }

        public bool kiemTraNgayMuonNgayTra(DateTime ngaymuon, DateTime ngaytra)
        {
            int res = DateTime.Compare(ngaytra, ngaymuon);
            if (res <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<PM_VIEW_SOLUONG> themVaoDGVSoLuong(string madocgia, string masach, int soluong, List<PM_VIEW_SOLUONG> list)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            SACH sach = entities.SACHes.Where(p => p.MASACH == masach).FirstOrDefault();
            if (list == null)
            {
                list = new List<PM_VIEW_SOLUONG>();
            }
            if (sach == null)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Ma sach khong ton tai");
                return list;
            }
            foreach (PM_VIEW_SOLUONG v in list)
            {
                if (v.MASACH == masach)
                {
                    CN_Thongbao f = new CN_Thongbao();
                    f.setNotice("Ma sach da ton tai trong bang");
                    return list;
                }
            }
            list.Add(new PM_VIEW_SOLUONG()
            {
                MASACH = masach,
                SOLUONG = soluong,
            });
            return list;
        }
        public List<PM_VIEW_SOLUONG> xoaDongDGVSoLuong(List<PM_VIEW_SOLUONG> list, string masach)
        { // xoa trong dgv => ko xoa trong du lieu
            foreach (PM_VIEW_SOLUONG v in list)
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
        public void themChiTietPhieuMuon(string maphieumuon, string madocgia, DateTime ngaymuon, DateTime ngaytra,
            string manguoidung, List<PM_VIEW_SOLUONG> list)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            // Them vao chitietphieumuon
            foreach (PM_VIEW_SOLUONG v in list)
            {
                CHITIETPHIEUMUON chitiet = new CHITIETPHIEUMUON()
                {
                    MAPHIEU = maphieumuon,
                    MASACH = v.MASACH,
                    SOLUONG = v.SOLUONG
                };
                entities.CHITIETPHIEUMUONs.Add(chitiet);
            }
            // Cap nhat sach
            foreach (PM_VIEW_SOLUONG v in list)
            {
                SACH sach = entities.SACHes.Where(p => p.MASACH == v.MASACH).FirstOrDefault();
                sach.SLHIENTAI -= v.SOLUONG;
                sach.SLDANGMUON += v.SOLUONG;
                entities.SaveChanges();
            }
            // cap nhat doc gia
            int tongsoluong = 0;
            DOCGIA docgia = entities.DOCGIAs.Where(p => p.MADOCGIA == madocgia).FirstOrDefault();
            foreach (PM_VIEW_SOLUONG v in list)
            {
                tongsoluong += v.SOLUONG;
            }
            docgia.SOSACHMUON += tongsoluong;
            entities.SaveChanges();
        }
        public string themPhieuMuon(string maphieumuon, string madocgia, DateTime ngaymuon, DateTime ngaytra,
            string manguoidung, List<PM_VIEW_SOLUONG> list)
        {
            string res = "Thêm thành công!";
            int slInDGV = 0;
            foreach (PM_VIEW_SOLUONG v in list)
            {
                slInDGV += v.SOLUONG;
            }
            if (slInDGV + BLL_DOCGIA.Instance.timDocGiaTheoMaDocGia(madocgia).SOSACHMUON > 20)
            {
                return "Thêm thất bại! Kiểm tra số lượng!";
            }
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            // Them vao phieu muon
            PHIEUMUON phieumuon = new PHIEUMUON()
            {
                MAPHIEU = maphieumuon,
                MADOCGIA = madocgia,
                MANGUOIDUNG = manguoidung,
                NGAYMUON = ngaymuon,
                NGAYTRA = ngaytra,
            };
            entities.PHIEUMUONs.Add(phieumuon);
            entities.SaveChanges();
            themChiTietPhieuMuon(maphieumuon, madocgia, ngaymuon, ngaytra, manguoidung, list);
            entities.SaveChanges();
            return res;
        }

        public List<PM_VIEW> getAllPhieuMuon()
        {
            List<PM_VIEW> list = new List<PM_VIEW>();
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            list = (entities.PHIEUMUONs.Select(p => new PM_VIEW()
            {
                MAPHIEUMUON = p.MAPHIEU,
                MADOCGIA = p.MADOCGIA,
                MANGUOIDUNG = p.MANGUOIDUNG,
                NGAYMUON = p.NGAYMUON,
                NGAYTRA = p.NGAYTRA,
            })).ToList();
            return list;
        }

        public PHIEUMUON timPhieuMuonTheoMaPhieu(string maphieumuon)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            PHIEUMUON phieumuon = entities.PHIEUMUONs.Where(p => p.MAPHIEU == maphieumuon).FirstOrDefault();
            return phieumuon;
        }
        public List<PM_VIEW_SOLUONG> getAllPMSoLuongTheoMaPhieu(string maphieumuon)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<PM_VIEW_SOLUONG> list_soluong = (entities.CHITIETPHIEUMUONs.
                Where(p => p.MAPHIEU == maphieumuon).
                Select(p => new PM_VIEW_SOLUONG()
                {
                    MASACH = p.MASACH,
                    SOLUONG = p.SOLUONG,
                })).ToList();
            return list_soluong;
        }

        public void suaPhieuMuon(string maphieumuon, string madocgia, DateTime ngaymuon, DateTime ngaytra,
            string manguoidung, List<PM_VIEW_SOLUONG> list)
        {
            int slInDGV = 0;
            foreach (PM_VIEW_SOLUONG v in list)
            {
                slInDGV += v.SOLUONG;
            }
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<CHITIETPHIEUMUON> list_chitietphieumuon = entities.CHITIETPHIEUMUONs.Where((p) => p.MAPHIEU == maphieumuon).ToList();
            foreach (CHITIETPHIEUMUON chitietphieumuon in list_chitietphieumuon)
            {
                slInDGV -= chitietphieumuon.SOLUONG;
            }
            if (slInDGV + BLL_DOCGIA.Instance.timDocGiaTheoMaDocGia(madocgia).SOSACHMUON > 20)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Thêm thất bại! Kiểm tra số lượng!");
                f.Show();
                return;
            }
            PHIEUMUON phieumuon = entities.PHIEUMUONs.Where(p => p.MAPHIEU == maphieumuon).FirstOrDefault();
            phieumuon.MADOCGIA = madocgia;
            phieumuon.NGAYTRA = ngaytra;
            entities.SaveChanges();
            xoaAllChiTietPhieuMuonTheoMaPhieuMuon(maphieumuon, madocgia);
            themChiTietPhieuMuon(maphieumuon, madocgia, ngaymuon, ngaytra, manguoidung, list);
        }

        public void xoaAllChiTietPhieuMuonTheoMaPhieuMuon(string maphieumuon, string madocgia)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<CHITIETPHIEUMUON> list_chitietphieumuon = entities.CHITIETPHIEUMUONs.
                Where((p) => p.MAPHIEU == maphieumuon).
                ToList();
            foreach (CHITIETPHIEUMUON chitietphieumuon in list_chitietphieumuon)
            {
                // xoa record trong chitietphieumuon
                entities.CHITIETPHIEUMUONs.Remove(chitietphieumuon);
                // cap nhat so luong sach
                SACH sach = entities.SACHes.Where(p => p.MASACH == chitietphieumuon.MASACH).FirstOrDefault();
                sach.SLHIENTAI += chitietphieumuon.SOLUONG;
                sach.SLDANGMUON -= chitietphieumuon.SOLUONG;
                // cap nhat so luong sach cua doc gia
                DOCGIA docgia = entities.DOCGIAs.Where(p => p.MADOCGIA == madocgia).FirstOrDefault();
                docgia.SOSACHMUON -= chitietphieumuon.SOLUONG;
            }
            entities.SaveChanges();
        }
        public void xoaPhieuMuon(string maphieumuon)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            PHIEUMUON phieumuon = entities.PHIEUMUONs.Where(p => p.MAPHIEU == maphieumuon).FirstOrDefault();
            entities.PHIEUMUONs.Remove(phieumuon);
            entities.SaveChanges();
        }

        public List<PM_CHITIET> timChiTietPhieumuon(string maphieumuon, string madocgia, string manguoidung, string masach)
        {
            if (maphieumuon == null)
            {
                maphieumuon = "";
            }
            if (madocgia == null)
            {
                madocgia = "";
            }
            if (manguoidung == null)
            {
                manguoidung = "";
            }
            if (masach == null)
            {
                masach = "";
            }
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<PM_CHITIET> list_pmchitiet = new List<PM_CHITIET>();
            list_pmchitiet = (from phieumuon in entities.PHIEUMUONs
                              join chitiet in entities.CHITIETPHIEUMUONs
                              on phieumuon.MAPHIEU equals chitiet.MAPHIEU
                              where phieumuon.MAPHIEU.Contains(maphieumuon)
                              where phieumuon.MADOCGIA.Contains(madocgia)
                              where phieumuon.MANGUOIDUNG.Contains(manguoidung)
                              where chitiet.MASACH.Contains(masach)
                              select new PM_CHITIET()
                              {
                                  MAPHIEUMUON = phieumuon.MAPHIEU,
                                  MANGUOIDUNG = phieumuon.MANGUOIDUNG,
                                  MADOCGIA = phieumuon.MADOCGIA,
                                  MASACH = chitiet.MASACH,
                                  SOLUONG = chitiet.SOLUONG,
                                  NGAYMUON = phieumuon.NGAYMUON,
                                  NGAYTRA = phieumuon.NGAYTRA
                              }).ToList();
            return list_pmchitiet;
        }
        public List<PM_CHITIET> sapXepPhieuMuon(List<PM_CHITIET> list, int value)
        {
            List<PM_CHITIET> list_new = list.OrderBy(o => o.MAPHIEUMUON).ToList();
            if (value == 2)
            {
                list_new = list.OrderBy(o => o.MANGUOIDUNG).ToList();
            }
            else if (value == 3)
            {
                list_new = list.OrderBy(o => o.MADOCGIA).ToList();
            }
            else if (value == 4)
            {
                list_new = list.OrderBy(o => o.NGAYMUON).ToList();
            }
            else if (value == 5)
            {
                list_new = list.OrderBy(o => o.NGAYTRA).ToList();
            }
            else if (value == 6)
            {
                list_new = list.OrderBy(o => o.MASACH).ToList();
            }
            else if (value == 7)
            {
                list_new = list.OrderBy(o => o.SOLUONG).ToList();
            }
            return list_new;
        }
    }
}
