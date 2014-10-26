using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Data;

namespace EMS.BLL
{
    public class fwLocBLL
    {
        public static List<IFwLocation> GetfwLoc(SearchCriteria searchCriteria)
        {
            return fwLocationDAL.GetfwLoc(searchCriteria);
        }

        public void SaveFwLoc(IFwLocation Loc)
        {
            fwLocationDAL.SaveLocation(Loc);
        }

        public static int DeletFwLoc(int LocID, int UserID)
        {
            return fwLocationDAL.DeleteFwLoc(LocID, UserID);
        }

        public static IFwLocation GetFLoc(int ID)
        {
            return fwLocationDAL.GetFLoc(ID);
        }
    }
}
