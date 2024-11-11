using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webbanhang.Controllers;
namespace Webbanhang.Models
{

    public class Giohang
    {
        
        public int MaSP { get; set; }
        public string sTenSP { get; set; }
        public int sSoLuong { get; set; }
        public decimal sDonGia { get; set; }
        public decimal dThanhtien { get; set; }
        public string sHinhAnh { get; set; }
        public Giohang(int iMaSP)
        {
            using (ShopsEntities18 db = new ShopsEntities18())
            {
                SanPham sanPham = db.SanPhams.Single(m => m.IDSP == iMaSP);
                this.MaSP = iMaSP;
                this.sTenSP = sanPham.TenSP;
                this.sDonGia = (decimal)sanPham.DonGia.Value;
                this.sHinhAnh = sanPham.HinhAnh1;
                this.dThanhtien = sDonGia * sSoLuong;
                this.sSoLuong = 1;
            }

        }
        public Giohang(int iMaSP, int sl)
        {
            using (ShopsEntities18 db = new ShopsEntities18())
            {
                SanPham sanPham = db.SanPhams.Single(m => m.IDSP == iMaSP);
                this.MaSP = iMaSP;
                this.sTenSP = sanPham.TenSP;
                this.sDonGia = (decimal) sanPham.DonGia.Value;
                this.sHinhAnh = sanPham.HinhAnh1;
                this.dThanhtien = sDonGia * sSoLuong;
                this.sSoLuong = sl;

            }

        }
        public Giohang()
        {
            

        }

    }


}