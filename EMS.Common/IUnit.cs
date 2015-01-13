using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IUnit : ICommon
    {
        long UnitTypeID { get; set; }
        string UnitName { get; set; }
        string Prefix { get; set; }
        bool UnitStatus { get; set; }
        string UnitType { get; set; }
    }
}
