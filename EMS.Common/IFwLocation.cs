using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IFwLocation : ICommon
    {
        IAddress LocAddress { get; set; }
        string LocName { get; set; }
        string Abbreviation { get; set; }
        string Phone { get; set; }
        int locID { get; set; }
        bool IsActive { get; set; }
        int DefaultLocation { get; set; }
        string CityPin { get; set; }
    }
}
