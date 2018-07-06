namespace BookCatalog.ViewModel.DataTable
{
    public class ColumnVM
    {
        public string Data { get; set; }

        public string Name { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public SearchVM Search { get; set; }
    }
}
