using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;
using EMS.Entity;
using EMS.WebApp.CustomControls;


namespace EMS.WebApp.Forwarding.Master
{
    public partial class AddEditUnit : System.Web.UI.Page
    {
        #region Private Member Variables


        private int _userId = 0;
        private string countryId = "";

        private int companyId = 1;
        //private bool _canAdd = false;
        //private bool _canEdit = false;
        //private bool _canDelete = false;
        //private bool _canView = false;
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;

        private int UnitId { get { if (ViewState["Id"] != null) { return Convert.ToInt32(ViewState["Id"]); } return 0; } set { ViewState["Id"] = value; } }
        private string Mode { get { if (ViewState["Id"] != null) { return "E"; } return "A"; } }
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {

            RetriveParameters();
            if (!IsPostBack)
            {
                LoadDefault();
                if (Request.QueryString["UnitId"] != string.Empty)
                {

                    try
                    {
                        var id = GeneralFunctions.DecryptQueryString(Request.QueryString["UnitId"]);
                        var pid = Convert.ToInt32(id);
                        if (pid > 0)
                        {
                            UnitId = pid;
                            LoadData(pid);
                        }
                    }
                    catch { }
                }
            }
           
           // CheckUserAccess(countryId);
        }

        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            //Get user permission.
            //EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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
            SaveUnit();

        }

        #endregion

        #region Private Methods

        private void LoadDefault()
        {
            rdoStatus.SelectedIndex = 0;
        }
        private void LoadData(int id)
        {
            var src = new UnitBLL().GetUnits(new SearchCriteria() { StringParams = new List<string>() { "" } }, id, companyId);
            if (src != null && src.Count() > 0)
            {
                var unit = src.FirstOrDefault();
                txtUnit.Text = unit.UnitName;
                //txtPrefix.Text = unit.Prefix;
                //rdoStatus.SelectedIndex = unit.UnitStatus?0:1;
                if (unit.UnitType == "NA")
                    rdoStatus.SelectedIndex = 0;
                else
                    rdoStatus.SelectedIndex = 1;
            }
        }
        private void ClearText()
        {
            txtUnit.Text = string.Empty;
            //txtPrefix.Text = string.Empty;
        }

        private UnitEntity ExtractData()
        {
            //var party = 

            return new UnitEntity
            {
                UnitName = txtUnit.Text,//
                //Prefix = txtPrefix.Text,//
                UnitType = rdoStatus.SelectedIndex==0?"N":"E",//
                UnitTypeID=UnitId,
                CompanyID =companyId,//
                CreatedBy = _userId//

            };

        }
        private void SaveUnit()
        {

            var result = new UnitBLL().AddEditUnit(ExtractData(), companyId, Mode);
            if (result > 0)
            {
                UnitId = result;
                Response.Redirect("~/Forwarding/Master/ManageUnits.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Master/ManageUnits.aspx");
        }
    }
}