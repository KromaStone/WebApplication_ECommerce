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
            Covertype = new CoverTypeRepository(_context);
            SpCall = new SpCall(_context);
            Product = new ProductRepository(_context);
            Company = new CompanyRepositry(_context);
            OrderHeader = new OrderheaderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);

        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository Covertype { get; private set; }
        public ISpCall SpCall { get; private set; }
        public IProductRepository Product { get; private set; }

        public ICompany Company { get; private set; }

        public IOrderHeader OrderHeader { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        ICoverTypeRepository IUnitOfWork.CoverType => throw new NotImplementedException();

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}