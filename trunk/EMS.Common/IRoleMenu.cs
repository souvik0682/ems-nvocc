using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IRoleMenu : IMenu, IRole
    {
        int MenuAccessID { get; set; }
        int CompanyID { get; set; }
        bool CanAdd { get; set; }
        bool CanEdit { get; set; }
        bool CanDelete { get; set; }
        bool CanView { get; set; }
    }
}
