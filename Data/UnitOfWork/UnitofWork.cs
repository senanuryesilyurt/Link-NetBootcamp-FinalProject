using Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Data.UnitOfWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DbContext _context;
        public UnitofWork(PostgreSqlContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
