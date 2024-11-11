using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Webbanhang.Models;
using System.Web;
using System.Collections.Generic;
namespace Webbanhang.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        ShopsEntities18 db = new ShopsEntities18();
        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }
      
        public ActionResult DanhsachTV()
        {
            List<ThanhVien> danhsachtv = db.ThanhViens.ToList();
            return View(danhsachtv);
        }
      
        [HttpGet]
        public ActionResult ChinhSuaTV(int? idtv)
        {
            if (idtv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ThanhVien thanhvien1 = db.ThanhViens.SingleOrDefault(n => n.MaTV == idtv);
            if (thanhvien1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.TenLoaiTV), "MaLoaiTV", "TenLoaiTV", thanhvien1.MaLoaiTV);           
            return View(thanhvien1);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSuaTV(ThanhVien model)
        {
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.TenLoaiTV), "MaLoaiTV", "TenLoaiTV", model.MaLoaiTV);          
            // Lấy tên hình ảnh           
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;         
            db.SaveChanges();
            return RedirectToAction("DanhsachTV");
        }
        //public ActionResult XoaTV(int idtv)
        // {         
        //ThanhVien thanhvien1 = db.ThanhViens.SingleOrDefault(n => n.MaTV == idtv);
        // DonDatHangg Xoadon = db.DonDatHanggs.SingleOrDefault(n => n.MaTV == idtv);
        //db.DonDatHanggs.Remove(Xoadon);
        //db.ThanhViens.Remove(thanhvien1);
        // db.SaveChanges();
        //return RedirectToAction("DanhsachTV");
        // }
       
        [HttpGet]
        public ActionResult DangkyAdmin()
        {
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiTV), "MaLoaiTV", "TenLoaiTV");
            return View();
        }
        [HttpPost]
        public ActionResult DangkyAdmin(ThanhVien TV, FormCollection collection)
        {

            var Taikhoan = collection["TaiKhoan"];
            var MatKhau = collection["MatKhau"];
            var Hoten = collection["Hoten"];
            var DiaChi = collection["DiaChi"];
            var Email = collection["Email"];
            var DienThoai = collection["DienThoai"];
           
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiTV), "MaLoaiTV", "TenLoaiTV");
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
                     
                        db.ThanhViens.Add(TV);
                        db.SaveChanges();
                        return RedirectToAction("DangnhapAdmin");

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
        public ActionResult DangnhapAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangnhapAdmin(FormCollection collection)
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
                        var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == thanhVien.MaLoaiTV);
                        try
                            {
                            string Quyen = "";
                            foreach (var item in lstQuyen)
                            {
                                Quyen += item.Quyen.MaQuyen + ",";
                            }
                            Quyen = Quyen.Substring(0, Quyen.Length - 1); //cắt đi dấu ,
                            PhanQuyen(thanhVien.TaiKhoan.ToString(), Quyen);
                            Session["TaiKhoanAdmin"] = thanhVien;
                            return RedirectToAction("Index2");
                        }
                        catch (Exception)
                        {
                            ViewBag.Loi = "Tên đăng nhập hoặc mật khẩu không đúng!";
                        }
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
        public void PhanQuyen(string Taikhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                Taikhoan,
                DateTime.Now,
                DateTime.Now.AddHours(3),
                false,
                Quyen,
                FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie (FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
        public ActionResult TaiKhoanAdmin(int? id)
        {
            ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(m => m.MaTV == id);
            return View(thanhVien);
        }
        public ActionResult DangXuatAdmin()
        {
            Session["TaiKhoanAdmin"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("DangnhapAdmin");
        }
    }
}