using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using System.Data;
using EMS.Entity;

namespace EMS.WebApp.Export
{
    public partial class AddEditSlotOperator : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string NVOCCId = "";
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            //_userId = EMS.BLL.UserBLL.GetLoggedInUserId();


            if (!IsPostBack)
            {
                NVOCCId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageSlotOperator.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtSlotOperatorName.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 50)";
                if (NVOCCId != "-1")
                    LoadData(NVOCCId);
            }
            RetriveParameters();
            CheckUserAccess(NVOCCId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            NVOCCId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(NVOCCId);

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

        private void LoadData(string NVOCCId)
        {
            ClearText();
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

            int intNVOCCId = 0;
            if (NVOCCId == "" || !Int32.TryParse(NVOCCId, out intNVOCCId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataTable dt = dbinteract.GetSlotOperator(Convert.ToInt32(NVOCCId), "", searchCriteria);

            if (!ReferenceEquals(dt, null) && dt.Rows.Count > 0)
            {
                txtSlotOperatorName.Text = dt.Rows[0]["SlotOperatorName"].ToString();
            }
        }
        private void ClearText()
        {
            txtSlotOperatorName.Text = "";
        }
        private void SaveData(string NVOCCId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            bool isedit = NVOCCId != "-1" ? true : false;

            if (!isedit)
                if (!dbinteract.IsUnique("exp.mstSlotOperator", "SlotOperatorName", txtSlotOperatorName.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Operator Name must be unique. The given name has already been used for another Line. Please try with another one.");
                    return;
                }

            int result = dbinteract.AddEditSlotOperator(_userId, Convert.ToInt32(NVOCCId), txtSlotOperatorName.Text.Trim(), isedit);


            if (result > 0)
            {
                Response.Redirect("~/Export/ManageSlotOperator.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/ManageSlotOperator.aspx");
        }

    }
}