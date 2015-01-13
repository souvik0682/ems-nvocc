using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.BLL;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Forwarding.Master
{
    public partial class AddEditFwdAgent : System.Web.UI.Page
    {
        fwdAgentEntity oAgentEntity;
        fwdAgentBLL oAgentBll;
        UserEntity oUserEntity;


        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _CompanyId = 1;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            RetriveParameters();
            if (!Page.IsPostBack)
            {
                ListItem Li = null;
                //Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.fwLine, ddlLine, 0);
                //ddlLocation.Items.Insert(0, Li);

                //Li = new ListItem("Select", "0");
                //PopulateDropDown((int)Enums.DropDownPopulationFor.Port, ddlToLocation, 0);
                //ddlToLocation.Items.Insert(0, Li);

                Li = new ListItem("Select", "0");
                //PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerSize, ddlContainerSize, 0);
                //ddlContainerSize.Items.Insert(0, Li);

                if (hdnAgentID.Value != "0")
                    LoadData();
            }
            CheckUserAccess(hdnAgentID.Value);
        }

        //private void CheckUserAccess()
        //{
        //    if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
        //    {
        //        IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

        //        if (ReferenceEquals(user, null) || user.Id == 0)
        //        {
        //            Response.Redirect("~/Login.aspx");
        //        }

        //        if (user.UserRole.Id != (int)UserRole.Admin)
        //        {
        //            Response.Redirect("~/Unauthorized.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}

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

                    if (!_canEdit && xID != "0")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "0")
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


        private void LoadData()
        {
            fwdAgentEntity oAgent = (fwdAgentEntity)fwdAgentBLL.GetAgent(Convert.ToInt32(hdnAgentID.Value));

            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            hdnFPOD.Value = oAgent.FPODID.ToString();
            ddlLine.SelectedValue = oAgent.LinerID.ToString();
            txtAddress.Text = Convert.ToString(oAgent.AgentAddress);
            txtContPerson.Text = Convert.ToString(oAgent.ContactPerson);
            txtPhone.Text = Convert.ToString(oAgent.Phone);
            txtEmail.Text = Convert.ToString(oAgent.email);
            txtFax.Text = Convert.ToString(oAgent.FAX);
            txtAgent.Text = Convert.ToString(oAgent.AgentName);
            txtFPOD.Text = Convert.ToString(oAgent.FPOD);
            hdnAgentID.Value = Convert.ToString(oAgent.AgentID);
            
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter, 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                //if (Convert.ToDecimal(txtWFrom.Text) > Convert.ToDecimal(txtWTo.Text))
                //{
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00077") + "');</script>", false);
                //    return;
                //}

                oAgentBll = new fwdAgentBLL();
                oAgentEntity = new fwdAgentEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oAgentEntity.LinerID = ddlLine.SelectedValue.ToInt();

                oAgentEntity.FPODID = hdnFPOD.Value;
                oAgentEntity.AgentAddress = txtAddress.Text.ToString();
                oAgentEntity.AgentName = Convert.ToString(txtAgent.Text.Trim());
                oAgentEntity.ContactPerson = Convert.ToString(txtContPerson.Text.Trim());
                oAgentEntity.AgentStatus = true;
                oAgentEntity.Phone = Convert.ToString(txtPhone.Text);
                oAgentEntity.FAX = Convert.ToString(txtFax.Text);
                oAgentEntity.email = Convert.ToString(txtEmail.Text);
               
                if (hdnAgentID.Value == "0") // Insert
                {
                    oAgentEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oAgentEntity.CreatedOn = DateTime.Today.Date;
                    oAgentEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oAgentEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oAgentBll.AddEditAgent(oAgentEntity, _CompanyId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            ClearAll();
                            break;
                        case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                            ClearAll();
                            break;
                    }
                }
                else // Update
                {
                    oAgentEntity.AgentID = Convert.ToInt32(hdnAgentID.Value);
                    oAgentEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oAgentEntity.ModifiedOn = DateTime.Today.Date;
                    oAgentEntity.Action = true;
                    //
                    switch (oAgentBll.AddEditAgent(oAgentEntity, _CompanyId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: Response.Redirect("~/Forwarding/Master/ManageFwdAgent.aspx");
                            break;
                    }
                }


            }
        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
                hdnAgentID.Value = _locId.ToString();
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Master/ManageFwdAgent.aspx");
        }

        void ClearAll()
        {
            ddlLine.SelectedIndex = 0;
            txtFPOD.Text = string.Empty;
            txtPhone.Text = string.Empty;
            hdnFPOD.Value = "0";
            txtPhone.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAgent.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
    }
}