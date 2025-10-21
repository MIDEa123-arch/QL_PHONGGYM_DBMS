using System;
using System.Collections.Generic;
using System.Linq;
using QL_PHONGGYM.Models;
using QL_PHONGGYM.ViewModel;

namespace QL_PHONGGYM.Repositories
{
    public class ListService
    {
        private readonly QL_PHONGGYMEntities2 _context;

        public ListService()
        {
            _context = new QL_PHONGGYMEntities2();
        }

        public List<ChuyenMon> GetList()
        {            
            var list = _context.ChuyenMon.Take(5).ToList();

            return list;
        }
    }
}
