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

namespace EMS.WebApp
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
            RetriveParameters();
            if (!Page.IsPostBack)
            {
                ListItem Li = null;
                Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.Port, ddlFromLocation, 0);
                ddlFromLocation.Items.Insert(0, Li);

                Li = new ListItem("Select", "0");
                PopulateDropDown((int)Enums.DropDownPopulationFor.Port, ddlToLocation, 0);
                ddlToLocation.Items.Insert(0, Li);

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

            ddlFromLocation.SelectedIndex = Convert.ToInt32(ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByValue(oImportHaulage.LocationFrom)));
            ddlToLocation.SelectedIndex = Convert.ToInt32(ddlToLocation.Items.IndexOf(ddlToLocation.Items.FindByValue(oImportHaulage.LocationTo)));
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
                oImportHaulageBll = new ImportHaulageBLL();
                oImportHaulageEntity = new ImportHaulageEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oImportHaulageEntity.ContainerSize = ddlContainerSize.SelectedItem.Text;
                oImportHaulageEntity.HaulageRate = Convert.ToDouble(txtRate.Text);
                oImportHaulageEntity.LocationFrom = ddlFromLocation.SelectedValue;
                oImportHaulageEntity.LocationTo = ddlToLocation.SelectedValue;
                oImportHaulageEntity.WeightFrom = Convert.ToDouble(txtWFrom.Text.Trim());
                oImportHaulageEntity.WeightTo = Convert.ToDouble(txtWTo.Text.Trim());
                oImportHaulageEntity.HaulageStatus = true;

                if (hdnHaulageChrgID.Value == "0") // Insert
                {
                    oImportHaulageEntity.CreatedBy = 1;// oUserEntity.Id;
                    oImportHaulageEntity.CreatedOn = DateTime.Today.Date;
                    oImportHaulageEntity.ModifiedBy = 1;// oUserEntity.Id;
                    oImportHaulageEntity.ModifiedOn = DateTime.Today.Date;

                }
                else // Update
                {
                    oImportHaulageEntity.HaulageChgID = Convert.ToInt32(hdnHaulageChrgID.Value);
                    oImportHaulageEntity.ModifiedBy = 2;// oUserEntity.Id;
                    oImportHaulageEntity.ModifiedOn = DateTime.Today.Date;
                }

                switch (oImportHaulageBll.AddEditImportHAulageChrg(oImportHaulageEntity))
                {
                    case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                        ClearAll();
                        break;
                    case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                        ClearAll();
                        break;
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
            Response.Redirect("~/ImportHaulage-list.aspx");
        }

        void ClearAll()
        {
            ddlContainerSize.SelectedIndex = 0;
            ddlFromLocation.SelectedIndex = 0;
            ddlToLocation.SelectedIndex = 0;
            hdnHaulageChrgID.Value = "0";
            txtRate.Text = string.Empty;
            txtWFrom.Text = string.Empty;
            txtWTo.Text = string.Empty;
        }
    }
}