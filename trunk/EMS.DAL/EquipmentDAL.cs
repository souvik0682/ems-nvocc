using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;

namespace EMS.DAL
{
  public  class EquipmentDAL
    {
      public static DataTable  GetEqpRepair(int userId,IEqpRepairing ieqp)//List<IEqpRepairing>
      {
          List<IEqpRepairing> lstEqpRepair = new List<IEqpRepairing>();
          DataTable dt = new DataTable();
          string proc = "prcGetEqpRepair";
          using (DbQuery oDq = new DbQuery(proc))
          {
              oDq.AddIntegerParam("@pk_RepairID", ieqp.pk_RepairID);
              oDq.AddVarcharParam("@CntrNo", 11, ieqp.ContainerNo);
              oDq.AddVarcharParam("@Location", 50, ieqp.Location);
              dt = oDq.GetTable();
              ////DataTableReader dr = oDq.GetTableReader();
              ////while (dr.Read())
              ////{
              ////    EquipmentRepairEntity erapair = new EquipmentRepairEntity(dr);
              ////    dt.Rows.Add(dr);
              ////    lstEqpRepair.Add(erapair);                                                                                                                                                                                                                                                                                                               
              ////}
              ////dr.Close();
          }
          return dt;// lstEqpRepair;
      }

      public static DataSet DDLGetLine()
      {
          DataSet ds = new DataSet();
          using (DbQuery dq = new DbQuery("Select pk_ProspectID ListItemValue,ProspectName ListItemText from DSR.dbo.mstProspectFor where active='y'", true))
          {
              ds = dq.GetTables();
          }
          return ds;
      }

      public static DataSet DDLGetStatus()
      {
          DataSet ds = new DataSet();
          using (DbQuery dq = new DbQuery("Select pk_movementid ListItemValue, MoveAbbr ListItemText from mstContainerMovement where movestatus=1", true))
          {
              ds = dq.GetTables();
          }
          return ds;
      }

      public static DataSet DDLGetContainerType()
      {
          DataSet ds = new DataSet();
          using (DbQuery dq = new DbQuery("Select pk_ContainerTypeid ListItemValue, ContainerAbbr ListItemText from mstContainerType", true))
          {
              ds = dq.GetTables();
          }
          return ds;
      }

      public static int AddEditEquipEstimate(int userID,bool isEdit, IEqpRepairing ieqp)
      {
          int result = 0;
          using (DbQuery dq = new DbQuery("prcAddEditEquipEstimate"))
          {
              dq.AddIntegerParam("@pk_RepairID", ieqp.pk_RepairID);
              dq.AddIntegerParam("@LocationId", ieqp.locId);
              dq.AddDecimalParam("@RepMaterialEst",12,2,ieqp.RepMaterialEst);
              dq.AddDecimalParam("@RepMaterialAppr",12,2,ieqp.RepMaterialAppr);
              dq.AddDecimalParam("@RepMaterialBilled",12,2,ieqp.RepMaterialBilled);
              dq.AddDecimalParam("@RepLabourEst", 12, 2, ieqp.RepLabourEst);
              dq.AddDecimalParam("@RepLabourBilled",12,2,ieqp.RepLabourBilled);
              dq.AddDecimalParam("@RepLabourAppr", 12, 2, ieqp.RepLabourAppr);
              dq.AddDateTimeParam("@TransactionDate", ieqp.TransactionDate);
              dq.AddVarcharParam("ContainerNo", 11, ieqp.ContainerNo);
              dq.AddVarcharParam("@EstimateReference", 100,ieqp.EstimateReference);
              dq.AddIntegerParam("@fk_NVOCCID",ieqp.NVOCCId);
              dq.AddBooleanParam("@OnHold",ieqp.onHold);
              dq.AddDateTimeParam("@Releasedon",ieqp.RealeasedOn);
              dq.AddBooleanParam("@Damaged",ieqp.Damaged);
              dq.AddDateTimeParam("@StockReturnDate",ieqp.StockReturnDate);
              dq.AddVarcharParam("@Reason",100,ieqp.Reason);
              dq.AddIntegerParam("@fk_UserApproved", ieqp.fk_UserApproved);
              dq.AddIntegerParam("@userId", userID);
              dq.AddBooleanParam("@isEdit", isEdit);
              result= dq.RunActionQuery();


             
            
          }

          return result; 
      }

      public static DataSet CheckContainerStatus(string ContainerNo)
      {
          DataSet ds=null;
          using (DbQuery dq = new DbQuery("prcGetContainerStatus"))
          {
              dq.AddVarcharParam("@CntrNo", 11, ContainerNo);
              ds= dq.GetTables();
          }

          return ds;
      }


      
      public static DataTable GetContainerList(int LocId, string Initial)
      {
          //GetBLNo
          string strExecution = "[trn].[GetContainerList]";
          DataTable myDataTable;

          using (DbQuery oDq = new DbQuery(strExecution))
          {
              oDq.AddVarcharParam("@InitialChar", 250, Initial);
              oDq.AddIntegerParam("@LocationID", LocId);

              myDataTable = oDq.GetTable();
          }
          return myDataTable;
      }


      public static DataTable GetContainerList(int LocId,int LineId, string Initial)
      {
          //GetBLNo
          string strExecution = "[trn].[GetContainerList]";
          DataTable myDataTable;

          using (DbQuery oDq = new DbQuery(strExecution))
          {
              oDq.AddVarcharParam("@InitialChar", 250, Initial);
              oDq.AddIntegerParam("@LocationID", LocId);
              oDq.AddIntegerParam("@LineId", LineId);              

              myDataTable = oDq.GetTable();
          }
          return myDataTable;
      }

    }
}
