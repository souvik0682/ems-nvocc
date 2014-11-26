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
    public partial class AddEditFLine : System.Web.UI.Page
    {
        fwLineEntity oLineEntity;
        fwLineBLL oLineBll;
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
                if (hdnFLineID.Value != "0")
                    LoadData();
            }
            CheckUserAccess(hdnFLineID.Value);
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
            fwLineEntity oLine = (fwLineEntity)fwLineBLL.GetFLine(Convert.ToInt32(hdnFLineID.Value));

            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            //hdnFLineID.Value = oLine.LineID.ToString();
            //ddlLineType.SelectedValue = oLine.LineType.ToString();
            txtLine.Text = Convert.ToString(oLine.LineName);
            txtPrefix.Text = Convert.ToString(oLine.Prefix);
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
                oLineBll = new fwLineBLL();
                oLineEntity = new fwLineEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily
                mode = "U";
                if (hdnFLineID.Value.ToInt() == 0)
                    mode = "A";
                //oLineEntity.LineType = ddlLineType.SelectedValue.ToString();
                oLineEntity.LineName = txtLine.Text.ToString();
                oLineEntity.LineID = hdnFLineID.ToInt();
                oLineEntity.Prefix = txtPrefix.Text;
                oLineEntity.CreatedBy = _userId;

                oLineBll.SaveFwLine(oLineEntity, _CompanyId, mode);
                if (mode == "A")
                    ClearAll();
                else
                    Response.Redirect("~/Forwarding/Master/ManageFLine.aspx");
            }
        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _LineId);
                hdnFLineID.Value = _LineId.ToString();
            }
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Master/ManageFLine.aspx");
        }

        void ClearAll()
        {
            //ddlLineType.SelectedIndex = 0;
            txtPrefix.Text = string.Empty;
            txtLine.Text = string.Empty;
            hdnFLineID.Value = "0";
           
        }
    }
}