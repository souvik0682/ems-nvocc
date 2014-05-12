using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using System.Data;
using System.Data.SqlClient;
using EMS.Entity;

namespace EMS.BLL
{
    public class OnHireBLL
    {
        public static DataTable GetContainerInfo(string ContainerId)
        {
            return EMS.DAL.OnHireDAL.GetContainerInfo(ContainerId);
        }
        public static bool ValidateContainerStatus(string ContainerId)
        {
            return EMS.DAL.OnHireDAL.ValidateContainerStatus(ContainerId);
        }
        public bool ValidateOnHire(string  ContainerId,string OnOrOff)
        {
            return EMS.DAL.OnHireDAL.ValidateOnHire(ContainerId,OnOrOff);
        }
        public bool DeleteOnHire(long HireId) {
            return EMS.DAL.OnHireDAL.DeleteOnHire(HireId);
        }
        public DataTable GetOnHire(ISearchCriteria searchCriteria) {
            return EMS.DAL.OnHireDAL.GetOnHire(searchCriteria);
        }
        public int SaveOnHire(EMS.Common.IEqpOnHire eqpOnHire)
        {

            return EMS.DAL.OnHireDAL.SaveOnHire(eqpOnHire);
        }
        public int UpdateOnHire(EMS.Common.IEqpOnHire eqpOnHire) { return EMS.DAL.OnHireDAL.UpdateOnHire(eqpOnHire); }

        public static DataTable GetLeaseRefList(int Loc, int Line)
        {
            return EMS.DAL.OnHireDAL.GetLeaseRefList(Loc, Line);
        }

        public static DataTable GetLeaseForOnhire(int LeaseID)
        {
            return EMS.DAL.OnHireDAL.GetLeaseForOnhire(LeaseID);
            
        }

        public static DataTable GetContainerType(int containerType)
        {
            return EMS.DAL.OnHireDAL.GetContainerType(containerType);
        }
    }
}
