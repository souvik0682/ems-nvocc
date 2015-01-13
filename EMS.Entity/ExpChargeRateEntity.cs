﻿using System;
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
    public class ExpChargeRateEntity : IExpChargeRate
    {
        [XmlAnyElement]
        public int ChargesRateId
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int ChargesId
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
        public int fk_CurrencyID
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
        public int SlabHigh
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int SlabLow
        {
            get;
            set;
        }

        decimal _ExchangeRate = 0.00M;
        [XmlAnyElement]
        public decimal ExchgRate
        {
            get { return _ExchangeRate; }
            set { _ExchangeRate = value; }
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
        public decimal RatePerTon
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

        [XmlAnyElement]
        public decimal ServiceTaxAmount
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

        public string ChargeName { get; set; }
        public string TerminalName { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal STax { get; set; }
        public decimal ServiceTaxCessAmount { get; set; }
        public decimal ServiceTaxACess { get; set; }
        public decimal TotalAmount { get; set; }
        public long InvoiceChargeId { get; set; }
        public long InvoiceId { get; set; }

        public List<IChargeRate> ConvertXMLToList(string XMLString)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<IChargeRate>));
            List<IChargeRate> result = (List<IChargeRate>)serializer.Deserialize(new StringReader(XMLString));
            return result;
        }

        public ExpChargeRateEntity() { }

        public ExpChargeRateEntity(DataTableReader reader)
        {
            this.ChargesId = Convert.ToInt32(reader["ChargesID"]);
            if (ColumnExists(reader, "ChargesRateID"))
                if (reader["ChargesRateID"] != DBNull.Value)
                    this.ChargesRateId = Convert.ToInt32(reader["ChargesRateID"]);

            this.SlabHigh = Convert.ToInt32(reader["SlabHigh"]);
            this.LocationId = Convert.ToInt32(reader["LocationID"]);
            this.SlabLow = Convert.ToInt32(reader["SlabLow"]);
            this.RateActive = Convert.ToBoolean(reader["RateActive"]);
            this.RatePerBL = Convert.ToDecimal(reader["RatePerBL"]);
            this.RatePerCBM = Convert.ToDecimal(reader["RatePerCBM"]);
            this.RatePerFEU = Convert.ToDecimal(reader["RatePerFEU"]);
            this.RatePerTEU = Convert.ToDecimal(reader["RatePerTEU"]);
            this.RatePerTon = Convert.ToDecimal(reader["RatePerTon"]);
            
            if (ColumnExists(reader, "TerminalID"))
                if (reader["TerminalID"] != DBNull.Value)
                    this.TerminalId = Convert.ToInt32(reader["TerminalID"]);

            if (ColumnExists(reader, "TerminalName"))
                if (reader["TerminalName"] != DBNull.Value)
                    this.TerminalName = Convert.ToString(reader["TerminalName"]);

            if (ColumnExists(reader, "ServiceTax"))
                if (reader["ServiceTax"] != DBNull.Value)
                    this.ServiceTaxAmount = Convert.ToDecimal(reader["ServiceTax"]);

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

            if (ColumnExists(reader, "fk_CurrencyID"))
                if (reader["fk_CurrencyID"] != DBNull.Value)
                    this.fk_CurrencyID = Convert.ToInt32(reader["fk_CurrencyID"]);

            if (ColumnExists(reader, "ExchgRate"))
                if (reader["ExchgRate"] != DBNull.Value)
                    this.ExchgRate = Convert.ToDecimal(reader["ExchgRate"]);
        
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