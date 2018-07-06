using System.Collections.Generic;

namespace BookCatalog.ViewModel.DataTable
{
    public class DataTableVM
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public ColumnVM[] Columns { get; set; }

        public SearchVM Search { get; set; }

        public List<OrderVM> Order { get; set; }
    }
}
