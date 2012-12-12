using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IRoleMenu
    {
        int MenuAccessID { get; set; }
        int CompanyID { get; set; }
        int MenuID { get; set; }
        int MainID { get; set; }
        int SubID { get; set; }
        int SubSubID { get; set; }
        string MenuName { get; set; }
        bool CanAdd { get; set; }
        bool CanEdit { get; set; }
        bool CanDelete { get; set; }
        bool CanView { get; set; }
    }
}
