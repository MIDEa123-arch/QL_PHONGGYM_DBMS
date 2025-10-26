using QL_PHONGGYM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace QL_PHONGGYM.Repositories
{
    public class CartRepository
    {
        private readonly QL_PHONGGYMEntities2 _context;

        public CartRepository(QL_PHONGGYMEntities2 context)
        {
            _context = context;
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

    }
}