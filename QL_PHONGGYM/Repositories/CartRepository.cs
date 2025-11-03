using QL_PHONGGYM.Models;
using QL_PHONGGYM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_PHONGGYM.Repositories
{
    public class CartRepository
    {
        private readonly QL_PHONGGYMEntities _context;

        public CartRepository(QL_PHONGGYMEntities context)
        {
            _context = context;
        }

        public void XoaDaChon(FormCollection form, int makh)
        {
            var list = _context.ChiTietGioHang.ToList().Where(kh => kh.MaKH == makh).Where(sp => form[sp.MaSP.ToString()] != null).ToList();
            _context.ChiTietGioHang.RemoveRange(list);
            _context.SaveChanges();
        }

        public List<GioHangViewModel> ChonSanPham(FormCollection form, int makh, List<GioHangViewModel> cart)
        {
            var list = cart.Where(kh => kh.MaKH == makh).Where(sp => form[sp.MaSP.ToString()] != null).ToList();
            return list;                        
        }
        public bool AddToCart(int maKH, int? maSP, int? maGoiTap, int? maLop, int soLuong)
        {
            try
            {
                _context.Database.ExecuteSqlCommand(
                    "EXEC sp_ThemVaoGioHang @MaKH, @MaSP, @MaGoiTap, @MaLop, @SoLuong",
                    new SqlParameter("@MaKH", maKH),
                    new SqlParameter("@MaSP", (object)maSP ?? DBNull.Value),
                    new SqlParameter("@MaGoiTap", (object)maGoiTap ?? DBNull.Value),
                    new SqlParameter("@MaLop", (object)maLop ?? DBNull.Value),
                    new SqlParameter("@SoLuong", soLuong)
                );

                return true;
            }
            catch
            {
                throw;
            }
        }

        public List<GioHangViewModel> GetCart(int maKH)
        {            
            try
            {
                var cart = _context.Database.SqlQuery<GioHangViewModel>("EXEC sp_LayGioHang @MaKH", new SqlParameter("@MaKH", maKH)).ToList();
                return cart;
            }
            catch
            {
                throw;
            }
        }

    }
}