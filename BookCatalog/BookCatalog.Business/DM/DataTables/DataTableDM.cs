using BookCatalog.ViewModel.DataTable;
using System;
using System.Linq;

namespace BookCatalog.Business.DM.DataTables
{
    static internal class DataTableDM
    {
        public static string BuildSelectQuery(string tableName, DataTableVM dataTableVM)
        {
            var reulstQuery = SelectPart(dataTableVM)
                + FromPart(tableName)
                + SearchPart(dataTableVM)
                + OrderByPart(dataTableVM)
                + RangePart(dataTableVM);

            return reulstQuery;
        }

        public static string BuildCountQuery(string tableName, DataTableVM dataTableVM)
        {
            var reulstQuery = CountPart()
                + FromPart(tableName)
                + SearchPart(dataTableVM);

            return reulstQuery;
        }

        public static string BuildTotalCount(string tableName, DataTableVM dataTableVM)
        {
            var reulstQuery = CountPart()
                + FromPart(tableName);

            return reulstQuery;
        }

        private static string FromPart(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception("Table name is null or empry.");
            }

            return $"FROM {tableName}" + Environment.NewLine;
        }

        private static string OrderByPart(DataTableVM dataTableVM)
        {
            OrderVM order = dataTableVM.Order.FirstOrDefault();

            return (order == null) || (dataTableVM.Columns.Length <= order.Column)
                ? string.Empty
                : $"ORDER BY {dataTableVM.Columns[order.Column].Name} {order.Dir.ToUpper()}" + Environment.NewLine;
        }

        private static string SelectPart(DataTableVM dataTableVM)
        {
            if (dataTableVM.Columns == null || dataTableVM.Columns.Length == 0)
            {
                throw new Exception("Table must contain at least one column.");
            }

            var tab = new string(' ', 5);
            var separator = "," + Environment.NewLine;

            string[] targetFields = dataTableVM.Columns
                .Where(x => !string.IsNullOrEmpty(x.Name))
                .Select(x => $"{tab}[{x.Name}]")
                .ToArray();
            string selectPart = $"SELECT " + Environment.NewLine 
                + string.Join(separator, targetFields) + Environment.NewLine;

            return selectPart;
        }

        private static string RangePart(DataTableVM dataTableVM)
        {
            if (dataTableVM.Length <= 0)
            {
                throw new Exception("Table page contain at least one row.");
            }

            return $"OFFSET {dataTableVM.Start} ROWS" + Environment.NewLine + $"FETCH NEXT {dataTableVM.Length} ROWS ONLY";
        }

        private static string CountPart()
        {
            return "SELECT COUNT(*)" + Environment.NewLine;
        }

        private static string SearchPart(DataTableVM dataTableVM)
        {
            if (dataTableVM.Columns == null || dataTableVM.Columns.Length == 0)
            {
                throw new Exception("Table must contain at least one column.");
            }

            var tab = new string(' ', 5);
            var separator = Environment.NewLine + "OR ";

            string[] targetFields = dataTableVM.Columns
                    .Where(x => !string.IsNullOrEmpty(x.Name))
                    .Select(x => $"{tab}[{x.Name}] LIKE '%{dataTableVM.Search.Value}%'")
                    .ToArray();
            string selectPart = $"WHERE " + Environment.NewLine 
                + string.Join(separator, targetFields) + Environment.NewLine;

            return string.IsNullOrEmpty(dataTableVM.Search.Value) 
                || string.IsNullOrWhiteSpace(dataTableVM.Search.Value)
                    ? string.Empty 
                    : selectPart;
        }
    }
}
