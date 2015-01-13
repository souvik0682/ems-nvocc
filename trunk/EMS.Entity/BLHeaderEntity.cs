using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class BLHeaderEntity : IBLHeader
    {

        public int CompanyID
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public long BLID
        {
            get;
            set;
        }

        public int NVOCCID
        {
            get;
            set;
        }

        public string ImpLineBLNo
        {
            get;
            set;
        }

        public DateTime ImpLineBLDate
        {
            get;
            set;
        }

        public long ImpVesselID
        {
            get;
            set;
        }

        public long ImpVoyageID
        {
            get;
            set;
        }

        public string ItemLinePrefix
        {
            get;
            set;
        }

        public string ItemLineNo
        {
            get;
            set;
        }

        public string ItemSubLineNo
        {
            get;
            set;
        }

        public string IGMBLNumber
        {
            get;
            set;
        }

        public DateTime IGMBLDate
        {
            get;
            set;
        }

        public string LineBLType
        {
            get;
            set;
        }

        public string MotherVessel
        {
            get;
            set;
        }

        public long BLIssuePortID
        {
            get;
            set;
        }

        public long PlaceReceipt
        {
            get;
            set;
        }

        public long PortLoading
        {
            get;
            set;
        }

        public long PortDischarge
        {
            get;
            set;
        }

        public long PlaceDelivery
        {
            get;
            set;
        }

        public long FinalDestination
        {
            get;
            set;
        }

        public long StockLocationID
        {
            get;
            set;
        }

        public string BillOfEntryNo
        {
            get;
            set;
        }

        public string TranShipment
        {
            get;
            set;
        }

        public string CargoNature
        {
            get;
            set;
        }

        public string CargoMovement
        {
            get;
            set;
        }

        public string ItemType
        {
            get;
            set;
        }

        public string CargoType
        {
            get;
            set;
        }

        public double GrossWeight
        {
            get;
            set;
        }

        public int UnitOfWeight
        {
            get;
            set;
        }

        public double Volume
        {
            get;
            set;
        }

        public int UnitOfVolume
        {
            get;
            set;
        }

        public long NumberPackage
        {
            get;
            set;
        }

        public int UnitPackage
        {
            get;
            set;
        }

        public string PackageDetail
        {
            get;
            set;
        }

        public bool HazFlag
        {
            get;
            set;
        }

        public string UNOCode
        {
            get;
            set;
        }

        public string IMOCode
        {
            get;
            set;
        }

        public string ShipperInformation
        {
            get;
            set;
        }

        public string ConsigneeInformation
        {
            get;
            set;
        }

        public string NotifyPartyInformation
        {
            get;
            set;
        }

        public string MarksNumbers
        {
            get;
            set;
        }

        public string GoodDescription
        {
            get;
            set;
        }

        public int AddressCFSId
        {
            get;
            set;
        }

        public string TPBondNo
        {
            get;
            set;
        }

        public int CarrierID
        {
            get;
            set;
        }

        public string CACode
        {
            get;
            set;
        }

        public int AddressCHAID
        {
            get;
            set;
        }

        public string TransportMode
        {
            get;
            set;
        }

        public string MLOCode
        {
            get;
            set;
        }

        public bool DOLock
        {
            get;
            set;
        }

        public string DOLockReason
        {
            get;
            set;
        }

        public int DetentionFree
        {
            get;
            set;
        }

        public string DetentionSlabType
        {
            get;
            set;
        }

        public int DetChangedByID
        {
            get;
            set;
        }

        public int PGR_FreeDays
        {
            get;
            set;
        }

        public string FreightType
        {
            get;
            set;
        }

        public double FreigthToCollect
        {
            get;
            set;
        }

        public bool FreeOut
        {
            get;
            set;
        }

        public bool PartBL
        {
            get;
            set;
        }

        public bool HookPoint
        {
            get;
            set;
        }

        public bool TaxExemption
        {
            get;
            set;
        }

        public bool Reefer
        {
            get;
            set;
        }

        public bool ONBR
        {
            get;
            set;
        }

        public bool DocFact
        {
            get;
            set;
        }

        public string BLComment
        {
            get;
            set;
        }

        public int NoofTEU
        {
            get;
            set;
        }

        public int NoofFEU
        {
            get;
            set;
        }

        public DateTime dtAdded
        {
            get;
            set;
        }

        public DateTime dtEdited
        {
            get;
            set;
        }

        public long UserAdded
        {
            get;
            set;
        }

        public long UserEdited
        {
            get;
            set;
        }

        public bool RsStatus
        {
            get;
            set;
        }

        public long PortFrtPayableID
        {
            get;
            set;
        }

        public bool DPT
        {
            get;
            set;
        }

        public string CFSNomination
        {
            get;
            set;
        }

        public string ShipperName
        {
            get;
            set;
        }

        public string ConsigneeName
        {
            get;
            set;
        }

        public string NotifyName
        {
            get;
            set;
        }

        public bool Billing
        {
            get;
            set;
        }

        public double WaiverPercent
        {
            get;
            set;
        }

        public string WaiverLetterUpload
        {
            get;
            set;
        }

        public string WaiverType
        {
            get;
            set;
        }

        public int WaiverFk_UserID
        {
            get;
            set;
        }

        public DateTime WaiverDate
        {
            get;
            set;
        }

        public int SurveyorAddressID
        {
            get;
            set;
        }

        public List<IBLFooter> BLFooter
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        public string Commodity { get; set; }
        public string LineBLVesselDetail { get; set; }
        public string CANTo { get; set; }

        public int DPTId { get; set; }

        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public string PortOfLoading { get; set; }
        public string PortOfDischarge { get; set; }

        public Int64 CHAId { get; set; }
        public bool blClosed { get; set; } 

        public BLHeaderEntity()
        {
            this.BLFooter = new List<IBLFooter>();
        }

        public BLHeaderEntity(DataTableReader reader)
        {
            this.AddressCFSId = Convert.ToInt32(reader["AddressCFSId"]);
            this.AddressCHAID = Convert.ToInt32(reader["AddressCHAID"]);
            this.Billing = Convert.ToBoolean(reader["Billing"]);
            this.BillOfEntryNo = Convert.ToString(reader["BillOfEntryNo"]);
            this.BLComment = Convert.ToString(reader["BLComment"]);
            this.BLID = Convert.ToInt32(reader["BLID"]);
            this.BLIssuePortID = Convert.ToInt32(reader["BLIssuePortID"]);
            this.CACode = Convert.ToString(reader["CACode"]);
            this.CargoMovement = Convert.ToString(reader["CargoMovement"]);
            this.CargoNature = Convert.ToString(reader["CargoNature"]);
            this.CargoType = Convert.ToString(reader["CargoType"]);
            this.CFSNomination = Convert.ToString(reader["CFSNomination"]);
            this.CompanyID = Convert.ToInt32(reader["CompanyID"]);
            this.ConsigneeInformation = Convert.ToString(reader["ConsigneeInformation"]);
            this.ConsigneeName = Convert.ToString(reader["ConsigneeName"]);
            this.DetChangedByID = Convert.ToInt32(reader["DetChangedByID"]);
            this.DetentionFree = Convert.ToInt32(reader["DetentionFree"]);
            this.DetentionSlabType = Convert.ToString(reader["DetentionSlabType"]);
            //this.DocFact = Convert.ToBoolean(reader["DocFact"]);
            this.DOLock = Convert.ToBoolean(reader["DOLock"]);
            this.DOLockReason = Convert.ToString(reader["DOLockReason"]);
            this.DPT = Convert.ToBoolean(reader["DPT"]);
            this.dtAdded = Convert.ToDateTime(reader["dtAdded"]);
            //this.dtEdited = Convert.ToDateTime(reader["dtEdited"]);
            this.FinalDestination = Convert.ToInt32(reader["FinalDestination"]);
            this.FreeOut = Convert.ToBoolean(reader["FreeOut"]);
            this.FreightType = Convert.ToString(reader["FreightType"]);
            this.FreigthToCollect = Convert.ToInt32(reader["FreigthToCollect"]);
            this.GoodDescription = Convert.ToString(reader["GoodDescription"]);
            this.GrossWeight = Convert.ToInt32(reader["GrossWeight"]);
            this.HazFlag = Convert.ToBoolean(reader["HazFlag"]);
            //this.HookPoint = Convert.ToBoolean(reader["HookPoint"]);
            this.IGMBLDate = Convert.ToDateTime(reader["IGMBLDate"]);
            this.IGMBLNumber = Convert.ToString(reader["IGMBLNumber"]);
            this.IMOCode = Convert.ToString(reader["IMOCode"]);
            this.ImpLineBLDate = Convert.ToDateTime(reader["ImpLineBLDate"]);
            this.ImpLineBLNo = Convert.ToString(reader["ImpLineBLNo"]);
            this.ImpVesselID = Convert.ToInt32(reader["ImpVesselID"]);
            this.ImpVoyageID = Convert.ToInt32(reader["ImpVoyageID"]);
            this.ItemLineNo = Convert.ToString(reader["ItemLineNo"]);
            this.ItemLinePrefix = Convert.ToString(reader["ItemLinePrefix"]);
            this.ItemSubLineNo = Convert.ToString(reader["ItemSubLineNo"]);
            this.ItemType = Convert.ToString(reader["ItemType"]);
            this.LineBLType = Convert.ToString(reader["LineBLType"]);
            this.LocationID = Convert.ToInt32(reader["LocationID"]);
            this.MarksNumbers = Convert.ToString(reader["MarksNumbers"]);
            this.MLOCode = Convert.ToString(reader["MLOCode"]);
            //this.MotherVessel = Convert.ToString(reader["MotherVessel"]);
            this.NoofFEU = Convert.ToInt32(reader["NoofFEU"]);
            this.NoofTEU = Convert.ToInt32(reader["NoofTEU"]);
            this.NotifyName = Convert.ToString(reader["NotifyName"]);
            this.NotifyPartyInformation = Convert.ToString(reader["NotifyPartyInformation"]);
            this.NumberPackage = Convert.ToInt32(reader["NumberPackage"]);
            this.NVOCCID = Convert.ToInt32(reader["NVOCCID"]);
            this.ONBR = Convert.ToBoolean(reader["ONBR"]);
            this.PackageDetail = Convert.ToString(reader["PackageDetail"]);
            this.PartBL = Convert.ToBoolean(reader["PartBL"]);
            this.PGR_FreeDays = Convert.ToInt32(reader["PGR_FreeDays"]);
            this.PlaceDelivery = Convert.ToInt32(reader["PlaceDelivery"]);
            this.PlaceReceipt = Convert.ToInt32(reader["PlaceReceipt"]);
            this.PortDischarge = Convert.ToInt32(reader["PortDischarge"]);
            this.PortFrtPayableID = Convert.ToInt32(reader["PortFrtPayableID"]);
            this.PortLoading = Convert.ToInt32(reader["PortLoading"]);
            this.Reefer = Convert.ToBoolean(reader["Reefer"]);
            this.RsStatus = Convert.ToBoolean(reader["RsStatus"]);
            this.ShipperInformation = Convert.ToString(reader["ShipperInformation"]);
            this.ShipperName = Convert.ToString(reader["ShipperName"]);
            this.StockLocationID = Convert.ToInt32(reader["StockLocationID"]);
            this.SurveyorAddressID = Convert.ToInt32(reader["SurveyorAddressID"]);
            this.TaxExemption = Convert.ToBoolean(reader["TaxExemption"]);
            //this.TPBondNo = Convert.ToString(reader["TPBondNo"]);
            this.TranShipment = Convert.ToString(reader["TranShipment"]);
            this.TransportMode = Convert.ToString(reader["TransportMode"]);
            this.UnitOfVolume = Convert.ToInt32(reader["UnitOfVolume"]);
            this.UnitOfWeight = Convert.ToInt32(reader["UnitOfWeight"]);
            this.UnitPackage = Convert.ToInt32(reader["UnitPackage"]);
            this.UNOCode = Convert.ToString(reader["UNOCode"]);
            this.UserAdded = Convert.ToInt32(reader["UserAdded"]);
            //this.UserEdited = Convert.ToInt32(reader["UserEdited"]);
            this.Volume = Convert.ToInt32(reader["Volume"]);
            this.WaiverDate = Convert.ToDateTime(reader["WaiverDate"]);
            this.WaiverFk_UserID = Convert.ToInt32(reader["WaiverFk_UserID"]);
            this.WaiverLetterUpload = Convert.ToString(reader["WaiverLetterUpload"]);
            this.WaiverPercent = Convert.ToInt32(reader["WaiverPercent"]);
            this.WaiverType = Convert.ToString(reader["WaiverType"]);
            this.Commodity = Convert.ToString(reader["Commodity"]);
            this.LineBLVesselDetail = Convert.ToString(reader["LineBLVesselDetail"]);
            this.CANTo = Convert.ToString(reader["CANTo"]);
            this.DPTId = Convert.ToInt32(reader["DPTId"]);

            if (ColumnExists(reader, "CarrierID"))
                if (reader["CarrierID"] != DBNull.Value)
                    this.CarrierID = Convert.ToInt32(reader["CarrierID"]);

            if (ColumnExists(reader, "PortOfLoading"))
                this.PortOfLoading = Convert.ToString(reader["PortOfLoading"]);

            if (ColumnExists(reader, "PortOfDischarge"))
                this.PortOfDischarge = Convert.ToString(reader["PortOfDischarge"]);

            if (ColumnExists(reader, "VoyageNo"))
                this.VoyageNo = Convert.ToString(reader["VoyageNo"]);

            if (ColumnExists(reader, "VesselName"))
                this.VesselName = Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "CHAId"))
                if (reader["CHAId"] != DBNull.Value)
                    this.CHAId = Convert.ToInt64(reader["CHAId"]);

            if (ColumnExists(reader, "blClosed"))
                if (reader["blClosed"] != DBNull.Value)
                    blClosed = Convert.ToBoolean(reader["blClosed"]);
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
