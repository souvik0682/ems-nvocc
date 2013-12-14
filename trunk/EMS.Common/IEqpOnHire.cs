using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    
    public interface IEqpOnHire
    {
        DateTime AddedOn { get; set; }
        int CompanyID { get; set; }
        DateTime? EditedOn { get; set; }
        int? FEUs { get; set; }
        int LeaseID { get; set; }
        long HireID { get; set; }
        string HireReference { get; set; }
        DateTime? HireReferenceDate { get; set; }
        string LinePrefix { get; set; }
        int LocationID { get; set; }
        string Narration { get; set; }
        int NVOCCID { get; set; }
        char OnOffHire { get; set; }
        DateTime? ReleaseRefDate { get; set; }
        string ReleaseRefNo { get; set; }
        long? ReturnedPortID { get; set; }
        DateTime? StatusRef { get; set; }
        int? TEUs { get; set; }
        int UserAdded { get; set; }
        int? UserLastEdited { get; set; }
        DateTime? ValidTill { get; set; }
        long? VesselID { get; set; }
        long? VoyageID { get; set; }
        IList<IEqpOnHireContainer> LstEqpOnHireContainer { get; set; }
    }
}
