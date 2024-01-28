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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
        }
        public ICategoryRepository Category { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
