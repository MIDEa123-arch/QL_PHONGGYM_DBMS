using QL_PHONGGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QL_PHONGGYM.ViewModel;

namespace QL_PHONGGYM.Repositories
{
    public class ProductRepository
    {
        private readonly QL_PHONGGYMEntities _context;

        public ProductRepository(QL_PHONGGYMEntities context)
        {
            _context = context;
        }
         public List<ChuyenMon> GetChuyenMons()
        {            
            var list = _context.ChuyenMon.Take(5).ToList();

            return list;
        }
        public List<LoaiSanPham> GetLoaiSanPhams()
        {
            return _context.LoaiSanPham.ToList();
        }

        public List<string> GetHangsByLoai()
        {
            return _context.SanPham.Where(sp => sp.Hang != null).Select(sp => sp.Hang).Distinct().ToList();
        }

        public List<string> GetXuatSu()
        {
            return _context.SanPham.Where(sp => sp.XuatXu != null).Select(sp => sp.XuatXu).Distinct().ToList();
        }

        public List<GoiTap> GetGoiTaps()
        {
            return _context.GoiTap.Where(sp => sp.Gia == 399000.00m || sp.Gia == 10000000.00m).ToList();
        }

        public List<SanPhamViewModel> GetSanPhams()
        {
            var list = (from sp in _context.SanPham
                        join ha in _context.HINHANH on sp.MaSP equals ha.MaSP into haGroup
                        select new SanPhamViewModel
                        {
                            MaSP = sp.MaSP,
                            TenSP = sp.TenSP,
                            LoaiSP = sp.LoaiSanPham.TenLoaiSP,
                            DonGia = sp.DonGia,
                            SoLuongTon = sp.SoLuongTon,
                            GiaKhuyenMai = (decimal)sp.GiaKhuyenMai,
                            Hang = sp.Hang,
                            XuatXu = sp.XuatXu,
                            BaoHanh = sp.BaoHanh,
                            MoTaChung = sp.MoTaChung,
                            MaTaChiTiet = sp.MoTaChiTiet,
                            UrlHinhAnhChinh = haGroup.FirstOrDefault(h => h.IsMain == true).Url,
                            UrlHinhAnhsPhu = haGroup.Where(h => h.IsMain == false)
                                                    .Select(h => h.Url)
                                                    .ToList()
                        }).ToList();

            return list;
        }


    }
}