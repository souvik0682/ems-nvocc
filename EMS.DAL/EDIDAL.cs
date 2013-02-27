using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;
using EMS.Entity;
using EMS.Common;

namespace EMS.DAL
{
    public class EDIDAL
    {
        public static List<IVesselVoyageEDI> GetVesselVoyageFrEDI(int VesselID,int VoyageID)
        {
            string ProcName = "prcGetVesselVoyageFrEDI";
            List<IVesselVoyageEDI> lstVesselVoyageEDI = new List<IVesselVoyageEDI>();

            using (DbQuery oDq = new DbQuery(ProcName))
            {
                oDq.AddIntegerParam("@VesselID", VesselID);
                oDq.AddIntegerParam("@VoyageID", VoyageID);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    VesselVoyageEDI vessVoyageEDi = new VesselVoyageEDI(reader);
                    vessVoyageEDi.VesselID = VesselID;
                    vessVoyageEDi.VoyageID = VoyageID;
                    lstVesselVoyageEDI.Add(vessVoyageEDi);
                }

                reader.Close();
            }

            return lstVesselVoyageEDI;
        }

        public static DataSet GetEDICargoInfo(int VesselID, int VoyageId)
        {
          
            string ProcName = "GetEDICargoInfo";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@VoyageID", VoyageId);
            dquery.AddIntegerParam("@VesselID", VesselID);

            return dquery.GetTables();

        }

        public static DataSet GetEDIContainerInfo(int VesselID, int VoyageId)
        {

            string ProcName = "prcGetContainEDI";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@VoyageID", VoyageId);
            dquery.AddIntegerParam("@VesselID", VesselID);

            return dquery.GetTables();

        }



        public static DataSet GetCustomHouse()
        {
            string ProcName = "prcGetCustomHouse";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);


            return dquery.GetTables();
        }

        public static DataSet GetTerminalOperator(int VoyageID)
        {
            string ProcName = "prcGetTerminalOperator";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@VoyageID", VoyageID);
           

            return dquery.GetTables();
        }
    }
}
