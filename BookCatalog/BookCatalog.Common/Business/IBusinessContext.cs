using BookCatalog.Common.Bootstrap;
using BookCatalog.Common.Data;

namespace BookCatalog.Common.Business
{
    public interface IBusinessContext
    {
        IDataContext DataContext { get; }

        IServiceProviderFactory Factory { get; }
    }
}
