using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    [Serializable]
    public class BookingChargesEntity : IBookingCharges, ICommon
    {
        public long BookingChargeId { get; set; }
        public long BookingId { get; set; }
        public int ChargeId { get; set; }
        public string ChargeName { get; set; }
        public int ChargeRateId { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public bool ChargeApplicable { get; set; }
        public string Size { get; set; }
        public int ContainerTypeId { get; set; }
        public string ContainerType { get; set; }
        public decimal WtInCBM { get; set; }
        public decimal WtInTon { get; set; }
        public decimal ActualRate { get; set; }
        public decimal ManifestRate { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal BrokerageBasic { get; set; }
        public bool ChargeStatus { get; set; }
        public int ChgExist { get; set; }
        public int DOExist { get; set; }
        public int BLExist { get; set; }

        public int Unit { get; set; }

        public bool ChargedEditable { get; set; }
        public bool RefundEditable { get; set; }
        public bool BrokerageEditable { get; set; }
        public bool ManifestEditabe { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public BookingChargesEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "BookingChargeId"))
                if (reader["BookingChargeId"] != DBNull.Value)
                    BookingChargeId = Convert.ToInt64(reader["BookingChargeId"]);

            if (ColumnExists(reader, "BookingId"))
                if (reader["BookingId"] != DBNull.Value)
                    BookingId = Convert.ToInt64(reader["BookingId"]);

            if (ColumnExists(reader, "ChargeId"))
                if (reader["ChargeId"] != DBNull.Value)
                    ChargeId = Convert.ToInt32(reader["ChargeId"]);

            if (ColumnExists(reader, "ChargeName"))
                if (reader["ChargeName"] != DBNull.Value)
                    ChargeName = Convert.ToString(reader["ChargeName"]);

            if (ColumnExists(reader, "ChargeRateId"))
                if (reader["ChargeRateId"] != DBNull.Value)
                    ChargeRateId = Convert.ToInt32(reader["ChargeRateId"]);

            if (ColumnExists(reader, "CurrencyId"))
                if (reader["CurrencyId"] != DBNull.Value)
                    CurrencyId = Convert.ToInt32(reader["CurrencyId"]);

            if (ColumnExists(reader, "CurrencyName"))
                if (reader["CurrencyName"] != DBNull.Value)
                    CurrencyName = Convert.ToString(reader["CurrencyName"]);

            if (ColumnExists(reader, "ChargeApplicable"))
                if (reader["ChargeApplicable"] != DBNull.Value)
                    ChargeApplicable = Convert.ToBoolean(reader["ChargeApplicable"]);

            if (ColumnExists(reader, "Size"))
                if (reader["Size"] != DBNull.Value)
                    Size = Convert.ToString(reader["Size"]);

            if (ColumnExists(reader, "ContainerTypeId"))
                if (reader["ContainerTypeId"] != DBNull.Value)
                    ContainerTypeId = Convert.ToInt32(reader["ContainerTypeId"]);

            if (ColumnExists(reader, "ContainerType"))
                if (reader["ContainerType"] != DBNull.Value)
                    ContainerType = Convert.ToString(reader["ContainerType"]);

            if (ColumnExists(reader, "WtInCBM"))
                if (reader["WtInCBM"] != DBNull.Value)
                    WtInCBM = Convert.ToDecimal(reader["WtInCBM"]);

            if (ColumnExists(reader, "ChgExist"))
                if (reader["ChgExist"] != DBNull.Value)
                    ChgExist = Convert.ToInt32(reader["ChgExist"]);

            if (ColumnExists(reader, "DOExist"))
                if (reader["DOExist"] != DBNull.Value)
                    DOExist = Convert.ToInt32(reader["DOExist"]);

            if (ColumnExists(reader, "BLExist"))
                if (reader["BLExist"] != DBNull.Value)
                    BLExist = Convert.ToInt32(reader["BLExist"]);

            if (ColumnExists(reader, "WtInTon"))
                if (reader["WtInTon"] != DBNull.Value)
                    WtInTon = Convert.ToDecimal(reader["WtInTon"]);

            if (ColumnExists(reader, "ActualRate"))
                if (reader["ActualRate"] != DBNull.Value)
                    ActualRate = Convert.ToDecimal(reader["ActualRate"]);

            if (ColumnExists(reader, "ManifestRate"))
                if (reader["ManifestRate"] != DBNull.Value)
                    ManifestRate = Convert.ToDecimal(reader["ManifestRate"]);

            if (ColumnExists(reader, "Unit"))
                if (reader["Unit"] != DBNull.Value)
                    Unit = Convert.ToInt32(reader["Unit"]);

            if (ColumnExists(reader, "RefundAmount"))
            {
                if (reader["RefundAmount"] != DBNull.Value)
                    RefundAmount = Convert.ToDecimal(reader["RefundAmount"]);
            }
            else
                RefundAmount = Convert.ToDecimal("0.00");

            if (ColumnExists(reader, "BrokerageBasic"))
            {
                if (reader["BrokerageBasic"] != DBNull.Value)
                    BrokerageBasic = Convert.ToDecimal(reader["BrokerageBasic"]);
            }
            else
                BrokerageBasic = Convert.ToDecimal("0.00");

            if (ColumnExists(reader, "ChargeStatus"))
            {
                if (reader["ChargeStatus"] != DBNull.Value)
                    ChargeStatus = Convert.ToBoolean(reader["ChargeStatus"]);
                else
                    ChargeStatus = true;
            }
            else
            {
                ChargeStatus = true;
            }

            RefundEditable = false;
            ChargedEditable = true;
            BrokerageEditable = false;

            if (ColumnExists(reader, "RateChangeable"))
            {
                if (reader["RateChangeable"] != DBNull.Value)
                    ManifestEditabe = Convert.ToBoolean(reader["RateChangeable"]);
            }
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper() == columnName.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
