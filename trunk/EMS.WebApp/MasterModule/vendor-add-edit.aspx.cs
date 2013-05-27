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
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckUserAccess();
            //IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            //_userId = user == null ? 0 : user.Id;

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
            CheckUserAccess(hdnVendorID.Value);
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

        private void LoadData()
        {
            VendorEntity oVendor = (VendorEntity)VendorBLL.GetVendor(Convert.ToInt32(hdnVendorID.Value));

            ddlVendorType.SelectedIndex =ddlVendorType.Items.IndexOf(ddlVendorType.Items.FindByValue(oVendor.VendorType));
            if (ddlVendorType.SelectedItem.Text == "CFS/ICD" || ddlVendorType.SelectedItem.Text =="ICD")
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
            TxtTAN.Text = oVendor.TANo;
            TxtPAN.Text = oVendor.PAN;
            TxtACNo.Text = oVendor.AcNo;
            TxtAcType.Text = oVendor.AcType;
            TxtBankName.Text = oVendor.BankName;
            TxtIEC.Text = oVendor.IEC;
            TxtEmail.Text = oVendor.EmailID;
            TxtBIN.Text = oVendor.BIN;
            TxtMob.Text = oVendor.Mobile;
            TxtCP.Text = oVendor.CP;
           
            
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter,0);
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
                oVendorEntity.AcNo = TxtACNo.Text;
                oVendorEntity.AcType = TxtAcType.Text;
                oVendorEntity.BankName = TxtBankName.Text;
                oVendorEntity.BIN = TxtBIN.Text;
                oVendorEntity.EmailID = TxtEmail.Text;
                oVendorEntity.IEC = TxtIEC.Text;
                oVendorEntity.Mobile = TxtMob.Text;
                oVendorEntity.TANo = TxtTAN.Text;
                oVendorEntity.PAN = TxtPAN.Text;
                oVendorEntity.CP = TxtCP.Text;


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
            if (ddlVendorType.SelectedItem.Text == "CFS/ICD" || ddlVendorType.SelectedItem.Text == "ICD")
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
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

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
            TxtBankName.Text = string.Empty;
            TxtBIN.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtMob.Text = string.Empty;
            TxtPAN.Text = string.Empty;
            TxtTAN.Text = string.Empty;
            TxtACNo.Text = string.Empty;
            TxtAcType.Text = string.Empty;
            TxtIEC.Text = string.Empty;
            TxtCP.Text = string.Empty;

        }
    }
}