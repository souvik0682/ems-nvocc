
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;

namespace EMS.DAL
{
    public class expVoyageDAL
    {
        public static List<IexpVoyage> GetVoyage(SearchCriteria searchCriteria)
        {
            string strExecution = "[exp].[prcGetVoyage]";
            List<IexpVoyage> lstVoyage = new List<IexpVoyage>();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@pk_VoyageID", searchCriteria.VoyageID);
                oDq.AddVarcharParam("@vesselName", 70, searchCriteria.Vessel);
                oDq.AddVarcharParam("@VoyageNo", 10, searchCriteria.Voyage);
                oDq.AddVarcharParam("@Location", 100, searchCriteria.Location);
                oDq.AddIntegerParam("@locationid", searchCriteria.LocationID);
                oDq.AddVarcharParam("@Terminal", 100, searchCriteria.Terminal);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IexpVoyage ivoyage = new expVoyageEntity(reader);
                    lstVoyage.Add(ivoyage);
                }

                reader.Close();
            }
            return lstVoyage;
        }

        public static long SaveVoyage(IexpVoyage voyage,bool isedit)
        {
            string strExecution = "[exp].[prcAddEditVoyage]";
            long voyageid = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                             
                    oDq.AddBigIntegerParam("@userID", voyage.UserAdded);
                    oDq.AddBooleanParam("@isedit", isedit);//false for insert and true for update
                    oDq.AddBigIntegerParam("@pk_VoyageID", voyage.VoyageID);
                    oDq.AddBigIntegerParam("@fk_VesselID", voyage.VesselID);
                    oDq.AddIntegerParam("@fk_LocationID", voyage.LocationID);
                    oDq.AddIntegerParam("@fk_TerminalID", voyage.TerminalID);
                    oDq.AddVarcharParam("@VoyageNo", 10, voyage.VoyageNo);
                    oDq.AddDateTimeParam("@SailingDate", voyage.SailDate);
                    oDq.AddDateTimeParam("@PCCDate", voyage.PCCDate);
                    oDq.AddVarcharParam("@PCCNo", 14, voyage.PCCNo);
                    oDq.AddVarcharParam("@AgentCode", 50, voyage.AgentCode);
                    oDq.AddVarcharParam("@LineCode", 50, voyage.LineCode);
                    oDq.AddVarcharParam("@VCNNo", 14, voyage.VCNNo);
                    oDq.AddVarcharParam("@VIA", 10, voyage.VIA);
                    oDq.AddVarcharParam("@RotationNo", 50, voyage.RotationNo);
                    oDq.AddDateTimeParam("@RotationDate", voyage.RotationDate);
                    oDq.AddBigIntegerParam("@fk_LoadPortID", voyage.POL);
                    oDq.AddBigIntegerParam("@fk_DestPortID", voyage.POD);
                    oDq.AddBigIntegerParam("@fk_NextPortID", voyage.NextPortID);
                    oDq.AddDateTimeParam("@ETD", voyage.ETD);
                    oDq.AddDateTimeParam("@ETA", voyage.ETA);
                    oDq.AddDateTimeParam("@ETANextPort", voyage.ETANextPort);
                    oDq.AddDateTimeParam("@VslCutoff", voyage.VesselCutOffDate);
                    oDq.AddDateTimeParam("@DocCutOff", voyage.DocsCutOffDate);
                    voyageid = Convert.ToInt64(oDq.GetScalar());
               
            }

            return voyageid;
        }
        public static DataTable GetTerminals(long LocationId)
        {
            string strExecution = "[exp].[usp_Voyage_GetTerminals]";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@LocationID", LocationId);
                dt = oDq.GetTable();
            }
            return dt;
        }
        //public static IexpVoyage GetImportHaulage()
        //{
        //    string strExecution = "[mst].[spGetImportHaulageCharge]";
        //    IexpVoyage ovg = null;
        //    using (DbQuery oDq = new DbQuery(strExecution))
        //    {
        //        //oDq.AddIntegerParam("@HaulageChgID", ID);
        //        DataTableReader reader = oDq.GetTableReader();

        //        while (reader.Read())
        //        {

        //            ovg = new expVoyageEntity(reader);
        //        }
        //        reader.Close();
        //    }
        //    return ovg;
        //}
        public static int DeleteVoyage(int VoyageID)
        {
            string strExecution = "[exp].[prcDeleteVoyage]";
            int ret = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@pk_VoyageID", VoyageID);

                ret = Convert.ToInt32(oDq.GetScalar());
            }

            return ret;
        }
        public static IexpVoyage GetVogageById(long voyageId)
        {
            string strExecution = "[exp].[usp_Voyage_GetVoyageById]";
            IexpVoyage voyage = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@voyageID", voyageId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    voyage = new expVoyageEntity(reader);
                }
                reader.Close();
            }

            return voyage;
        }

        public static DataTable GetVessels()
        {
            string strExecution = "[exp].[GetAllVessels]";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //oDq.AddBigIntegerParam("@LocationID", LocationId);
                dt = oDq.GetTable();
            }
            return dt;
        }

    }
}
