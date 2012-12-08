using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IMenu : IBase<int>
    {
        int MainID { get; set; }
        int SubID { get; set; }
        int SubSubID { get; set; }
    }
}
