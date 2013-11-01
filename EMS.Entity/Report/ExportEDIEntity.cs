using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.Entity.Report
{
    public class ExportEDIEntity
    {
        #region Public Properties

        public string MainLineOperator { get; set; }
        public string VesselOperator { get; set; }
        public string AgentName { get; set; }
        public string VesselVoyage { get; set; }
        public string ExportRotationNo { get; set; }
        public DateTime? ExportRotationDate { get; set; }
        public DateTime? SailDate { get; set; }
        public string GatewayPort { get; set; }
        public string ShippingBillNo { get; set; }
        public DateTime? ShippingBillDate { get; set; }
        public string SBFiled { get; set; }
        public string DestinationPort { get; set; }
        public string TotalQty { get; set; }
        public string Package { get; set; }
        public string CntrNos { get; set; }
        public string Status { get; set; }

        #endregion

        #region Constructors

        public ExportEDIEntity()
        {

        }

        public ExportEDIEntity(DataTableReader reader, bool isHeader)
        {
            if (isHeader)
            {
                //this.MainLineOperator = Convert.ToString(reader["MainLineOperator"]);
                this.VesselOperator = Convert.ToString(reader["VESSELOPERATOR"]);
                //this.AgentName = Convert.ToString(reader["AgentName"]);
                //this.VesselVoyage = Convert.ToString(reader["VesselVoyage"]);
                this.ExportRotationNo = Convert.ToString(reader["EGMNo"]);
                if (reader["EGMDate"] != DBNull.Value) this.ExportRotationDate = Convert.ToDateTime(reader["EGMDate"]);
                //if (reader["SailDate"] != DBNull.Value) this.SailDate = Convert.ToDateTime(reader["SailDate"]);
                this.GatewayPort = Convert.ToString(reader["GateWayPort"]);
            }
            else
            {
                this.GatewayPort = Convert.ToString(reader["GateWayPort"]);
                this.ShippingBillNo = Convert.ToString(reader["ShippingBillNo"]);
                if (reader["ShippingBillDate"] != DBNull.Value) this.ShippingBillDate = Convert.ToDateTime(reader["ShippingBillDate"]);
                //this.SBFiled = Convert.ToString(reader["SBFiled"]);
                this.DestinationPort = Convert.ToString(reader["DestinationPort"]);
                //this.TotalQty = Convert.ToString(reader["TotalQty"]);
                this.Package = Convert.ToString(reader["PACKAGE"]);
                this.CntrNos = Convert.ToString(reader["CONTAINERNO"]);
                this.Status = Convert.ToString(reader["CONTSTATUS"]);
            }
        }

        #endregion
    }
}
