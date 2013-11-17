using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;

namespace EMS.DAL
{
    public class VoyageDAL
    {
        public static DataSet DDLGetVoyageAccVessel_Loc(int locationId, int vesselId, int nvoccId)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcGetVoyageAccVessel_Loc"))
            {
                dq.AddIntegerParam("@LocationId", locationId);
                dq.AddIntegerParam("@vesselId", vesselId);
                dq.AddIntegerParam("@nvoccID", nvoccId);
                ds = dq.GetTables();
            }
            return ds;
        }

        public static DataSet DDLGetVoyageAccVessel_Loc_ICD(int locationId, int vesselId, int nvoccId)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcGetVoyageAccVessel_Loc_ICD"))
            {
                dq.AddIntegerParam("@LocationId", locationId);
                dq.AddIntegerParam("@vesselId", vesselId);
                dq.AddIntegerParam("@nvoccID", nvoccId);
                ds = dq.GetTables();
            }
            return ds;
        }

        public static DataTable IfExistInBL(int vesselId, int voyageId)
        {

            DataTable ds = new DataTable();
            using (DbQuery dq = new DbQuery("select * from ImpBLHeader where fk_ImpVesselID=" + vesselId + " and fk_ImpVoyageID=" + @voyageId, true))
            {
                //dq.AddIntegerParam("@vesselId", vesselId);
                //dq.AddIntegerParam("@voyageId", voyageId);

                ds = dq.GetTable();
            }
            return ds;
        }

        public static int VoyageLandingDateEntry(int vesselId, int voyageId, int Pod, DateTime? LandingDate, DateTime? OldLandingDate, int UserId)
        {
            string ProcName = "prcVoyageLandingDateEntry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@UserId", UserId);
            dquery.AddIntegerParam("@vesselId", vesselId);
            dquery.AddIntegerParam("@voyageId", voyageId);
            dquery.AddDateTimeParam("@LandingDate", LandingDate);
            dquery.AddDateTimeParam("@OldLandingDate", OldLandingDate);
            dquery.AddIntegerParam("@Pod", Pod);

            return dquery.RunActionQuery();
        }

        public static string CheckVoyageEntryAbilty(int vesselId, string VoyageNo, int Pod, DateTime? LandingDate, decimal XchangeRate, bool isEdit)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcCheckVoyageEntryAbilty"))
            {
                dq.AddIntegerParam("@vesselId", vesselId);
                dq.AddVarcharParam("@VoyageNo", 10, VoyageNo);
                dq.AddIntegerParam("@Pod", Pod);
                dq.AddDateTimeParam("@LandingDate", LandingDate);
                dq.AddDecimalParam("@XchangeRate", 12, 2, XchangeRate);
                dq.AddBooleanParam("@isEdit", isEdit);
                ds = dq.GetTables();
            }
            if (Convert.ToString(ds.Tables[0].Rows[0][0]).ToLower() == "false")
                return Convert.ToString(ds.Tables[0].Rows[0][1]);
            else
                return "True";
        }

        public static int AddEditMLVoyage(string voyageId, string vesselId, string MLvoyageNo, int _userId, bool isEdit)
        {
            int intvesselId = 0;
            int.TryParse(vesselId, out  intvesselId);

            int intVoyageId = 0;
            int.TryParse(voyageId, out intVoyageId);

            //dtActivity = dtActivity == "" ? DateTime.Now.ToShortDateString() : dtActivity;
            //DateTime dt = Convert.ToDateTime(dtActivity);
            string ProcName = "prcAddEditMLVoyage";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@UserId", _userId);
            dquery.AddIntegerParam("@VoyageId",intVoyageId);
            dquery.AddIntegerParam("@vesselId", intvesselId);
            dquery.AddVarcharParam("@MLvoyageNo", 15, MLvoyageNo);
            //dquery.AddDateTimeParam("@ActivityDate", dt);
            //dquery.AddIntegerParam("@fk_PortID", fk_PortID);
            dquery.AddBooleanParam("@isEdit", isEdit);

            return dquery.RunActionQuery();
        }

        public static DataSet GetVessleByNVOCC(int nvoccId)
        {
            DataSet ds = new DataSet();
            using (DbQuery dq = new DbQuery("prcGetVesselByNVOCCID"))
            {
                dq.AddIntegerParam("@line", nvoccId);
                ds = dq.GetTables();
            }
            return ds;
        }

    }
}
