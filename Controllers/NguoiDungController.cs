using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    public class NguoiDungController : Controller
    {
        ShopsEntities18 db = new ShopsEntities18();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky( ThanhVien TV, FormCollection collection)
        {

            var Taikhoan = collection["TaiKhoan"];
            var MatKhau = collection["MatKhau"];
            var Hoten = collection["Hoten"];
            var DiaChi = collection["DiaChi"];
            var Email = collection["Email"];
            var DienThoai = collection["DienThoai"];
            int maloaitv = 2;
            if (String.IsNullOrEmpty(Taikhoan))
            {
                ViewBag.Taikhoannull = "Tài khoản không được để trống !";
            }
            if (String.IsNullOrEmpty(MatKhau))
            {
                ViewBag.Matkhaunull = "Mật khẩu không được để trống !";
            }
             if (String.IsNullOrEmpty(Hoten))
            {
                ViewBag.Hotennull = "Họ tên không được để trống !";
            }
             if (String.IsNullOrEmpty(DiaChi))
            {
                ViewBag.diachinull = "Địa chỉ không được để trống !";
            }
            if (String.IsNullOrEmpty(Email))
            {
                ViewBag.emailnull = "Email không được để trống !";
            }
            if (String.IsNullOrEmpty(DienThoai))
            {
                ViewBag.Dienthoainull = "Điện thoại không được để trống !";
            }
            else
            {             
                
                    if (ModelState.IsValid)//Kiểm tra xem liệu dữ liệu được submit từ trang web có hợp lệ hay không. 
                    {                   
                        ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(m => m.TaiKhoan == TV.TaiKhoan);
                        if (thanhVien == null)
                        {
                            
                            TV.Hoten = Hoten;
                            TV.MatKhau = MatKhau;
                            TV.TaiKhoan = Taikhoan;
                            TV.DiaChi = DiaChi;
                            TV.Email = Email;
                            TV.DienThoai = DienThoai;
                            TV.MaLoaiTV = maloaitv;
                            db.ThanhViens.Add(TV);
                            db.SaveChanges();
                         return RedirectToAction("Dangnhap");

                        }
                        else
                        {
                            ViewBag.TrungLap = "Tài khoản này đã tồn tại";
                        }
                    }
                    else
                    {
                        ViewBag.ThongBao = "Đăng ký không thành công";
                    }
                
            }
            return View();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var Taikhoan = collection["TaiKhoan"];
            var Matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(Taikhoan))
            {
                ViewBag.Taikhoannull = "Tài khoản không được để trống !";
            }
            if (String.IsNullOrEmpty(Matkhau))
            {
                ViewBag.Matkhaunull = "Mật khẩu không được để trống !";
            }
            else
            {
                if (ModelState.IsValid)//Kiểm tra xem liệu dữ liệu được submit từ trang web có hợp lệ hay không. 
                {
                    ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(m => m.TaiKhoan == Taikhoan && m.MatKhau == Matkhau);
                    if (thanhVien != null)
                    {
                       
                        Session["TaiKhoan"] = thanhVien;
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ViewBag.Sai = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Đăng ký không thành công";
                }
            }
            return View();
        }
        public ActionResult TaiKhoanCuaToi(int? id)
        {
            ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(m => m.MaTV == id);
            return View(thanhVien);
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}