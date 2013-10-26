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
    public class DalExpSlotCost
    {
        public static DataTable GetSlotOperatorList(ISearchCriteria searchCriteria)
        {
            string strExecution = "[exp].[prcGetSlotOperatorList]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@SlotOperatorID", Convert.ToInt64(searchCriteria.StringParams[0]));
                oDq.AddVarcharParam("@SchOperatorName", 10, searchCriteria.StringParams[1]);
                oDq.AddVarcharParam("@SortExpression", 30, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);

                return oDq.GetTable();
            }
            //return (DataTable)null;
        }

        public static DataTable GetSlotCost(ISearchCriteria searchCriteria)
        {
            string strExecution = "[exp].[prcGetSlotList]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CompanyID", Convert.ToInt32(searchCriteria.StringParams[4]));
                oDq.AddBigIntegerParam("@SlotID", Convert.ToInt64(searchCriteria.StringParams[5]));
                oDq.AddVarcharParam("@SchOperatorName", 50, searchCriteria.StringParams[1]);
                oDq.AddVarcharParam("@SchPOLName", 30, searchCriteria.StringParams[2]);
                oDq.AddVarcharParam("@SchPODName", 30, searchCriteria.StringParams[3]);
                oDq.AddVarcharParam("@SchLine", 30, searchCriteria.StringParams[0]);
                //oDq.AddVarcharParam("@SchEffDate", 30, searchCriteria.StringParams[4]);
                oDq.AddVarcharParam("@SortExpression", 30, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);

                return oDq.GetTable();
            }
            return (DataTable)null;
        }
        public static bool DeleteSlotCost(long SlotId)
        {
            string strExecution = "[exp].[prcDeleteSlot]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //a.OnOffHire,a.HireReference,a.HireReferenceDate,a.ValidTill,a.ReleaseRefNo,e.PortName
                oDq.AddBigIntegerParam("@pk_SlotID", SlotId);

                var result = oDq.RunActionQuery();
                if (result > 0) return true;
            }
            return false;
        }
        public static int SaveSlotCost(SlotCostModel slotCostModel)
        {
            if (slotCostModel != null)
            {
                string strExecution = "[exp].[prcAddEditSlot]";
                int result = 0, r1;
                using (DbQuery oDq = new DbQuery(strExecution))
                {

                    oDq.AddIntegerParam("@userID", slotCostModel.UserId);
                    oDq.AddBooleanParam("@isedit", false);
                    oDq.AddIntegerParam("@pk_SlotID", 0);
                    oDq.AddIntegerParam("@fk_CompanyID", slotCostModel.CompanyID);
                    oDq.AddIntegerParam("@fk_SlotOperatorID", Convert.ToInt32( slotCostModel.Slot.OPERATOR));
                    oDq.AddIntegerParam("@fk_LineID", Convert.ToInt32(slotCostModel.Slot.LINE));
                    oDq.AddBigIntegerParam("@fk_POD", slotCostModel.Slot.PORTLOADING);
                    oDq.AddBigIntegerParam("@fk_POL", slotCostModel.Slot.PORTDISCHARGE);
                    oDq.AddDateTimeParam("@EffDate", slotCostModel.Slot.EFFECTIVEDATE);
                    oDq.AddVarcharParam("@PODterminal", 50, slotCostModel.Slot.PODTERMINAL);
                    oDq.AddBooleanParam("@SlotStatus", true);
                    oDq.AddBigIntegerParam("@fk_MovOrg", slotCostModel.Slot.MOVORIGIN);
                    //oDq.AddBigIntegerParam("@fk_MovDst", slotCostModel.Slot.MOVDESTINATION);
                    oDq.AddIntegerParam("@Result", 0, QueryParameterDirection.Output);
                    oDq.RunActionQuery();
                    result = Convert.ToInt32(oDq.GetParaValue("@Result"));
                }

                if (result > 0)
                {
                    strExecution = "[exp].[prcAddEditSlotCost]";
                    if (slotCostModel.SlotCostList != null)
                    {
                        int i = slotCostModel.SlotCostList.Count();
                        int j = 0;
                        foreach (var t in slotCostModel.SlotCostList)
                        {
                            using (DbQuery oDq = new DbQuery(strExecution))
                            {
                                oDq.AddIntegerParam("@pk_SlotCostID", 0);
                                oDq.AddIntegerParam("@fk_SlotID", result);
                                oDq.AddCharParam("@CargoType", 1, t.CARGO);
                                oDq.AddIntegerParam("@fk_ContainerType", Convert.ToInt32(t.CONTAINERTYPE));
                                oDq.AddVarcharParam("@CntrSize", 2, t.SIZE);
                                oDq.AddCharParam("@SpecialType", 1, t.TYPE);
                                oDq.AddDecimalParam("@RatePerTon", 12, 2, t.REVTON);
                                oDq.AddDecimalParam("@RatePerCBM", 12, 2, 0);
                                oDq.AddDecimalParam("@ContainerRate", 12, 2, t.AMOUNT);
                                oDq.AddBooleanParam("@SlotCostStatus", true);
                                r1 = oDq.RunActionQuery();
                                if (r1 > 0)
                                {
                                    j++;
                                }
                            }

                        }
                        if (j == i)
                            return i;
                    }
                }


            }

            return 0;
        }
        public static int UpdateSlotCost(SlotCostModel slotCostModel)
        {
            if (slotCostModel != null)
            {
                DeleteSlotCost(slotCostModel.Slot.SLOTID);
                return SaveSlotCost(slotCostModel);
            }
            return 0;
        }

        public static DataTable GetMovementType()
        {
            string strExecution = "[exp].[prcGetMovementType]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                return oDq.GetTable();
            }
            //return (DataTable)null;
        }

        public static SlotCostModel GetSlotCost(long slotID)
        {
            SlotCostModel slotCostModel = null;
            string strExecution = "[exp].[prcGetSlotModel]";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@SlotID", Convert.ToInt64(slotID));
                DataSet ds = oDq.GetTables();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    slotCostModel = new SlotCostModel();
                    slotCostModel.Slot = ds.Tables[0].DataTableToType<Slot>();
                    slotCostModel.SlotCostList = ds.Tables[1].DataTableToCollectionType<SlotCost>();
                }
            }
            return slotCostModel;
        }
    }
}
