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

        [XmlAnyElement]
        public int SlNo
        {
            get;
            set;
        }
    }
}
