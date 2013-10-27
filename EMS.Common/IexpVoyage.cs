using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
   public interface IexpVoyage
    {
        long VoyageID { get; set; }
        int CompanyID { get; set; }
        long VesselID { get; set; }
        int LocationID { get; set; }
        string VoyageNo { get; set; }
        string VesselName { get; set; }
        string TerminalName { get; set; }
        Int32 POL { get; set; }
        DateTime? ETD { get; set; }
        int TerminalID { get; set; }
        Int32 NextPortID { get; set; }
        string VIA { get; set; }
        string RotationNo { get; set; }
        DateTime? RotationDate { get; set; }
        DateTime? VesselCutOffDate { get; set; }
        DateTime? DocsCutOffDate { get; set; }
        string LoadPort { get; set; }
        string NextPort { get; set; }
        Int32 POD { get; set; }
        string AgentCode { get; set; }
        string LineCode { get; set; }
        string PCCNo { get; set; }
        DateTime? PCCDate { get; set; }
        DateTime? ETA { get; set; }
        DateTime? ETANextPort { get; set; }
        string VCNNo { get; set; }
        DateTime? SailDate { get; set; }
        DateTime? dtAdded { get; set; }
        DateTime? dtEdited { get; set; }
        long UserAdded { get; set; }
        long UserEdited { get; set; }
        bool VoyageStatus { get; set; }
    }
}
