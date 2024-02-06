using Data_Library.IRepository;
using Model_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_ECommerce.Data;

namespace Data_Library.Repository
{
    public class CompanyRepositry : Repository<Company>, ICompany
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepositry(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
    }
}