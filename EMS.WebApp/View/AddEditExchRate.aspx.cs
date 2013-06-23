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
using System.Configuration;
using System.Globalization;

namespace EMS.WebApp.View
{
    public partial class AddEditExchRate : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _exchRateId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess(_exchRateId);
            SetAttributes();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveExchangeRate();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                if (_exchRateId == -1) //Add mode
                {
                    if (!_canAdd) btnSave.Visible = false;
                }
                else
                {
                    if (!_canEdit) btnSave.Visible = false;
                }

                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageExchRate.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                rfvDate.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00065");
                rfvRate.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00066");
                //rfvFreeDays.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00067");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _exchRateId);
            }
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

        //        //if (user.UserRole.Id != (int)UserRole.Admin)
        //        //{
        //        //    Response.Redirect("~/Unauthorized.aspx");
        //        //}
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }

        //    if (!_canView)
        //    {
        //        Response.Redirect("~/Unauthorized.aspx");
        //    }

        //    if (_exchRateId == 0)
        //        Response.Redirect("~/View/ManageExchRate.aspx");
        //}

        private void CheckUserAccess(int xID)
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

                    if (!_canEdit && xID != -1)
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == -1)
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
            IExchangeRate exchRate = new ChargeBLL().GetExchangeRate(_exchRateId);

            if (!ReferenceEquals(exchRate, null))
            {
                txtDate.Text = exchRate.ExchangeDate.ToString(Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]));
                txtRate.Text = exchRate.USDExchangeRate.ToString();
                //txtFreeDays.Text = exchRate.FreeDays.ToString();
            }
        }

        private void SaveExchangeRate()
        {
            ChargeBLL chargeBll = new ChargeBLL();
            IExchangeRate exchRate = new ExchangeRateEntity();
            string message = string.Empty;
            BuildExchangeRateEntity(exchRate);
            message = chargeBll.SaveExchangeRate(exchRate, _userId);

            if (message == string.Empty)
                Response.Redirect("~/View/ManageExchRate.aspx");
            else
                GeneralFunctions.RegisterAlertScript(this, message);
        }

        private void BuildExchangeRateEntity(IExchangeRate exchRate)
        {
            exchRate.ExchangeRateID = _exchRateId;
            exchRate.ExchangeDate = Convert.ToDateTime(txtDate.Text, _culture);
            exchRate.USDExchangeRate = Convert.ToDecimal(txtRate.Text);
            //exchRate.FreeDays = Convert.ToInt32(txtFreeDays.Text);

        }

        #endregion
    }
}