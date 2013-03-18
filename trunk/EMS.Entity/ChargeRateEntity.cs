using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace EMS.Entity
{
    [Serializable]
    public class ChargeRateEntity : IChargeRate
    {
        [XmlAnyElement]
        public int ChargesRateID
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int ChargesID
        {
            get;
            set;
        }

        [XmlAnyElement]
        public bool RateActive
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int LocationId
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int TerminalId
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int WashingType
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int High
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int Low
        {
            get;
            set;
        }

        decimal _RatePerBL = 0.00M;
        [XmlAnyElement]
        public decimal RatePerBL
        {
            get { return _RatePerBL; }
            set { _RatePerBL = value; }
        }

        decimal _RatePerTEU = 0.00M;
        [XmlAnyElement]
        public decimal RatePerTEU
        {
            get { return _RatePerTEU; }
            set { _RatePerTEU = value; }
        }

        decimal _RatePerFEU = 0.00M;
        [XmlAnyElement]
        public decimal RatePerFEU
        {
            get { return _RatePerFEU; }
            set { _RatePerFEU = value; }
        }

        decimal _RatePerTON = 0.00M;
        [XmlAnyElement]
        public decimal RatePerTON
        {
            get { return _RatePerTON; }
            set { _RatePerTON = value; }
        }

        decimal _RatePerCBM = 0.00M;
        [XmlAnyElement]
        public decimal RatePerCBM
        {
            get { return _RatePerCBM; }
            set { _RatePerCBM = value; }
        }

        decimal _SharingBL = 0.00M;
        [XmlAnyElement]
        public decimal SharingBL
        {
            get { return _SharingBL; }
            set { _SharingBL = value; }
        }

        decimal _SharingTEU = 0.00M;
        [XmlAnyElement]
        public decimal SharingTEU
        {
            get { return _SharingTEU; }
            set { _SharingTEU = value; }
        }

        decimal _SharingFEU = 0.00M;
        [XmlAnyElement]
        public decimal SharingFEU
        {
            get { return _SharingFEU; }
            set { _SharingFEU = value; }
        }

        [XmlAnyElement]
        public decimal ServiceTax
        {
            get;
            set;
        }

        public string ChangeName { get; set; }
        public string TerminalName { get; set; }
        public string WashingTypeName { get; set; }
        public decimal Usd { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal STax { get; set; }
        public decimal ServiceTaxCessAmount { get; set; }
        public decimal ServiceTaxACess { get; set; }
        public decimal TotalAmount { get; set; }
        public int InvoiceChargeId { get; set; }
        public long InvoiceId { get; set; }

        public List<IChargeRate> ConvertXMLToList(string XMLString)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<IChargeRate>));
            List<IChargeRate> result = (List<IChargeRate>)serializer.Deserialize(new StringReader(XMLString));
            return result;
        }

        public ChargeRateEntity() { }

        public ChargeRateEntity(DataTableReader reader)
        {
            this.ChargesID = Convert.ToInt32(reader["ChargesID"]);
            if (ColumnExists(reader, "ChargesRateID"))
                if (reader["ChargesRateID"] != DBNull.Value)
                    this.ChargesRateID = Convert.ToInt32(reader["ChargesRateID"]);

            this.High = Convert.ToInt32(reader["SlabHigh"]);
            this.LocationId = Convert.ToInt32(reader["LocationID"]);
            this.Low = Convert.ToInt32(reader["SlabLow"]);
            this.RateActive = Convert.ToBoolean(reader["RateActive"]);
            this.RatePerBL = Convert.ToDecimal(reader["RatePerBL"]);
            this.RatePerCBM = Convert.ToDecimal(reader["RatePerCBM"]);
            this.RatePerFEU = Convert.ToDecimal(reader["RatePerFEU"]);
            this.RatePerTEU = Convert.ToDecimal(reader["RatePerTEU"]);
            this.RatePerTON = Convert.ToDecimal(reader["RatePerTon"]);
            this.SharingBL = Convert.ToDecimal(reader["PRatePerBL"]);
            this.SharingFEU = Convert.ToDecimal(reader["PRatePerFEU"]);
            this.SharingTEU = Convert.ToDecimal(reader["PRatePerTEU"]);
            if (ColumnExists(reader, "TerminalID"))
                if (reader["TerminalID"] != DBNull.Value)
                    this.TerminalId = Convert.ToInt32(reader["TerminalID"]);

            if (ColumnExists(reader, "TerminalName"))
                if (reader["TerminalName"] != DBNull.Value)
                    this.TerminalName = Convert.ToString(reader["TerminalName"]);

            if (ColumnExists(reader, "WashingType"))
                if (reader["WashingType"] != DBNull.Value)
                    this.WashingType = Convert.ToInt32(reader["WashingType"]);

            if (ColumnExists(reader, "ServiceTax"))
                if (reader["ServiceTax"] != DBNull.Value)
                    this.ServiceTax = Convert.ToDecimal(reader["ServiceTax"]);

            if (ColumnExists(reader, "InvoiceChargeId"))
                if (reader["InvoiceChargeId"] != DBNull.Value)
                    this.InvoiceChargeId = Convert.ToInt32(reader["InvoiceChargeId"]);

            if (ColumnExists(reader, "InvoiceId"))
                if (reader["InvoiceId"] != DBNull.Value)
                    this.InvoiceId = Convert.ToInt64(reader["InvoiceId"]);

            if (ColumnExists(reader, "ServiceTaxAmount"))
                if (reader["ServiceTaxAmount"] != DBNull.Value)
                    this.STax = Convert.ToDecimal(reader["ServiceTaxAmount"]);

            if (ColumnExists(reader, "GrossAmount"))
                if (reader["GrossAmount"] != DBNull.Value)
                    this.GrossAmount = Convert.ToDecimal(reader["GrossAmount"]);

            if (ColumnExists(reader, "RateUSD"))
                if (reader["RateUSD"] != DBNull.Value)
                    this.Usd = Convert.ToDecimal(reader["RateUSD"]);

            if (ColumnExists(reader, "ChargeName"))
                if (reader["ChargeName"] != DBNull.Value)
                    this.ChangeName = Convert.ToString(reader["ChargeName"]);

            if (ColumnExists(reader, "ServiceTaxCessAmount"))
                if (reader["ServiceTaxCessAmount"] != DBNull.Value)
                    this.ServiceTaxCessAmount = Convert.ToInt32(reader["ServiceTaxCessAmount"]);

            if (ColumnExists(reader, "ServiceTaxACess"))
                if (reader["ServiceTaxACess"] != DBNull.Value)
                    this.ServiceTaxACess = Convert.ToInt32(reader["ServiceTaxACess"]);

            if (ColumnExists(reader, "TotalAmount"))
                if (reader["TotalAmount"] != DBNull.Value)
                    this.TotalAmount = Convert.ToInt32(reader["TotalAmount"]);
            //this.ServiceTax = Convert.ToDecimal(reader["ServiceTax"]);
        }

        //public string ConvertListToXML(List<ChargeRateEntity> Items)
        //{
        //    StringWriter stringWriter = new StringWriter();
        //    XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
        //    XmlSerializer serializer = new XmlSerializer(typeof(List<ChargeRateEntity>));
        //    serializer.Serialize(xmlWriter, Items);
        //    return stringWriter.ToString();
        //}

        [XmlAnyElement]
        public int SlNo
        {
            get;
            set;
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
