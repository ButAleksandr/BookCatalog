using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookCatalog.ViewModel.DataTable
{
    [DataContract]
    public class DataTableResult<TResult>
    {
        public DataTableResult()
        {
            Data = new List<TResult>();
        }

        [DataMember(Name = "draw")]
        public long Draw { get; set; }

        [DataMember(Name = "recordsTotal")]
        public long RecordsTotal { get; set; }

        [DataMember(Name = "recordsFiltered")]
        public long RecordsFiltered { get; set; }

        [DataMember(Name = "data")]
        public List<TResult> Data { get; set; }
    }
}
