namespace BookCatalog.Common.Data
{
    public interface IDataContext
    {
        /// <summary>
        /// Gets a database connection string
        /// </summary>
        string DbConnection { get; }

        /// <summary>
        /// Saves all unsaved data into context
        /// </summary>
        /// <remarks>
        /// <see cref="TransactedFlow"/> calls this method before complete
        /// </remarks>
        void Flush();
    }
}
