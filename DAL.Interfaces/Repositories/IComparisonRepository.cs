using System.Collections.Generic;
using DAL.Interfaces.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface IComparisonRepository
    {
        IEnumerable<Comparison> GetUserComparisons(string userName);

        void MigrateComparisons(string userName, string oldUserName);

        void Create(Comparison comparison);

        void Delete(int productId, string userName);

        void DeleteAll(string userName);
    }
}