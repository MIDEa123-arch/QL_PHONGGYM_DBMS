using QL_PHONGGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace QL_PHONGGYM.Repositories
{
    public class KhachHangRepository
    {
        private readonly QL_PHONGGYMEntities _context;

        public KhachHangRepository(QL_PHONGGYMEntities context)
        {
            _context = context;
        }
        public KhachHang ThongTinKH(int makh)
        {
            return _context.KhachHang.FirstOrDefault(kh => kh.MaKH == makh);

        }
        public LoaiKhachHang LoaiKh(int maloai)
        {
            return _context.LoaiKhachHang.FirstOrDefault(kh => kh.MaLoaiKH == maloai);

        }

        public DiaChi GetDiaChi(int makh)
        {
            var diaChiList = _context.DiaChi.Where(dc => dc.MaKH == makh).OrderByDescending(dc => dc.NgayThem).ToList();

            if (!diaChiList.Any())
                return null; 
            
            var diaChi = diaChiList.FirstOrDefault(dc => dc.LaDiaChiMacDinh);

            return diaChi ?? diaChiList.First();
        }


        public void ThemDiaChi(int makh, FormCollection form)
        {
            string tinh = form["province"];
            string huyen = form["district"];
            string xa = form["ward"];
            string diaChiCuThe = form["address"];

            if (string.IsNullOrEmpty(tinh) || string.IsNullOrEmpty(huyen) || string.IsNullOrEmpty(xa)) return;

            var diaChiTonTai = _context.DiaChi
                .FirstOrDefault(dc =>
                    dc.MaKH == makh &&
                    dc.TinhThanhPho == tinh &&
                    dc.QuanHuyen == huyen &&
                    dc.PhuongXa == xa &&
                    dc.DiaChiCuThe == diaChiCuThe);

            if (diaChiTonTai == null)
            {
                var diaChiMoi = new DiaChi
                {
                    MaKH = makh,
                    TinhThanhPho = tinh,
                    QuanHuyen = huyen,
                    PhuongXa = xa,
                    DiaChiCuThe = diaChiCuThe,
                    LaDiaChiMacDinh = false,
                    NgayThem = DateTime.Now
                };

                _context.DiaChi.Add(diaChiMoi);                
            }
            else
            {
                diaChiTonTai.NgayThem = DateTime.Now;
                _context.Entry(diaChiTonTai).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}