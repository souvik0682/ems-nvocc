using System;
namespace EMS.Common
{
   public interface IContainerType
    {
        DateTime? AddedOn { get; set; }
        int? CntrHTFT { get; set; }
        int? CntrHTIN { get; set; }
        string ContainerAbbr { get; set; }
        string CotainerDesc { get; set; }
        DateTime? EditedOn { get; set; }
        string ISO20 { get; set; }
        string ISO40 { get; set; }
        int ContainerTypeID { get; set; }
        int? TareWeight20 { get; set; }
        int? TareWeight40 { get; set; }
        int? UserAdded { get; set; }
        int? UserLastEdited { get; set; }
    }
}
