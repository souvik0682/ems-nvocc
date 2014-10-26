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
    public partial class AddEditFLoc : System.Web.UI.Page
    {
        fwLocationEntity oLineEntity;
        fwLocBLL oLineBll;
        UserEntity oUserEntity;

        #region Private Member Variables

        private int _userId = 0;
        private int _LineId = 0;
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
                if (hdnFLocID.Value != "0")
                    LoadData();
            }
            CheckUserAccess(hdnFLocID.Value);
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
            fwLocationEntity oLoc = (fwLocationEntity)fwLocBLL.GetFLoc(Convert.ToInt32(hdnFLocID.Value));

            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            //hdnFLineID.Value = oLine.LineID.ToString();
            txtLocation.Text = Convert.ToString(oLoc.LocName);
            txtAbbr.Text = Convert.ToString(oLoc.Abbreviation);
            txtCity.Text = Convert.ToString(oLoc.LocAddress.City);
            TxtPin.Text = Convert.ToString(oLoc.LocAddress.Pin);
            txtAddress.Text = Convert.ToString(oLoc.LocAddress.Address);
            txtPhone.Text = Convert.ToString(oLoc.Phone);
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
                string mode;
                
                oLineBll = new fwLocBLL();
                oLineEntity = new fwLocationEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily
                mode = "U";
                if (hdnFLocID.Value.ToInt() == 0)
                    mode = "A";
                oLineEntity.LocName = txtLocation.Text.ToString();
                oLineEntity.locID = hdnFLocID.ToInt();
                oLineEntity.LocAddress.Pin = TxtPin.Text;
                oLineEntity.LocAddress.Address = txtAddress.Text;
                oLineEntity.LocAddress.City = txtCity.Text;
                oLineEntity.Phone = txtPhone.Text;
                oLineEntity.Abbreviation = txtAbbr.Text;
                oLineEntity.CreatedBy = _userId;

                oLineBll.SaveFwLoc(oLineEntity);
                if (mode == "A")
                    ClearAll();
                else
                    Response.Redirect("~/Forwarding/Master/ManageFLoc.aspx");
            }
        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _LineId);
                hdnFLocID.Value = _LineId.ToString();
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Master/ManageFLoc.aspx");
        }

        void ClearAll()
        {
            txtPhone.Text = string.Empty;
            TxtPin.Text = string.Empty;
            hdnFLocID.Value = "0";
            txtAbbr.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;


        }
    }
}