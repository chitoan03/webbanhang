using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    public class GiohangController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        public List<Giohang> LayGioHang()
        {
            //Gio hang da ton tai
            List<Giohang> stGioHang = Session["Giohang"] as List<Giohang>;
            if (stGioHang == null)
            {
                stGioHang = new List<Giohang>();
                Session["Giohang"] = stGioHang;
            }
            return stGioHang;
        }
        //themgiohang
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.IDSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
            }
            //Lay gio hang
            List<Giohang> stGioHang = LayGioHang();
            //truong hop 1 sp ton tai trong gio hang
            Giohang spCheck = stGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                if (sp.SoLuongTon < spCheck.sSoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.sSoLuong++;
                spCheck.dThanhtien = spCheck.sSoLuong * spCheck.sDonGia;
                return Redirect(strURL);
            }
            Giohang itemGH = new Giohang(MaSP);
            if (sp.SoLuongTon < itemGH.sSoLuong)
            {
                return View("ThongBao");
            }
            stGioHang.Add(itemGH);
            return Redirect(strURL);
        }
        public double TinhTongSoLuong()
        {
            List<Giohang> stGioHang = Session["Giohang"] as List<Giohang>;
            if (stGioHang == null)
            {
                return 0;
            }
            return stGioHang.Sum(n => n.sSoLuong);
        }
        public decimal TinhTongTien()
        {
            List<Giohang> stGioHang = Session["Giohang"] as List<Giohang>;
            if (stGioHang == null)
            {
                return 0;
            }
            return stGioHang.Sum(n => n.dThanhtien);
        }
        public ActionResult XemGioHang()
        {
            List<Giohang> stGioHang = LayGioHang();
            return View(stGioHang);
        }
        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }

        public ActionResult SuaGioHang(int MaSP)
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.IDSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lay list gio hang tu session
            List<Giohang> stGiohang = LayGioHang();
            //kiem tra xem sp do co ton tai trong gio hang hay khong
            Giohang spCheck = stGiohang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Lay list gio hang tao giao dien
            ViewBag.GioHang = stGiohang;
            return View(spCheck);
        }
        [HttpPost]
        public ActionResult CapnhatGioHang(int MaSP, FormCollection f)
        {
            List<Giohang> stGiohang = LayGioHang();
            Giohang sanpham = stGiohang.Find(n => n.MaSP == MaSP);
            if (sanpham != null)
            {
                sanpham.sSoLuong = int.Parse(f["txtSoluong"].ToString());
                sanpham.dThanhtien = sanpham.sSoLuong * sanpham.sDonGia;
            }
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XoaGiohang(int MaSP)
        {
            List<Giohang> stGiohang = LayGioHang();
            Giohang sanpham = stGiohang.SingleOrDefault(n => n.MaSP == MaSP);
            if (sanpham != null)
            {
                stGiohang.RemoveAll(n => n.MaSP == MaSP);
                return RedirectToAction("XemGioHang");
            }
            if (stGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XoatatcaGH()
        {
            List<Giohang> stGiohang = LayGioHang();
            stGiohang.Clear();
            return RedirectToAction("XemGioHang");
        }
        public ActionResult DatHang()
        {

            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> stGiohang = LayGioHang();
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return View(stGiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DonDatHang ddh = new DonDatHang();
            ThanhVien TV = (ThanhVien)Session["TaiKhoan"];
            var loai = db.LoaiThanhViens.Find(TV.MaLoaiTV);
            List<Giohang> gh = LayGioHang();
            ddh.MaTV= TV.MaTV;
            ddh.NgayDat = DateTime.Now;
            ddh.TrinhTrangGiao = 0;
            ddh.DaThanhToan = 0;
            ddh.UuDai = loai.UuDai.ToString();
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.IDSP = item.MaSP;
                ctdh.TenSP = item.sTenSP;
                ctdh.Soluong = item.sSoLuong;
                ctdh.DonGia = Convert.ToSingle(item.sDonGia);
                db.ChiTietDonHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XemGioHang", "Giohang");
        }
    }
}
