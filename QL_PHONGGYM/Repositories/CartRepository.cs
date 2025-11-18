using QL_PHONGGYM.Models;
using QL_PHONGGYM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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

        public bool DangKyPT(FormCollection form, int makh)
        {
            try
            { 
                DangKyPT dangKy = new DangKyPT
                {
                    MaKH = makh,
                    SoBuoi = Convert.ToInt32(form["soBuoi"]),
                    NgayDangKy = DateTime.Now,
                    TrangThai = "Chờ duyệt",
                    GhiChu = form["ghiChu"]
                };
                _context.DangKyPT.Add(dangKy);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool KiemTraTrung(int makh)
        {
            var goiHienTai = _context.DangKyGoiTap
                         .Where(x => x.MaKH == makh && x.TrangThai == "Còn hiệu lực")
                         .OrderByDescending(x => x.NgayKetThuc)
                         .FirstOrDefault();
            if (goiHienTai == null)
            {
                return false;
            }
            else
                return true;
        }

        public void TaoHoaDon(FormCollection form, int makh, List<GioHangViewModel> cart = null, int? maGoiTap = null)
        {            
            HoaDon hoaDon = new HoaDon
            {
                MaKH = makh,
                TongTien = Convert.ToDecimal(form["tongTien"]),
                ThanhTien = Convert.ToDecimal(form["thanhTien"]),
                TrangThai = form["paymentMethod"] == "BANK" ? "Đã thanh toán" : "Chưa thanh toán",
                GiamGia = form["giamGia"] != null ? Convert.ToDecimal(form["giamGia"]) : 0,
                NgayLap = DateTime.Now,
            };
            _context.HoaDon.Add(hoaDon);
            _context.SaveChanges();

            if (cart != null && cart.Count > 0)
            {
                foreach (var item in cart)
                {
                    if (item.MaSP != null)
                    {
                        var DonGia = item.GiaKhuyenMaiSP ?? item.DonGia;
                        ChiTietHoaDon ct = new ChiTietHoaDon
                        {
                            MaHD = hoaDon.MaHD,
                            MaSP = item.MaSP,
                            MaDKGT = null,
                            MaDKLop = null,
                            MaDKPT = null,
                            SoLuong = item.SoLuong ?? 1,
                            DonGia = DonGia
                        };
                        _context.ChiTietHoaDon.Add(ct);

                        var list = _context.ChiTietGioHang.Where(sp => sp.MaSP == item.MaSP && sp.MaKH == makh);
                        _context.ChiTietGioHang.RemoveRange(list);
                        
                    }
                }
            }

            if (maGoiTap.HasValue)
            {
                var goiTap = _context.GoiTap.FirstOrDefault(gt => gt.MaGoiTap == maGoiTap);
                if (goiTap != null)
                {                    
                    var goiHienTai = _context.DangKyGoiTap
                        .Where(x => x.MaKH == makh && x.TrangThai == "Còn hiệu lực")
                        .OrderByDescending(x => x.NgayKetThuc)
                        .FirstOrDefault();

                    DangKyGoiTap dangKy;

                    if (goiHienTai != null)
                    {                        
                        goiHienTai.NgayKetThuc = goiHienTai.NgayKetThuc.AddMonths(goiTap.ThoiHan);
                        _context.SaveChanges();
                        dangKy = goiHienTai;
                    }
                    else
                    {                        
                        dangKy = new DangKyGoiTap
                        {
                            MaKH = makh,
                            MaGoiTap = goiTap.MaGoiTap,
                            NgayDangKy = DateTime.Now,
                            NgayBatDau = DateTime.Now,
                            NgayKetThuc = DateTime.Now.AddMonths(goiTap.ThoiHan),
                            TrangThai = "Còn hiệu lực"
                        };
                        _context.DangKyGoiTap.Add(dangKy);
                        _context.SaveChanges();
                    }
                    
                    ChiTietHoaDon ctGoiTap = new ChiTietHoaDon
                    {
                        MaHD = hoaDon.MaHD,
                        MaSP = null,
                        MaDKGT = dangKy.MaDKGT,
                        MaDKLop = null,
                        MaDKPT = null,
                        SoLuong = 1,
                        DonGia = goiTap.Gia
                    };
                    _context.ChiTietHoaDon.Add(ctGoiTap);
                }
            }

            _context.SaveChanges();
        }
        public void Xoa(int id)
        {
            var item = _context.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == id);
            item.SoLuong = item.SoLuong - 1;

            if (item.SoLuong <= 0)
                _context.ChiTietGioHang.Remove(item);

            _context.SaveChanges();
        }
        public void Them(int id)
        {
            var item = _context.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == id);
            var product = _context.SanPham.FirstOrDefault(sp => sp.MaSP == id);
            if (item.SoLuong >= product.SoLuongTon)
            {
                item.SoLuong = product.SoLuongTon;
            }
            item.SoLuong = item.SoLuong + 1;
            _context.SaveChanges();
        }
        public void XoaDon(int id, int makh)
        {


            var item = _context.ChiTietGioHang.FirstOrDefault(c => c.MaKH == makh &&
                                                            ((c.MaSP != null && c.MaSP == id) ||
                                                             (c.MaGoiTap != null && c.MaGoiTap == id) ||
                                                             (c.MaLop != null && c.MaLop == id)));
            if (item != null)
            {
                _context.ChiTietGioHang.Remove(item);
                _context.SaveChanges();
            }

        }
        public void XoaDaChon(FormCollection form, int makh)
        {
            string[] selected = form.GetValues("selectedItems");
            List<int> selectedIds = selected.Select(int.Parse).ToList();
            foreach (var id in selectedIds)
            {
                var item = _context.ChiTietGioHang.FirstOrDefault(c => c.MaKH == makh && c.MaSP == id);
                if (item == null)
                {
                    item = _context.ChiTietGioHang.FirstOrDefault(c => c.MaKH == makh && c.MaGoiTap == id);
                }
                if (item == null)
                {
                    item = _context.ChiTietGioHang.FirstOrDefault(c => c.MaKH == makh && c.MaLop == id);
                }
                if (item != null)
                    _context.ChiTietGioHang.Remove(item);
            }

            _context.SaveChanges();
        }


        public List<GioHangViewModel> ChonSanPham(FormCollection form, int makh)
        {
            string[] selected = form.GetValues("selectedItems");
            List<int> selectedIds = selected.Select(int.Parse).ToList();
            List<GioHangViewModel> list = new List<GioHangViewModel>();
            var cart = GetCart(makh);

            foreach (var id in selectedIds)
            {
                var item = cart.FirstOrDefault(c => c.MaKH == makh && c.MaSP == id);
                if (item == null)
                {
                    item = cart.FirstOrDefault(c => c.MaKH == makh && c.MaGoiTap == id);
                }
                if (item == null)
                {
                    item = cart.FirstOrDefault(c => c.MaKH == makh && c.MaLop == id);
                }
                if (item != null)
                    list.Add(item);
            }

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
                var cart = _context.ChiTietGioHang
                    .Where(c => c.MaKH == maKH)
                    .Select(c => new GioHangViewModel
                    {
                        MaChiTietGH = c.MaChiTietGH,
                        MaKH = c.MaKH,
                        MaSP = c.MaSP,
                        MaGoiTap = c.MaGoiTap,
                        MaLop = c.MaLop,
                        SoLuong = c.SoLuong,
                        DonGia = c.DonGia,
                        NgayThem = c.NgayThem,
                        GiaKhuyenMaiSP = c.MaSP != null ? c.SanPham.GiaKhuyenMai : null,
                        TenMonHang = c.MaSP != null ? c.SanPham.TenSP
                                     : c.MaGoiTap != null ? c.GoiTap.TenGoi
                                     : c.LopHoc.TenLop,
                        AnhDaiDienSP = c.MaSP != null ? c.SanPham.HINHANH.FirstOrDefault(a => a.IsMain.HasValue && a.IsMain.Value == true).Url : null,
                        SoLuongTon = c.MaSP != null ? c.SanPham.SoLuongTon : 0
                    }).ToList();

                return cart;
            }
            catch
            {
                throw;
            }
        }



    }
}