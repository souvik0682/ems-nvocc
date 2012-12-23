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


        public DataSet PopulateDDLDS(string tableName, string textField, string valuefield)
        {

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select [" + textField + "] ListItemValue, [" + valuefield + "] ListItemText from dbo." + tableName, true);

            return dquery.GetTables();

        }
        #endregion

        #region country

        public DataSet GetCountry(params object[] sqlParam)
        {
            string ProcName = "admin.prcGetCountry";
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
            string ProcName = "admin.prcGetPort";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_PortId", pk_PortId);
            dquery.AddVarcharParam("@PortCode", 6, PortCode);
            dquery.AddVarcharParam("@PortName", 30, PortName);


            return dquery.GetTables();
        }

        public void DeletePort(int PortId)
        {
            string ProcName = "admin.prcDeletePort";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_PortID", PortId);
            dquery.RunActionQuery();

        }

        public int AddEditPort(int userID, int pk_PortId, string PortName, string PortCode, bool ICDIndicator, string PortAddressee, string Address2, string Address3,string ExportPort, bool isEdit)
        {
            string ProcName = "admin.prcAddEditPort";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_PortId", pk_PortId);
            dquery.AddVarcharParam("@PortName", 200, PortName);
            dquery.AddVarcharParam("@PortCode", 6, PortCode);
            dquery.AddBooleanParam("@ICDIndicator", ICDIndicator);
            dquery.AddVarcharParam("@PortAddressee", 50, PortAddressee);
            dquery.AddVarcharParam("@Address2", 50, Address2);
            dquery.AddVarcharParam("@Address3", 50, Address3);
            dquery.AddVarcharParam("@ExportPort", 3, ExportPort);
            dquery.AddBooleanParam("@isEdit", isEdit);

            return dquery.RunActionQuery();

        }

        #endregion

        #region NVOCC/Line

        public DataSet GetNVOCCLine(int pk_NVOCCID, string NVOCCName)
        {
            string ProcName = "admin.prcGetNVOCCLine";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_NVOCCID", pk_NVOCCID);
            dquery.AddVarcharParam("@NVOCCName", 6, NVOCCName);

            return dquery.GetTables();
        }

        public int AddEditLine(int userID, int pk_NVOCCId, string NVOCCName, int DefaultFreeDays, string ContAgentCode, string logoPath, bool isEdit)
        {
            string ProcName = "admin.prcAddEditLine";
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
            string ProcName = "admin.prcDeleteLine";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_NVOCCId", pk_NVOCCId);
            dquery.RunActionQuery();

        }

        #endregion

        #region STax

        public DataSet GetSTaxDate(DateTime? Startdt)
        {
            string ProcName = "admin.prcGetSTaxDate";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddDateTimeParam("@StartDate", Startdt);
        

            return dquery.GetTables();
        }

        public DataSet GetSTax(int pk_StaxID, DateTime? StartDate)
        {
            string ProcName = "admin.prcGetSTax";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_StaxID", pk_StaxID);
            dquery.AddDateTimeParam("@StartDate", StartDate);
         
            return dquery.GetTables();
        }

        public int AddEditSTax(int userID, int pk_StaxID, DateTime? StartDate, decimal TaxAddCess, decimal TaxCess, decimal TaxPer, bool TaxStatus, bool isEdit)
        {
            string ProcName = "admin.prcAddEditSTax";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_StaxID", pk_StaxID);
            dquery.AddDateTimeParam("@StartDate", StartDate);
            dquery.AddDecimalParam("@TaxAddCess",6,2, TaxAddCess);
            dquery.AddDecimalParam("@TaxCess", 6, 2, TaxCess);
            dquery.AddDecimalParam("@TaxPer", 6, 2, TaxPer);
            dquery.AddBooleanParam("@TaxStatus", TaxStatus);
            dquery.AddBooleanParam("@isEdit", isEdit);

            return dquery.RunActionQuery();

        }
        #endregion

        #region country

        public DataSet GetVessel(int vesselId,int vesselPrefix, string vesselName,string vesselFlag,int countryId)
        {
            string ProcName = "prcGetVessel";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            //dquery.AddIntegerParam("@pk_countryId", Convert.ToInt32(sqlParam[0]));
            //dquery.AddVarcharParam("@CountryName", 200, Convert.ToString(sqlParam[1]));


            return dquery.GetTables();
        }

        public void DeleteVessel(int vesselId)
        {
            string ProcName = "prcDeleteVessel";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_vesselId", vesselId);
            dquery.RunActionQuery();

        }
        //public DataSet get
        //public int AddEditCountry(int userID, int pk_CountryID, string CountryName, string CountryAbbr, bool isEdit)
        //{
        //    string ProcName = "prcAddEditCountry";
        //    DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
        //    dquery.AddIntegerParam("@userID", userID);
        //    dquery.AddIntegerParam("@pk_CountryId", pk_CountryID);
        //    dquery.AddVarcharParam("@CountryName", 200, CountryName);
        //    dquery.AddVarcharParam("@CountryAbbr", 5, CountryName);
        //    dquery.AddBooleanParam("@isEdit", isEdit);

        //    return dquery.RunActionQuery();

        //}

        #endregion

    }
}
