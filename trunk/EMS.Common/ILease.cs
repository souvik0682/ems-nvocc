using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ILease : ICommon
    {
        int LeaseID { get; set; }
        bool Action { get; set; }
        string LeaseNo { get; set; }
        DateTime LeaseDate { get; set; }
        string EmptyYard { get; set; }
        string LineName { get; set; }
        int LocationID { get; set; }
        string LocationName { get; set; }
        bool LeaseStatus { get; set; }
        int LinerID { get; set; }
        DateTime LeaseValidTill { get; set; }
        string Description { get; set; }
        int fk_EmptyYardID { get; set; }
        string LeaseCompany { get; set; }
    }
}
