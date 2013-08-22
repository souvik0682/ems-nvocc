using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.Utilities;
using EMS.Entity;
using EMS.BLL;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditFreeDays : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string FreeLinkID = "";
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
      
        #endregion

        BLL.DBInteraction dbinteract = new BLL.DBInteraction();
        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            //_userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            
            if (!IsPostBack)
            {
                FreeLinkID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                GeneralFunctions.PopulateDropDownList(ddlLine, dbinteract.PopulateDDLDS("DSR.dbo.MSTPROSPECTFOR", "pk_prospectid", "ProspectName", true), true);
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageFreeDays.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                if (FreeLinkID == "-1")
                {
                    //txtFreeDays.Visible = false;
                    //ddlStatus.Enabled = false;
                }
                else
                {
                    //DatePicker1.Visible = false;
                    LoadData(FreeLinkID);
                }
            }
            RetriveParameters();
            CheckUserAccess(FreeLinkID);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FreeLinkID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(FreeLinkID);

        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            //Get user permission.
            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess(string xID)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                btnSave.Visible = true;

                if (!_canAdd && !_canEdit)
                    btnSave.Visible = false;
                else
                {

                    if (!_canEdit && xID != "-1")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "-1")
                    {
                        btnSave.Visible = false;
                    }
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadData(string FreeLinkID)
        {
            ClearText();
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

            BLL.DBInteraction dbinteract = new BLL.DBInteraction();

            int intServTaxId = 0;
            if (FreeLinkID == "" || !Int32.TryParse(FreeLinkID, out intServTaxId))
                return;

            System.Data.DataSet ds = dbinteract.GetFreeDays(FreeLinkID.ToInt(), "", "", searchCriteria);

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtFreeDays.Text = ds.Tables[0].Rows[0]["DefaultFreeDays"].ToString();
                ddlLoc.SelectedValue = ds.Tables[0].Rows[0]["fk_LocationID"].ToString();
                ddlLine.SelectedValue = ds.Tables[0].Rows[0]["fk_NVOCCID"].ToString();
                //ddlStatus.SelectedIndex = Convert.ToString(ds.Tables[0].Rows[0]["LinkStatus"]) == "Active" ? 1 : 0;
            }
        }
        private void ClearText()
        {
            txtFreeDays.Text = "";
            ddlLine.SelectedIndex = 0;
            ddlLoc.SelectedIndex = 0;
        }
        private void SaveData(string ServTaxId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
    
            bool isedit = FreeLinkID != "-1" ? true : false;

            int result = dbinteract.AddEditFreeDays(_userId, Convert.ToInt32(FreeLinkID), Convert.ToInt32(txtFreeDays.Text), Convert.ToInt32(ddlLoc.SelectedValue), Convert.ToInt32(ddlLine.SelectedValue), isedit);

            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManageFreeDays.aspx");
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

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}