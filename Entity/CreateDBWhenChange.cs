using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QuanLyThuVien.Entity
{
    public class CreateDBWhenChange : DropCreateDatabaseAlways<QuanLyThuVienEntities>
    {
        protected override void Seed(QuanLyThuVienEntities context)
        {
            // NGONNGU
            context.NGONNGUs.AddRange(new NGONNGU[]
            {
                new NGONNGU {MANGONNGU = "001",TENNGONNGU = "Tieng Viet"},
                new NGONNGU {MANGONNGU = "002",TENNGONNGU = "Tieng Anh"},
                new NGONNGU {MANGONNGU = "003",TENNGONNGU = "Tieng Phap"},
                new NGONNGU {MANGONNGU = "004",TENNGONNGU = "Tieng Nhat"},
            });
            // THELOAI
            context.THELOAIs.AddRange(new THELOAI[]
            {
                new THELOAI {MATHELOAI = "001",TENTHELOAI = "Khoa hoc"},
                new THELOAI {MATHELOAI = "002",TENTHELOAI = "Ky su"},
                new THELOAI {MATHELOAI = "003",TENTHELOAI = "Giao trinh"},
                new THELOAI {MATHELOAI = "004",TENTHELOAI = "CNTT"},
            });
            // NXB
            context.NXBs.AddRange(new NXB[]
             {
                new NXB {MANXB = "001",TENNXB = "Giao Duc"},
                new NXB {MANXB = "002",TENNXB = "Tre"},
                new NXB {MANXB = "003",TENNXB = "Dan Tri"},
                new NXB {MANXB = "004",TENNXB = "Ha Noi"},
                new NXB {MANXB = "005",TENNXB = "HCM"},
                new NXB {MANXB = "006",TENNXB = "Cong Thuong"},
             });
            // DOCGIA
            context.DOCGIAs.AddRange(new DOCGIA[]
            {
                new DOCGIA {MADOCGIA = "001",HOTEN = "Le Duc Huy",DIACHI = "Da Nang",SOSACHMUON = 7},
                new DOCGIA {MADOCGIA = "002",HOTEN = "Duong Trung Huy",DIACHI = "Da Nang",SOSACHMUON = 8},
                new DOCGIA {MADOCGIA = "003",HOTEN = "Dang Huynh Khanh Duong",DIACHI = "Da Nang",SOSACHMUON = 8},
                new DOCGIA {MADOCGIA = "004",HOTEN = "Huynh Ba Thuan",DIACHI = "Da Nang",SOSACHMUON = 6},
                new DOCGIA {MADOCGIA = "005",HOTEN = "Tran Thi E",DIACHI = "Quang Nam",SOSACHMUON = 7},
                new DOCGIA {MADOCGIA = "006",HOTEN = "Le Ba F",DIACHI = "Hue",SOSACHMUON = 7},
                new DOCGIA {MADOCGIA = "007",HOTEN = "Le Thi G",DIACHI = "Binh Dinh",SOSACHMUON = 8},
            });
            // KHOA
            context.KHOAs.AddRange(new KHOA[]
            {
               new KHOA {MAKHOA = "001", TENKHOA = "CNTT"},
               new KHOA {MAKHOA = "002", TENKHOA = "Co Khi Giao Thong"},
               new KHOA {MAKHOA = "003", TENKHOA = "Dien Tu Vien Thong"},
               new KHOA {MAKHOA = "004", TENKHOA = "Cong Nghe Sinh Hoc"},
               new KHOA {MAKHOA = "005", TENKHOA = "Cong Nghe Moi Truong"},
            });
            // LOPSH
            context.LOPSHes.AddRange(new LOPSH[]
            {
                new LOPSH {MALOPSH = "001", TENLOPSH = "20T1", MAKHOA = "001"},
                new LOPSH {MALOPSH = "002", TENLOPSH = "20TCLC_KHDL", MAKHOA = "002"},
                new LOPSH {MALOPSH = "003", TENLOPSH = "20DT1", MAKHOA = "003"},
                new LOPSH {MALOPSH = "004", TENLOPSH = "20T2", MAKHOA = "004"},
                new LOPSH {MALOPSH = "005", TENLOPSH = "20SH1", MAKHOA = "005"},
            });
            // NHIEMVU
            context.NHIEMVUs.AddRange(new NHIEMVU[]
            {
                new NHIEMVU {MANHIEMVU = "001", TENNHIEMVU = "Admin"},
                new NHIEMVU {MANHIEMVU = "002", TENNHIEMVU = "Thu kho"},
            });
            // SACH
            context.SACHes.AddRange(new SACH[]
            {
                new SACH {MASACH = "001", NHANDE = "Truyen Kieu", MATHELOAI = "001", MANXB = "001", NAMXB = DateTime.Now, MANGONNGU = "001", TACGIA = "Nguyen Du", TONGSL = 98, SLDANGMUON = 8, SLHIENTAI = 90, SLTONKHO = 2},
                new SACH {MASACH = "002", NHANDE = "Lap trinh C", MATHELOAI = "002", MANXB = "002", NAMXB = DateTime.Now, MANGONNGU = "001",TACGIA = "Khanh Duong",TONGSL = 90, SLDANGMUON = 0, SLHIENTAI = 90, SLTONKHO = 10},
                new SACH {MASACH = "003", NHANDE = "Deep Learning", MATHELOAI = "002", MANXB = "001", NAMXB = DateTime.Now, MANGONNGU = "002", TACGIA = "Yoshua Bengio", TONGSL = 90, SLDANGMUON = 0, SLHIENTAI = 90, SLTONKHO = 100},
                new SACH {MASACH = "004", NHANDE = "Lieu IT Da Het Thoi", MATHELOAI = "001", MANXB = "002", NAMXB = DateTime.Now, MANGONNGU = "002", TACGIA = "N.G.Carr", TONGSL = 98, SLDANGMUON = 8, SLHIENTAI = 90, SLTONKHO = 2 },
                new SACH {MASACH = "005", NHANDE = "Code dao ky su", MATHELOAI = "002", MANXB = "003", NAMXB = DateTime.Now, MANGONNGU = "001", TACGIA = "Pham Huy Hoang", TONGSL = 98, SLDANGMUON = 8, SLHIENTAI = 90, SLTONKHO = 2 },
                new SACH {MASACH = "006", NHANDE = "The Pragmatic Programmer", MATHELOAI = "001", MANXB = "004", NAMXB = DateTime.Now, MANGONNGU = "002", TACGIA = "Andrew Hunt", TONGSL = 97, SLDANGMUON = 7, SLHIENTAI = 90, SLTONKHO = 3 },
                new SACH {MASACH = "007", NHANDE = "The Design of Everyday Things", MATHELOAI = "004", MANXB = "005", NAMXB = DateTime.Now, MANGONNGU = "002", TACGIA = "Donald A. Norman", TONGSL = 96, SLDANGMUON = 6, SLHIENTAI = 90, SLTONKHO = 4 },
                new SACH {MASACH = "008", NHANDE = "Gian diep mang", MATHELOAI = "004", MANXB = "006", NAMXB = DateTime.Now, MANGONNGU = "001", TACGIA = "Clifford Stoll", TONGSL = 95, SLDANGMUON = 5, SLHIENTAI = 90, SLTONKHO = 5 },
                new SACH {MASACH = "009", NHANDE = "Nghe thuat an minh", MATHELOAI = "004", MANXB = "006", NAMXB = DateTime.Now, MANGONNGU = "001", TACGIA = "Kevin Mitnick", TONGSL = 94, SLDANGMUON = 4, SLHIENTAI = 90, SLTONKHO = 6 },
                new SACH {MASACH = "010", NHANDE = "Lap trinh va cuoc song", MATHELOAI = "004", MANXB = "003", NAMXB = DateTime.Now, MANGONNGU = "001", TACGIA = "Jeff Atwood", TONGSL = 91, SLDANGMUON = 1, SLHIENTAI = 90, SLTONKHO = 9 },
            });
            // SINHVIEN
            context.SINHVIENs.AddRange(new SINHVIEN[]
            {
                new SINHVIEN {MADOCGIA = "001", MASINHVIEN = "102200218", MALOPSH = "001"},
                new SINHVIEN {MADOCGIA = "002", MASINHVIEN = "102200318", MALOPSH = "003"},
                new SINHVIEN {MADOCGIA = "004", MASINHVIEN = "102200018", MALOPSH = "002"},
                new SINHVIEN {MADOCGIA = "007", MASINHVIEN = "102200118", MALOPSH = "005"},
            });
            // GIANGVIEN
            context.GIANGVIENs.AddRange(new GIANGVIEN[]
            {
                new GIANGVIEN {MADOCGIA = "003", MAKHOA = "001", HOCVI = "Tien Si" },
                new GIANGVIEN {MADOCGIA = "005", MAKHOA = "004", HOCVI = "P.Giao Su" },
                new GIANGVIEN {MADOCGIA = "006", MAKHOA = "005", HOCVI = "Giao Su" },
            });
            // NGUOIDUNG
            context.NGUOIDUNGs.AddRange(new NGUOIDUNG[]
            {
                new NGUOIDUNG {MANGUOIDUNG = "001", MANHIEMVU = "001", TAIKHOAN = "thuthu1", MATKHAU = "1", HOTEN = "Le Thi Tram", NAMSINH = DateTime.Now, GIOITINH = false, DIENTHOAI = "0202020202", EMAIL = "tramlt@gmail.com"},
                new NGUOIDUNG {MANGUOIDUNG = "002", MANHIEMVU = "002", TAIKHOAN = "thukho1", MATKHAU = "1", HOTEN = "Nguyen Thi Huyen", NAMSINH = DateTime.Now, GIOITINH = false, DIENTHOAI = "0303030303", EMAIL = "huyennt@gmail.com"},
            });
            // NHAPSACH
            context.NHAPSACHes.AddRange(new NHAPSACH[]
            {
                new NHAPSACH {MANHAPSACH = "001", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "002", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "003", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "004", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "005", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "006", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "007", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "008", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "009", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
                new NHAPSACH {MANHAPSACH = "010", NGAYNHAP = DateTime.Now, MANGUOIDUNG = "002"},
            });
            // PHIEUMUON
            context.PHIEUMUONs.AddRange(new PHIEUMUON[]
            {
                new PHIEUMUON {MAPHIEU = "001", MADOCGIA = "001", MANGUOIDUNG = "001", NGAYMUON = DateTime.Now, NGAYTRA = new DateTime(2022,7,30)},
                new PHIEUMUON {MAPHIEU = "002", MADOCGIA = "002", MANGUOIDUNG = "001", NGAYMUON = DateTime.Now, NGAYTRA = new DateTime(2022,7,20)},
                new PHIEUMUON {MAPHIEU = "003", MADOCGIA = "003", MANGUOIDUNG = "001", NGAYMUON = DateTime.Now, NGAYTRA = new DateTime(2022,7,10)},
                new PHIEUMUON {MAPHIEU = "004", MADOCGIA = "004", MANGUOIDUNG = "001", NGAYMUON = DateTime.Now, NGAYTRA = new DateTime(2022,7,15)},
                new PHIEUMUON {MAPHIEU = "005", MADOCGIA = "005", MANGUOIDUNG = "001", NGAYMUON = DateTime.Now, NGAYTRA = new DateTime(2022,7,29)},
                new PHIEUMUON {MAPHIEU = "006", MADOCGIA = "006", MANGUOIDUNG = "001", NGAYMUON = DateTime.Now, NGAYTRA = new DateTime(2022,7,6)},
                new PHIEUMUON {MAPHIEU = "007", MADOCGIA = "007", MANGUOIDUNG = "001", NGAYMUON = DateTime.Now, NGAYTRA = new DateTime(2022,7,8)},
            });
            // BANGIAO
            context.BANGIAOs.AddRange(new BANGIAO[]
            {
                new BANGIAO {MABANGIAO = "001", MANGUOIBANGIAO = "001", MANGUOIXACNHAN = "002", NGAYBANGIAO = DateTime.Now, NGAYXACNHAN = DateTime.Now},
                new BANGIAO {MABANGIAO = "002", MANGUOIBANGIAO = "001", MANGUOIXACNHAN = "002", NGAYBANGIAO = DateTime.Now, NGAYXACNHAN = DateTime.Now},
                new BANGIAO {MABANGIAO = "003", MANGUOIBANGIAO = "001", MANGUOIXACNHAN = "002", NGAYBANGIAO = DateTime.Now, NGAYXACNHAN = DateTime.Now},
            });
            // CHITIETBANGIAO
            context.CHITIETBANGIAOs.AddRange(new CHITIETBANGIAO[]
            {
                new CHITIETBANGIAO {MABANGIAO = "001", MASACH = "001", SOLUONG = 10},
                new CHITIETBANGIAO {MABANGIAO = "002", MASACH = "002", SOLUONG = 10},
                new CHITIETBANGIAO {MABANGIAO = "003", MASACH = "003", SOLUONG = 10},
            });
            // CHITIETNHAPSACH
            context.CHITIETNHAPSACHes.AddRange(new CHITIETNHAPSACH[]
            {
                new CHITIETNHAPSACH {MANHAPSACH = "001", MASACH = "001", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "002", MASACH = "002", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "003", MASACH = "003", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "004", MASACH = "004", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "005", MASACH = "005", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "006", MASACH = "006", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "007", MASACH = "007", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "008", MASACH = "008", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "009", MASACH = "009", SOLUONG = 10},
                new CHITIETNHAPSACH {MANHAPSACH = "010", MASACH = "010", SOLUONG = 10},
            });
            // CHITIETPHIEUMUON
            context.CHITIETPHIEUMUONs.AddRange(new CHITIETPHIEUMUON[]
            {
                new CHITIETPHIEUMUON {MAPHIEU = "001", MASACH = "001", SOLUONG = 10},
                new CHITIETPHIEUMUON {MAPHIEU = "002", MASACH = "002", SOLUONG = 10},
                new CHITIETPHIEUMUON {MAPHIEU = "003", MASACH = "003", SOLUONG = 10},
                new CHITIETPHIEUMUON {MAPHIEU = "004", MASACH = "004", SOLUONG = 10},
                new CHITIETPHIEUMUON {MAPHIEU = "005", MASACH = "005", SOLUONG = 10},
                new CHITIETPHIEUMUON {MAPHIEU = "006", MASACH = "006", SOLUONG = 10},
                new CHITIETPHIEUMUON {MAPHIEU = "007", MASACH = "007", SOLUONG = 10},
            });
        }
    }
}
