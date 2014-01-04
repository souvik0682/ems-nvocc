using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.DAL;
using EMS.Common;

namespace EMS.BLL
{
    public class TerminalDispatchBLL 
    {
        public DataTable GetTerminalDispatchStatement(int VesselID, long POLID, int LineID, int LocationID, int VoyageID)
        {
            return TerminalDiapatchDAL.GetTerminalDispatch(VesselID, POLID, LineID, LocationID, VoyageID);
        }
        //public static ITerminalDispatch GetVessel(int ID)
        //{
        //    //return TerminalDiapatchDAL.GetImportHaulage(ID);
        //}
    }
}
