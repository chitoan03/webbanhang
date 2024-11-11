using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    [Authorize(Roles = "QuanLySanPham")]
    public class QuanLySanPhamController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        // GET: QuanLySanPham        
        public ActionResult Index()
        {
            return View(db.SanPhams.Where(n => n.DaXoa == 0));
        }
        [HttpGet]       
        public ActionResult TaoMoi()
        {
            ViewBag.IDNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.IDNCC), "IDNCC", "TenNCC");
            ViewBag.IDLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.IDLSP), "IDLSP", "TenLSP");
            ViewBag.IDNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.IDNSX), "IDNSX", "TenNSX");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(SanPham sanPham, HttpPostedFileBase HinhAnh1)
        {
            ViewBag.IDNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.IDNCC), "IDNCC", "TenNCC");
            ViewBag.IDLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.IDLSP), "IDLSP", "TenLSP");
            ViewBag.IDNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.IDNSX), "IDNSX", "TenNSX");
            if (HinhAnh1 == null)
            {
                ViewBag.Images = "Cần chọn hình trước khi lưu";
                return View();
            }
            if (HinhAnh1 != null && HinhAnh1.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh1.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/assets/images/product"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh1.SaveAs(path);
                    sanPham.HinhAnh1 = fileName;
                }
            }
            sanPham.DaXoa = 0;
            db.SanPhams.Add(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.IDSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "IDNCC", "TenNCC", sp.IDNCC);
            ViewBag.IDLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLSP), "IDLSP", "TenLSP", sp.IDLSP);
            ViewBag.IDNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "IDNSX", "TenNSX", sp.IDNSX);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SanPham model, HttpPostedFileBase HinhAnh1)
        {
            ViewBag.IDNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "IDNCC", "TenNCC", model.IDNCC);
            ViewBag.IDLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLSP), "IDLSP", "TenLSP", model.IDLSP);
            ViewBag.IDNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "IDNSX", "TenNSX", model.IDNSX);
            if (HinhAnh1 != null && HinhAnh1.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh1.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/assets/images/product"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh1.SaveAs(path);
                    model.HinhAnh1 = fileName;
                }
            }
            // Lấy tên hình ảnh           
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            model.HinhAnh1 = model.HinhAnh1;
            model.DaXoa = 0;
            db.SaveChanges();
            return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Xoa(int? id)
    {
        if (id == null)
        {
            Response.StatusCode = 404;
            return null;
        }
        SanPham sp = db.SanPhams.SingleOrDefault(n => n.IDSP == id);
        if (sp == null)
        {
            return HttpNotFound();
        }
        ViewBag.IDNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "IDNCC", "TenNCC", sp.IDNCC);
        ViewBag.IDLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLSP), "IDLSP", "TenLSP", sp.IDLSP);
        ViewBag.IDNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "IDNSX", "TenNSX", sp.IDNSX);
        return View(sp);
    }
    [HttpPost]
    public ActionResult Xoa(int id)
    {
        if (id == null)
        {
            Response.StatusCode = 404;
            return null;
        }
        SanPham sp = db.SanPhams.SingleOrDefault(n => n.IDSP == id);
        if (sp == null)
        {
            return HttpNotFound();
        }
        db.SanPhams.Remove(sp);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}
}



