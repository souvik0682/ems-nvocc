using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IImportHaulage : ICommon
    {
        int HaulageChgID { get; set; }
        string LocationFrom { get; set; }
        string LFCode { get; set; }
        string LocationTo { get; set; }
        string LinerName { get; set; }
        string LTCode { get; set; }
        string ContainerSize { get; set; }
        decimal WeightFrom { get; set; }
        decimal WeightTo { get; set; }
        decimal HaulageRate { get; set; }
        bool HaulageStatus { get; set; }
        int LinerID { get; set; }
        DateTime EffectDate { get; set; }
    }
}
