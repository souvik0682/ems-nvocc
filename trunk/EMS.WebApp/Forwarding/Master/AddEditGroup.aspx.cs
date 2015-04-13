using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;
using EMS.Entity;
using EMS.WebApp.CustomControls;
using System.Globalization;
using System.Configuration;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Forwarding.Master
{
    public partial class AddEditGroup : System.Web.UI.Page
    {
        GroupEntity oPartyEntity;
        #region Private Member Variables


        private int _userId = 0;
        private string countryId = "";
        //private bool _canAdd = false;
        //private bool _canEdit = false;
        //private bool _canDelete = false;
        //private bool _canView = false;
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;

        private int PartyId { get { if (ViewState["Id"] != null) { return Convert.ToInt32(ViewState["Id"]); } return 0; } set { ViewState["Id"] = value; } }
        private string Mode { get { if (ViewState["Id"] != null) { return "E"; } return "A"; } }
        #endregion


        #region Protected Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            if (!IsPostBack)
            {
                LoadDefault();
                //lblUploadedFileName.Text = "";

                if (Request.QueryString["GroupId"] != string.Empty)
                {

                    try
                    {
                        var id = GeneralFunctions.DecryptQueryString(Request.QueryString["GroupId"]);
                        var pid = Convert.ToInt32(id);
                        if (pid > 0)
                        {
                            PartyId = pid;
                            LoadData(pid);
                        }
                    }
                    catch { }
                }
            }
            CheckUserAccess(PartyId.ToString());
           
        }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveGroup();

        }

        #endregion

        #region Private Methods

        private void LoadDefault()
        {
            //var line = new CommonBLL().GetfwLineByType(new SearchCriteria { StringOption1 = "S,A" });
  

        }
        private void LoadData(int id)
        {
            var src = new PartyBLL().GetGroup(id, new SearchCriteria() { StringParams = new List<string>() { "0", "" } });
            if (src != null && src.Count() > 0)
            {
                var party = src.FirstOrDefault();
                txtAddress.Text = party.GroupAddress;
                txtPartyName.Text = party.GroupName;
             }
        }
        private void ClearText()
        {
            txtPartyName.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private GroupEntity ExtractData()
        {
            

            return new GroupEntity
            {
               
                GroupName = txtPartyName.Text,//
                GroupAddress = txtAddress.Text,//
                UserID = _userId//


            };

        }
        private void SaveGroup()
        {


            var result = new PartyBLL().SaveGroup(ExtractData(), Mode);
            if (result > 0)
            {
                PartyId = result;
                
                Response.Redirect("~/Forwarding/Master/ManageGroup.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Master/ManageGroup.aspx");
        }

    }
}