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



        [XmlAnyElement]
        public int Type
        {
            get;
            set;
        }

        [XmlAnyElement]
        public string Size
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal RatePerUnit
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal RatePerDoc
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int fk_CurrencyID
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal ExchgRate
        {
            get;
            set;
        }
       

        public string ChargeName { get; set; }
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
        public decimal Units { get; set; }
        public int UnitTypeID { get; set; }
        public string UnitType { get; set; }
        public decimal STaxPer { get; set; }

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
            if (ColumnExists(reader, "SlabHigh"))
                if (reader["SlabHigh"] != DBNull.Value)
                    this.High = Convert.ToInt32(reader["SlabHigh"]);
            if (ColumnExists(reader, "LocationID"))
                if (reader["LocationID"] != DBNull.Value)
                    this.LocationId = Convert.ToInt32(reader["LocationID"]);
            if (ColumnExists(reader, "SlabLow"))
                if (reader["SlabLow"] != DBNull.Value)
                    this.Low = Convert.ToInt32(reader["SlabLow"]);
            if (ColumnExists(reader, "RateActive"))
                if (reader["RateActive"] != DBNull.Value)
                    this.RateActive = Convert.ToBoolean(reader["RateActive"]);
            if (ColumnExists(reader, "RatePerBL"))
                if (reader["RatePerBL"] != DBNull.Value)
                    this.RatePerBL = Convert.ToDecimal(reader["RatePerBL"]);
            if (ColumnExists(reader, "RatePerCBM"))
                if (reader["RatePerCBM"] != DBNull.Value)
                    this.RatePerCBM = Convert.ToDecimal(reader["RatePerCBM"]);
            if (ColumnExists(reader, "RatePerFEU"))
                if (reader["RatePerFEU"] != DBNull.Value)
                    this.RatePerFEU = Convert.ToDecimal(reader["RatePerFEU"]);
            if (ColumnExists(reader, "RatePerTEU"))
                if (reader["RatePerTEU"] != DBNull.Value)
                    this.RatePerTEU = Convert.ToDecimal(reader["RatePerTEU"]);
            if (ColumnExists(reader, "RatePerTon"))
                if (reader["RatePerTon"] != DBNull.Value)
                    this.RatePerTON = Convert.ToDecimal(reader["RatePerTon"]);
            if (ColumnExists(reader, "PRatePerBL"))
                if (reader["PRatePerBL"] != DBNull.Value)
                    this.SharingBL = Convert.ToDecimal(reader["PRatePerBL"]);
            if (ColumnExists(reader, "PRatePerFEU"))
                if (reader["PRatePerFEU"] != DBNull.Value)
                    this.SharingFEU = Convert.ToDecimal(reader["PRatePerFEU"]);
            if (ColumnExists(reader, "PRatePerTEU"))
                if (reader["PRatePerTEU"] != DBNull.Value)
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

            if (ColumnExists(reader, "STaxPer"))
                if (reader["STaxPer"] != DBNull.Value)
                    this.STaxPer = Convert.ToDecimal(reader["STaxPer"]);

            if (ColumnExists(reader, "ServiceTaxAmount"))
                if (reader["ServiceTaxAmount"] != DBNull.Value)
                    this.STax = Convert.ToDecimal(reader["ServiceTaxAmount"]);

            if (ColumnExists(reader, "ServiceTaxAmount"))
                if (reader["ServiceTaxAmount"] != DBNull.Value)
                    this.ServiceTax = Convert.ToDecimal(reader["ServiceTaxAmount"]);

            if (ColumnExists(reader, "GrossAmount"))
                if (reader["GrossAmount"] != DBNull.Value)
                    this.GrossAmount = Convert.ToDecimal(reader["GrossAmount"]);

            if (ColumnExists(reader, "RateUSD"))
                if (reader["RateUSD"] != DBNull.Value)
                    this.Usd = Convert.ToDecimal(reader["RateUSD"]);

            if (ColumnExists(reader, "ChargeName"))
                if (reader["ChargeName"] != DBNull.Value)
                    this.ChargeName = Convert.ToString(reader["ChargeName"]);

            if (ColumnExists(reader, "ServiceTaxCessAmount"))
                if (reader["ServiceTaxCessAmount"] != DBNull.Value)
                    this.ServiceTaxCessAmount = Convert.ToDecimal(reader["ServiceTaxCessAmount"]);

            if (ColumnExists(reader, "ServiceTaxACess"))
                if (reader["ServiceTaxACess"] != DBNull.Value)
                    this.ServiceTaxACess = Convert.ToDecimal(reader["ServiceTaxACess"]);

            if (ColumnExists(reader, "TotalAmount"))
                if (reader["TotalAmount"] != DBNull.Value)
                    this.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
            //this.ServiceTax = Convert.ToDecimal(reader["ServiceTax"]);

            if (ColumnExists(reader, "Type"))
                if (reader["Type"] != DBNull.Value)
                    this.Type = Convert.ToInt32(reader["Type"]);
            //this.Type = Convert.ToInt32(reader["Type"]);

            if (ColumnExists(reader, "Size"))
                if (reader["Size"] != DBNull.Value)
                    this.Size = Convert.ToString(reader["Size"]);
            //this.Size = Convert.ToString(reader["Size"]);

            if (ColumnExists(reader, "RatePerUnit"))
                if (reader["RatePerUnit"] != DBNull.Value)
                    this.RatePerUnit = Convert.ToDecimal(reader["RatePerUnit"]);
            //this.RatePerUnit = Convert.ToDecimal(reader["RatePerUnit"]);

            if (ColumnExists(reader, "RatePerDoc"))
                if (reader["RatePerDoc"] != DBNull.Value)
                    this.RatePerDoc = Convert.ToDecimal(reader["RatePerDoc"]);
            //this.RatePerDoc = Convert.ToDecimal(reader["RatePerDoc"]);

            if (ColumnExists(reader, "fk_CurrencyID"))
                if (reader["fk_CurrencyID"] != DBNull.Value)
                    this.fk_CurrencyID = Convert.ToInt32(reader["fk_CurrencyID"]);

            if (ColumnExists(reader, "ExchgRate"))
                if (reader["ExchgRate"] != DBNull.Value)
                    this.ExchgRate = Convert.ToDecimal(reader["ExchgRate"]);

            if (ColumnExists(reader, "Units"))
                if (reader["Units"] != DBNull.Value)
                    this.Units = Convert.ToDecimal(reader["Units"]);

            if (ColumnExists(reader, "UnitTypeID"))
                if (reader["UnitTypeID"] != DBNull.Value)
                    this.UnitTypeID = Convert.ToInt32(reader["UnitTypeID"]);

            if (ColumnExists(reader, "CntrSize"))
                if (reader["CntrSize"] != DBNull.Value)
                    this.Size = Convert.ToString(reader["CntrSize"]);

            if (ColumnExists(reader, "UnitType"))
                if (reader["UnitType"] != DBNull.Value)
                    this.UnitType = Convert.ToString(reader["UnitType"]);

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
