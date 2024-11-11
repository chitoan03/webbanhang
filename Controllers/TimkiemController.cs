using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
using PagedList.Mvc;
using PagedList;

namespace Webbanhang.Controllers
{
    public class TimkiemController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        // GET: Timkiem
        public ActionResult KQTimKiem(string sTuKhoa, int? Page)
        {
            int pageSize = 12;
            int pageNumber = (Page ?? 1);
            var listSanPham = db.SanPhams.Where(m => m.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            if (listSanPham != null)
            {
                ViewBag.TinhSoLuongtimkiem = listSanPham.Count();
            }
            return View(listSanPham.OrderBy(m => m.TenSP).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult LayTuKhoaTimKiem(string tuKhoa)
        {
            // Láy từ khóa tìm kiếm sau đó gọi đến KetQuaTimKiem
            return RedirectToAction("KQTimKiem", new { @sTuKhoa = tuKhoa });
        }
    }
}