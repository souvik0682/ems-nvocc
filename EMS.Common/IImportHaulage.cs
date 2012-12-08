using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IImportHaulage : ICommon
    {
        int HaulageChgID { get; set; }
        int LocationFrom { get; set; }
        int LocationTo { get; set; }
        string ContainerSize { get; set; }
        decimal WeightFrom { get; set; }
        decimal WeightTo { get; set; }
        decimal HaulageRate { get; set; }
        bool HaulageStatus { get; set; }       
    }
}
