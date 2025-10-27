﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_PHONGGYM.ViewModel
{
    public class GioHangViewModel
    {
        public int MaChiTietGH { get; set; }
        public int MaKH { get; set; }
        public int? MaSP { get; set; }
        public int? MaGoiTap { get; set; }
        public int? MaLop { get; set; }
        public int? SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayThem { get; set; }
        public string TenMonHang { get; set; }
        public string AnhDaiDienSP { get; set; }
    }
}