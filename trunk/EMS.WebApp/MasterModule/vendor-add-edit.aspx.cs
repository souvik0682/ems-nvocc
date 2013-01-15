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
    public partial class vendor_add_edit : System.Web.UI.Page
    {
        VendorEntity oVendorEntity;
        VendorBLL oVendorBll;
        UserEntity oUserEntity;

        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;

            RetriveParameters();
            if (!Page.IsPostBack)
            {
                ListItem Li = null;
                Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.VendorType, ddlVendorType, 0);
                ddlVendorType.Items.Insert(0, Li);

                Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlLocationID, 0);
                ddlLocationID.Items.Insert(0, Li);

                #region Salutation
                foreach (Enums.Salutation r in Enum.GetValues(typeof(Enums.Salutation)))
                {
                    Li = new ListItem("Select", "0");
                    ListItem item = new ListItem(Enum.GetName(typeof(Enums.Salutation), r).Replace('_', '/'), ((int)r).ToString());
                    ddlSalutation.Items.Add(item);
                }
                ddlSalutation.Items.Insert(0, Li);
                #endregion

                if (hdnVendorID.Value != "0")
                    LoadData();
            }
        }

        private void LoadData()
        {
            VendorEntity oVendor = (VendorEntity)VendorBLL.GetVendor(Convert.ToInt32(hdnVendorID.Value));

            ddlVendorType.SelectedIndex = Convert.ToInt32(ddlVendorType.Items.IndexOf(ddlVendorType.Items.FindByValue(oVendor.VendorType)));
            if (ddlVendorType.SelectedItem.Text == "CFS")
            {
                txtCfsCode.Enabled = true;
                rfvCfdCode.Enabled = true;
                ddlTerminalCode.Enabled = true;
            }
            else
            {
                txtCfsCode.Enabled = false;
                rfvCfdCode.Enabled = false;
                ddlTerminalCode.Enabled = false;
            }
            ddlLocationID.SelectedIndex = Convert.ToInt32(ddlLocationID.Items.IndexOf(ddlLocationID.Items.FindByValue(oVendor.LocationName)));
            PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlTerminalCode, Convert.ToInt32(ddlLocationID.SelectedValue));

            ddlSalutation.SelectedIndex = Convert.ToInt32(ddlSalutation.Items.IndexOf(ddlSalutation.Items.FindByValue(oVendor.VendorSalutation.ToString())));
            if (ddlTerminalCode.Items.Count > 0)
            {
                ddlTerminalCode.SelectedIndex = Convert.ToInt32(ddlTerminalCode.Items.IndexOf(ddlTerminalCode.Items.FindByValue(oVendor.Terminalid.ToString())));
            }

            txtName.Text = oVendor.VendorName;
            txtAddress.Text = oVendor.VendorAddress;
            txtCfsCode.Text = oVendor.CFSCode;

        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                oVendorBll = new VendorBLL();
                oVendorEntity = new VendorEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oVendorEntity.VendorType = ddlVendorType.SelectedValue;
                oVendorEntity.LocationID = Convert.ToInt32(ddlLocationID.SelectedValue);
                oVendorEntity.VendorSalutation = Convert.ToInt32(ddlSalutation.SelectedValue);
                oVendorEntity.VendorName = txtName.Text.Trim();
                oVendorEntity.VendorAddress = txtAddress.Text.Trim();
                oVendorEntity.CompanyID = 1;//Need to populate from data base. This will be the company ID of the currently loggedin user.
                oVendorEntity.VendorActive = true;


                oVendorEntity.CFSCode = txtCfsCode.Text.Trim();
                if (ddlTerminalCode.Items.Count > 0 && ddlTerminalCode.SelectedIndex > 0)
                    oVendorEntity.Terminalid = Convert.ToInt32(ddlTerminalCode.SelectedValue);

                if (hdnVendorID.Value == "0") // Insert
                {
                    oVendorEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oVendorEntity.CreatedOn = DateTime.Today.Date;
                    oVendorEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oVendorEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oVendorBll.AddEditVndor(oVendorEntity))
                    {
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                            ClearAll();
                            break;
                    }
                }
                else // Update
                {
                    oVendorEntity.VendorId = Convert.ToInt32(hdnVendorID.Value);
                    oVendorEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oVendorEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oVendorBll.AddEditVndor(oVendorEntity))
                    {
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: Response.Redirect("~/MasterModule/vendor-list.aspx");
                            break;
                    }
                }


            }
        }

        protected void ddlLocationID_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDropDown((int)Enums.DropDownPopulationFor.TerminalCode, ddlTerminalCode, Convert.ToInt32(ddlLocationID.SelectedValue));
        }

        protected void ddlVendorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVendorType.SelectedItem.Text == "CFS")
            {
                txtCfsCode.Enabled = true;
                rfvCfdCode.Enabled = true;
                ddlTerminalCode.Enabled = true;
            }
            else
            {
                txtCfsCode.Enabled = false;
                rfvCfdCode.Enabled = false;
                txtCfsCode.Text = string.Empty;
                ddlTerminalCode.Enabled = false;
            }

        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
                hdnVendorID.Value = _locId.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/vendor-list.aspx");
        }

        void ClearAll()
        {
            ddlLocationID.SelectedIndex = 0;
            ddlSalutation.SelectedIndex = 0;
            if (ddlTerminalCode.Items.Count > 0)
                ddlTerminalCode.SelectedIndex = 0;
            ddlVendorType.SelectedIndex = 0;
            txtAddress.Text = string.Empty;
            txtCfsCode.Text = string.Empty;
            txtName.Text = string.Empty;
            hdnVendorID.Value = "0";
        }
    }
}