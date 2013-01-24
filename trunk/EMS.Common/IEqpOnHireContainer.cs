using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Common
{
    
    public interface IEqpOnHireContainer
    {
        DateTime? ActualOnHireDate { get; set; }
        DateTime AddedOn { get; set; }
        string CntrSize { get; set; }
        string ContainerNo { get; set; }
        int? ContainerTypeID { get; set; }
        DateTime? EditedOn { get; set; }
        long HireContainerID { get; set; }
        long? HireID { get; set; }
        DateTime? IGMDate { get; set; }
        long? IGMNo { get; set; }
        string LGNo { get; set; }
        long? MovementOptID { get; set; }
        int? NVOCCID { get; set; }
        long? ReturnPortID { get; set; }
        int UserAdded { get; set; }
        int? UserLastEdited { get; set; }
        DateTime? ValidTill { get; set; }
        IList<IEqpOnHireContainer> GetEqpOnHireContainerList(DataTable dt);
    }
}
