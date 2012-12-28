using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity.Report
{
    public class ImpRegisterEntity
    {
        #region Public Properties

        public string Location
        {
            get;
            set;
        }

        public string Line
        {
            get;
            set;
        }

        public string ItemLineNo
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

        public int TEU
        {
            get;
            set;
        }

        public int FEU
        {
            get;
            set;
        }

        public int TotalTEU
        {
            get;
            set;
        }

        public string PortLoading
        {
            get;
            set;
        }

        public string PortDischarge
        {
            get;
            set;
        }

        public string FinalDestination
        {
            get;
            set;
        }

        public DateTime DischargeDate
        {
            get;
            set;
        }

        public string IGMBLNumber
        {
            get;
            set;
        }

        public decimal? GrossWeight
        {
            get;
            set;
        }

        public string GoodDescription
        {
            get;
            set;
        }

        public Int64 NumberPackage
        {
            get;
            set;
        }

        public int UnitPackage
        {
            get;
            set;
        }

        public int? PGRFreeDays
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string CFSCode
        {
            get;
            set;
        }

        public string ICD
        {
            get;
            set;
        }

        public string TBNO
        {
            get;
            set;
        }

        public string CACode
        {
            get;
            set;
        }

        public string MovementCode
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

        #endregion

        #region Constructors

        public ImpRegisterEntity()
        {

        }

        public ImpRegisterEntity(DataTableReader reader)
        {
            if (HasColumn(reader, "Location") && reader["Location"] != DBNull.Value)
                this.Location = Convert.ToString(reader["Location"]);

            if (HasColumn(reader, "Line") && reader["Line"] != DBNull.Value)
                this.Line = Convert.ToString(reader["Line"]);

            if (HasColumn(reader, "ItemLineNo") && reader["ItemLineNo"] != DBNull.Value)
                this.ItemLineNo = Convert.ToString(reader["ItemLineNo"]);

            if (HasColumn(reader, "ImpLineBLNo") && reader["ImpLineBLNo"] != DBNull.Value)
                this.ImpLineBLNo = Convert.ToString(reader["ImpLineBLNo"]);

            if (HasColumn(reader, "ImpLineBLDate") && reader["ImpLineBLDate"] != DBNull.Value)
                this.ImpLineBLDate = Convert.ToDateTime(reader["ImpLineBLDate"]);

            if (HasColumn(reader, "TEU") && reader["TEU"] != DBNull.Value)
                this.TEU = Convert.ToInt32(reader["TEU"]);

            if (HasColumn(reader, "FEU") && reader["FEU"] != DBNull.Value)
                this.FEU = Convert.ToInt32(reader["FEU"]);

            if (HasColumn(reader, "TotalTEU") && reader["TotalTEU"] != DBNull.Value)
                this.TotalTEU = Convert.ToInt32(reader["TotalTEU"]);

            if (HasColumn(reader, "PortLoading") && reader["PortLoading"] != DBNull.Value)
                this.PortLoading = Convert.ToString(reader["PortLoading"]);

            if (HasColumn(reader, "PortDischarge") && reader["PortDischarge"] != DBNull.Value)
                this.PortDischarge = Convert.ToString(reader["PortDischarge"]);

            if (HasColumn(reader, "FinalDestination") && reader["FinalDestination"] != DBNull.Value)
                this.FinalDestination = Convert.ToString(reader["FinalDestination"]);

            if (HasColumn(reader, "DischargeDate") && reader["DischargeDate"] != DBNull.Value)
                this.DischargeDate = Convert.ToDateTime(reader["DischargeDate"]);

            if (HasColumn(reader, "IGMBLNumber") && reader["IGMBLNumber"] != DBNull.Value)
                this.IGMBLNumber = Convert.ToString(reader["IGMBLNumber"]);

            if (HasColumn(reader, "GrossWeight") && reader["GrossWeight"] != DBNull.Value)
                this.GrossWeight = Convert.ToDecimal(reader["GrossWeight"]);

            if (HasColumn(reader, "GoodDescription") && reader["GoodDescription"] != DBNull.Value)
                this.GoodDescription = Convert.ToString(reader["GoodDescription"]);

            if (HasColumn(reader, "NumberPackage") && reader["NumberPackage"] != DBNull.Value)
                this.NumberPackage = Convert.ToInt64(reader["NumberPackage"]);

            if (HasColumn(reader, "UnitPackage") && reader["UnitPackage"] != DBNull.Value)
                this.UnitPackage = Convert.ToInt32(reader["UnitPackage"]);

            if (HasColumn(reader, "Status") && reader["Status"] != DBNull.Value)
                this.Status = Convert.ToString(reader["Status"]);

            if (HasColumn(reader, "PGRFreeDays") && reader["PGRFreeDays"] != DBNull.Value)
                this.PGRFreeDays = Convert.ToInt32(reader["PGRFreeDays"]);

            if (HasColumn(reader, "CFSCode") && reader["CFSCode"] != DBNull.Value)
                this.CFSCode = Convert.ToString(reader["CFSCode"]);

            if (HasColumn(reader, "ICD") && reader["ICD"] != DBNull.Value)
                this.ICD = Convert.ToString(reader["ICD"]);

            if (HasColumn(reader, "TBNO") && reader["TBNO"] != DBNull.Value)
                this.TBNO = Convert.ToString(reader["TBNO"]);

            if (HasColumn(reader, "CACode") && reader["CACode"] != DBNull.Value)
                this.CACode = Convert.ToString(reader["CACode"]);

            if (HasColumn(reader, "MovementCode") && reader["MovementCode"] != DBNull.Value)
                this.MovementCode = Convert.ToString(reader["MovementCode"]);

            if (HasColumn(reader, "ConsigneeInformation") && reader["ConsigneeInformation"] != DBNull.Value)
                this.ConsigneeInformation = Convert.ToString(reader["ConsigneeInformation"]);

            if (HasColumn(reader, "NotifyPartyInformation") && reader["NotifyPartyInformation"] != DBNull.Value)
                this.NotifyPartyInformation = Convert.ToString(reader["NotifyPartyInformation"]);

            if (HasColumn(reader, "MarksNumbers") && reader["MarksNumbers"] != DBNull.Value)
                this.MarksNumbers = Convert.ToString(reader["MarksNumbers"]);
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