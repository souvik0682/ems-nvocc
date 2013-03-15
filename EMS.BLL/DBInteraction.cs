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
        public DataSet PopulateDDLDS(string tableName, string textField, string valuefield, bool isDSR)
        {

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select [" + textField + "] ListItemValue, [" + valuefield + "] ListItemText from " + tableName + " order by ListItemText", true);

            return dquery.GetTables();

        }

        public DataSet PopulateDDLDS(string tableName, string textField, string valuefield, string WhereClause)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select [" + textField + "] ListItemValue, [" + valuefield + "] ListItemText from dbo." + tableName + " " + WhereClause, true);
            return dquery.GetTables();
        }


        public int GetId(string ItemName, string ItemValue)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcGetId");
            dquery.AddVarcharParam("@ItemName", 15, ItemName);
            dquery.AddVarcharParam("@ItemValue", 50, ItemValue);
            return Convert.ToInt32(dquery.GetScalar());
        }

        public decimal GetExchnageRate(DateTime dt)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcGetExchnageRate");
            dquery.AddDateTimeParam("@Exdate", dt);
            return Convert.ToDecimal(dquery.GetScalar());
        }

        public DataSet GetShipLine_Tax(int userid)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcGetShipLine_Tax");
            dquery.AddIntegerParam("@userid", userid);
            return dquery.GetTables();
        }

        public void DeleteGeneral(string formName, int pk_tableID)
        {
            string ProcName = "prcDeleteGeneral";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_tableID", pk_tableID);
            dquery.AddVarcharParam("@formName", 20, formName);
            dquery.RunActionQuery();

        }

        public int InvoiceDateCheck(DateTime dt)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcInvoiceDateCheck");
            dquery.AddDateTimeParam("@StaxDate", dt);
            return Convert.ToInt32(dquery.GetScalar());
        }
        #endregion

        #region country

        public DataSet GetCountry(params object[] sqlParam)
        {
            string ProcName = "admin.prcGetCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_countryId", Convert.ToInt32(sqlParam[0]));
            dquery.AddVarcharParam("@CountryName", 200, Convert.ToString(sqlParam[1]));
            dquery.AddVarcharParam("@CountryAbbr", 2, Convert.ToString(sqlParam[2]));

            return dquery.GetTables();
        }

        public void DeleteCountry(int countryId)
        {
            string ProcName = "admin.prcDeleteCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_countryId", countryId);
            dquery.RunActionQuery();

        }

        public int AddEditCountry(int userID, int pk_CountryID, string CountryName, string CountryAbbr,string GMT,string ISD,string Sector, bool isEdit)
        {
            string ProcName = "admin.prcAddEditCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_CountryId", pk_CountryID);
            dquery.AddVarcharParam("@CountryName", 200, CountryName);
            dquery.AddVarcharParam("@CountryAbbr", 5, CountryAbbr);
            dquery.AddVarcharParam("@GMT", 10, GMT);
            dquery.AddVarcharParam("@ISD", 10, ISD);
            dquery.AddVarcharParam("@Sector", 50, Sector);
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

        public int AddEditPort(int userID, int pk_PortId, string PortName, string PortCode, bool ICDIndicator, string PortAddressee, string Address2, string Address3, string ExportPort, bool isEdit)
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

        public int AddEditLine(int userID, int pk_NVOCCId, string NVOCCName, int DefaultFreeDays, string ContAgentCode, decimal ImpCommsn, decimal ExpCommsn, char ExpBook,decimal rBuffer, string logoPath, bool isEdit)
        {
            string ProcName = "admin.prcAddEditLine";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_NVOCCId", pk_NVOCCId);
            dquery.AddVarcharParam("@NVOCCName", 50, NVOCCName);
            dquery.AddIntegerParam("@DefaultFreeDays", DefaultFreeDays);
            dquery.AddVarcharParam("@ContAgentCode", 10, ContAgentCode);
            dquery.AddDecimalParam("@ImportCommission", 6, 2, ImpCommsn);
            dquery.AddDecimalParam("@ExportCommission", 6, 2, ExpCommsn);
            dquery.AddDecimalParam("@RBuffer", 6, 2, rBuffer);
            dquery.AddCharParam("@ExportBooking", 1, ExpBook);
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
            dquery.AddDecimalParam("@TaxAddCess", 6, 2, TaxAddCess);
            dquery.AddDecimalParam("@TaxCess", 6, 2, TaxCess);
            dquery.AddDecimalParam("@TaxPer", 6, 2, TaxPer);
            dquery.AddBooleanParam("@TaxStatus", TaxStatus);
            dquery.AddBooleanParam("@isEdit", isEdit);

            return dquery.RunActionQuery();

        }
        #endregion

        #region Vessel

        public DataSet GetVessel(int vesselId, int vesselPrefix, string vesselName, int vesselFlag)
        {
            string ProcName = "prcGetVessel";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_vesselId", vesselId);
            dquery.AddIntegerParam("@fk_VesselPrefixID", vesselPrefix);
            dquery.AddVarcharParam("@vesselName", 70, vesselName);
            dquery.AddIntegerParam("@vesselFlag", vesselFlag);


            return dquery.GetTables();
        }

        public void DeleteVessel(int vesselId)
        {
            string ProcName = "[admin].[prcDeleteVessel]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_vesselId", vesselId);
            dquery.RunActionQuery();

        }

        public int AddEditVessel(int userID, bool isEdit, EMS.Entity.Vessel vessel)
        {
            string ProcName = "prcAddEditVessel";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddBooleanParam("@isEdit", isEdit);

            dquery.AddIntegerParam("@pk_VesselId", vessel.VesselID);
            dquery.AddVarcharParam("@AgentCode", 10, vessel.AgentCode);
            // dquery.AddVarcharParam("@CallSign", 14, vessel.CallSign);
            dquery.AddVarcharParam("@IMONumber", 14, vessel.IMONumber);
            // dquery.AddIntegerParam("@LastPortCalled", vessel.LastPortCalled);
            dquery.AddVarcharParam("@MasterName", 60, vessel.MasterName);
            dquery.AddVarcharParam("@PANNo", 15, vessel.PANNo);
            dquery.AddVarcharParam("@ShippingLineCode", 10, vessel.ShippingLineCode);
            dquery.AddIntegerParam("@VesselFlag", vessel.VesselFlag);
            dquery.AddVarcharParam("@VesselName", 60, vessel.VesselName);
            dquery.AddIntegerParam("@VesselPrefix", vessel.VesselPrefix);


            return dquery.RunActionQuery();

        }

        #endregion

        #region Voyage

        public int AddEditVoyage(int userID, bool isEdit, EMS.Entity.VoyageEntity voyage)
        {
            string ProcName = "prcAddEditVoyage";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddBooleanParam("@isEdit", isEdit);

            dquery.AddIntegerParam("@pk_VoyageID", voyage.pk_VoyageID);
            dquery.AddIntegerParam("@fk_VesselID", voyage.fk_VesselID);
            dquery.AddIntegerParam("@fl_TerminalID", voyage.fl_TerminalID);
            dquery.AddVarcharParam("@VoyageNo", 10, voyage.VoyageNo);
            dquery.AddDecimalParam("@ImpXChangeRate", 6, 2, voyage.ImpXChangeRate);
            dquery.AddVarcharParam("@IGMNo", 10, voyage.IGMNo);
            dquery.AddVarcharParam("@CallSign", 14, voyage.CallSign);
            dquery.AddDateTimeParam("@IGMDate", voyage.IGMDate);
            dquery.AddDateTimeParam("@LandingDate", voyage.LandingDate);
            dquery.AddDateTimeParam("@ETADate", voyage.ETADate);
            dquery.AddDateTimeParam("@AddLandingDate", voyage.AddLandingDate);
            dquery.AddDateTimeParam("@PCCDate", voyage.PCCDate);
            dquery.AddVarcharParam("@PCCNo", 14, voyage.PCCNo);
            dquery.AddVarcharParam("@VoyageType", 1, "I");
            dquery.AddIntegerParam("@fk_LPortID", voyage.fk_LPortID);
            dquery.AddIntegerParam("@fk_LPortID1", voyage.fk_LPortID1);
            dquery.AddIntegerParam("@fk_LPortID2", voyage.fk_LPortID2);
            dquery.AddVarcharParam("@LGNo", 40, voyage.LGNo);
            dquery.AddVarcharParam("@AltLGNo", 40, voyage.AltLGNo);
            dquery.AddVarcharParam("@VesselType", 1, voyage.VesselType);
            dquery.AddVarcharParam("@MotherDaughterDtl", 500, voyage.MotherDaughterDtl);
            dquery.AddVarcharParam("@TotalLines", 5, voyage.TotalLines);
            dquery.AddVarcharParam("@CargoDesc", 50, voyage.CargoDesc);
            dquery.AddVarcharParam("@VIANo", 10, voyage.VIANo);
            dquery.AddVarcharParam("@VCN", 14, voyage.VCN);
            dquery.AddVarcharParam("@ETATime", 7, voyage.ETATime);
            dquery.AddIntegerParam("@LightHouseDue", voyage.LightHouseDue);
            dquery.AddBooleanParam("@SameButtonCargo", voyage.SameButtonCargo == "1" ? true : false);
            dquery.AddBooleanParam("@ShipStoreSubmitted", voyage.ShipStoreSubmitted == "1" ? true : false);
            dquery.AddBooleanParam("@CrewList", voyage.CrewList == "1" ? true : false);
            dquery.AddBooleanParam("@PassengerList", voyage.PassengerList == "1" ? true : false);
            dquery.AddBooleanParam("@CrewEffectList", voyage.CrewEffectList == "1" ? true : false);
            dquery.AddBooleanParam("@MaritimeList", voyage.MaritimeList == "1" ? true : false);
            dquery.AddIntegerParam("@LocId", voyage.locid);
            dquery.AddIntegerParam("@fk_pod", voyage.fk_Pod);
            return dquery.RunActionQuery();

        }
        public void DeleteVoyage(int voyageID)
        {
            string ProcName = "[admin].[prcDeleteVoyage]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_voyageID", voyageID);
            dquery.RunActionQuery();

        }

        public DataSet GetVoyage(int VoyageId, string voyageType, string VesselName, string voyageNo, string igmNo)
        {
            string ProcName = "prcGetVoyage";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_VoyageID", VoyageId);
            dquery.AddCharParam("@VoyageType", 1, voyageType.ToCharArray()[0]);
            dquery.AddVarcharParam("@vesselName", 70, VesselName);
            dquery.AddVarcharParam("@VoyageNo", 10, voyageNo);
            dquery.AddVarcharParam("@IGMNo", 10, igmNo);

            return dquery.GetTables();
        }


        #endregion

        #region Import_ExportText

        public DataSet GetImportEDIforCustom(int VoyageId, int VesselID)
        {
            string ProcName = "prcGetEDICustom";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_VoyageID", VoyageId);
            dquery.AddIntegerParam("@pk_VesselID", VesselID);

            return dquery.GetTables();
        }

       
        

        #endregion



        
    }
}
