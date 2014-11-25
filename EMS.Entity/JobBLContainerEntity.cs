using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Common;

namespace EMS.Entity
{
    [Serializable]
    public class JobBLContainerEntity : IJobBLContainer
    {
        public long ContainerId { get; set; }
        public string HireContainerNumber { get; set; }
        public long HireContainerId { get; set; }
        public long BookingId { get; set; }
        public long BLId { get; set; }
        public int VesselId { get; set; }
        public int VoyageId { get; set; }
        public string ContainerSize { get; set; }
        public int ContainerTypeId { get; set; }
        public string ContainerType { get; set; }
        public string SealNumber { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal TareWeight { get; set; }
        public int Package { get; set; }
        public bool Part { get; set; }
        public string ShippingBillNumber { get; set; }
        public DateTime? ShippingBillDate { get; set; }
        public long ImportBLFooterId { get; set; }
        public bool IsDeleted { get; set; }
        public string Unit { get; set; }

        public JobBLContainerEntity()
        {
        }
        public JobBLContainerEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "ContainerId"))
                if (reader["ContainerId"] != DBNull.Value)
                    ContainerId = Convert.ToInt64(reader["ContainerId"]);

            if (ColumnExists(reader, "HireContainerNumber"))
                if (reader["HireContainerNumber"] != DBNull.Value)
                    HireContainerNumber = Convert.ToString(reader["HireContainerNumber"]);

            if (ColumnExists(reader, "HireContainerId"))
                if (reader["HireContainerId"] != DBNull.Value)
                    HireContainerId = Convert.ToInt64(reader["HireContainerId"]);

            if (ColumnExists(reader, "BookingId "))
                if (reader["BookingId "] != DBNull.Value)
                    BookingId = Convert.ToInt64(reader["BookingId "]);

            if (ColumnExists(reader, "BLId"))
                if (reader["BLId"] != DBNull.Value)
                    BLId = Convert.ToInt64(reader["BLId"]);

            if (ColumnExists(reader, "VesselId"))
                if (reader["VesselId"] != DBNull.Value)
                    VesselId = Convert.ToInt32(reader["VesselId"]);

            if (ColumnExists(reader, "VoyageId"))
                if (reader["VoyageId"] != DBNull.Value)
                    VoyageId = Convert.ToInt32(reader["VoyageId"]);

            if (ColumnExists(reader, "ContainerSize"))
                if (reader["ContainerSize"] != DBNull.Value)
                    ContainerSize = Convert.ToString(reader["ContainerSize"]);

            if (ColumnExists(reader, "ContainerTypeId"))
                if (reader["ContainerTypeId"] != DBNull.Value)
                    ContainerTypeId = Convert.ToInt32(reader["ContainerTypeId"]);

            if (ColumnExists(reader, "ContainerType"))
                if (reader["ContainerType"] != DBNull.Value)
                    ContainerType = Convert.ToString(reader["ContainerType"]);

            if (ColumnExists(reader, "SealNumber"))
                if (reader["SealNumber"] != DBNull.Value)
                    SealNumber = Convert.ToString(reader["SealNumber"]);

            if (ColumnExists(reader, "GrossWeight"))
                if (reader["GrossWeight"] != DBNull.Value)
                    GrossWeight = Convert.ToDecimal(reader["GrossWeight"]);

            if (ColumnExists(reader, "TareWeight"))
                if (reader["TareWeight"] != DBNull.Value)
                    TareWeight = Convert.ToDecimal(reader["TareWeight"]);

            if (ColumnExists(reader, "Package"))
                if (reader["Package"] != DBNull.Value)
                    Package = Convert.ToInt32(reader["Package"]);

            if (ColumnExists(reader, "Part"))
                if (reader["Part"] != DBNull.Value)
                    Part = Convert.ToBoolean(reader["Part"]);

            if (ColumnExists(reader, "ShippingBillNumber"))
                if (reader["ShippingBillNumber"] != DBNull.Value)
                    ShippingBillNumber = Convert.ToString(reader["ShippingBillNumber"]);

            if (ColumnExists(reader, "ShippingBillDate"))
            {
                if (reader["ShippingBillDate"] != DBNull.Value)
                    ShippingBillDate = Convert.ToDateTime(reader["ShippingBillDate"]);
                else
                    ShippingBillDate = null;
            }

            if (ColumnExists(reader, "ImportBLFooterId"))
                if (reader["ImportBLFooterId"] != DBNull.Value)
                    ImportBLFooterId = Convert.ToInt64(reader["ImportBLFooterId"]);

            if (ColumnExists(reader, "IsDeleted"))
            {
                if (reader["IsDeleted"] != DBNull.Value)
                    IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                else
                    IsDeleted = false;
            }
            else
            {
                IsDeleted = false;
            }

            if (ColumnExists(reader, "Unit"))
                if (reader["Unit"] != DBNull.Value)
                    Unit = Convert.ToString(reader["Unit"]);
                else
                    Unit = "0";
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
