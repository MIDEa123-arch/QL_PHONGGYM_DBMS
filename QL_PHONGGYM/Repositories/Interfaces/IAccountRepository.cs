using QL_PHONGGYM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM.Repositories.Interfaces
{
    internal interface IAccountRepository
    {
        void Register(KhachHangRegisterViewModel model);
    }
}
