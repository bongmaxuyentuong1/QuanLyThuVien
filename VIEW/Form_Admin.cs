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
    public partial class Form_Admin : Form
    {

        private string user;
        private string role;
        private string manguoidung;
        public Form_Admin(string admin, string nhiemvu)
        {
            InitializeComponent();
            customizeDesing();
            user = admin;
            role = nhiemvu;
            lbTenND.Text = user;
            lbPhanQuyen.Text = role;
            this.manguoidung = BLL_NGUOIDUNG.Instance.getMaNguoiDungTheoTaiKhoan(user);
        }
        public void customizeDesing()
        {
            pnTailieu.Visible = false;
            pnMuontra.Visible = false;
            pnDocgia.Visible = false;
            pnBCTK.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                subMenu.Visible = true;
            }
            else subMenu.Visible = false;
        }
        private void btnQLTL_Click(object sender, EventArgs e)
        {
            showSubMenu(pnTailieu);
        }
        #region QLTLSubMenu
        private void btnQLTL1_Click(object sender, EventArgs e)
        {
            Sach_Danhsach f = new Sach_Danhsach(this.manguoidung);
            openChildForm(f);
        }
        private void btnQLTL2_Click(object sender, EventArgs e)
        {
            Sach_Them f = new Sach_Them(this.manguoidung);
            openChildForm(f);
        }
        private void btnQLTL3_Click(object sender, EventArgs e)
        {
            Sach_Tim f = new Sach_Tim();
            openChildForm(f);
        }
        #endregion
        private void btnQLDG_Click(object sender, EventArgs e)
        {
            showSubMenu(pnDocgia);
        }
        #region QLDGSubMenu
        private void btnQLDG1_Click(object sender, EventArgs e)
        {
            Docgia_Danhsach f = new Docgia_Danhsach(this.manguoidung);
            openChildForm(f);
        }
        private void btnQLDG2_Click(object sender, EventArgs e)
        {
            Docgia_Them f =new Docgia_Them();
            openChildForm(f);
        }
        private void btnQLDG3_Click(object sender, EventArgs e)
        {
            Docgia_Tim f = new Docgia_Tim(null);
            openChildForm(f);
        }

        #endregion
        private void btnQLMT_Click(object sender, EventArgs e)
        {
            showSubMenu(pnMuontra);
        }
        #region QLMTSubMenu
        private void btnQLMT1_Click(object sender, EventArgs e)
        {
            PM_Them f = new PM_Them(this.manguoidung);
            openChildForm(f);
        }
        private void btnQLMT2_Click(object sender, EventArgs e)
        {
            PM_Sua f = new PM_Sua(this.manguoidung);
            openChildForm(f);
        }
        private void btnQLMT3_Click(object sender, EventArgs e)
        {
            PM_Tim f = new PM_Tim();
            openChildForm(f);
        }
        #endregion
        private void btnBCTK_Click(object sender, EventArgs e)
        {
            showSubMenu(pnBCTK);
        }
        #region BCTKMenu
        private void btnBCTK3_Click(object sender, EventArgs e)
        {
            Thongke_Docgiavipham f = new Thongke_Docgiavipham();
            openChildForm(f);
        }

        private void btnBCTK2_Click(object sender, EventArgs e)
        {
            Thongke_Docgiamuonnhieu f = new Thongke_Docgiamuonnhieu();
            openChildForm(f);
        }

        private void btnBCTK1_Click(object sender, EventArgs e)
        {
            Thongke_Sachhet f = new Thongke_Sachhet(this.manguoidung);
            openChildForm(f);
        }
        #endregion
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.ForeColor = childForm.ForeColor;
            //childForm.BackColor = Color.FromArgb(201, 228, 255);
            pnChildForm.Controls.Add(childForm);
            pnChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        #region MenuHeThong
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnThaydoiMK_Click(object sender, EventArgs e)
        {
            CN_Doipass cN_Doipass = new CN_Doipass(user);
            openChildForm(cN_Doipass);
        }
        private void danhSáchNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User_Danhsach f = new User_Danhsach();
            openChildForm(f);
        }
        private void btnDangnhaplai_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.reLogin();
            this.Close();
        }
        private void tìmKiếmNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User_Tim f = new User_Tim();
            openChildForm(f);
        }
        private void thêmNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User_Them f = new User_Them();
            openChildForm(f);
        }
        #endregion

        #region MenuTailieu
        private void thôngTinNhậpTàiLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sach_Danhsach f = new Sach_Danhsach(this.manguoidung);
            openChildForm(f);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Sach_Them f = new Sach_Them(this.manguoidung);
            openChildForm(f);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Sach_Tim sach_Tim = new Sach_Tim();
            openChildForm(sach_Tim);
        }
        #endregion

        #region MenuDocgia
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Docgia_Danhsach f = new Docgia_Danhsach(this.manguoidung);
            openChildForm(f);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Docgia_Them f = new Docgia_Them(this.manguoidung);
            openChildForm(f);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Docgia_Tim f = new Docgia_Tim(manguoidung);
            openChildForm(f);
        }
        #endregion
    
        #region MenuPhieuMuon
        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            PM_Them f = new PM_Them(this.manguoidung);
            openChildForm(f);
        }

        private void danhSáchPhiếuMượnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PM_Danhsach f = new PM_Danhsach(this.manguoidung);
            openChildForm(f);
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            PM_Sua f =  new PM_Sua(this.manguoidung);
            openChildForm(f);
        }
        #endregion

        #region MenuBCTK
        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            Thongke_Sachhet f = new Thongke_Sachhet(this.manguoidung);
            openChildForm(f);
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            Thongke_Sachmuonnhieu f = new Thongke_Sachmuonnhieu();
            openChildForm(f);
        }

        private void thốngKêĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongke_Docgiamuonnhieu f = new Thongke_Docgiamuonnhieu();
            openChildForm(f);
        }

        private void thốngKêĐộcGiảViPhạmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongke_Docgiavipham f = new Thongke_Docgiavipham();
            openChildForm(f);
        }
        #endregion

        #region MenuBangiao
        private void yêuCầuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bangiao_Them f = new Bangiao_Them(this.manguoidung);
            openChildForm(f);
        }

        private void xácNhậnBànGiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bangiao_Danhsachchuaxacnhan f = new Bangiao_Danhsachchuaxacnhan(this.manguoidung);
            openChildForm(f);
        }
        #endregion
    }
}
