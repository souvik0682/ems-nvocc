using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ISearchCriteria
    {
        string SortExpression { get; set; }
        string SortDirection { get; set; }
        string StringOption1 { get; set; }
        string StringOption2 { get; set; }
        string StringOption3 { get; set; }
        string StringOption4 { get; set; }
        DateTime? Date { get; set; }
        IList<string> StringParams { get; set; }
        int PartyID { get; set; }
        string LocAbbr { get; set; }
        // string Phone { get; set; }
        string PartyName { get; set; }
        int LineID { get; set; }
        string LineName { get; set; }
        
        // string PartyType { get; set; }

        int PageIndex
        {
            get;
            set;
        }

        int PageSize
        {
            get;
            set;
        }
    }
}
