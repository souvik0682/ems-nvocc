using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IFwLine : ICommon
    {
        int LineID { get; set; }
        string LineName { get; set; }
        bool LineActive { get; set; }
        string LineType { get; set; }
        string Prefix { get; set; }
    }
}
