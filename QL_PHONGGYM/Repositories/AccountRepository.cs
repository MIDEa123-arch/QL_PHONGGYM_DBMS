using QL_PHONGGYM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QL_PHONGGYM.Models;

namespace QL_PHONGGYM.Repositories
{
    public class AccountRepository
    {
        private readonly QL_PHONGGYMEntities2 _context;

        public AccountRepository(QL_PHONGGYMEntities2 context)
        {
            _context = context;
        }

        public void Register(KhachHangRegisterViewModel model)
        {
            _context.sp_KhachHangDangKy(
                model.TenKH,
                model.GioiTinh,
                model.NgaySinh,
                model.SDT,
                model.Email,
                model.TenDangNhap,
                model.MatKhau
            );
        }
    }
}