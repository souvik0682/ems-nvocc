using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IMenu
    {
        int MenuID { get; set; }
        int MainID { get; set; }
        int SubID { get; set; }
        int SubSubID { get; set; }
        string MenuName { get; set; }
    }
}
