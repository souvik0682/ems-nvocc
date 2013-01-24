using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities.ResourceManager;
using EMS.Utilities;
using EMS.Common;

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditCountry : System.Web.UI.Page
    {

        #region Private Member Variables

       
        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string countryId = "";
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {

            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();
            if (!IsPostBack)
            {
                countryId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();

                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageCountry.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtCountryName.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
                if (countryId != "-1")
                    LoadData(countryId);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            countryId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveCountry(countryId);

        }

        #endregion

        #region Private Methods

        private void LoadData(string countryID)
        {
            ClearText();
            int intCountryId = 0;
            if (countryID == "" || !Int32.TryParse(countryID, out intCountryId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataSet ds = dbinteract.GetCountry(Convert.ToInt32(countryID), "","");//country

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtCountryName.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                txtAbbr.Text = ds.Tables[0].Rows[0]["CountryAbbr"].ToString();
            }
        }
        private void ClearText()
        {
            txtAbbr.Text = "";
            txtCountryName.Text = "";
        }
        private void SaveCountry(string countryId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            bool isedit = countryId != "-1" ? true : false;
            bool s = dbinteract.IsUnique("mstCountry", "CountryAbbr", txtAbbr.Text.Trim());
            if (!dbinteract.IsUnique("mstCountry", "CountryAbbr", txtAbbr.Text.Trim()) && !isedit)
            {
                GeneralFunctions.RegisterAlertScript(this, "Country Abbr Must Be unique");
                return;
            }
            int result = dbinteract.AddEditCountry(_userId, Convert.ToInt32(countryId), txtCountryName.Text.Trim(), txtAbbr.Text.Trim(), isedit);


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManageCountry.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}