using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EMS.Entity.Report
{
    public class ImpRegisterEntity
    {
        #region Public Properties

        public string Location { get; set; }

        public string Line { get; set; }

        public Int64 BLId { get; set; }

        public string BLNo { get; set; }

        public DateTime BLDate { get; set; }

        public string ItemLineNo { get; set; }

        public string IGMBLNo { get; set; }

        public string PortLoading { get; set; }

        public string PortDischarge { get; set; }

        public string FinalDestination { get; set; }

        public DateTime? DischargeDate { get; set; }

        public string CargoMovement { get; set; }

        public decimal? GrossWeight { get; set; }

        public string WeightUnit { get; set; }

        public decimal? Volume { get; set; }

        public string VolumeUnit { get; set; }

        public Int64 NumberPackage { get; set; }

        public string PackageUnit { get; set; }

        public string ConsigneeInformation { get; set; }

        public string NotifyPartyInformation { get; set; }

        public string MarksNumbers { get; set; }

        public string GoodsDescription { get; set; }

        public string AddressCFS { get; set; }

        public string ICD { get; set; }

        public string TPBondNo { get; set; }

        public string CACode { get; set; }

        public int PGRFreeDays { get; set; }

        public int? TEU { get; set; }

        public int? FEU { get; set; }

        public int? TotalTEU { get; set; }

        public bool? RsStatus { get; set; }

        //For footer
        public string ContainerNo { get; set; }

        public string ContainerSize { get; set; }

        public string ContainerType { get; set; }

        public string SealNo { get; set; }

        public decimal? PayLoad { get; set; }

        #endregion

        #region BLHeader

        //public int LocationId { get; set; }

        //public string Location { get; set; }

        //public int LineId { get; set; }

        //public string Line { get; set; }

        //public Int64 BLId { get; set; }

        //public string BLNo { get; set; }

        //public DateTime BLDate { get; set; }

        //public string Voyage { get; set; }

        //public string Vessel { get; set; }

        //public string ItemLinePrefix { get; set; }

        //public string ItemLineNo { get; set; }

        //public string ItemSubLineNo { get; set; }

        //public string IGMBLNo { get; set; }

        //public DateTime IGMBLDate { get; set; }

        //public string LineBLType { get; set; }

        //public string MotherVessel { get; set; }

        //public string BLIssuePort { get; set; }

        //public string PlaceReceipt { get; set; }

        //public string PortLoading { get; set; }

        //public string PortDischarge { get; set; }

        //public string PlaceDelivery { get; set; }

        //public string FinalDestination { get; set; }

        //public string StockLocation { get; set; }

        //public string BillOfEntryNo { get; set; }

        //public string TranShipment { get; set; }

        //public string CargoNature { get; set; }

        //public string CargoMovement { get; set; }

        //public string ItemType { get; set; }

        //public string CargoType { get; set; }

        //public decimal? GrossWeight { get; set; }

        //public string WeightUnit { get; set; }

        //public decimal? Volume { get; set; }

        //public string VolumeUnit { get; set; }

        //public Int64 NumberPackage { get; set; }

        //public string PackageUnit { get; set; }

        //public string PackageDetail { get; set; }

        //public string HazardousCargo { get; set; }

        //public string UNOCode { get; set; }

        //public string IMOCode { get; set; }

        //public string ShipperInformation { get; set; }

        //public string ConsigneeInformation { get; set; }

        //public string NotifyPartyInformation { get; set; }

        //public string MarksNumbers { get; set; }

        //public string GoodsDescription { get; set; }

        //public string AddressCFS { get; set; }

        //public string TPBondNo { get; set; }

        //public string CACode { get; set; }

        //public string AddressCHA { get; set; }

        //public string TransportMode { get; set; }

        //public string MLOCode { get; set; }

        //public string DOLock { get; set; }

        //public string DOLockReason { get; set; }

        //public int DetentionFree { get; set; }

        //public string DetentionSlabType { get; set; }

        //public string DetentionChangedBy { get; set; }

        //public int PGRFreeDays { get; set; }

        //public string FreightType { get; set; }

        //public decimal? FreightToCollect { get; set; }

        //public bool FreeOut { get; set; }

        //public bool PartBL { get; set; }

        //public bool HookPoint { get; set; }

        //public bool TaxExemption { get; set; }

        //public bool Reefer { get; set; }

        //public bool? ONBR { get; set; }

        //public bool DocFact { get; set; }

        //public string BLComment { get; set; }

        //public int? TEU { get; set; }

        //public int? FEU { get; set; }

        //public int? TotalTEU { get; set; }

        //public bool? RsStatus { get; set; }

        //public string PortFrtPayable { get; set; }

        //public bool DPT { get; set; }

        //public string CFSNomination { get; set; }

        #endregion

        #region BLFooter

        //public Int64 BLFooterID { get; set; }

        //public string ContainerNo { get; set; }

        //public string ContainerSize { get; set; }

        //public string ContainerType { get; set; }

        //public string SealNo { get; set; }

        //public string Comodity { get; set; }

        //public decimal? ContainerGrossWeight { get; set; }

        //public decimal Package { get; set; }

        //public decimal CargoWeight { get; set; }

        //public string SOC { get; set; }

        //public string ISOCode { get; set; }

        //public decimal? Temperature { get; set; }

        //public string TemperatureUnit { get; set; }

        //public string CustomSeal { get; set; }

        //public string CAgent { get; set; }

        //public decimal? TemperatureMax { get; set; }

        //public decimal? TemperatureMin { get; set; }

        //public string PCSTemp { get; set; }

        //public string DIMCode { get; set; }

        //public decimal? ODLength { get; set; }

        //public decimal? ODWidth { get; set; }

        //public decimal? ODHeight { get; set; }

        //public string Cargo { get; set; }

        //public string Stowage { get; set; }

        //public string CallNo { get; set; }

        //public string IMCO { get; set; }

        //public string MovementDescription { get; set; }

        //public DateTime DateLastMoved { get; set; }

        #endregion
    }
}