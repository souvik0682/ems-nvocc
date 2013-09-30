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

namespace EMS.WebApp.MasterModule
{
    public partial class AddEditServices : System.Web.UI.Page
    {
        ServiceEntity oServiceEntity;
        ServiceBLL oServiceBll;
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
                PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0);
                PopulateDropDown((int)Enums.DropDownPopulationFor.Service, ddlServices, 0);
                //ddlLocation.Items.Insert(0, Li);

                //Li = new ListItem("Select", "0");
                //PopulateDropDown((int)Enums.DropDownPopulationFor.Port, ddlToLocation, 0);
                //ddlToLocation.Items.Insert(0, Li);

                Li = new ListItem("Select", "0");
                //PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerSize, ddlContainerSize, 0);
                //ddlContainerSize.Items.Insert(0, Li);

                if (hdnServiceID.Value != "0")
                    LoadData();
            }
            CheckUserAccess(hdnServiceID.Value);
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
            ServiceEntity oService = (ServiceEntity)ServiceBLL.GetService(Convert.ToInt32(hdnServiceID.Value));

            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            hdnFPOD.Value = oService.FPODID.ToString();
            ddlLine.SelectedValue = oService.LinerID.ToString();
            ddlServices.SelectedValue = oService.ServiceNameID.ToString();
            //txtService.Text = Convert.ToString(oService.ServiceName);
            txtFPOD.Text = Convert.ToString(oService.FPOD);
            hdnServiceID.Value = Convert.ToString(oService.ServiceID);

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

                oServiceBll = new ServiceBLL();
                oServiceEntity = new ServiceEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oServiceEntity.LinerID = ddlLine.SelectedValue.ToInt();

                oServiceEntity.FPODID = hdnFPOD.Value;
                oServiceEntity.ServiceNameID = ddlServices.SelectedValue.ToInt();
                //oServiceEntity.ServiceName = Convert.ToString(txtService.Text.Trim());

                if (hdnServiceID.Value == "0") // Insert
                {
                    oServiceEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oServiceEntity.CreatedOn = DateTime.Today.Date;
                    oServiceEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oServiceEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oServiceBll.AddEditService(oServiceEntity, _CompanyId))
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
                    oServiceEntity.ServiceID = Convert.ToInt32(hdnServiceID.Value);
                    oServiceEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oServiceEntity.ModifiedOn = DateTime.Today.Date;
                    oServiceEntity.Action = true;
                    //
                    switch (oServiceBll.AddEditService(oServiceEntity, _CompanyId))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: Response.Redirect("~/MasterModule/ManageService.aspx");
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
                hdnServiceID.Value = _locId.ToString();
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/ManageService.aspx");
        }

        void ClearAll()
        {
            ddlLine.SelectedIndex = 0;
            txtFPOD.Text = string.Empty;
            hdnFPOD.Value = "0";
            ddlServices.SelectedIndex = 0;
        }
    }
}