using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IExportBL : ICommon
    {
        string BookingNumber { get; set; }
        long BookingId { get; set; }
        DateTime BookingDate { get; set; }
        string BLNumber { get; set; }
        long BLId { get; set; }
        DateTime BLDate { get; set; }
        string BookingParty { get; set; }
        string RefBookingNumber { get; set; }
        string Location { get; set; }
        int LocationId { get; set; }
        string Nvocc { get; set; }
        int NvoccId { get; set; }
        string Vessel { get; set; }
        int VesselId { get; set; }
        string Voyage { get; set; }
        int VoyageId { get; set; }
        string POR { get; set; }
        string POL { get; set; }
        string PORDesc { get; set; }
        string POLDesc { get; set; }
        string POD { get; set; }
        string FPOD { get; set; }
        string PODDesc { get; set; }
        string FPODDesc { get; set; }
        int fk_FPOD { get; set; }
        int NoOfBL { get; set; }
        string BLType { get; set; }
        int ShipmentMode { get; set; }
        string Commodity { get; set; }
        string Containers { get; set; }
        int BLIssuePlaceId { get; set; }
        string BLIssuePlace { get; set; }
        decimal NetWeight { get; set; }
        DateTime BLReleaseDate { get; set; }

        string ShipperName { get; set; }
        string Shipper { get; set; }
        string Consignee { get; set; }
        string ConsigneeName { get; set; }
        string NotifyPartyName { get; set; }
        string NotifyParty { get; set; }
        string BLClause { get; set; }
        string GoodDesc { get; set; }
        string MarksNumnbers { get; set; }
        int AgentId { get; set; }

        string TotalTEU { get; set; }
        string TotalFEU { get; set; }
        string TotalTon { get; set; }
        string TotalCBM { get; set; }

        string EdgeBLNumber { get; set; }
        string RefBLNumber { get; set; }
        bool BLStatus { get; set; }
    }
}
