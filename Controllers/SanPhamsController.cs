using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    public class SanPhamsController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        // GET: SanPham
        public ActionResult ChiTietSanPham(int? id)
        {
            var sanpham = db.SanPhams.SingleOrDefault(m => m.IDSP == id);
            return View(sanpham);
        }
    }
}