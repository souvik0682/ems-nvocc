using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
   public interface IIGMReport
    {
       string ShipLine { get; set; }
       string Ship { get; set; }
       string VoyageNo { get; set; }

    }
}
