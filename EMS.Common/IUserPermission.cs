using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IUserPermission
    {
        bool CanAdd { get; set; }
        bool CanEdit { get; set; }
        bool CanDelete { get; set; }
        bool CanView { get; set; }
    }
}
