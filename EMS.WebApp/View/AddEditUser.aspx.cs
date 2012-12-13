using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.View
{
    public partial class AddEditUser : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _uId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateRole();
                PopulateLocation();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveUser();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLoc.Enabled = false;
            ddlMultiLoc.Enabled = false;

            IRole role = new UserBLL().GetRole(Convert.ToInt32(ddlRole.SelectedValue));

            if (!ReferenceEquals(role, null))
            {
                if (role.LocationSpecific.HasValue && role.LocationSpecific.Value)
                {
                    ddlLoc.Enabled = true;
                    ddlMultiLoc.Enabled = true;
                }
            }
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _uId);
            }

            UserBLL.GetMenuAccessByUser(_userId, (int)PageName.UserMaster, out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void SetAttributes()
        {
            spnName.Style["display"] = "none";
            spnFName.Style["display"] = "none";
            spnLName.Style["display"] = "none";
            spnEmail.Style["display"] = "none";
            spnRole.Style["display"] = "none";
            spnLoc.Style["display"] = "none";

            if (!IsPostBack)
            {
                if (_uId == -1) //Add mode
                {
                    if (!_canAdd) btnSave.Visible = false;
                }
                else
                {
                    if (!_canEdit) btnSave.Visible = false;
                }

                ddlLoc.Enabled = false;
                ddlMultiLoc.Enabled = false;
                //rfvUserName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00036");
                //rfvFName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00037");
                //rfvLName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00038");
                //rfvEmail.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00039");
                //rfvRole.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00040");
                //rfvLoc.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00025");

                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageUser.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                revEmail.ValidationExpression = Constants.EMAIL_REGX_EXP;
                revEmail.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00026");

                spnName.InnerText = ResourceManager.GetStringWithoutName("ERR00048");
                spnFName.InnerText = ResourceManager.GetStringWithoutName("ERR00049");
                spnLName.InnerText = ResourceManager.GetStringWithoutName("ERR00050");
                spnEmail.InnerText = ResourceManager.GetStringWithoutName("ERR00051");
                spnRole.InnerText = ResourceManager.GetStringWithoutName("ERR00052");
                spnLoc.InnerText = ResourceManager.GetStringWithoutName("ERR00037");
            }

            if (_uId == -1)
            {
                chkActive.Checked = true;
                chkActive.Enabled = false;
            }

            if (_uId > 0)
            {
                txtUserName.Enabled = false;
            }
        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (_uId == 0)
                Response.Redirect("~/View/ManageUser.aspx");

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        //private bool IsSalesRole(int roleId)
        //{
        //    bool isSalesRole = false;

        //    if (roleId == (int)UserRole.SalesExecutive)
        //    {
        //        isSalesRole = true;
        //    }
        //    else
        //    {
        //        isSalesRole = false;
        //    }

        //    //IRole role = new CommonBLL().GetRole(roleId);
        //    //bool isSalesRole = false;

        //    //if (!ReferenceEquals(role, null))
        //    //{
        //    //    if (role.SalesRole.HasValue && role.SalesRole.Value == 'Y')
        //    //    {
        //    //        isSalesRole = true;
        //    //    }
        //    //}

        //    return isSalesRole;
        //}

        private void PopulateRole()
        {
            UserBLL userBll = new UserBLL();
            List<IRole> lstRole = userBll.GetActiveRole();
            GeneralFunctions.PopulateDropDownList(ddlRole, lstRole, "Id", "Name", true);
        }

        private void PopulateLocation()
        {
            CommonBLL commonBll = new CommonBLL();
            List<ILocation> lstLoc = commonBll.GetActiveLocation();
            GeneralFunctions.PopulateDropDownList(ddlLoc, lstLoc, "Id", "Name", true);
        }

        private void LoadData()
        {
            IUser user = new UserBLL().GetUser(_uId);

            if (!ReferenceEquals(user, null))
            {
                txtUserName.Text = user.Name;
                txtFName.Text = user.FirstName;
                txtLName.Text = user.LastName;
                txtEmail.Text = user.EmailId;
                ddlRole.SelectedValue = Convert.ToString(user.UserRole.Id);
                ddlLoc.SelectedValue = Convert.ToString(user.UserLocation.Id);

                if (user.AllowMutipleLocation)
                    ddlMultiLoc.SelectedValue = "1";
                else
                    ddlMultiLoc.SelectedValue = "0";

                if (user.IsActive)
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

                if (_uId == 1)
                    chkActive.Enabled = false;
            }
        }

        private bool ValidateControls(IUser user)
        {
            bool isValid = true;

            if (user.Name == string.Empty)
            {
                isValid = false;
                spnName.Style["display"] = "";
            }

            if (user.FirstName == string.Empty)
            {
                isValid = false;
                spnFName.Style["display"] = "";
            }

            if (user.LastName == string.Empty)
            {
                isValid = false;
                spnLName.Style["display"] = "";
            }

            if (user.EmailId == string.Empty)
            {
                isValid = false;
                spnEmail.Style["display"] = "";
            }

            if (user.UserRole.Id == 0)
            {
                isValid = false;
                spnRole.Style["display"] = "";
            }
            else
            {
                if (user.UserRole.LocationSpecific.HasValue && user.UserRole.LocationSpecific.Value)
                {
                    if (user.UserLocation.Id == 0)
                    {
                        isValid = false;
                        spnLoc.Style["display"] = "";
                    }
                }
            }

            //if (user.UserLocation.Id == 0)
            //{
            //    isValid = false;
            //    spnLoc.Style["display"] = "";
            //}
            //else
            //{
            //    if(user.UserRole.LocationSpecific.Value)
            //        user.UserRole.
            //}

            return isValid;
        }

        private void SaveUser()
        {
            UserBLL userBll = new UserBLL();
            IUser user = new UserEntity();
            string message = string.Empty;
            BuildUserEntity(user);

            if (ValidateControls(user))
            {
                message = userBll.SaveUser(user, _userId);

                if (message == string.Empty)
                {
                    if (_uId == 0)
                        SendEmail(user);

                    Response.Redirect("~/View/ManageUser.aspx");
                }
                else
                {
                    GeneralFunctions.RegisterAlertScript(this, message);
                }
            }
        }

        private void BuildUserEntity(IUser user)
        {
            user.Id = _uId;
            user.Name = txtUserName.Text.Trim().ToUpper();
            user.Password = UserBLL.GetDefaultPassword();
            user.FirstName = txtFName.Text.Trim().ToUpper();
            user.LastName = txtLName.Text.Trim().ToUpper();
            user.EmailId = txtEmail.Text.Trim().ToUpper();
            user.UserRole.Id = Convert.ToInt32(ddlRole.SelectedValue);
            user.UserLocation.Id = Convert.ToInt32(ddlLoc.SelectedValue);

            IRole role = new UserBLL().GetRole(Convert.ToInt32(ddlRole.SelectedValue));

            user.UserRole.LocationSpecific = false;

            if (!ReferenceEquals(role, null))
            {
                if (role.LocationSpecific.HasValue && role.LocationSpecific.Value)
                {
                    user.UserRole.LocationSpecific = true;
                }
            }

            if (ddlMultiLoc.SelectedValue == "1")
                user.AllowMutipleLocation = true;
            else
                user.AllowMutipleLocation = false;

            if (chkActive.Checked)
                user.IsActive = true;
            else
                user.IsActive = false;
        }

        private void SendEmail(IUser user)
        {
            string url = Convert.ToString(ConfigurationManager.AppSettings["ApplicationUrl"]) + "/View/ChangePassword.aspx?id=" + GeneralFunctions.EncryptQueryString(user.Id.ToString());
            string msgBody = "Hello " + user.UserFullName + "<br/>We have received new account creation request for your account " + user.Name + ". <br/>If this request was initiated by you, please click on following link and change your password:<br/><a href='" + url + "'>" + url + "</a>";

            try
            {
                CommonBLL.SendMail(Convert.ToString(ConfigurationManager.AppSettings["Sender"]), Convert.ToString(ConfigurationManager.AppSettings["DisplayName"]), user.EmailId, string.Empty, "New account creation", msgBody, Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserAccount"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserPwd"]));
            }
            catch (Exception ex)
            {
                CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
            }
        }

        #endregion
    }
}