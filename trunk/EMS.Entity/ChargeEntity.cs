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
    public class ChargeEntity : ICharge
    {
        public int ChargesID
        {
            get;
            set;
        }

        public int CompanyID
        {
            get;
            set;
        }

        public string ChargeDescr
        {
            get;
            set;
        }

        public int ChargeType
        {
            get;
            set;
        }

        public char IEC
        {
            get;
            set;
        }

        public int NVOCCID
        {
            get;
            set;
        }

        public int Sequence
        {
            get;
            set;
        }

        public bool RateChangeable
        {
            get;
            set;
        }

        public bool ChargeActive
        {
            get;
            set;
        }

        public bool IsFreightComponent
        {
            get;
            set;
        }

        public DateTime EffectDt
        {
            get;
            set;
        }

        public bool IsTerminal
        {
            get;
            set;
        }

        public bool IsWashing
        {
            get;
            set;
        }

        public bool PrincipleSharing
        {
            get;
            set;
        }

        public int Currency
        {
            get;
            set;
        }

        public bool ServiceTax
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

        public List<IChargeRate> ChargeRates
        {
            get;
            set;
        }

        public ChargeEntity()
        {

        }

        public ChargeEntity(DataTableReader reader)
        {
            this.ChargeActive = Convert.ToBoolean(reader["IsActive"]);

            this.ChargeDescr = Convert.ToString(reader["ChargeName"]);
            this.ChargesID = Convert.ToInt32(reader["ChargeId"]);
            this.ChargeType = Convert.ToInt32(reader["ChargeType"]);
            this.CompanyID = Convert.ToInt32(reader["CompanyId"]);
            this.Currency = Convert.ToInt32(reader["Currency"]);
            this.EffectDt = Convert.ToDateTime(reader["EffectDate"]);
            this.IEC = Convert.ToChar(reader["ImportExport"]);
            this.NVOCCID = Convert.ToInt32(reader["Line"]);
            this.Sequence = Convert.ToInt32(reader["DisplayOrder"]);

            this.IsFreightComponent = Convert.ToBoolean(reader["IsFreight"]);
            this.IsTerminal = Convert.ToBoolean(reader["IsTerminal"]);            
            this.IsWashing = Convert.ToBoolean(reader["IsWashing"]);            
            this.PrincipleSharing = Convert.ToBoolean(reader["IsPrincipleShared"]);
            this.RateChangeable = Convert.ToBoolean(reader["RateChangable"]);            
            this.ServiceTax = Convert.ToBoolean(reader["ServiceTax"]);

            if (ColumnExists(reader, "FPOD"))
                if (reader["FPOD"] != DBNull.Value)
                    this.FPOD = Convert.ToString(reader["FPOD"]);
            //this.FPOD = Convert.ToString(reader["Fpod"]);

            if (ColumnExists(reader, "DestinationCharge"))
                if (reader["DestinationCharge"] != DBNull.Value)
                    this.DestinationCharge = Convert.ToBoolean(reader["DestinationCharge"]);
            //this.DestinationCharge = Convert.ToBoolean(reader["DestinationCharge"]);

            if (ColumnExists(reader, "Service"))
                if (reader["Service"] != DBNull.Value)
                    this.Service = Convert.ToInt32(reader["Service"]);
            //this.Service = Convert.ToInt32(reader["Service"]);

            if (ColumnExists(reader, "IsSpecialRate"))
                if (reader["IsSpecialRate"] != DBNull.Value)
                    this.IsSpecialRate = Convert.ToBoolean(reader["IsSpecialRate"]);

            if (ColumnExists(reader, "DeliveryMode"))
                if (reader["DeliveryMode"] != DBNull.Value)
                    this.DeliveryMode = Convert.ToChar(reader["DeliveryMode"]);

            if (ColumnExists(reader, "DocType"))
                if (reader["DocType"] != DBNull.Value)
                    this.DocumentType = Convert.ToInt32(reader["DocType"]);
            

            if (ColumnExists(reader, "LocationId"))
                if (reader["LocationId"] != DBNull.Value)
                    this.Location = Convert.ToInt32(reader["LocationId"]);

            //if (ColumnExists(reader, "Service"))
            //    if (reader["LocationId"] != DBNull.Value)
            //        this.Location = Convert.ToInt32(reader["LocationId"]); 

        }

        public string ConvertListToXML(List<IChargeRate> Items)
        {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
            XmlSerializer serializer = new XmlSerializer(typeof(List<ChargeRateEntity>));
            serializer.Serialize(xmlWriter, Items);
            return stringWriter.ToString();
        }
        
        public int Location
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


        public bool IsSpecialRate
        {
            get;
            set;
        }

        public char DeliveryMode
        {
            get;
            set;
        }


        public int DocumentType
        {
            get;
            set;
        }

        public int Service
        {
            get;
            set;
        }

        public bool DestinationCharge
        {
            get;
            set;
        }


        public string FPOD
        {
            get;
            set;
        }
    }
}
