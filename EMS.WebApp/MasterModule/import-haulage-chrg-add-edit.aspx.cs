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
    public partial class import_haulage_chrg_add_edit : System.Web.UI.Page
    {
        ImportHaulageEntity oImportHaulageEntity;
        ImportHaulageBLL oImportHaulageBll;
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
                //Li = new ListItem("Select", "0");
                //PopulateDropDown((int)Enums.DropDownPopulationFor.Port, ddlLocation, 0);
                //ddlLocation.Items.Insert(0, Li);

                //Li = new ListItem("Select", "0");
                //PopulateDropDown((int)Enums.DropDownPopulationFor.Port, ddlToLocation, 0);
                //ddlToLocation.Items.Insert(0, Li);

                Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.ContainerSize, ddlContainerSize, 0);
                ddlContainerSize.Items.Insert(0, Li);

                if (hdnHaulageChrgID.Value != "0")
                    LoadData();
            }
        }

        private void LoadData()
        {
            ImportHaulageEntity oImportHaulage = (ImportHaulageEntity)ImportHaulageBLL.GetImportHaulage(Convert.ToInt32(hdnHaulageChrgID.Value));

            //ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            hdnFromLocation.Value = oImportHaulage.LocationFrom.Substring(oImportHaulage.LocationFrom.IndexOf('|') + 1);
            txtFromLocation.Text = oImportHaulage.LocationFrom.Substring(0, oImportHaulage.LocationFrom.IndexOf('|'));
            //ddlToLocation.SelectedIndex = Convert.ToInt32(ddlToLocation.Items.IndexOf(ddlToLocation.Items.FindByValue(oImportHaulage.LocationTo)));

            hdnToLocation.Value = oImportHaulage.LocationTo.Substring(oImportHaulage.LocationTo.IndexOf('|') + 1);
            txtToLocation.Text = oImportHaulage.LocationTo.Substring(0, oImportHaulage.LocationTo.IndexOf('|'));

            ddlContainerSize.SelectedIndex = Convert.ToInt32(ddlContainerSize.Items.IndexOf(ddlContainerSize.Items.FindByText(oImportHaulage.ContainerSize)));


            txtWFrom.Text = Convert.ToString(oImportHaulage.WeightFrom);
            txtWTo.Text = Convert.ToString(oImportHaulage.WeightTo);
            txtRate.Text = Convert.ToString(oImportHaulage.HaulageRate);

        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                if (Convert.ToDecimal(txtWFrom.Text) > Convert.ToDecimal(txtWTo.Text))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00070") + "');</script>", false);
                    return;
                }

                oImportHaulageBll = new ImportHaulageBLL();
                oImportHaulageEntity = new ImportHaulageEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oImportHaulageEntity.ContainerSize = ddlContainerSize.SelectedItem.Text;
                oImportHaulageEntity.HaulageRate = Convert.ToDouble(txtRate.Text);
                oImportHaulageEntity.LocationFrom = hdnFromLocation.Value;
                oImportHaulageEntity.LocationTo = hdnToLocation.Value;
                oImportHaulageEntity.WeightFrom = Convert.ToDouble(txtWFrom.Text.Trim());
                oImportHaulageEntity.WeightTo = Convert.ToDouble(txtWTo.Text.Trim());
                oImportHaulageEntity.HaulageStatus = true;

                if (hdnHaulageChrgID.Value == "0") // Insert
                {
                    oImportHaulageEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oImportHaulageEntity.CreatedOn = DateTime.Today.Date;
                    oImportHaulageEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oImportHaulageEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oImportHaulageBll.AddEditImportHAulageChrg(oImportHaulageEntity))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            ClearAll();
                            break;
                        case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                            ClearAll();
                            break;
                    }
                }
                else // Update
                {
                    oImportHaulageEntity.HaulageChgID = Convert.ToInt32(hdnHaulageChrgID.Value);
                    oImportHaulageEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oImportHaulageEntity.ModifiedOn = DateTime.Today.Date;
                    //
                    switch (oImportHaulageBll.AddEditImportHAulageChrg(oImportHaulageEntity))
                    {
                        case -1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00076");
                            break;
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");                            
                            break;
                        case 1: Response.Redirect("~/MasterModule/ImportHaulage-list.aspx"); 
                            break;
                    }
                }               


            }
        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
                hdnHaulageChrgID.Value = _locId.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/ImportHaulage-list.aspx");
        }

        void ClearAll()
        {
            ddlContainerSize.SelectedIndex = 0;
            txtFromLocation.Text = string.Empty;
            txtToLocation.Text = string.Empty;
            hdnHaulageChrgID.Value = "0";
            txtRate.Text = string.Empty;
            txtWFrom.Text = string.Empty;
            txtWTo.Text = string.Empty;
        }
    }
}