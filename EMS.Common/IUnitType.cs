using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IUnitType : ICommon
    {
        long UnitTypeID { get; set; }
        int CompanyID { get; set; }
        string UnitName { get; set; }
        string Prefix { get; set; }
        bool UnitStatus { get; set; }
        
    }
}
