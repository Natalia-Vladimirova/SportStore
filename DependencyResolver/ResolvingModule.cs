using System.Data.Entity;
using Ninject;
using Ninject.Web.Common;
using DAL;
using DAL.Interfaces.Repositories;
using DAL.Repositories;

namespace DependencyResolver
{
    public static class ResolvingModule
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            kernel.Bind<DbContext>().To<StoreDbContext>().InRequestScope();
           
            kernel.Bind<ICartRepository>().To<CartRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IComparisonRepository>().To<ComparisonRepository>();
            kernel.Bind<IOrderDetailRepository>().To<OrderDetailRepository>();
            kernel.Bind<IOrderRepository>().To<OrderRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}
