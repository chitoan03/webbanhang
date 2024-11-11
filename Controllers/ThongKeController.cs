using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    [Authorize(Roles = "ThongKe")]
    public class ThongKeController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        // GET: ThongKe
        public ActionResult ThongKe()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.SoNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString(); //lấy số lượng người truy caạptừ application được tạo
            ViewBag.TongKeDoanhThu = ThongKeDoanhThu();
            ViewBag.TongDDH = ThongKeDonHang();
            ViewBag.TongThanhVien = ThongKeThanhVien();

            return View();
        }
        public decimal ThongKeDoanhThu()
        {
            //Thống kê theo tất cả doanh thu từ khi web thành lập
            decimal TongDoanhThu = (decimal)db.ChiTietDonHangs.Sum(n => n.Soluong * n.DonGia);
            return TongDoanhThu;
        }
        public double ThongKeDonHang()
        {
            double slddh = db.DonDatHangs.Count();
            return slddh;
        }
        public double ThongKeThanhVien()
        {
            double sltv = db.ThanhViens.Count();
            return sltv;
        }
        public decimal ThongKeDoanhThuThang(int Thang, int Nam)
        {
            //Thống kê theo tất cả doanh thu từ khi web thành lập
           //List ra những đơn hàng nào có tháng, năm tương ứng
           var sDDH1 = db.DonDatHangs.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);
            decimal TongTien = 0;
            foreach (var item in sDDH1)
            {
                TongTien += (decimal)item.ChiTietDonHangs.Sum(n => n.Soluong * n.DonGia);
            }    
            return TongTien;
        }
    }
}