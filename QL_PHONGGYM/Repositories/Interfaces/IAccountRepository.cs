using QL_PHONGGYM.Models;
using QL_PHONGGYM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        bool CusRegister(KhachHangRegisterViewModel model);
        KhachHang CusLogin(string tenDangNhap, string matKhau);
    }
}
