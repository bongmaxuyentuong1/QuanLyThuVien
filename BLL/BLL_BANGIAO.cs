using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DTO;
using QuanLyThuVien.VIEW;
using QuanLyThuVien.Entity;
namespace QuanLyThuVien.BLL
{
    internal class BLL_BANGIAO
    {
        private static BLL_BANGIAO _Instance;
        private BLL_BANGIAO()
        {

        }
        public static BLL_BANGIAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_BANGIAO();
                }
                return _Instance;
            }
            private set { }
        }

        public List<BANGIAO_VIEW_CHITIET> getAllBanGiaoChiTiet()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<BANGIAO_VIEW_CHITIET> list = (from bangiao in entities.BANGIAOs
                                               join chitiet in entities.CHITIETBANGIAOs
                                               on bangiao.MABANGIAO equals chitiet.MABANGIAO
                                               select new BANGIAO_VIEW_CHITIET()
                                               {
                                                   MABANGIAO = bangiao.MABANGIAO,
                                                   MANGUOIBANGIAO = bangiao.MANGUOIBANGIAO,
                                                   MANGUOIXACNHAN = bangiao.MANGUOIXACNHAN,
                                                   NGAYBANGIAO = (DateTime)bangiao.NGAYBANGIAO,
                                                   NGAYXACNHAN = (DateTime)(bangiao.NGAYXACNHAN == null ? default(DateTime) : bangiao.NGAYXACNHAN),
                                                   MASACH = chitiet.MASACH,
                                                   SOLUONG = chitiet.SOLUONG
                                               }).ToList();
            return list;
        }

        public List<BANGIAO_VIEW_CHITIET> sapXepBanGiao(List<BANGIAO_VIEW_CHITIET> list, int value)
        {
            List<BANGIAO_VIEW_CHITIET> new_list = list.OrderBy(o => o.MABANGIAO).ToList();
            if (value == 2)
            {
                new_list = list.OrderBy(o => o.MANGUOIBANGIAO).ToList();
            }
            else if (value == 3)
            {
                new_list = list.OrderBy(o => o.MANGUOIXACNHAN).ToList();
            }
            else if (value == 4)
            {
                new_list = list.OrderBy(o => o.NGAYBANGIAO).ToList();
            }
            else if (value == 5)
            {
                new_list = list.OrderBy(o => o.NGAYXACNHAN).ToList();
            }
            else if (value == 6)
            {
                new_list = list.OrderBy(o => o.MASACH).ToList();
            }
            return new_list;
        }

        public List<BANGIAO_VIEW_CHITIET> timBanGiaoChiTiet(string mabangiao, string manguoibangiao, string manguoixacnhan, string masach)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<BANGIAO_VIEW_CHITIET> list = (from bangiao in entities.BANGIAOs
                                               join chitiet in entities.CHITIETBANGIAOs
                                               on bangiao.MABANGIAO equals chitiet.MABANGIAO
                                               where bangiao.MABANGIAO.Contains(mabangiao)
                                               where bangiao.MANGUOIBANGIAO.Contains(manguoibangiao)
                                               where bangiao.MANGUOIXACNHAN.Contains(manguoixacnhan)
                                               where chitiet.MASACH.Contains(masach)
                                               select new BANGIAO_VIEW_CHITIET()
                                               {
                                                   MABANGIAO = bangiao.MABANGIAO,
                                                   MANGUOIBANGIAO = bangiao.MANGUOIBANGIAO,
                                                   MANGUOIXACNHAN = bangiao.MANGUOIXACNHAN,
                                                   NGAYBANGIAO = (DateTime)bangiao.NGAYBANGIAO,
                                                   NGAYXACNHAN = (DateTime)(bangiao.NGAYXACNHAN == null ?
                                                   default(DateTime) : bangiao.NGAYXACNHAN),
                                                   MASACH = chitiet.MASACH,
                                                   SOLUONG = chitiet.SOLUONG
                                               }).ToList();
            return list;
        }
        public List<string> getAllMaBanGiao()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<string> list = entities.BANGIAOs.Select(p => p.MABANGIAO).ToList();
            return list;
        }

        public string getMaBanGiaoMoi()
        {
            List<string> list_id = getAllMaBanGiao();
            return BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(list_id));
        }

        public List<BANGIAO_VIEW_SOLUONG> themDongDGVSoLuong(List<BANGIAO_VIEW_SOLUONG> list, string masach, int soluong)
        {
            if (soluong <= 0)
            {
                Bangiao_Them.showMessage("Số lượng nhập vào không phù hợp!");
            }
            else
            {
                bool inDGV = false;
                if (list == null)
                {
                    list = new List<BANGIAO_VIEW_SOLUONG>();
                }
                foreach (BANGIAO_VIEW_SOLUONG v in list)
                {
                    if (v.MASACH == masach)
                    {
                        inDGV = true;
                        break;
                    }
                }
                if (inDGV)
                {
                    Bangiao_Them.showMessage("Mã sách đã tồn tại trong đợt bàn giao này!");
                }
                else
                {
                    bool inDB = false;
                    List<SACH_VIEW> list_sach = BLL_SACH.Instance.getAllSach();
                    SACH_VIEW target = null;
                    foreach (SACH_VIEW v in list_sach)
                    {
                        if (v.MASACH == masach)
                        {
                            target = v;
                            inDB = true;
                            break;
                        }
                    }
                    if (!inDB)
                    {
                        Bangiao_Them.showMessage("Mã sách không tồn tại trong dữ liệu");
                    }
                    else
                    {
                        if (target.SLTONKHO < soluong)
                        {
                            Bangiao_Them.showMessage("Số lượng sách trong kho không đủ!");
                        }
                        else
                        {
                            list.Add(new BANGIAO_VIEW_SOLUONG()
                            {
                                MASACH = masach,
                                SOLUONG = soluong,
                            });
                        }
                    }
                }
            }
            return list;
        }

        public List<BANGIAO_VIEW_SOLUONG> xoaDongDGV(List<BANGIAO_VIEW_SOLUONG> list, string masach)
        {
            foreach (BANGIAO_VIEW_SOLUONG v in list)
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

        public void themBanGiao(string mabangiao, string manguoibangiao, DateTime ngaybangiao)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            BANGIAO bangiao = new BANGIAO()
            {
                MABANGIAO = mabangiao,
                MANGUOIBANGIAO = manguoibangiao,
                NGAYBANGIAO = ngaybangiao,
                MANGUOIXACNHAN = null,
                NGAYXACNHAN = null,
                // NGAYXACNHAN = null
            };
            entities.BANGIAOs.Add(bangiao);
            entities.SaveChanges();
        }
        public void xoaBanGiao(string mabangiao)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            BANGIAO bangiao = entities.BANGIAOs.Where(x => x.MABANGIAO == mabangiao).FirstOrDefault();
            if (bangiao != null)
            {
                entities.BANGIAOs.Remove(bangiao);
                entities.SaveChanges();
            }
        }
        public void themChiTietBanGiao(string mabangiao, List<BANGIAO_VIEW_SOLUONG> list)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            foreach (BANGIAO_VIEW_SOLUONG v in list)
            {
                CHITIETBANGIAO chitiet = new CHITIETBANGIAO()
                {
                    MABANGIAO = mabangiao,
                    MASACH = v.MASACH,
                    SOLUONG = v.SOLUONG,
                };
                //
                entities.CHITIETBANGIAOs.Add(chitiet);
                SACH sach = entities.SACHes.Where(s => s.MASACH == v.MASACH).FirstOrDefault();
                sach.SLHIENTAI += v.SOLUONG;
                sach.SLTONKHO -= v.SOLUONG;
                sach.TONGSL = sach.SLHIENTAI + sach.SLDANGMUON;
                entities.SaveChanges();
            }
        }
        public void xoaChiTietBanGiao(string mabangiao)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<CHITIETBANGIAO> list_chitiet = entities.CHITIETBANGIAOs.Where(s => s.MABANGIAO == mabangiao).ToList();
            if (list_chitiet != null && list_chitiet.Count > 0)
            {
                foreach (CHITIETBANGIAO chitiet in list_chitiet)
                {
                    entities.CHITIETBANGIAOs.Remove(chitiet);
                    SACH sach = entities.SACHes.Where(s => s.MASACH == chitiet.MASACH).FirstOrDefault();
                    sach.SLHIENTAI -= chitiet.SOLUONG;
                    sach.SLTONKHO += chitiet.SOLUONG;
                    sach.TONGSL = sach.SLHIENTAI + sach.SLDANGMUON;
                    entities.SaveChanges();
                }
            }

        }
        public void taoMoiBanGiao(string mabangiao, string manguoibangiao, DateTime ngaybangiao, List<BANGIAO_VIEW_SOLUONG> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            themBanGiao(mabangiao, manguoibangiao, ngaybangiao);
            themChiTietBanGiao(mabangiao, list);
        }
        public void chinhSuaBanGiao(string mabangiao, List<BANGIAO_VIEW_SOLUONG> list)
        {
            xoaChiTietBanGiao(mabangiao);
            themChiTietBanGiao(mabangiao, list);
        }

        public void xoaBanGiao(List<string> list_mabangiao)
        {
            foreach (string mabangiao in list_mabangiao)
            {
                xoaChiTietBanGiao(mabangiao);
                xoaBanGiao(mabangiao);
            }
        }
        public List<BANGIAO_VIEW> getAllBanGiao()
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.BANGIAOs.Select(bangiao => new BANGIAO_VIEW()
            {
                MABANGIAO = bangiao.MABANGIAO,
                MANGUOIBANGIAO = bangiao.MANGUOIBANGIAO,
                MANGUOIXACNHAN = bangiao.MANGUOIXACNHAN,
                NGAYBANGIAO = (DateTime)bangiao.NGAYBANGIAO,
                NGAYXACNHAN = (DateTime)(bangiao.NGAYXACNHAN == null ? default(DateTime) : bangiao.NGAYXACNHAN),
            }).ToList();
        }
        public BANGIAO getBanGiaoTheoMaBanGiao(string mabangiao)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            return entities.BANGIAOs.Where(p => p.MABANGIAO == mabangiao).FirstOrDefault();
        }

        public List<BANGIAO_VIEW_SOLUONG> getSoLuongTheoMaBanGiao(string mabangiao)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            List<BANGIAO_VIEW_SOLUONG> list_soluong = entities.CHITIETBANGIAOs.Where(p => p.MABANGIAO == mabangiao).Select(p => new BANGIAO_VIEW_SOLUONG()
            {
                MASACH = p.MASACH,
                SOLUONG = p.SOLUONG,
            }).ToList();
            return list_soluong;
        }
        public List<BANGIAO_VIEW> getBanGiaoChuaXacNhan(string manguoidung)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            NGUOIDUNG nguoidung = entities.NGUOIDUNGs.Where(p => p.MANGUOIDUNG == manguoidung).FirstOrDefault();
            List<BANGIAO_VIEW> list = new List<BANGIAO_VIEW>();
            if (nguoidung.MANHIEMVU == "001") // admin
            {
                list = entities.BANGIAOs.
                    Where(p => p.MANGUOIXACNHAN == null || p.MANGUOIXACNHAN == "").
                    Where(p => p.NGUOIDUNG.MANHIEMVU == "002").
                    Select(bangiao => new BANGIAO_VIEW()
                    {
                        MABANGIAO = bangiao.MABANGIAO,
                        MANGUOIBANGIAO = bangiao.MANGUOIBANGIAO,
                        MANGUOIXACNHAN = bangiao.MANGUOIXACNHAN,
                        NGAYBANGIAO = (DateTime)bangiao.NGAYBANGIAO,
                        NGAYXACNHAN = (DateTime)(bangiao.NGAYXACNHAN == null ? default(DateTime) : bangiao.NGAYXACNHAN),
                    }).
                    ToList();
            }
            else if (nguoidung.MANHIEMVU == "002")
            {
                list = entities.BANGIAOs.
                    Where(p => p.MANGUOIXACNHAN == null || p.MANGUOIXACNHAN == "").
                    Where(p => p.NGUOIDUNG.MANHIEMVU == "001").
                    Select(bangiao => new BANGIAO_VIEW()
                    {
                        MABANGIAO = bangiao.MABANGIAO,
                        MANGUOIBANGIAO = bangiao.MANGUOIBANGIAO,
                        MANGUOIXACNHAN = bangiao.MANGUOIXACNHAN,
                        NGAYBANGIAO = (DateTime)bangiao.NGAYBANGIAO,
                        NGAYXACNHAN = (DateTime)(bangiao.NGAYXACNHAN == null ? default(DateTime) : bangiao.NGAYXACNHAN),
                    }).
                    ToList();

            }
            return list;
        }

        public void tuChoiBanGiao(List<string> list_mabangiao)
        {
            xoaBanGiao(list_mabangiao);
        }

        public void xacNhanBanGiao(List<string> list_mabangiao, string manguoixacnhan, DateTime ngayxacnhan)
        {
            QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
            foreach (string mabangiao in list_mabangiao)
            {
                BANGIAO bangiao = entities.BANGIAOs.Where(p => p.MABANGIAO == mabangiao).FirstOrDefault();
                bangiao.NGAYXACNHAN = ngayxacnhan;
                bangiao.MANGUOIXACNHAN = manguoixacnhan;
                entities.SaveChanges();
            }
        }
    }
}
