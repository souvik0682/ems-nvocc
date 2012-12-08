using System;
using System.Collections.Generic;
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
    public partial class AddEditHaulageCharge : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _chgId = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateControls();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveHaulageCharge();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageHaulageCharge.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _chgId);
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

                //if (user.UserRole.Id != (int)UserRole.Admin)
                //{
                //    Response.Redirect("~/Unauthorized.aspx");
                //}
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (_chgId == 0)
                Response.Redirect("~/View/ManageHaulageCharge.aspx");
        }

        private void PopulateControls()
        {
            //UserBLL userBll = new UserBLL();
            //GeneralFunctions.PopulateDropDownList<IUser>(ddlManager, userBll.GetManagers(), "Id", "UserFullName", true);
        }

        private void LoadData()
        {
            IImportHaulage haulage = new ChargeBLL().GetHaulageCharge(_chgId);

            if (!ReferenceEquals(haulage, null))
            {
                ddlLocFrom.SelectedValue = haulage.LocationFrom.ToString();
                ddlLocTo.SelectedValue = haulage.LocationTo.ToString();
                ddlSize.SelectedValue = haulage.ContainerSize;
                txtWeightFrom.Text = haulage.WeightFrom.ToString();
                txtWeightTo.Text = haulage.WeightTo.ToString();
                txtRate.Text = haulage.HaulageRate.ToString();
            }
        }

        private void SaveHaulageCharge()
        {
            ChargeBLL chargeBll = new ChargeBLL();
            IImportHaulage haulage = new ImportHaulageEntity();
            string message = string.Empty;
            BuildHaulageChargeEntity(haulage);
            chargeBll.SaveHaulageCharge(haulage, _userId);
            Response.Redirect("~/View/ManageHaulageCharge.aspx");
        }

        private void BuildHaulageChargeEntity(IImportHaulage haulage)
        {
            haulage.HaulageChgID = _chgId;
            haulage.LocationFrom = Convert.ToInt32(ddlLocFrom.SelectedValue);
            haulage.LocationTo = Convert.ToInt32(ddlLocTo.SelectedValue);
            haulage.ContainerSize = ddlSize.SelectedValue;
            haulage.WeightFrom = Convert.ToDouble(txtWeightFrom.Text);
            haulage.WeightTo = Convert.ToDouble(txtWeightTo.Text);
        }

        #endregion
    }
}