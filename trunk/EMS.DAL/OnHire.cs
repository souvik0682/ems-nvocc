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
    public sealed class OnHireDAL
    {
        public static bool ValidateOnHire(string  ContainerId,string OnOrOff)
        {
            string strExecution = "[admin].[prcCheckContainer]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@ContainerNo", 11, ContainerId);
                oDq.AddVarcharParam("@OnOrOff", 1, OnOrOff);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["cnt"]) > 0)
                        return true;
                }

                reader.Close();
            }
            return false;
        }
        public static bool ValidateContainerStatus(string ContainerId)
        {
            string strExecution = "[admin].[uspCheckContainerStatus]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@ContainerNo", 11, ContainerId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["cnt"]) > 0)
                        return true;
                }

                reader.Close();
            }
            return false;
        }
        public static DataTable GetOnHire(ISearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[prcGetAllOnHire]";
            

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //a.OnOffHire,a.HireReference,a.HireReferenceDate,a.ValidTill,a.ReleaseRefNo,e.PortName
                oDq.AddVarcharParam("@HireID", 11, searchCriteria.StringOption4);
                oDq.AddVarcharParam("@ContainerNo", 11, searchCriteria.StringOption1);
                oDq.AddVarcharParam("@RefNo", 50, searchCriteria.StringOption2);
                oDq.AddVarcharParam("@RefDate", 50, searchCriteria.StringOption3);
                oDq.AddVarcharParam("@Direct",50, searchCriteria.SortDirection);
                oDq.AddVarcharParam("@SortBy", 50, searchCriteria.SortExpression);                
               return oDq.GetTable();
            }
           // return (DataTable)null;
        }
        public static bool DeleteOnHire(long HireId)
        {
            string strExecution = "[admin].[uspRemoveHire]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //a.OnOffHire,a.HireReference,a.HireReferenceDate,a.ValidTill,a.ReleaseRefNo,e.PortName
                oDq.AddBigIntegerParam("@HireId", HireId);
                oDq.AddIntegerParam("@Status", 0, QueryParameterDirection.Output);
                oDq.RunActionQuery();
               var result = Convert.ToInt32(oDq.GetParaValue("@Status"));
               if (result == 1) return true;
            }
            return false;
        }

        
        public static int SaveOnHire(EMS.Common.IEqpOnHire eqpOnHire)
        {
            string strExecution = "[admin].[uspSaveEqpOnHire]";
            int result = 0;            
            using (DbQuery oDq = new DbQuery(strExecution))
            {
               
                oDq.AddCharParam("@OnOffHire", 1, eqpOnHire.OnOffHire);
                oDq.AddIntegerParam("@NVOCCID", eqpOnHire.NVOCCID);
                oDq.AddIntegerParam("@LeaseID", eqpOnHire.LeaseID);
                oDq.AddIntegerParam("@CompanyID", eqpOnHire.CompanyID);
                oDq.AddIntegerParam("@LocationID", eqpOnHire.LocationID);
                oDq.AddVarcharParam("@HireReference", 20, eqpOnHire.HireReference);
                oDq.AddDateTimeParam("@HireReferenceDate", eqpOnHire.HireReferenceDate);
                oDq.AddDateTimeParam("@ValidTill", eqpOnHire.ValidTill);
                oDq.AddBigIntegerParam("@ReturnedPortID", eqpOnHire.ReturnedPortID);
                oDq.AddVarcharParam("@Narration", 100, eqpOnHire.Narration);
                oDq.AddVarcharParam("@ReleaseRefNo", 100, eqpOnHire.ReleaseRefNo);
                oDq.AddDateTimeParam("@ReleaseRefDate", eqpOnHire.ReleaseRefDate);
                oDq.AddIntegerParam("@TEUs", eqpOnHire.TEUs);
                oDq.AddIntegerParam("@FEUs", eqpOnHire.FEUs);
                oDq.AddIntegerParam("@UserAdded", eqpOnHire.UserAdded);
                oDq.AddIntegerParam("@UserLastEdited", eqpOnHire.UserLastEdited);
                oDq.AddDateTimeParam("@AddedOn", DateTime.Now);
                oDq.AddDateTimeParam("@EditedOn", DateTime.Now);
                oDq.AddVarcharParam("@xml",int.MaxValue, eqpOnHire.LstEqpOnHireContainer.CreateFromEnumerable());
                oDq.AddIntegerParam("@return",0,QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@return"));
            }

            return result;
        }
        public static int   UpdateOnHire(EMS.Common.IEqpOnHire eqpOnHire)
        {
            string strExecution = "[admin].[uspUpdateEqpOnHire]";
            int result = 0;
            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddBigIntegerParam("@HireId", eqpOnHire.HireID);
                oDq.AddCharParam("@OnOffHire", 1, eqpOnHire.OnOffHire);
                oDq.AddIntegerParam("@NVOCCID", eqpOnHire.NVOCCID);
                oDq.AddIntegerParam("@CompanyID", eqpOnHire.CompanyID);

                oDq.AddIntegerParam("@LocationID", eqpOnHire.LocationID);
                oDq.AddVarcharParam("@HireReference", 20, eqpOnHire.HireReference);
                oDq.AddDateTimeParam("@HireReferenceDate", eqpOnHire.HireReferenceDate);
                oDq.AddDateTimeParam("@ValidTill", eqpOnHire.ValidTill);

                oDq.AddBigIntegerParam("@ReturnedPortID", eqpOnHire.ReturnedPortID);
                oDq.AddVarcharParam("@Narration", 100, eqpOnHire.Narration);
                oDq.AddVarcharParam("@ReleaseRefNo", 100, eqpOnHire.ReleaseRefNo);
                oDq.AddDateTimeParam("@ReleaseRefDate", eqpOnHire.ReleaseRefDate);

                oDq.AddIntegerParam("@TEUs", eqpOnHire.TEUs);
                oDq.AddIntegerParam("@FEUs", eqpOnHire.FEUs);
                oDq.AddIntegerParam("@UserAdded", eqpOnHire.UserAdded);
                oDq.AddIntegerParam("@UserLastEdited", eqpOnHire.UserLastEdited);

                oDq.AddDateTimeParam("@AddedOn", eqpOnHire.AddedOn);
                oDq.AddDateTimeParam("@EditedOn", DateTime.Now);
                oDq.AddVarcharParam("@xml", int.MaxValue, eqpOnHire.LstEqpOnHireContainer.CreateFromEnumerable());
                oDq.AddIntegerParam("@return", 0, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@return"));
            }

            return result;
        }

        public static DataTable GetContainerInfo(string ContainerId)
        {
            string strExecution = "[admin].[uspContainerInfo]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@ContainerNo", 11, ContainerId);
                return oDq.GetTable();
            }
            // return (DataTable)null;
        }

        public static DataTable GetLeaseRefList(int Loc, int Line)
        {
            string strExecution = "[dbo].[uspGetPendingLeaseList]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocID", Loc);
                oDq.AddIntegerParam("@LineID", Line);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetLeaseForOnhire(int LeaseID)
        {
            string strExecution = "[dbo].[prcGetLeaseList]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_LeaseID", LeaseID);
                oDq.AddVarcharParam("@SortExpression", 30, "");
                oDq.AddVarcharParam("@SortDirection", 4, "");  
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
    }
}
