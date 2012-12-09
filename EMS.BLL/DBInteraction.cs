using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EMS.BLL
{
    public class DBInteraction
    {
        #region General
        public bool IsUnique(string tableName, string colName, string val)
        {

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select count(*) from " + tableName + " where " + colName + " = '" + val + "'", true);
            bool returnval = false;
            try
            {
                returnval = Convert.ToInt32(dquery.GetScalar()) > 0 ? false : true;
            }
            catch (Exception)
            {


            }

            return returnval;

        }
        #endregion

        #region country

        public DataSet GetCountry(params object[] sqlParam)
        {
            string ProcName = "prcGetCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_countryId", Convert.ToInt32(sqlParam[0]));
            dquery.AddVarcharParam("@CountryName", 200, Convert.ToString(sqlParam[1]));


            return dquery.GetTables();
        }

        public void DeleteCountry(int countryId)
        {
            string ProcName = "prcDeleteCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_countryId", countryId);
            dquery.RunActionQuery();

        }

        public int AddEditCountry(int userID, int pk_CountryID, string CountryName, string CountryAbbr, bool isEdit)
        {
            string ProcName = "prcAddEditCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_CountryId", pk_CountryID);
            dquery.AddVarcharParam("@CountryName", 200, CountryName);
            dquery.AddVarcharParam("@CountryAbbr", 5, CountryName);
            dquery.AddBooleanParam("@isEdit", isEdit);

            return dquery.RunActionQuery();

        }

        #endregion

        #region Port

        public DataSet GetPort(int pk_PortId, string PortCode, string PortName)
        {
            string ProcName = "prcGetPort";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_PortId", pk_PortId);
            dquery.AddVarcharParam("@PortCode", 6, PortCode);
            dquery.AddVarcharParam("@PortName", 30, PortName);


            return dquery.GetTables();
        }

        public void DeletePort(int PortId)
        {
            string ProcName = "prcDeletePort";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_PortID", PortId);
            dquery.RunActionQuery();

        }

        public int AddEditPort(int userID, int pk_PortId, string PortName, string PortCode, bool ICDIndicator, string PortAddressee, string Address2, string Address3, bool isEdit)
        {
            string ProcName = "prcAddEditPort";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_PortId", pk_PortId);
            dquery.AddVarcharParam("@PortName", 200, PortName);
            dquery.AddVarcharParam("@PortCode", 6, PortCode);
            dquery.AddBooleanParam("@ICDIndicator", ICDIndicator);
            dquery.AddVarcharParam("@PortAddressee", 50, PortAddressee);
            dquery.AddVarcharParam("@Address2", 50, Address2);
            dquery.AddVarcharParam("@Address3", 50, Address3);
            dquery.AddBooleanParam("@isEdit", isEdit);

            return dquery.RunActionQuery();

        }

        #endregion

        #region NVOCC/Line

        public DataSet GetNVOCCLine(int pk_NVOCCID, string NVOCCName)
        {
            string ProcName = "prcGetNVOCCLine";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_NVOCCID", pk_NVOCCID);
            dquery.AddVarcharParam("@NVOCCName", 6, NVOCCName);

            return dquery.GetTables();
        }

        public int AddEditLine(int userID, int pk_NVOCCId, string NVOCCName, int DefaultFreeDays, string ContAgentCode, string logoPath, bool isEdit)
        {
            string ProcName = "prcAddEditLine";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_NVOCCId", pk_NVOCCId);
            dquery.AddVarcharParam("@NVOCCName", 50, NVOCCName);
            dquery.AddIntegerParam("@DefaultFreeDays", DefaultFreeDays);
            dquery.AddVarcharParam("@ContAgentCode", 10, ContAgentCode);
            dquery.AddVarcharParam("@logoPath", 150, logoPath);
            dquery.AddBooleanParam("@isEdit", isEdit);

            return dquery.RunActionQuery();

        }

        public void DeleteLine(int pk_NVOCCId)
        {
            string ProcName = "prcDeleteLine";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_NVOCCId", pk_NVOCCId);
            dquery.RunActionQuery();

        }

        #endregion

    }
}
