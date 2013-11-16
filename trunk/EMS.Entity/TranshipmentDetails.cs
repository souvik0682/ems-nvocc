using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    [Serializable]
    public class TranshipmentDetails : ITranshipmentDetails
    {
        public Int64 ActualTranshipmentId { get; set; }
        public Int64 PortId { get; set; }
        public Int64 ExportBLId { get; set; }
        public Int64 ImportBLFooterId { get; set; }
        public Int64 ExpBLContainerID { get; set; }
        public Int64 HireContainerId { get; set; }
        public Int64 TranshipmentId { get; set; }
        public DateTime ActualArrival { get; set; }
        public DateTime ActualDeparture { get; set; }

        public string PortName { get; set; }
        public string PortCode { get; set; }
        public string ExportBLNo { get; set; }
        public DateTime ExportBLDate { get; set; }
        public Int64 BookingId { get; set; }
        public string BookingCode { get; set; }
        public Int64 VesselId { get; set; }
        public string VesselName { get; set; }
        public Int64 VoyageId { get; set; }
        public string VoyageNo { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerType { get; set; }

        public string Size { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public TranshipmentDetails(){}

        public TranshipmentDetails(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_ExpBLID"))
                if (reader["pk_ExpBLID"] != DBNull.Value)
                    this.ExportBLId = Convert.ToInt64(reader["pk_ExpBLID"]);

            if (ColumnExists(reader, "ExpBLNo"))
                if (reader["ExpBLNo"] != DBNull.Value)
                    this.ExportBLNo = Convert.ToString(reader["ExpBLNo"]);

            if (ColumnExists(reader, "ExpBLDate"))
                if (reader["ExpBLDate"] != DBNull.Value)
                    this.ExportBLDate = Convert.ToDateTime(reader["ExpBLDate"]);

            if (ColumnExists(reader, "fk_BookingID"))
                if (reader["fk_BookingID"] != DBNull.Value)
                    this.BookingId = Convert.ToInt64(reader["fk_BookingID"]);

            if (ColumnExists(reader, "BookingNo"))
                if (reader["BookingNo"] != DBNull.Value)
                    this.BookingCode = Convert.ToString(reader["BookingNo"]);

            if (ColumnExists(reader, "fk_VesselID"))
                if (reader["fk_VesselID"] != DBNull.Value)
                    this.VesselId = Convert.ToInt64(reader["fk_VesselID"]);

            if (ColumnExists(reader, "VesselName"))
                if (reader["VesselName"] != DBNull.Value)
                    this.VesselName = Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "fk_VoyageID"))
                if (reader["fk_VoyageID"] != DBNull.Value)
                    this.VoyageId = Convert.ToInt64(reader["fk_VoyageID"]);

            if (ColumnExists(reader, "VoyageNo"))
                if (reader["VoyageNo"] != DBNull.Value)
                    this.VoyageNo = Convert.ToString(reader["VoyageNo"]);

            if (ColumnExists(reader, "ContainerType"))
                if (reader["ContainerType"] != DBNull.Value)
                    this.ContainerType = Convert.ToString(reader["ContainerType"]);

            if (ColumnExists(reader, "pk_ExpBLContainerID"))
                if (reader["pk_ExpBLContainerID"] != DBNull.Value)
                    this.ExpBLContainerID = Convert.ToInt64(reader["pk_ExpBLContainerID"]);
  
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
