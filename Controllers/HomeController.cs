using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
using PagedList;
using PagedList.Mvc;
namespace Webbanhang.Controllers
{
    public class HomeController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        public ActionResult Index(int ? page)
        {
            int pageSize = 10;
            int pageNum = (page ?? 1);
            var sanpham = db.SanPhams.Where(ms => ms.DaXoa == 0).OrderBy(m=>m.LuotXem).ToList();   
             if (sanpham != null)
            {
                ViewBag.TinhSoLuongIndex = sanpham.Count();
            }
            return View(sanpham.ToPagedList(pageNum,pageSize));
        }
        public ActionResult Headertop()
        {
            return View();
        }
        public ActionResult HeaderSearch()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Slide()
        {
            return View();
        }
        public ActionResult Tulanh(int? page)
        {
            int pageSize = 10;
            int pageNum = (page ?? 1);
            var sanphams = db.SanPhams.Where(m => m.IDLSP == 3).OrderBy(m => m.LuotXem).ToList();
            if (sanphams != null)
            {
                ViewBag.TinhSoLuong = sanphams.Count();
            }
            return View(sanphams.ToPagedList(pageNum, pageSize));
           
        }     


    public ActionResult TulanhSamsung()
        {
            var sanphams = db.SanPhams.Where(m => m.MaSP == "TuLanhSamsung");
            if(sanphams != null)
            {
                ViewBag.TinhSoLuongss = sanphams.Count();
            }               
            return View(sanphams);

        }     
        public ActionResult DanhSachSanPham()
        {
            List<SanPham> danhSachsangpham = db.SanPhams.ToList();          
            return View(danhSachsangpham);
        }
        public ActionResult Giadung()
        {
            var danhSachsp = db.SanPhams.Where(x => x.IDLSP == 1);
            if (danhSachsp != null)
            {
                ViewBag.TinhSoLuonggiadung = danhSachsp.Count();
            }         
            return View(danhSachsp);
        }
        public ActionResult maylocnuoc()
        {
            var sanphams = db.SanPhams.Where(m => m.IDLSP == 6);
            if (sanphams != null)
            {
                ViewBag.maylocnuoc = sanphams.Count();
            }
            return View(sanphams);

        }
        public ActionResult noicom()
        {
            var sanphams = db.SanPhams.Where(m => m.IDLSP == 9);
            if (sanphams != null)
            {
                ViewBag.noicom = sanphams.Count();
            }
            return View(sanphams);

        }
        public ActionResult noichien()
        {
            var sanphams = db.SanPhams.Where(m => m.IDLSP == 10);
            if (sanphams != null)
            {
                ViewBag.noichien = sanphams.Count();
            }
            return View(sanphams);

        }
        public ActionResult bepdien()
        {
            var sanphams = db.SanPhams.Where(m => m.IDLSP == 11);
            if (sanphams != null)
            {
                ViewBag.bepdien = sanphams.Count();
            }
            return View(sanphams);

        }
              
        public ActionResult TulanhAqua()
        {
            var sanphams = db.SanPhams.Where(m => m.MaSP == "TulanhAqua");
            if (sanphams != null)
            {
                ViewBag.tulanhaqua = sanphams.Count();
            }
            return View(sanphams);

        }
        public ActionResult TulanhToshiba()
        {
            var sanphams = db.SanPhams.Where(m => m.MaSP == "TuLanhToshiba");
            if (sanphams != null)
            {
                ViewBag.Tulanhtoshiba = sanphams.Count();
            }
            return View(sanphams);

        }
        public ActionResult TulanhLG()
        {
            var sanphams = db.SanPhams.Where(m => m.MaSP == "TulanhLG");
            if (sanphams != null)
            {
                ViewBag.tulanhLG = sanphams.Count();
            }
            return View(sanphams);

        }
        public ActionResult Dogiadung(int? page)
        {
            int pageSize = 7;
            int pageNum = (page ?? 1);
            var sanphams = db.SanPhams.Where(m => m.IDLSP == 10 || m.IDLSP == 9 || m.IDLSP == 11 || m.IDLSP == 6 || m.IDLSP == 1).OrderBy(m => m.LuotXem).ToList(); 
            if (sanphams != null)
            {
                ViewBag.dogiadung = sanphams.Count();
            }
            return View(sanphams.ToPagedList(pageNum, pageSize));

        }       
        public ActionResult SanPhamTheoNhaSanXuat(int? maLoai, int? maNSX, int? Page)
        {
            var sanpham = db.SanPhams.Where(m => m.IDNSX == maNSX && m.IDLSP == maLoai && m.DaXoa == 0).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            ViewBag.MaLoai = maLoai;
            ViewBag.MaNSX = maNSX;
            return View(sanpham.OrderBy(m => m.DonGia).ToPagedList(pageNumbber, pageSize));
        }

    }
}