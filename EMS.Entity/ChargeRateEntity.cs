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

        [XmlAnyElement]
        public decimal RatePerBL
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal RatePerTEU
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal RatePerFEU
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal RatePerTON
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal RatePerCBM
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal SharingBL
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal SharingTEU
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal SharingFEU
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal ServiceTax
        {
            get;
            set;
        }

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
            this.TerminalId = Convert.ToInt32(reader["TerminalID"]);
            this.WashingType = Convert.ToInt32(reader["WashingType"]);
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
    }
}
