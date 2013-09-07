﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL.DbManager;
using EMS.Common;

namespace EMS.DAL
{
   public class TerminalDiapatchDAL
    {
       public static DataTable GetTerminalDispatch(int VesselID, long POLID, int LineID, int LocationID)
        {
            string strExecution = "[exp].[rptTerminalDespatchReport]";
            DataTable dt = new DataTable();
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                dt = oDq.GetTable();
            }
            return dt;
        }    
       public static int GetVesselId(string VesseleName)
       {
           string strExecution = "uspGetVesselIdByVesselName";
           int vesselId = 0;


           using (DbQuery oDq = new DbQuery(strExecution))
           {
               oDq.AddVarcharParam("@VesselName", 60, VesseleName);

               vesselId = Convert.ToInt32(oDq.GetScalar());
           }

           return vesselId;
       }

    }
}
