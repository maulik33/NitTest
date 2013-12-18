using System;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Utilities
{
    public class SortHelper
    {
        private const string ASCENDING = "ASC";
        private const string DESCENDING = "DESC";

        public static SortInfo Parse(string sortingConfiguration)
        {
            string[] configuration = sortingConfiguration.Split('|');
            if (configuration.Length != 2)
            {
                throw new InvalidOperationException(string.Format("Cannot Parse {0} using SortingHelper.Parse method.", sortingConfiguration));
            }

            return Parse(configuration[0], configuration[1]);
        }

        public static SortInfo Parse(string sortExpression, string sortDirection)
        {
            return new SortInfo()
            {
                SortExpression = sortExpression,
                Direction = (string.Compare(sortDirection, "DESC", true) == 0) ? SortOrder.Descending : SortOrder.Ascending
            };
        }

        public static string Parse(SortOrder direction)
        {
            return (direction == SortOrder.Descending) ? DESCENDING : ASCENDING;
        }

        public static string Compare(string sortExpression, string sortDirection, string currentConfiguration)
        {
            SortInfo comparisionResult = new SortInfo();

            SortInfo newInfo = SortHelper.Parse(sortExpression, sortDirection);
            SortInfo currentInfo = SortHelper.Parse(currentConfiguration);

            comparisionResult.SortExpression = newInfo.SortExpression;
            if (string.Compare(newInfo.SortExpression, currentInfo.SortExpression, true) == 0)
            {
                comparisionResult.Direction = ToggleSortOrder(currentInfo.Direction);
            }
            else
            {
                comparisionResult.Direction = SortOrder.Ascending;
            }

            return comparisionResult.ToString();
        }

        public static SortOrder ToggleSortOrder(SortOrder currentOrder)
        {
            return (currentOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
        }
    }
}
