using System.Data.Entity;
using DAL.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context?.SaveChanges();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}