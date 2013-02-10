using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IBLHeader : ICommon
    {
        int CompanyID { get; set; }
        int LocationID { get; set; }
        Int64 BLID { get; set; }
        int NVOCCID { get; set; }
        string ImpLineBLNo { get; set; }
        DateTime ImpLineBLDate { get; set; }
        Int64 ImpVesselID { get; set; }
        Int64 ImpVoyageID { get; set; }
        string ItemLinePrefix { get; set; }
        string ItemLineNo { get; set; }
        string ItemSubLineNo { get; set; }
        string IGMBLNumber { get; set; }
        DateTime IGMBLDate { get; set; }
        string LineBLType { get; set; }
        string MotherVessel { get; set; }
        Int64 BLIssuePortID { get; set; }
        Int64 PlaceReceipt { get; set; }
        Int64 PortLoading { get; set; }
        Int64 PortDischarge { get; set; }
        Int64 PlaceDelivery { get; set; }
        Int64 FinalDestination { get; set; }
        Int64 StockLocationID { get; set; }
        string BillOfEntryNo { get; set; }
        string TranShipment { get; set; }
        string CargoNature { get; set; }
        string CargoMovement { get; set; }
        string ItemType { get; set; }
        string CargoType { get; set; }
        double GrossWeight { get; set; }
        int UnitOfWeight { get; set; }
        double Volume { get; set; }
        int UnitOfVolume { get; set; }
        Int64 NumberPackage { get; set; }
        int UnitPackage { get; set; }
        string PackageDetail { get; set; }
        bool HazFlag { get; set; }
        string UNOCode { get; set; }
        string IMOCode { get; set; }
        string ShipperInformation { get; set; }
        string ConsigneeInformation { get; set; }
        string NotifyPartyInformation { get; set; }
        string MarksNumbers { get; set; }
        string GoodDescription { get; set; }
        int AddressCFSId { get; set; }
        string TPBondNo { get; set; }
        string CACode { get; set; }
        int AddressCHAID { get; set; }
        string TransportMode { get; set; }
        string MLOCode { get; set; }
        bool DOLock { get; set; }
        string DOLockReason { get; set; }
        int DetentionFree { get; set; }
        string DetentionSlabType { get; set; }
        int DetChangedByID { get; set; }
        int PGR_FreeDays { get; set; }
        string FreightType { get; set; }
        double FreigthToCollect { get; set; }
        bool FreeOut { get; set; }
        bool PartBL { get; set; }
        bool HookPoint { get; set; }
        bool TaxExemption { get; set; }
        bool Reefer { get; set; }
        bool ONBR { get; set; }
        bool DocFact { get; set; }
        string BLComment { get; set; }
        int NoofTEU { get; set; }
        int NoofFEU { get; set; }
        DateTime dtAdded { get; set; }
        DateTime dtEdited { get; set; }
        Int64 UserAdded { get; set; }
        Int64 UserEdited { get; set; }
        bool RsStatus { get; set; }
        Int64 PortFrtPayableID { get; set; }
        bool DPT { get; set; }
        string CFSNomination { get; set; }
        string ShipperName { get; set; }
        string ConsigneeName { get; set; }
        string NotifyName { get; set; }
        bool Billing { get; set; }
        double WaiverPercent { get; set; }
        string WaiverLetterUpload { get; set; }
        string WaiverType { get; set; }
        int WaiverFk_UserID { get; set; }
        DateTime WaiverDate { get; set; }
        int SurveyorAddressID { get; set; }
        string Commodity { get; set; }
        string LineBLVesselDetail { get; set; }
        string CANTo { get; set; }
        int DPTId { get; set; }

        string VesselName { get; set; }
        string VoyageNo { get; set; }
        string PortOfLoading { get; set; }
        string PortOfDischarge { get; set; }
        
        List<IBLFooter> BLFooter { get; set; }
    }
}
