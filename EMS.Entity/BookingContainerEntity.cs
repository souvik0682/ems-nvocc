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
    public class BookingContainerEntity : IBookingContainer
    {
        [XmlAnyElement]
        public Int32 BookingID
        {
            get;
            set;
        }

        [XmlAnyElement]
        public Int32 BookingContainerID
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int SlNo
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int ContainerTypeID
        {
            get;
            set;
        }

        [XmlAnyElement]
        public string CntrSize
        {
            get;
            set;
        }

        [XmlAnyElement]
        public int NoofContainers
        {
            get;
            set;
        }

        [XmlAnyElement]
        public decimal wtPerCntr
        {
            get;
            set;
        }

        [XmlAnyElement]
        public bool BkCntrStatus
        {
            get;
            set;
        }

        [XmlAnyElement]
        public string ContainerType
        {
            get;
            set;
        }
        


        public List<IBookingContainer> ConvertXMLToList(string XMLString)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<IBookingContainer>));
            List<IBookingContainer> result = (List<IBookingContainer>)serializer.Deserialize(new StringReader(XMLString));
            return result;
        }

        public BookingContainerEntity() { }

        public BookingContainerEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "fk_BookingID"))
                if (reader["fk_BookingID"] != DBNull.Value)
                    this.BookingID = Convert.ToInt32(reader["fk_BookingID"]);

            if (ColumnExists(reader, "pk_BookingCntrID"))
                if (reader["pk_BookingCntrID"] != DBNull.Value)
                    this.BookingContainerID = Convert.ToInt32(reader["pk_BookingCntrID"]);

            if (ColumnExists(reader, "fk_LeaseID"))
                if (reader["fk_LeaseID"] != DBNull.Value)
                    this.BookingID = Convert.ToInt32(reader["fk_LeaseID"]);

            if (ColumnExists(reader, "pk_LeaseCntrID"))
                if (reader["pk_LeaseCntrID"] != DBNull.Value)
                    this.BookingContainerID = Convert.ToInt32(reader["pk_LeaseCntrID"]);

            if (ColumnExists(reader, "fk_ContainerTypeID"))
                if (reader["fk_ContainerTypeID"] != DBNull.Value)
                    this.ContainerTypeID = Convert.ToInt32(reader["fk_ContainerTypeID"]);

            if (ColumnExists(reader, "CntrSize"))
                if (reader["CntrSize"] != DBNull.Value)
                    this.CntrSize = Convert.ToString(reader["CntrSize"]);

            if (ColumnExists(reader, "Nos"))
                if (reader["Nos"] != DBNull.Value)
                    this.NoofContainers = Convert.ToChar(reader["Nos"]);

            if (ColumnExists(reader, "wtPerCntr"))
                if (reader["wtPerCntr"] != DBNull.Value)
                    this.wtPerCntr = Convert.ToDecimal(reader["wtPerCntr"]);

            if (ColumnExists(reader, "bkCntrStatus"))
                if (reader["bkCntrStatus"] != DBNull.Value)
                    this.BkCntrStatus = Convert.ToBoolean(reader["bkCntrStatus"]);

            if (ColumnExists(reader, "ContainerType"))
                if (reader["ContainerType"] != DBNull.Value)
                    this.ContainerType = Convert.ToString(reader["ContainerType"]);
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
