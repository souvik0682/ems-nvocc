using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Entity
{
    public class EquipmentRepairEntity : EMS.Common.IEqpRepairing
    {
        #region EntityMember

        public int pk_RepairID { get; set; }
        public string ContainerNo { get; set; }
        public string Location { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string EstimateReference { get; set; }
        public string Line { get; set; }
        

        public decimal? RepLabourEst { get; set; }
        public decimal? RepLabourBilled { get; set; }
        public decimal? RepMaterialBilled { get; set; }
        public decimal? RepMaterialAppr { get; set; }
        public decimal? RepLabourAppr { get; set; }
        public int? fk_UserApproved { get; set; }
        public bool onHold { get; set; }
        public DateTime? RealeasedOn { get; set; }
        public bool Damaged { get; set; }
        public DateTime? StockReturnDate { get; set; }
        public string Reason { get; set; }
       
        public decimal? RepMaterialEst { get; set; }
        public int? NVOCCId { get; set; }

        public int ProspectID { get; set; }
        public int locId { get; set; }

        #endregion

        public EquipmentRepairEntity() { }
        public EquipmentRepairEntity(System.Data.DataTableReader dr)
        {
            this.pk_RepairID = Convert.ToInt32(dr["pk_RepairID"]);
            this.ContainerNo = Convert.ToString(dr["ContainerNo"]);
            this.Location = Convert.ToString(dr["Location"]);
            this.Damaged = Convert.ToBoolean(dr["Damaged"]);
            this.EstimateReference = Convert.ToString(dr["EstimateReference"]);
            this.fk_UserApproved = Convert.ToInt32(dr["fk_UserApproved"]);
           
            this.Line = Convert.ToString(dr["Line"]);
            this.RepMaterialAppr = Convert.ToDecimal(dr["MaterialAppr"]);
            this.RepMaterialEst = Convert.ToDecimal(dr["MaterialEst"]);
            this.RepLabourEst = Convert.ToDecimal(dr["LabourEst"]);
            this.onHold = Convert.ToBoolean(dr["onHold"]);
            this.RealeasedOn = DBNull.ReferenceEquals(dr["RealeasedOn"],DBNull.Value)? (Nullable<DateTime>)null:  Convert.ToDateTime(dr["RealeasedOn"]);
            this.NVOCCId = Convert.ToInt32(dr["NVOCCID"]);
            this.Reason = Convert.ToString(dr["Reason"]);
            this.RepLabourAppr = Convert.ToDecimal(dr["RepLabourAppr"]);
            this.RepLabourBilled = Convert.ToDecimal(dr["RepLabourBilled"]);
           
            this.RepMaterialBilled = Convert.ToDecimal(dr["RepMaterialBilled"]);

            this.StockReturnDate = DBNull.ReferenceEquals(dr["StockReturnDate"], DBNull.Value) ? (Nullable<DateTime>)null : Convert.ToDateTime(dr["StockReturnDate"]);
            this.TransactionDate = DBNull.ReferenceEquals(dr["TransactionDate"], DBNull.Value) ? (Nullable<DateTime>)null : Convert.ToDateTime(dr["TransactionDate"]);
            

        }




        
        
    }
}
