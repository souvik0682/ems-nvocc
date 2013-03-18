﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;

using EMS.Common;
using EMS.Entity;
using EMS.BLL;

namespace EMS.WebApp.Equipment
{
    public partial class AddEditRepairEstimate : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string EqEstId = "";
        #endregion

        BLL.DBInteraction dbinteract = new BLL.DBInteraction();
        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            EqEstId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());



                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('RepairingEstimate.aspx','" + EMS.Utilities.ResourceManager.ResourceManager.GetStringWithoutName("ERR00017") + "')";
                if (EqEstId != "-1")
                {
                    // GeneralFunctions.PopulateDropDownList(ddlUser, EMS.BLL.UserBLL.GetAdminUsers(_userId, Convert.ToInt32(ddlLoc.SelectedValue)));

                    LoadData(_userId, EqEstId);
                    System.Data.DataSet ds = UserBLL.GetUserById(_userId);
                    if (ds.Tables[0].Rows.Count > 0)
                        txtAppUser.Text = ds.Tables[0].Rows[0]["Name"].ToString();

                }

                int dis = DisableControls();
            }
            if (EqEstId == "-1")
            {
                txtReleasedOn.Enabled = false;
                txtStockRetDate.Enabled = false;
            }

        }

        private int DisableControls()
        {
            int[] adm_mgrRoles = { 1, 2, 3, 6 };
            if (Array.IndexOf(adm_mgrRoles, EMS.BLL.UserBLL.GetLoggedInUserRoleId()) < 0)
            {
                //ddlUser.Enabled = false;
                txtAppUser.ReadOnly = true;
                txtAppUser.Style.Add("background-color", "#E6E6E6");
                txtMaterialApp.ReadOnly = true;
                txtMaterialApp.Style.Add("background-color", "#E6E6E6");
                txtLabourApp.ReadOnly = true;
                txtLabourApp.Style.Add("background-color", "#E6E6E6");
                if (txtAppUser.Text.Trim() != "")
                    btnSave.Enabled = false;
                return 0;
            }
            else return 1;

        }

        private void LoadData(int _userId, string EqEstId)
        {
            int intEqEstId = 0;
            if (EqEstId == "" || !Int32.TryParse(EqEstId, out intEqEstId))
                return;
            ClearControls();
            IEqpRepairing iequip = new EquipmentRepairEntity();
            iequip.pk_RepairID = intEqEstId;
            iequip.Location = "";
            iequip.ContainerNo = "";
            System.Data.DataTable dt = EquipmentBLL.GetEqpRepair(_userId, iequip);


            if (!ReferenceEquals(dt, null) && dt.Rows.Count > 0)
            {
                txtContainerNo.Text = dt.Rows[0]["ContainerNo"].ToString();
                txtEstimateRef.Text = dt.Rows[0]["EstimateReference"].ToString();
                txtLabourBill.Text = dt.Rows[0]["LabourBill"].ToString();
                txtLabourEst.Text = dt.Rows[0]["LabourEst"].ToString();
                txtLabourApp.Text = dt.Rows[0]["LabourApp"].ToString();
                txtMaterialApp.Text = dt.Rows[0]["MaterialAppr"].ToString();
                txtMaterialBill.Text = dt.Rows[0]["MaterialBill"].ToString();
                txtMaterialEst.Text = dt.Rows[0]["MaterialEst"].ToString();
                txtReason.Text = dt.Rows[0]["Reason"].ToString();
                txtReleasedOn.Text = dt.Rows[0]["ReleasedOn"].ToString().Split(' ')[0];
                txtStockRetDate.Text = dt.Rows[0]["StockRetDate"].ToString().Split(' ')[0];
                txtTransactionDate.Text = dt.Rows[0]["TransactionDate"].ToString().Split(' ')[0];
                ddlLine.SelectedValue = dt.Rows[0]["pk_prospectID"].ToString();
                ddlLoc.SelectedValue = dt.Rows[0]["pk_locId"].ToString();
                //ddlUser.SelectedValue = dt.Rows[0]["fk_UserApproved"].ToString();
                txtAppUser.Text = dt.Rows[0]["fk_UserApproved"].ToString();
                chkpOnHold.Checked = Convert.ToBoolean(dt.Rows[0]["onHold"]);
                chkDamage.Checked = Convert.ToBoolean(dt.Rows[0]["Damaged"]);
            }
        }

        private void ClearControls()
        {
            // txtContainerNo.Text = "";
            txtEstimateRef.Text = "";
            txtLabourBill.Text = "";
            txtLabourEst.Text = "";
            txtMaterialApp.Text = "";
            txtMaterialBill.Text = "";
            txtMaterialEst.Text = "";
            txtReason.Text = "";
            txtReleasedOn.Text = "";
            txtStockRetDate.Text = "";
            // txtTransactionDate.Text = "";
            ddlLine.SelectedIndex = 0;
            // ddlLoc.SelectedIndex = 0;
            //ddlUser.SelectedIndex = 0;
            //txtAppUser.Text = "";
            lblError.Text = "";
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            EqEstId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(EqEstId);
        }

        private void SaveData(string EqEstId)
        {
            if (chkDamage.Checked || chkpOnHold.Checked)
                ClearControls();
            else
                if (!checkContainerStatus(true)) return;



            if (DisableControls() == 1 && txtAppUser.Text.Trim() == "")//ddlUser.SelectedIndex == 0)
            {
                lblError.Text = "Approver name cannot be blank";
                return;
            }
            IEqpRepairing iequip = new EquipmentRepairEntity();
            iequip.pk_RepairID = Convert.ToInt32(EqEstId);
            iequip.locId = Convert.ToInt32(ddlLoc.SelectedValue);
            iequip.ContainerNo = txtContainerNo.Text;
            iequip.EstimateReference = txtEstimateRef.Text;
            iequip.RepLabourBilled = txtLabourBill.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtLabourBill.Text);
            iequip.RepLabourEst = txtLabourEst.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtLabourEst.Text);
            iequip.RepLabourAppr = txtLabourApp.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtLabourApp.Text);
            iequip.RepMaterialAppr = txtMaterialApp.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtMaterialApp.Text);
            iequip.RepMaterialBilled = txtMaterialBill.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtMaterialBill.Text);
            iequip.RepMaterialEst = txtMaterialEst.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtMaterialEst.Text);
            iequip.Reason = txtReason.Text;
            iequip.RealeasedOn = txtReleasedOn.Text.Trim() == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(txtReleasedOn.Text);
            iequip.StockReturnDate = txtStockRetDate.Text.Trim() == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(txtStockRetDate.Text);
            iequip.TransactionDate = txtTransactionDate.Text.Trim() == "" ? (Nullable<DateTime>)null : Convert.ToDateTime(txtTransactionDate.Text);
            iequip.ProspectID = Convert.ToInt32(ddlLine.SelectedValue);
            iequip.locId = Convert.ToInt32(ddlLoc.SelectedValue);
            iequip.NVOCCId = Convert.ToInt32(ddlLine.SelectedValue);
            try
            {
                iequip.fk_UserApproved = Convert.ToInt32(txtAppUser.Text);// Convert.ToInt32(ddlUser.SelectedValue);
            }
            catch
            {

                iequip.fk_UserApproved = 0;
            }

            iequip.onHold = chkpOnHold.Checked;
            iequip.Damaged = chkDamage.Checked;


            if (iequip.RepLabourBilled <= iequip.RepLabourAppr || iequip.RepMaterialBilled <= iequip.RepMaterialAppr)
            {
                // GeneralFunctions.RegisterAlertScript(this, "Approved amount cannot be greater then Billable amount");
                lblError.Text = "Approved amount cannot be greater then Billable amount";
                return;
            }

            if (iequip.RepLabourEst <= iequip.RepLabourAppr || iequip.RepMaterialEst <= iequip.RepMaterialAppr)
            {
                // GeneralFunctions.RegisterAlertScript(this, "Approved amount cannot be greater then Billable amount");
                lblError.Text = "Approved amount cannot be greater then Estimate amount";
                return;
            }

            bool isedit = EqEstId != "-1" ? true : false;
            int result = EquipmentBLL.AddEditEquipEstimate(_userId, isedit, iequip);
            if (result > 0)
            {
                Response.Redirect("~/Equipment/RepairingEstimate.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }

        }

        private bool checkContainerStatus(bool fromSaveButton)
        {
            lblError.Text = "";
            System.Data.DataSet ds = EquipmentBLL.CheckContainerStatus(txtContainerNo.Text.Trim());
            string abbr = string.Empty;
            try
            {
                if (ds.Tables.Count > 0)
                    abbr = Convert.ToString(ds.Tables[0].Rows[0]["MoveAbbr"]);
            }
            catch
            {


            }



            bool canEditable = false;
            if (abbr == string.Empty)
            {
                GeneralFunctions.RegisterAlertScript(this, "This container-number doesn't exists");
                lblError.Text = "This container-number doesn't exists or its movement id is null";
            }
            else
            {
                if (abbr.ToUpper() != "RCVE" && abbr.ToUpper() != "OFFH")
                {
                    GeneralFunctions.RegisterAlertScript(this, "Container status is not RCVE/OFFH and hence repair entry is not possible");

                }
                else
                {
                    if (!fromSaveButton)
                        GeneralFunctions.RegisterAlertScript(this, "Container status is RCVE/OFFH.");
                    canEditable = true;
                }
            }


            return canEditable;
        }

        protected void lnkStatus_Click(object sender, EventArgs e)
        {
            checkContainerStatus(false);
        }

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GeneralFunctions.PopulateDropDownList(ddlUser, EMS.BLL.UserBLL.GetAdminUsers(_userId, Convert.ToInt32(ddlLoc.SelectedValue)));
        }
    }
}