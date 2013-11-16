﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EMS.Common;
using EMS.Entity;
using EMS.DAL;

namespace EMS.BLL
{
   public class expVoyageBLL
    {
        public List<IexpVoyage> GetVoyage(SearchCriteria searchCriteria)
        {
            return expVoyageDAL.GetVoyage(searchCriteria);
        }
        public long SaveVoyage(IexpVoyage voyage,bool isedit)
        {
            long voyageid = 0;      
            voyageid = expVoyageDAL.SaveVoyage(voyage,isedit);            
            return voyageid;
        }
        public int DeleteVoyage(int voyageid)
        {
            return expVoyageDAL.DeleteVoyage(voyageid);    

        }
        public DataTable GetTerminals(long LocationId)
        {
            return expVoyageDAL.GetTerminals(LocationId);
        }
        //public static IexpVoyage GetImportHaulage()
        //{
        //    return expVoyageDAL.GetImportHaulage();
        //}
        public IexpVoyage GetVoyageById(long VoyageId)
        {
            return expVoyageDAL.GetVogageById(VoyageId);
        }

        public DataTable GetVessels()
        {
            return expVoyageDAL.GetVessels();
        }
    }
}