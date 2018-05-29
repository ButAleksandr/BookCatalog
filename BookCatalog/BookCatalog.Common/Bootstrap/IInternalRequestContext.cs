using BookCatalog.Common.Data;

namespace BookCatalog.Common.Bootstrap
{
    /// <summary>
    /// Represents object provides access to current operation context
    /// </summary>
    public interface IInternalRequestContext
    {
        /// <summary>
        /// Gets instance of ORM entity
        /// </summary>
        IDataContext DataContext { get; }

        /// <summary>
        /// Gets an instance of domain model /repository factory
        /// </summary>
        IServiceProviderFactory Factory { get; }
    }
}
