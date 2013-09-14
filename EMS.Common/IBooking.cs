using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IBooking : ICommon
    {
        int BookingID { get; set; }
        bool Action { get; set; }
        string NVOCC { get; set; }
        string FPOD { get; set; }
        string POR { get; set; }
        string POL { get; set; }
        string POD { get; set; }
        string FPODID { get; set; }
        string PODID { get; set; }
        string POLID { get; set; }
        string PORID { get; set; }
        int CustID { get; set; }
        int LocationID { get; set; }
        int VesselID { get; set; }
        Int32 VoyageID { get; set; }
        int MainLineVesselID { get; set; }
        Int32 MainLineVoyageID { get; set; }
        string MainLineVesselName { get; set; }
        string BookingNo { get; set; }
        DateTime BookingDate { get; set; }
        string RefBookingNo {get; set;}
        DateTime RefBookingDate { get; set; }
        bool HazCargo { get; set; }
        string IMO { get; set; }
        string UNO { get; set; }
        string Commodity { get; set; }
        string Accounts { get; set; }
        char ShipmentType { get; set; }
        bool BLThruApp { get; set; }
        decimal GrossWt { get; set; }
        decimal CBM { get; set; }
        int TotalFEU { get; set; }
        int TotalTEU { get; set; }
        bool Reefer { get; set; }
        decimal TempMax { get; set; }
        decimal TempMin { get; set; }
        bool AcceptBooking { get; set; }
        bool BookingStatus { get; set; }
        int ServicesID { get; set; }
        string Services { get; set; }
        int LinerID { get; set; }

    }
}
