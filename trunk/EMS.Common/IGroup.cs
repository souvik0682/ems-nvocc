using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IGroup
    {
        int GroupID { get; set; }
        string GroupName { get; set; }
        string GroupAddress { get; set; }
        int UserID { get; set; }
        bool GroupStatus { get; set; }
    }
}
