using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity.Report
{
    public class ImpBLChkLstEntity
    {
        #region Public Properties

        public string IGMBLNumber
        {
            get;
            set;
        }

        public string ImpLineBLNo
        {
            get;
            set;
        }

        public string MotherVessel
        {
            get;
            set;
        }

        public string ItemLineNo
        {
            get;
            set;
        }

        public string TransportMode
        {
            get;
            set;
        }

        public string PortLoading
        {
            get;
            set;
        }

        public string FinalDestination
        {
            get;
            set;
        }

        public string HazCargo
        {
            get;
            set;
        }

        public string CargoType
        {
            get;
            set;
        }

        public string PackageDetail
        {
            get;
            set;
        }

        public string PackUnit
        {
            get;
            set;
        }

        public decimal? Volume
        {
            get;
            set;
        }

        public string VolUnit
        {
            get;
            set;
        }

        public string FreightType
        {
            get;
            set;
        }

        public string CargoNature
        {
            get;
            set;
        }

        public string TPBondNo
        {
            get;
            set;
        }

        public string FreeOut
        {
            get;
            set;
        }

        public string CACode
        {
            get;
            set;
        }

        public string IGMBLDate
        {
            get;
            set;
        }

        public string LineBLType
        {
            get;
            set;
        }

        public string ItemSubLineNo
        {
            get;
            set;
        }

        public string TaxExemption
        {
            get;
            set;
        }

        public string PortDischarge
        {
            get;
            set;
        }

        public string MovementCode
        {
            get;
            set;
        }

        public string MLOCode
        {
            get;
            set;
        }

        public string ItemType
        {
            get;
            set;
        }

        public decimal? GrossWeight
        {
            get;
            set;
        }

        public string WtUnit
        {
            get;
            set;
        }

        public decimal? FreigthToCollect
        {
            get;
            set;
        }

        public string PartBL
        {
            get;
            set;
        }

        public string DirectPortTransfer
        {
            get;
            set;
        }

        public string TranShipment
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

        public string ConsigneeName
        {
            get;
            set;
        }
        public string NotifyPartyInformation
        {
            get;
            set;
        }

        public string NotifyPartyName
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

        public string CargoArrivNotice
        {
            get;
            set;
        }

        public int? DetentionFree
        {
            get;
            set;
        }

        public int? PGRFreeDays
        {
            get;
            set;
        }

        public string DetentionSlab
        {
            get;
            set;
        }

        public string CntrNo
        {
            get;
            set;
        }

        public string CntrSize
        {
            get;
            set;
        }

        public string SealNo
        {
            get;
            set;
        }

        public string CAgent
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public decimal? Package
        {
            get;
            set;
        }

        public decimal? CntrGrossWeight
        {
            get;
            set;
        }

        public string SOC
        {
            get;
            set;
        }

        public decimal? Height
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public decimal? TareWgt
        {
            get;
            set;
        }

        public string Comodity
        {
            get;
            set;
        }

        public Int64 BLId
        {
            get;
            set;
        }

        public Int64 FooterId
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public ImpBLChkLstEntity()
        {

        }

        public ImpBLChkLstEntity(DataTableReader reader)
        {
            if (HasColumn(reader, "BLId") && reader["BLId"] != DBNull.Value)
                this.BLId = Convert.ToInt64(reader["BLId"]);

            if (HasColumn(reader, "FooterId") && reader["FooterId"] != DBNull.Value)
                this.FooterId = Convert.ToInt64(reader["FooterId"]);

            if (HasColumn(reader, "IGMBLNumber") && reader["IGMBLNumber"] != DBNull.Value)
                this.IGMBLNumber = Convert.ToString(reader["IGMBLNumber"]);

            if (HasColumn(reader, "ImpLineBLNo") && reader["ImpLineBLNo"] != DBNull.Value)
                this.ImpLineBLNo = Convert.ToString(reader["ImpLineBLNo"]);

            if (HasColumn(reader, "MotherVessel") && reader["MotherVessel"] != DBNull.Value)
                this.MotherVessel = Convert.ToString(reader["MotherVessel"]);

            if (HasColumn(reader, "ItemLineNo") && reader["ItemLineNo"] != DBNull.Value)
                this.ItemLineNo = Convert.ToString(reader["ItemLineNo"]);

            if (HasColumn(reader, "TransportMode") && reader["TransportMode"] != DBNull.Value)
                this.TransportMode = Convert.ToString(reader["TransportMode"]);

            if (HasColumn(reader, "PortLoading") && reader["PortLoading"] != DBNull.Value)
                this.PortLoading = Convert.ToString(reader["PortLoading"]);

            if (HasColumn(reader, "FinalDestination") && reader["FinalDestination"] != DBNull.Value)
                this.FinalDestination = Convert.ToString(reader["FinalDestination"]);

            if (HasColumn(reader, "HazCargo") && reader["HazCargo"] != DBNull.Value)
                this.HazCargo = Convert.ToString(reader["HazCargo"]);

            if (HasColumn(reader, "CargoType") && reader["CargoType"] != DBNull.Value)
                this.CargoType = Convert.ToString(reader["CargoType"]);

            if (HasColumn(reader, "PackageDetail") && reader["PackageDetail"] != DBNull.Value)
                this.PackageDetail = Convert.ToString(reader["PackageDetail"]);

            if (HasColumn(reader, "PackUnit") && reader["PackUnit"] != DBNull.Value)
                this.PackUnit = Convert.ToString(reader["PackUnit"]);

            if (HasColumn(reader, "Volume") && reader["Volume"] != DBNull.Value)
                this.Volume = Convert.ToDecimal(reader["Volume"]);

            if (HasColumn(reader, "VolUnit") && reader["VolUnit"] != DBNull.Value)
                this.VolUnit = Convert.ToString(reader["VolUnit"]);

            if (HasColumn(reader, "FreightType") && reader["FreightType"] != DBNull.Value)
                this.FreightType = Convert.ToString(reader["FreightType"]);

            if (HasColumn(reader, "CargoNature") && reader["CargoNature"] != DBNull.Value)
                this.CargoNature = Convert.ToString(reader["CargoNature"]);

            if (HasColumn(reader, "TPBondNo") && reader["TPBondNo"] != DBNull.Value)
                this.TPBondNo = Convert.ToString(reader["TPBondNo"]);

            if (HasColumn(reader, "FreeOut") && reader["FreeOut"] != DBNull.Value)
                this.FreeOut = Convert.ToString(reader["FreeOut"]);

            if (HasColumn(reader, "CACode") && reader["CACode"] != DBNull.Value)
                this.CACode = Convert.ToString(reader["CACode"]);

            //if (HasColumn(reader, "IGMBLDate") && reader["IGMBLDate"] != DBNull.Value)
            //    this.IGMBLDate = Convert.ToString(reader["IGMBLDate"]);
            if (HasColumn(reader, "IGMBLDate") && reader["IGMBLDate"] != DBNull.Value)
                this.IGMBLDate = Convert.ToDateTime(reader["IGMBLDate"]).ToString("dd/MM/yyyy");


            if (HasColumn(reader, "LineBLType") && reader["LineBLType"] != DBNull.Value)
                this.LineBLType = Convert.ToString(reader["LineBLType"]);

            if (HasColumn(reader, "ItemSubLineNo") && reader["ItemSubLineNo"] != DBNull.Value)
                this.ItemSubLineNo = Convert.ToString(reader["ItemSubLineNo"]);

            if (HasColumn(reader, "TaxExemption") && reader["TaxExemption"] != DBNull.Value)
                this.TaxExemption = Convert.ToString(reader["TaxExemption"]);

            if (HasColumn(reader, "PortDischarge") && reader["PortDischarge"] != DBNull.Value)
                this.PortDischarge = Convert.ToString(reader["PortDischarge"]);

            if (HasColumn(reader, "MovementCode") && reader["MovementCode"] != DBNull.Value)
                this.MovementCode = Convert.ToString(reader["MovementCode"]);

            if (HasColumn(reader, "MLOCode") && reader["MLOCode"] != DBNull.Value)
                this.MLOCode = Convert.ToString(reader["MLOCode"]);

            if (HasColumn(reader, "ItemType") && reader["ItemType"] != DBNull.Value)
                this.ItemType = Convert.ToString(reader["ItemType"]);

            if (HasColumn(reader, "GrossWeight") && reader["GrossWeight"] != DBNull.Value)
                this.GrossWeight = Convert.ToDecimal(reader["GrossWeight"]);

            if (HasColumn(reader, "WtUnit") && reader["WtUnit"] != DBNull.Value)
                this.WtUnit = Convert.ToString(reader["WtUnit"]);

            if (HasColumn(reader, "FreigthToCollect") && reader["FreigthToCollect"] != DBNull.Value)
                this.FreigthToCollect = Convert.ToDecimal(reader["FreigthToCollect"]);

            if (HasColumn(reader, "PartBL") && reader["PartBL"] != DBNull.Value)
                this.PartBL = Convert.ToString(reader["PartBL"]);

            if (HasColumn(reader, "DirectPortTransfer") && reader["DirectPortTransfer"] != DBNull.Value)
                this.DirectPortTransfer = Convert.ToString(reader["DirectPortTransfer"]);

            if (HasColumn(reader, "TranShipment") && reader["TranShipment"] != DBNull.Value)
                this.TranShipment = Convert.ToString(reader["TranShipment"]);

            if (HasColumn(reader, "ShipperInformation") && reader["ShipperInformation"] != DBNull.Value)
                this.ShipperInformation = Convert.ToString(reader["ShipperInformation"]);

            if (HasColumn(reader, "ConsigneeInformation") && reader["ConsigneeInformation"] != DBNull.Value)
                this.ConsigneeInformation = Convert.ToString(reader["ConsigneeInformation"]);

            if (HasColumn(reader, "ConsigneeName") && reader["ConsigneeName"] != DBNull.Value)
                this.ConsigneeName = Convert.ToString(reader["ConsigneeName"]);

            if (HasColumn(reader, "NotifyPartyInformation") && reader["NotifyPartyInformation"] != DBNull.Value)
                this.NotifyPartyInformation = Convert.ToString(reader["NotifyPartyInformation"]);

            if (HasColumn(reader, "NotifyPartyName") && reader["NotifyPartyNme"] != DBNull.Value)
                this.NotifyPartyName = Convert.ToString(reader["NotifyPartyName"]);

            if (HasColumn(reader, "MarksNumbers") && reader["MarksNumbers"] != DBNull.Value)
                this.MarksNumbers = Convert.ToString(reader["MarksNumbers"]);

            if (HasColumn(reader, "GoodDescription") && reader["GoodDescription"] != DBNull.Value)
                this.GoodDescription = Convert.ToString(reader["GoodDescription"]);

            if (HasColumn(reader, "CargoArrivNotice") && reader["CargoArrivNotice"] != DBNull.Value)
                this.CargoArrivNotice = Convert.ToString(reader["CargoArrivNotice"]);

            if (HasColumn(reader, "DetentionFree") && reader["DetentionFree"] != DBNull.Value)
                this.DetentionFree = Convert.ToInt32(reader["DetentionFree"]);

            if (HasColumn(reader, "PGRFreeDays") && reader["PGRFreeDays"] != DBNull.Value)
                this.PGRFreeDays = Convert.ToInt32(reader["PGRFreeDays"]);

            if (HasColumn(reader, "DetentionSlab") && reader["DetentionSlab"] != DBNull.Value)
                this.DetentionSlab = Convert.ToString(reader["DetentionSlab"]);

            if (HasColumn(reader, "CntrNo") && reader["CntrNo"] != DBNull.Value)
                this.CntrNo = Convert.ToString(reader["CntrNo"]);

            if (HasColumn(reader, "CntrSize") && reader["CntrSize"] != DBNull.Value)
                this.CntrSize = Convert.ToString(reader["CntrSize"]);

            if (HasColumn(reader, "SealNo") && reader["SealNo"] != DBNull.Value)
                this.SealNo = Convert.ToString(reader["SealNo"]);

            if (HasColumn(reader, "CAgent") && reader["CAgent"] != DBNull.Value)
                this.CAgent = Convert.ToString(reader["CAgent"]);

            if (HasColumn(reader, "Status") && reader["Status"] != DBNull.Value)
                this.Status = Convert.ToString(reader["Status"]);

            if (HasColumn(reader, "Package") && reader["Package"] != DBNull.Value)
                this.Package = Convert.ToInt32(reader["Package"]);

            if (HasColumn(reader, "CntrGrossWeight") && reader["CntrGrossWeight"] != DBNull.Value)
                this.CntrGrossWeight = Convert.ToDecimal(reader["CntrGrossWeight"]);

            if (HasColumn(reader, "SOC") && reader["SOC"] != DBNull.Value)
                this.SOC = Convert.ToString(reader["SOC"]);

            if (HasColumn(reader, "Height") && reader["Height"] != DBNull.Value)
                this.Height = Convert.ToDecimal(reader["Height"]);

            if (HasColumn(reader, "Type") && reader["Type"] != DBNull.Value)
                this.Type = Convert.ToString(reader["Type"]);

            if (HasColumn(reader, "TareWgt") && reader["TareWgt"] != DBNull.Value)
                this.TareWgt = Convert.ToDecimal(reader["TareWgt"]);

            if (HasColumn(reader, "Comodity") && reader["Comodity"] != DBNull.Value)
                this.Comodity = Convert.ToString(reader["Comodity"]);
        }

        #endregion

        #region Private Methods

        private bool HasColumn(DataTableReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion
    }
}