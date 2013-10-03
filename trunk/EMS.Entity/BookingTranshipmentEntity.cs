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
    public class BookingTranshipmentEntity : IBookingTranshipment
    {
        [XmlAnyElement]
        public Int32 BookingID 
        { 
            get; 
            set; 
        }

        [XmlAnyElement]
        public Int32 BookingTranshipmentID 
        { 
            get; 
            set; 
        }

        [XmlAnyElement]
        public int PortId 
        { 
            get; 
            set; 
        }

        [XmlAnyElement]
        public DateTime ArrivalDate 
        { 
            get; 
            set; 
        }

        [XmlAnyElement]
        public DateTime DepartureDate 
        { 
            get; 
            set; 
        }

        [XmlAnyElement]
        public int OrderNo 
        { 
            get; 
            set; 
        }

        [XmlAnyElement]
        public bool BkTransStatus 
        { 
            get; 
            set; 
        }

        [XmlAnyElement]
        public string PortName 
        { 
            get; 
            set; 
        }

        public List<IBookingTranshipment> ConvertXMLToList(string XMLString)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<IBookingTranshipment>));
            List<IBookingTranshipment> result = (List<IBookingTranshipment>)serializer.Deserialize(new StringReader(XMLString));
            return result;
        }

        public BookingTranshipmentEntity() { }

        public BookingTranshipmentEntity(DataTableReader reader)
        {
            this.BookingID = Convert.ToInt32(reader["fk_BookingID"]);
            this.BookingTranshipmentID = Convert.ToInt32(reader["pk_TranshipmentID"]);

            if (ColumnExists(reader, "fk_PortID"))
                if (reader["fk_PortID"] != DBNull.Value)
                    this.PortId = Convert.ToInt32(reader["fk_PortID"]);

            if (ColumnExists(reader, "ArrivalDate"))
                if (reader["ArrivalDate"] != DBNull.Value)
                    this.ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"]);

            if (ColumnExists(reader, "DepartureDate"))
                if (reader["DepartureDate"] != DBNull.Value)
                    this.DepartureDate = Convert.ToDateTime(reader["NDepartureDateos"]);

            if (ColumnExists(reader, "OrdNo"))
                if (reader["OrdNo"] != DBNull.Value)
                    this.OrderNo = Convert.ToInt32(reader["OrdNo"]);

            if (ColumnExists(reader, "bkTransStatus"))
                if (reader["bkTransStatus"] != DBNull.Value)
                    this.BkTransStatus = Convert.ToBoolean(reader["bkTransStatus"]);

            if (ColumnExists(reader, "PortName"))
                if (reader["PortName"] != DBNull.Value)
                    this.PortName = reader["PortName"].ToString();
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
