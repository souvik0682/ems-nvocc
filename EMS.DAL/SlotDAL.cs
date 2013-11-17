using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using System.Data.SqlClient;
using EMS.Entity;
using EMS.Utilities;

namespace EMS.DAL
{
    public sealed class SlotDAL
    {
        private SlotDAL()
        {

        }
        public static int AddEditSlot(ISlot Slot)
        {
            string strExecution = "[exp].[prcAddEditSlot]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@SlotID", Slot.SlotID);
                oDq.AddIntegerParam("@fk_CompanyID", Slot.CompanyID);
                oDq.AddIntegerParam("@fk_LineID", Slot.LineID);
                oDq.AddIntegerParam("@fk_SlotOperator", Slot.SlotOperatorID);
                oDq.AddBooleanParam("@SlotStatus", Slot.SlotStatus);
                oDq.AddDateTimeParam("@EffectDt", Slot.EffectDt);
                oDq.AddVarcharParam("@PODTerminal", 50, Slot.PODTerminal);
                oDq.AddBigIntegerParam("@fk_POD", Slot.PODID);
                oDq.AddBigIntegerParam("@fk_POL", Slot.POLID);
                oDq.AddIntegerParam("@userID", Slot.CreatedBy);
                oDq.AddIntegerParam("@RESULT", Result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                Result = Convert.ToInt32(oDq.GetParaValue("@RESULT"));

            }
            return Result;
        }

        public static int DeleteSlot(int SlotId)
        {
            string strExecution = "[exp].[prcDeleteSlot]";
            int Result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_SlotID", SlotId);
                Result = oDq.RunActionQuery();
            }
            return Result;
        }

        public static DataTable GetAllSlots(SearchCriteria searchCriteria, int CompanyId)
        {
            string strExecution = "[exp].[prcGetSlotList]";
            //List<ICharge> lstCharge = new List<ICharge>();
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CompanyId", CompanyId);
                oDq.AddVarcharParam("@SchOperatorName", 200, searchCriteria.SlotOperatorName);
                oDq.AddVarcharParam("@SchLine", 200, searchCriteria.LineName);
                oDq.AddVarcharParam("@SchPOLName", 50, searchCriteria.POL);
                oDq.AddVarcharParam("@SchPODName", 50, searchCriteria.POD);
                //oDq.AddDateTimeParam("@EDate", searchCriteria.Date);
                oDq.AddVarcharParam("@SortExpression", 20, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);

                dt = oDq.GetTable();
                //DataTableReader reader = oDq.GetTableReader();

                //while (reader.Read())
                //{
                //    ICharge Chg = new ChargeEntity(reader);
                //    lstCharge.Add(Chg);
                //}
                //reader.Close();
            }
            return dt;
        }

        public static DataTable GetSlot(SearchCriteria searchCriteria)
        {
            string strExecution = "[exp].[prcGetSlotListForEdit]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //a.OnOffHire,a.HireReference,a.HireReferenceDate,a.ValidTill,a.ReleaseRefNo,e.PortName
                oDq.AddIntegerParam("@SlotID", Convert.ToInt32(searchCriteria.StringOption4));
                //oDq.AddVarcharParam("@SlotID", 11, searchCriteria.StringOption4);
                //oDq.AddVarcharParam("@ContainerNo", 11, searchCriteria.StringOption1);
                //oDq.AddVarcharParam("@RefNo", 50, searchCriteria.StringOption2);
                //oDq.AddVarcharParam("@RefDate", 50, searchCriteria.StringOption3);
                //oDq.AddVarcharParam("@SortDirection", 50, searchCriteria.SortDirection);
                //oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                return oDq.GetTable();
            }
            // return (DataTable)null;
        }

        public static int SaveSlot(EMS.Common.ISlot Slot, int isEdit)
        {
            string strExecution = "[exp].[prcAddEditSlot]";
            int result = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                
                oDq.AddIntegerParam("@UserID", Slot.CreatedBy);
                oDq.AddIntegerParam("@isEdit", isEdit);
                oDq.AddIntegerParam("@fk_LineID", Slot.LineID);
                oDq.AddIntegerParam("@fk_CompanyID", Slot.CompanyID);
                oDq.AddIntegerParam("@fk_SlotOperatorID", Slot.SlotOperatorID);
                oDq.AddIntegerParam("@fk_POL", Slot.POLID);
                oDq.AddIntegerParam("@fk_POD", Slot.PODID);
                oDq.AddDateTimeParam("@EffDate", Slot.EffectDt);
                oDq.AddVarcharParam("@PODTerminal", 50, Slot.PODTerminal);
                oDq.AddIntegerParam("@UserLastEdited", Slot.ModifiedBy);
                oDq.AddDateTimeParam("@AddedOn", DateTime.Now);
                oDq.AddDateTimeParam("@EditedOn", DateTime.Now);
                //oDq.AddVarcharParam("@xml", int.MaxValue, Slot.LstSlotCost.CreateFromEnumerable());
                oDq.AddIntegerParam("@return", 0, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@return"));
            }

            return result;
        }
    }
}
