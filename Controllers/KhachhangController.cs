using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    public class KhachhangController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        // GET: Khachhang
        public ActionResult Khachhang()
        {
            List<KhachHang> danhSachKhachHang = db.KhachHangs.ToList();
            return View(danhSachKhachHang);
        }
        public ActionResult ThemKhachhang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemKhachhang(KhachHang model)
        {
            db.KhachHangs.Add(model);
            db.SaveChanges();
            return RedirectToAction("Khachhang");
        }
        public ActionResult Capnhat(int id)
        {
            ShopsEntities18 db = new ShopsEntities18();
            KhachHang modeL2 = db.KhachHangs.Find(id);
            return View(modeL2);
        }
        [HttpPost]
        public ActionResult Capnhat(KhachHang model)
        {
            var updatemodel = db.KhachHangs.Find(model.MaKH);
            updatemodel.TenKhachHang = model.TenKhachHang;
            updatemodel.NgaySinh = model.NgaySinh;
            updatemodel.DiaChi = model.DiaChi;
            updatemodel.SDT = model.SDT;
            updatemodel.Email = model.Email;
            db.SaveChanges();
            return RedirectToAction("Khachhang");
        }
        public ActionResult Xoa(int id)
        {
            var XoaID = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(XoaID);
            db.SaveChanges();
            return RedirectToAction("Khachhang");
        }
    }
}
