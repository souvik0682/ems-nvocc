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
    public partial class EditLocation : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;
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
                PopulateControls();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveLocation();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                if (_locId == -1) //Add mode
                {
                    if (!_canAdd) btnSave.Visible = false;
                }
                else
                {
                    if (!_canEdit) btnSave.Visible = false;
                }

                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageLocation.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtCAN.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 300)";
                txtSlot.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 300)";
                txtCarting.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 300)";
                txtPickup.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 300)";
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
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

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }

            if (_locId == 0)
                Response.Redirect("~/View/ManageLocation.aspx");
        }

        private void PopulateControls()
        {
            //UserBLL userBll = new UserBLL();
            //GeneralFunctions.PopulateDropDownList<IUser>(ddlManager, userBll.GetManagers(), "Id", "UserFullName", true);
        }

        private void LoadData()
        {
            ILocation location = new CommonBLL().GetLocation(_locId);

            if (!ReferenceEquals(location, null))
            {
                txtLocName.Text = location.Name;

                if (!ReferenceEquals(location.LocAddress, null))
                {
                    txtAddress.Text = location.LocAddress.Address;
                    txtCity.Text = location.LocAddress.City;
                    txtPin.Text = location.LocAddress.Pin;
                }

                txtAbbr.Text = location.Abbreviation;
                txtPhone.Text = location.Phone;
                txtCAN.Text = location.CanFooter;
                txtSlot.Text = location.SlotFooter;

                if (location.PGRFreeDays.HasValue)
                    txtPGR.Text = location.PGRFreeDays.Value.ToString();

                txtCustomhouseCode.Text = location.CustomHouseCode;
                txtGatewayPort.Text = location.GatewayPort;
                txtICEGATE.Text = location.ICEGateLoginD;
                txtPCS.Text = location.PCSLoginID;
                txtISO20.Text = location.ISO20;
                txtISO40.Text = location.ISO40;
                txtCarting.Text = location.CartingFooter;
                txtPickup.Text = location.PickUpFooter;
            }
        }

        private void SaveLocation()
        {
            CommonBLL commonBll = new CommonBLL();
            ILocation loc = new LocationEntity();
            string message = string.Empty;
            BuildLocationEntity(loc);
            commonBll.SaveLocation(loc, _userId);
            Response.Redirect("~/View/ManageLocation.aspx");
        }

        private void BuildLocationEntity(ILocation loc)
        {
            loc.Id = _locId;

            if (!string.IsNullOrEmpty(txtPGR.Text))
                loc.PGRFreeDays = Convert.ToInt32(txtPGR.Text);

            loc.CanFooter = txtCAN.Text.Trim();
            loc.SlotFooter = txtSlot.Text.Trim();
            loc.CartingFooter = txtCarting.Text.Trim();
            loc.PickUpFooter = txtPickup.Text.Trim();
            loc.CustomHouseCode = txtCustomhouseCode.Text.Trim();
            loc.GatewayPort = txtGatewayPort.Text.Trim();
            loc.ICEGateLoginD = txtICEGATE.Text.Trim();
            loc.PCSLoginID = txtPCS.Text.Trim();
            loc.ISO20 = txtISO20.Text.Trim();
            loc.ISO40 = txtISO40.Text.Trim();
        }

        #endregion
    }
}