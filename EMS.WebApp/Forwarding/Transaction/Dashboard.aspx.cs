﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using EMS.BLL;
using EMS.Common;
using EMS.Utilities;
using System.Data;
using EMS.Utilities.ResourceManager;
using Microsoft.Win32;
using System.IO;
using System.Text;

namespace EMS.WebApp.Farwarding.Transaction
{
    public partial class Dashboard : System.Web.UI.Page
    {
        ImportBLL oImportBLL = new ImportBLL();
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        private static string DocumentTypeIdForDr = "37";
        private static string DocumentTypeIdForCr = "38";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();

            if (!IsPostBack)
            {
                if (!ReferenceEquals(Request.QueryString["JobId"], null))
                {
                    int JobId = 1;
                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["JobId"].ToString()), out JobId);
                    btnCloseJob.Attributes.Add("onclick", "javascript:return confirm('Are you sure to Open/Close Job?');");
                    if (JobId > 0)
                    {
                        ViewState["JOBID"] = JobId;
                        LoadDashboard();
                    }
                    else
                    {
                        ViewState["JOBID"] = null;
                    }
                }
            }
        }

        private void LoadDashboard()
        {
            DataSet dsDashboard = JobBLL.GetDashBoard(Convert.ToInt32(ViewState["JOBID"]));

            //Jpb Details & Job Summary
            DataTable dtJob = dsDashboard.Tables[0];


            if (!ReferenceEquals(dtJob, null) && dtJob.Rows.Count > 0)
            {
                lblTTL20.CssClass = "right_align";
                lblTTL40.CssClass = "right_align";
                lblClosed.Text = "";
                lblApprover.Text = "";
                btnCloseJob.Text = "Close Job";

                if (dtJob.Rows[0]["JobDate"] != DBNull.Value)
                    lblJobDate.Text = Convert.ToString(dtJob.Rows[0]["JobDate"]).Split(' ')[0];
                if (dtJob.Rows[0]["FJobNo"] != DBNull.Value)
                    lblJobNumber.Text = Convert.ToString(dtJob.Rows[0]["FJobNo"]);
                if (dtJob.Rows[0]["CargoSource"] != DBNull.Value)
                    lblCargoSource.Text = Convert.ToString(dtJob.Rows[0]["CargoSource"]);
                if (dtJob.Rows[0]["OpsLoc"] != DBNull.Value)
                    lblOps.Text = Convert.ToString(dtJob.Rows[0]["OpsLoc"]);
                if (dtJob.Rows[0]["DocLoc"] != DBNull.Value)
                    lblDoc.Text = Convert.ToString(dtJob.Rows[0]["DocLoc"]);
                if (dtJob.Rows[0]["Salesman"] != DBNull.Value)
                    lblSales.Text = Convert.ToString(dtJob.Rows[0]["Salesman"]);
                if (dtJob.Rows[0]["ApprovedBy"] != DBNull.Value)
                    lblApprover.Text = Convert.ToString(dtJob.Rows[0]["ApprovedBy"]); //missing
                if (dtJob.Rows[0]["ClosedBy"] != DBNull.Value)
                    lblClosed.Text = Convert.ToString(dtJob.Rows[0]["ClosedBy"]); //missing
                if (dtJob.Rows[0]["JobType"] != DBNull.Value)
                    lblJobType.Text = Convert.ToString(dtJob.Rows[0]["JobType"]);
                if (dtJob.Rows[0]["JobScope"] != DBNull.Value)
                    lblJobScope.Text = Convert.ToString(dtJob.Rows[0]["JobScope"]);
                if (dtJob.Rows[0]["ShippingMode"] != DBNull.Value)
                    lblShipping.Text = Convert.ToString(dtJob.Rows[0]["ShippingMode"]);
                if (dtJob.Rows[0]["DocName"] != DBNull.Value)
                    lblPrimeDocs.Text = Convert.ToString(dtJob.Rows[0]["DocName"]);
                if (dtJob.Rows[0]["DocumentNo"] != DBNull.Value)
                    lblPrimeDocNo.Text = Convert.ToString(dtJob.Rows[0]["DocumentNo"]);
                if (dtJob.Rows[0]["Revton"] != DBNull.Value)
                    lblRevenue.Text = Convert.ToString(dtJob.Rows[0]["Revton"]);
                if (dtJob.Rows[0]["wtKG"] != DBNull.Value)
                    lblWeight.Text = Convert.ToString(dtJob.Rows[0]["wtKG"]);
                if (dtJob.Rows[0]["ttl20"] != DBNull.Value)
                    lblTTL20.Text = Convert.ToString(dtJob.Rows[0]["ttl20"]);
                if (dtJob.Rows[0]["ttl40"] != DBNull.Value)
                    lblTTL40.Text = Convert.ToString(dtJob.Rows[0]["ttl40"]);
                //if (dtJob.Rows[0]["grwt"] != DBNull.Value)
                //    lblWeight.Text = Convert.ToString(dtJob.Rows[0]["grwt"]);
                //if (dtJob.Rows[0]["RevTon"] != DBNull.Value)
                //    lblRevenue.Text = Convert.ToString(dtJob.Rows[0]["RevTon"]);
                if (dtJob.Rows[0]["PlaceOfReceipt"] != DBNull.Value)
                    lblPlaceReceive.Text = Convert.ToString(dtJob.Rows[0]["PlaceOfReceipt"]);
                if (dtJob.Rows[0]["PlaceOfDelivery"] != DBNull.Value)
                    lblPlaceDelivery.Text = Convert.ToString(dtJob.Rows[0]["PlaceOfDelivery"]);
                if (dtJob.Rows[0]["Carrier"] != DBNull.Value)
                    lblCarrier.Text = Convert.ToString(dtJob.Rows[0]["Carrier"]); //missing
                if (dtJob.Rows[0]["CustName"] != DBNull.Value)
                    lblCustomer.Text = Convert.ToString(dtJob.Rows[0]["CustName"]);
                if (dtJob.Rows[0]["AgentName"] != DBNull.Value)
                    lblCustomerAgent.Text = Convert.ToString(dtJob.Rows[0]["AgentName"]);
                if (dtJob.Rows[0]["Transporter"] != DBNull.Value)
                    lblTransporter.Text = Convert.ToString(dtJob.Rows[0]["Transporter"]);
                if (dtJob.Rows[0]["OverseasAgent"] != DBNull.Value)
                    lblOverseas.Text = Convert.ToString(dtJob.Rows[0]["OverseasAgent"]);
                if (dtJob.Rows[0]["EstPayable"] != DBNull.Value)
                    lblTotalEstimatePayable.Text = Convert.ToString(dtJob.Rows[0]["EstPayable"]);

                if (lblShipping.Text == "LCL" || lblShipping.Text == "BREAK-BULK" || lblShipping.Text == "FCL")
                {
                    lblwtText.Text = "Weight MT";
                    lblRevText.Text = "Volume CBM";
                }
                else if (lblShipping.Text == "AIR CONSOLE")
                {
                    lblwtText.Text = "Weight (Kgs.)";
                    lblRevText.Text = "Revenue Ton";
                }
                //if (dtJob.Rows[0]["TotPaid"] != DBNull.Value)
                //    lblTotalPaid.Text = Convert.ToString(dtJob.Rows[0]["TotPaid"]);
                //if (dtJob.Rows[0]["TotReceived"] != DBNull.Value)
                //    lblTotalReceived.Text = Convert.ToString(dtJob.Rows[0]["TotReceived"]);
                if (dtJob.Rows[0]["EstReceivable"] != DBNull.Value)
                    lblTotalEstimateReceiveable.Text = Convert.ToString(dtJob.Rows[0]["EstReceivable"]);
                if (dtJob.Rows[0]["EstREceivable"] != DBNull.Value && dtJob.Rows[0]["EstPayable"] != DBNull.Value)
                    lblProjectedGrossProfit.Text = (Convert.ToDecimal(dtJob.Rows[0]["EstREceivable"]) - Convert.ToDecimal(dtJob.Rows[0]["EstPayable"])).ToString();
                if (dtJob.Rows[0]["TotReceived"] != DBNull.Value && dtJob.Rows[0]["TotPaid"] != DBNull.Value)
                    lblArchievedGrossProfit.Text = (Convert.ToDecimal(dtJob.Rows[0]["TotReceived"]) - Convert.ToDecimal(dtJob.Rows[0]["TotPaid"])).ToString();
                if (dtJob.Rows[0]["LoadPort"] != DBNull.Value)
                    lblPOL.Text = Convert.ToString(dtJob.Rows[0]["LoadPort"]);
                if (dtJob.Rows[0]["DischPort"] != DBNull.Value)
                    lblPOD.Text = Convert.ToString(dtJob.Rows[0]["DischPort"]);
                hdnCustID.Value = dtJob.Rows[0]["fk_CustID"].ToString();
                ViewState["JobActive"] = dtJob.Rows[0]["JobActive"].ToString();
                
                
                //if (_roleId == 23 && dtJob.Rows[0]["JobActive"].ToString() == "P")
                //{
                //    btnApprove.Enabled = true;
                //    btnCloseJob.Enabled = false;
                //}

                //if (_roleId == 23 && dtJob.Rows[0]["JobActive"].ToString() == "P")
                //{
                //    btnApprove.Enabled = true;
                //    btnCloseJob.Enabled = false;
                //}
                lblTotalEstimatePayable.Text = "0";
                lblTotalEstimateReceiveable.Text = "0";
                lblTotalPaid.Text = "0";
                lblTotalReceived.Text = "0";
                    
                if (dsDashboard.Tables[5].Rows.Count > 0)
                    lblTotalEstimatePayable.Text = dsDashboard.Tables[5].Rows[0]["Payable"].ToString();

                if (dsDashboard.Tables[6].Rows.Count > 0)
                    lblTotalEstimateReceiveable.Text = dsDashboard.Tables[6].Rows[0]["Receivable"].ToString();


                lblProjectedGrossProfit.Text = (lblTotalEstimateReceiveable.Text.ToDecimal() - lblTotalEstimatePayable.Text.ToDecimal()).ToString();
                //(dsDashboard.Tables[6].Rows[0]["Receivable"].ToDecimal() - dsDashboard.Tables[5].Rows[0]["Payable"].ToDecimal()).ToString();

                if (dsDashboard.Tables[7].Rows.Count > 0)
                    lblTotalPaid.Text = (Convert.ToDecimal(dsDashboard.Tables[7].Rows[0]["CreInvTotal"]) - Convert.ToDecimal(dsDashboard.Tables[7].Rows[0]["CreStax"])).ToString();

                if (dsDashboard.Tables[8].Rows.Count > 0)
                    lblTotalReceived.Text = (dsDashboard.Tables[8].Rows[0]["DrInvTotal"].ToDecimal() - dsDashboard.Tables[8].Rows[0]["CNAmt"].ToDecimal() - dsDashboard.Tables[8].Rows[0]["InvStax"].ToDecimal()).ToString();

                lblArchievedGrossProfit.Text = (lblTotalReceived.Text.ToDecimal() - lblTotalPaid.Text.ToDecimal()).ToString();

                if (dtJob.Rows[0]["JobActive"].ToString() == "O")
                {
                    btnApprove.Text = "Approve Job";
                    if (_roleId == (int)UserRole.fuser)
                        btnCloseJob.Enabled = false;
                    else
                        btnCloseJob.Enabled = true;
                    Button2.Visible = false;
                    Button4.Visible = false;
                    btnApprove.Enabled = false;
                    //if (dtJob.Rows[0]["UserConfirm"].ToInt() == 1 && _roleId != 23 && string.IsNullOrEmpty(lblApprover.Text))
                    //{
                    //    btnApprove.Enabled = true;
                    //}
                    //else
                    //{
                    //    btnApprove.Enabled = false;
                    //}
                }
                else if (dtJob.Rows[0]["JobActive"].ToString() == "P" && (_roleId == (int)UserRole.fuser || _roleId == (int)UserRole.Admin))
                {
                    btnApprove.Enabled = true;
                    btnCloseJob.Enabled = false;
                    btnApprove.Text = "Conf Estimate";
                    Button5.Visible = false;
                    Button6.Visible = false;
                    Button1.Visible = false;
                    Button3.Visible = false;
                    if (lblTotalEstimatePayable.Text.ToDecimal() < lblTotalEstimateReceiveable.Text.ToDecimal() && dtJob.Rows[0]["UserConfirm"].ToInt() == 0)
                    {
                        btnApprove.Enabled = true;
                        ViewState["NextStatus"] = "A";
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                    }
                }
                else if (dtJob.Rows[0]["JobActive"].ToString() == "P" && _roleId != (int)UserRole.fuser)
                {
                    btnApprove.Enabled = false;
                    btnCloseJob.Enabled = false;
                    btnApprove.Text = "Conf Estimate";
                    Button5.Visible = false;
                    Button6.Visible = false;
                    Button1.Visible = false;
                    Button3.Visible = false;
                    if (lblTotalEstimatePayable.Text.ToDecimal() < lblTotalEstimateReceiveable.Text.ToDecimal() && dtJob.Rows[0]["UserConfirm"].ToInt() == 0)
                    {
                        btnApprove.Enabled = true;
                        ViewState["NextStatus"] = "A";
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                    }
                }
                else if (dtJob.Rows[0]["JobActive"].ToString() == "A" && (_roleId == (int)UserRole.fuser))
                {
                    btnApprove.Enabled = false;
                    btnCloseJob.Enabled = false;
                    btnApprove.Text = "Approve Job";
                    Button5.Visible = false;
                    Button6.Visible = false;
                    Button1.Visible = false;
                    Button3.Visible = false;
                    //if (lblTotalEstimatePayable.Text.ToDecimal() < lblTotalEstimateReceiveable.Text.ToDecimal() && dtJob.Rows[0]["UserConfirm"].ToInt() == 1 && string.IsNullOrEmpty(lblApprover.Text))
                    //{
                    //    btnApprove.Enabled = false;
                    //    ViewState["NextStatus"] = "O";
                    //}
                    //else
                    //{
                    //    btnApprove.Enabled = false;
                    //}
                }
                else if (dtJob.Rows[0]["JobActive"].ToString() == "A" && _roleId != (int)UserRole.fuser)
                {
                    btnApprove.Enabled = true;
                    btnCloseJob.Enabled = false;
                    btnApprove.Text = "Approve Job";
                    Button5.Visible = false;
                    Button6.Visible = false;
                    Button1.Visible = false;
                    Button3.Visible = false;
                    if (lblTotalEstimatePayable.Text.ToDecimal() < lblTotalEstimateReceiveable.Text.ToDecimal() && dtJob.Rows[0]["UserConfirm"].ToInt() == 1 && string.IsNullOrEmpty(lblApprover.Text))
                    {
                        btnApprove.Enabled = true;
                        ViewState["NextStatus"] = "O";
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                    }
                }
                else if (dtJob.Rows[0]["JobActive"].ToString() == "C" && _roleId != (int)UserRole.fuser)
                {
                    btnApprove.Enabled = false;
                    btnCloseJob.Enabled = true;
                    btnCloseJob.Text = "Open Job";
                    Button1.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    Button4.Visible = false;
                    Button5.Visible = false;
                    Button6.Visible = false;
                }
                else if (dtJob.Rows[0]["JobActive"].ToString() == "C" && _roleId == (int)UserRole.fuser)
                {
                    btnApprove.Enabled = false;
                    btnCloseJob.Enabled = false;
                    Button1.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    Button4.Visible = false;
                    Button5.Visible = false;
                    Button6.Visible = false;
                }

                //lblArchievedGrossProfit.Text = (dsDashboard.Tables[8].Rows[0]["DrInvTotal"].ToDecimal() - dsDashboard.Tables[8].Rows[0]["CNAmt"].ToDecimal() - dsDashboard.Tables[7].Rows[0]["CreInvTotal"].ToDecimal()).ToString();
                //if (dtJob.Rows[0]["JobActive"].ToString() == "P" && _roleId != 23)
                //{
                //    if (lblTotalEstimatePayable.Text.ToDecimal() < lblTotalEstimateReceiveable.Text.ToDecimal())
                //    {
                //        btnApprove.Enabled = true;
                //        btnCloseJob.Enabled = true;
                //    }
                //    else
                //    {
                //        btnApprove.Enabled = false;
                //        btnCloseJob.Enabled = false;
                //    }
                //}
            }

            //Estimate Payable
            DataTable dtEstPayable = dsDashboard.Tables[1];
            gvEstimatePayable.DataSource = dtEstPayable;
            gvEstimatePayable.DataBind();

            //Estimate Receivable
            DataTable dtEstReceivable = dsDashboard.Tables[2];
            gvEstimateReceivable.DataSource = dtEstReceivable;
            gvEstimateReceivable.DataBind();

            //Creditors Invoice
            DataTable dtCredInv = dsDashboard.Tables[3];
            gvCreditors.DataSource = dtCredInv;
            gvCreditors.DataBind();

            //Debitors Invoice
            DataTable dtDebtInv = dsDashboard.Tables[4];
            gvDebtors.DataSource = dtDebtInv;
            gvDebtors.DataBind();
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

                if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.fadmin && user.UserRole.Id != (int)UserRole.fmanager)
                {

                    if (_canView == false)
                    {
                        Response.Redirect("~/Unauthorized.aspx");
                    }

                    if (_canAdd == false)
                    {
                        btnAddInvoiceCred.Enabled = false;
                        btnAddInvoiceDebt.Enabled = false;
                        btnAddPayable.Enabled = false;
                        btnAddRecovery.Enabled = false;
                        btnAdvanceReceipt.Enabled = false;
                        btnAdvPayment.Enabled = false;
                        btnApprove.Enabled = false;
                        btnCloseJob.Enabled = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();

            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    _roleId = user.UserRole.Id;
                    UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
                }
            }
        }

        protected void gvEstimatePayable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                //e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UnitName"));

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstimateNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstimateDate")).Split(' ')[0];
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BillFrom"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PartyName"));

                e.Row.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "INRAmount")), 2));
                //e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmount"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "StaxAmount"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));

                string EstimateId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_EstimateID"));

                //ImageButton btnDashboard = (ImageButton)e.Row.FindControl("btnBillReceipt");
                //btnDashboard.ToolTip = "Bill Reciept";
                //btnDashboard.CommandArgument = EstimateId;

                ImageButton btnUpload = (ImageButton)e.Row.FindControl("btnUpload");
                btnUpload.ToolTip = "Download";
                btnUpload.CommandArgument = EstimateId;
                //btnUpload.Attributes.Add("onclick", "javascript:popWin('" + EstimateId + "');");

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = "Edit";
                btnEdit.CommandArgument = EstimateId;

                //Delete link
                if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = "Delete";
                    btnRemove.CommandArgument = EstimateId;
                    //btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }

                if (ViewState["JobActive"].ToString() == "O")
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }
                    
            }
        }

        protected void gvEstimatePayable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string estimateId = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/Forwarding/Transaction/AddEditEstimate.aspx?jobid=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                + "&IsPayment=" + GeneralFunctions.EncryptQueryString("1")
                + "&DlName=" + GeneralFunctions.EncryptQueryString(lblCustomer.Text)
                + "&DlValues=" + GeneralFunctions.EncryptQueryString(hdnCustID.Value)
                + "&JobNo=" + GeneralFunctions.EncryptQueryString(lblJobNumber.Text)
                + "&EstimateId=" + GeneralFunctions.EncryptQueryString(estimateId)
                + "&ShippingMode=" + GeneralFunctions.EncryptQueryString(lblShipping.Text)
                );
                //RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                JobBLL.DeleteDashBoardData(Convert.ToInt32(e.CommandArgument), "Estimate");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert4", "<script>javascript:void alert('Record deleted successfully!');</script>", false);
            }
            else if (e.CommandName == "BillReceipt")
            {
            }
            else if (e.CommandName == "Upload")
            {

                var path = Server.MapPath("~/Forwarding/QuoUpload/" + "Quotation" + Convert.ToString(e.CommandArgument) + ".pdf");
                var filename = "Quotation" + Convert.ToString(e.CommandArgument) + ".pdf";
                    //"Quotation" + Convert.ToString(e.CommandArgument)+".pdf";

                var ext = System.IO.Path.GetExtension(filename);
                string filePath = string.Format(path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);

                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.Buffer = true;
                    Response.ContentType = MimeType(ext);
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", filename, ""));
                    Response.WriteFile(filePath);
                    Response.End();
                }
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:popWin('" + e.CommandArgument.ToString() + "');</script>", false);
            }
        }

        protected void gvEstimateReceivable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                //e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UnitName"));

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstimateNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstimateDate")).Split(' ')[0];
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BillFrom"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PartyName"));

                e.Row.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "INRAmount")), 2));
                //e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmount"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "StaxAmount"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));

                string EstimateId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_EstimateID"));

                //ImageButton btnDashboard = (ImageButton)e.Row.FindControl("btnBillReceipt");
                //btnDashboard.ToolTip = "Bill Reciept";
                //btnDashboard.CommandArgument = EstimateId;

                ImageButton btnUpload = (ImageButton)e.Row.FindControl("btnUpload");
                btnUpload.ToolTip = "Download";
                btnUpload.CommandArgument = EstimateId;
                //btnUpload.Attributes.Add("onclick", "javascript:popWin('" + EstimateId + "');");

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = "Edit";
                btnEdit.CommandArgument = EstimateId;

                //Delete link
                if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = "Delete";
                    btnRemove.CommandArgument = EstimateId;
                    //btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }

                if (ViewState["JobActive"].ToString() == "O")
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
            //    ScriptManager sManager = ScriptManager.GetCurrent(this);

            //    //e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UnitName"));
            //    e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstimateNo"));
            //    e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EstimateDate")).Split(' ')[0];
            //    e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BillFrom"));
            //    e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PartyName"));

            //    //e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CurrencyName"));
            //    //e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChgAmt"));
            //    //e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ROE"));
            //    //e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PaymentBy"));
            //    e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmount"));
            //    e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "StaxAmount"));
            //    e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            //    string EstimateId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_EstimateID"));

            //    //ImageButton btnDashboard = (ImageButton)e.Row.FindControl("btnGenInv");
            //    //btnDashboard.ToolTip = "Generate Invoice";
            //    //btnDashboard.CommandArgument = EstimateId;

            //    //ImageButton btnUpload = (ImageButton)e.Row.FindControl("btnUpload");
            //    //btnUpload.ToolTip = "Upload";
            //    //btnUpload.CommandArgument = EstimateId;
            //    //btnUpload.Attributes.Add("onclick", "javascript:popWin('" + EstimateId + "');");

            //    //Edit Link
            //    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
            //    btnEdit.ToolTip = "Edit";
            //    btnEdit.CommandArgument = EstimateId;

            //    //Delete link
            //    if (_canDelete == true)
            //    {
            //        ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
            //        btnRemove.Visible = true;
            //        btnRemove.ToolTip = "Delete";
            //        btnRemove.CommandArgument = EstimateId;
            //        //btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
            //    }
            //    else
            //    {
            //        ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
            //        btnRemove.Visible = false;
            //    }
            //}
        }
        protected void gvEstimateReceivable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            

            if (e.CommandName == "Edit")
            {
                string estimateId = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/Forwarding/Transaction/AddEditEstimate.aspx?jobid=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                + "&IsPayment=" + GeneralFunctions.EncryptQueryString("1")
                + "&DlName=" + GeneralFunctions.EncryptQueryString(lblCustomer.Text)
                + "&DlValues=" + GeneralFunctions.EncryptQueryString(hdnCustID.Value)
                + "&JobNo=" + GeneralFunctions.EncryptQueryString(lblJobNumber.Text)
                + "&EstimateId=" + GeneralFunctions.EncryptQueryString(estimateId)
                + "&ShippingMode=" + GeneralFunctions.EncryptQueryString(lblShipping.Text)
                );
                //RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                JobBLL.DeleteDashBoardData(Convert.ToInt32(e.CommandArgument), "Estimate");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert4", "<script>javascript:void alert('Record deleted successfully!');</script>", false);
            }
            else if (e.CommandName == "BillReceipt")
            {
            }
            else if (e.CommandName == "Upload")
            {

                var path = Server.MapPath("~/Forwarding/QuoUpload/" + "Quotation" + Convert.ToString(e.CommandArgument) + ".pdf");
                var filename = "Quotation" + Convert.ToString(e.CommandArgument) + ".pdf";
                //"Quotation" + Convert.ToString(e.CommandArgument)+".pdf";

                var ext = System.IO.Path.GetExtension(filename);
                string filePath = string.Format(path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);

                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.Buffer = true;
                    Response.ContentType = MimeType(ext);
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", filename, ""));
                    Response.WriteFile(filePath);
                    Response.End();
                }
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:popWin('" + e.CommandArgument.ToString() + "');</script>", false);
            }
        }


        protected void gvCreditors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PartyName"));
                //e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvType"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceNo"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceDate")).Split(' ')[0];
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLARefNo"));
                //e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLARefDate")).Split(' ')[0];
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmt"));

                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Stax"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Total"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PmtAmt"));

                // download link
                ImageButton btnUpload = (ImageButton)e.Row.FindControl("btnUpload");
                btnUpload.ToolTip = "Download";
                btnUpload.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CInvoiceID"));

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = "Edit";
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CInvoiceID"));

               
                //btnUpload.Attributes.Add("onclick", "javascript:popWin('" + EstimateId + "');");

                //Delete link
                if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = "Delete";
                    btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CInvoiceID"));
                    //btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }

                ImageButton btnPayment = (ImageButton)e.Row.FindControl("btnPayment");
                //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                //ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnPayment.ToolTip = "Payment";
                btnPayment.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CInvoiceID"));
                if (DataBinder.Eval(e.Row.DataItem, "Approved").ToInt() == 1)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    //btnDashboard.Visible = false;
                    //btnEdit.Visible = false;
                    btnPayment.Visible = true;
                    e.Row.ForeColor = System.Drawing.Color.Green;
                    btnEdit.Visible = false;
                    btnRemove.Visible = false;
                    //e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "Approved").ToInt() == 2)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    btnEdit.Visible = true;
                    btnRemove.Visible = true;
                    btnPayment.Visible = false;
                }
                else
                    btnPayment.Visible = false;

            }
        }
        protected void gvCreditors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Payment")
            {
                string CInvoiceId = Convert.ToString(e.CommandArgument);
                string docTypeId = DocumentTypeIdForCr;
                string jobNo = lblJobNumber.Text;
                Response.Redirect("~/Forwarding/Transaction/CreditorPayment.aspx?invid=" + GeneralFunctions.EncryptQueryString(CInvoiceId)
                    + "&JobID=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                    + "&paymenttype=" + GeneralFunctions.EncryptQueryString("C")
                  );
            }
            else if (e.CommandName == "Edit")
            {
                string CInvoiceId = Convert.ToString(e.CommandArgument);
                string docTypeId = DocumentTypeIdForCr;
                string jobNo = lblJobNumber.Text;

                Response.Redirect("~/Forwarding/Transaction/CreInvoice.aspx?docTypeId=" + GeneralFunctions.EncryptQueryString(docTypeId)
                        + "&JobID=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                        + "&CreInvoiceId=" + GeneralFunctions.EncryptQueryString(CInvoiceId)
                      );

                //RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                JobBLL.DeleteDashBoardData(Convert.ToInt32(e.CommandArgument), "Credit");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert2", "<script>javascript:void alert('Record deleted successfully!');</script>", false);
            }
            else if (e.CommandName == "Upload")
            {
                var path = Server.MapPath("~/Forwarding/QuoUpload/CreInvoice" + Convert.ToString(e.CommandArgument) + ".pdf");
                var filename = "CreInvoice" + Convert.ToString(e.CommandArgument) + ".pdf";
                //"Quotation" + Convert.ToString(e.CommandArgument)+".pdf";

                var ext = System.IO.Path.GetExtension(filename);
                string filePath = string.Format(path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);

                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.Buffer = true;
                    Response.ContentType = MimeType(ext);
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", filename, ""));
                    Response.WriteFile(filePath);
                    Response.End();
                }
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:popWin('" + e.CommandArgument.ToString() + "');</script>", false);
            }
        }


        protected void gvDebtors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                //e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceType"));
                //e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceDate")).Split(' ')[0];
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CurrencyCode"));
                e.Row.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount")) - Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STAX")), 2));
                e.Row.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STAX")), 2));
                //e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Amount"));
                //e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ReceivedAmt"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CNAmt"));
                e.Row.Cells[7].Text = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount")) - Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReceivedAmt")) - Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CNAmt")), 2));
                //e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "")); //balance amt missing

                //ImageButton btnPrint = (ImageButton)e.Row.FindControl("btnPrint");
                //btnPrint.ToolTip = "Print";
                //btnPrint.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceID"));

                //ImageButton btnAddMR = (ImageButton)e.Row.FindControl("btnAddMR");
                //btnAddMR.ToolTip = "Add MR";
                //btnAddMR.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceID"));

                //ImageButton btnAddCRN = (ImageButton)e.Row.FindControl("btnAddCRN");
                //btnAddCRN.ToolTip = "Add CRN";
                //btnAddCRN.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceID"));

                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = "Edit Invoice";
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceID"));

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
                    HiddenField hdnInvID = (HiddenField)e.Row.FindControl("hdnInvID");
                    System.Web.UI.HtmlControls.HtmlAnchor aPrint = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("aPrint");
                    //aPrint.Visible = false;
                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "fk_CurrencyID")) == 1)
                    {
                        aPrint.Attributes.Add("onclick", string.Format("return ReportPrint1('{0}','{1}','{2}','{3}');",
                        "reportName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("FwdInvoice"),
                            //"&LineBLNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(txtBlNo.Text),
                        "&Location=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("1"),
                        "&LoginUserName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(user.FirstName + " " + user.LastName),
                        "&InvoiceId=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(hdnInvID.Value)));
                    }
                    else
                    {
                        aPrint.Attributes.Add("onclick", string.Format("return ReportPrint1('{0}','{1}','{2}','{3}');",
                       "reportName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("FwdInvoiceUSD"),
                            //"&LineBLNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(txtBlNo.Text),
                       "&Location=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("1"),
                       "&LoginUserName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(user.FirstName + " " + user.LastName),
                       "&InvoiceId=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(hdnInvID.Value)));
                    }
                }

            }
        }

        protected void gvDebtors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "AddMR")
            {
                string CInvoiceId = Convert.ToString(e.CommandArgument);
                string docTypeId = DocumentTypeIdForCr;
                string jobNo = lblJobNumber.Text;

                Response.Redirect("~/Forwarding/Transaction/CreInvoice.aspx?docTypeId=" + GeneralFunctions.EncryptQueryString(docTypeId)
                        + "&JobID=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                        + "&CreInvoiceId=" + GeneralFunctions.EncryptQueryString(CInvoiceId)
                      );
            }
            else if (e.CommandName == "AddCRN")
            {

            }
            else if (e.CommandName == "Delete")
            {
                JobBLL.DeleteDashBoardData(Convert.ToInt32(e.CommandArgument), "Debtor");
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert2", "<script>javascript:void alert('Record deleted successfully!');</script>", false);


                //Response.Redirect("~/Forwarding/Transaction/FwdInvoice.aspx?invid=" + GeneralFunctions.EncryptQueryString(e.CommandArgument.ToString()));
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            
            //JobBLL.UpdateJobStatus(Convert.ToInt32(ViewState["JOBID"]), "O", _userId);
            JobBLL.UpdateJobStatus(Convert.ToInt32(ViewState["JOBID"]), ViewState["NextStatus"].ToString(), _userId);
            JobBLL.SendConfMail(Convert.ToInt32(ViewState["JOBID"]), ViewState["NextStatus"].ToString(), _userId, lblProjectedGrossProfit.Text.ToDecimal());
            
            //SendConfirmationMail(_userId, lblJobNumber.Text);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert100", "<script>javascript:void alert('Approved successfully!');</script>", false);
            LoadDashboard();
        }

        protected void btnCloseJob_Click(object sender, EventArgs e)
        {
            JobBLL.UpdateJobStatus(Convert.ToInt32(ViewState["JOBID"]), "C", _userId);
            if (ViewState["JobActive"].ToString() == "O")
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert101", "<script>javascript:void alert('Job Closed successfully!');</script>", false);
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert101", "<script>javascript:void alert('Job Opened successfully!');</script>", false);
            LoadDashboard();
        }

        protected void btnAdvPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/Advance.aspx?jobid=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
            + "&paymenttype=" + GeneralFunctions.EncryptQueryString("C")
        );
        }

        protected void btnAddPayable_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/AddEditEstimate.aspx?jobid=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                + "&IsPayment=" + GeneralFunctions.EncryptQueryString("1")
            + "&DlName=" + GeneralFunctions.EncryptQueryString(lblCustomer.Text)
                 + "&DlValues=" + GeneralFunctions.EncryptQueryString(hdnCustID.Value)
                 + "&JobNo=" +GeneralFunctions.EncryptQueryString(lblJobNumber.Text)
                );
        }

        protected void btnAdvanceReceipt_Click(object sender, EventArgs e)
        {
             Response.Redirect("~/Forwarding/Transaction/Advance.aspx?jobid=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
            + "&paymenttype=" + GeneralFunctions.EncryptQueryString("D")
            );
        }

        protected void btnAddRecovery_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/AddEditEstimate.aspx?jobid=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                  + "&IsPayment=" + GeneralFunctions.EncryptQueryString("0")
              + "&DlName=" + GeneralFunctions.EncryptQueryString(lblCustomer.Text)
                   + "&DlValues=" + GeneralFunctions.EncryptQueryString(hdnCustID.Value)
                   + "&JobNo=" + GeneralFunctions.EncryptQueryString(lblJobNumber.Text)
                  );
        }

        protected void btnAddInvoiceCred_Click(object sender, EventArgs e)
        {
            string docTypeId = DocumentTypeIdForCr;
            string jobNo = lblJobNumber.Text;
            string estimateId = "0";

            Response.Redirect("~/Forwarding/Transaction/CreInvoice.aspx?docTypeId=" + GeneralFunctions.EncryptQueryString(docTypeId)
                    + "&JobID=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())
                    + "&EstimateID=" + GeneralFunctions.EncryptQueryString(estimateId)
                  );
        }

        protected void btnAddInvoiceDebt_Click(object sender, EventArgs e)
        {
            string docTypeId = DocumentTypeIdForDr;
            string jobNo = lblJobNumber.Text;
            string estimateId = "0";
            string containers = lblTTL20.Text + " x 20' / " + lblTTL40.Text + " x 40'";


            //Response.Redirect("~/Forwarding/Transaction/FwdInvoice.aspx?docTypeId=" + GeneralFunctions.EncryptQueryString(docTypeId)
            //        + "&jobNo=" + GeneralFunctions.EncryptQueryString(jobNo)
            //        + "&estimateId=" + GeneralFunctions.EncryptQueryString(estimateId)
            //        + "&containers=" + GeneralFunctions.EncryptQueryString(containers)
            //      );
            Response.Redirect("~/Forwarding/Transaction/FwdInvoice.aspx?jobNo=" + GeneralFunctions.EncryptQueryString(jobNo)
                           + "&JobID=" + GeneralFunctions.EncryptQueryString(Convert.ToInt32(ViewState["JOBID"]).ToString())

      );
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/JobList.aspx");
        }

        private static string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;

            string ext = Extension.ToLower();
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }

        protected void ShowReceivedAmt(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor a = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            headerTest.InnerText = "Received Amount Details";

            GridViewRow Row = (GridViewRow)a.NamingContainer;
            HiddenField hdnInvID = (HiddenField)Row.FindControl("hdnInvID");

            DataTable dtDoc = oImportBLL.GetReceivedAmtBreakup(Convert.ToInt64(hdnInvID.Value));

            StringBuilder sbr = new StringBuilder();
            sbr.Append("<table style='width: 100%; border: none;' cellpadding='0' cellspacing='0'>");
            sbr.Append("<tr style='background-color:#328DC4;color:White; font-weight:bold;'>");
            sbr.Append("<td style='width: 70px;padding-left:2px;'>MRNo.</td>");
            sbr.Append("<td style='width: 80px;'>Date</td>");
            sbr.Append("<td style='width: 80px;text-align:right;'>Cash</td>");
            sbr.Append("<td style='width: 80px;text-align:right;'>Cheque</td>");
            sbr.Append("<td style='width: 80px;text-align:right;'>TDS</td>");
            sbr.Append("<td style='width: 50px;text-align:center;'>Print</td>");
            sbr.Append("</tr>");

            for (int rowCount = 0; rowCount < dtDoc.Rows.Count; rowCount++)
            {
                string MRID = dtDoc.Rows[rowCount]["MRID"].ToString();
                string CASH = dtDoc.Rows[rowCount]["CASH"].ToString();
                string CHEQUE = dtDoc.Rows[rowCount]["CHEQUE"].ToString();
                string DATE = Convert.ToDateTime(dtDoc.Rows[rowCount]["DATE"].ToString()).ToString("dd/MM/yyyy");
                string MRNO = dtDoc.Rows[rowCount]["MRNO"].ToString();
                string TDS = dtDoc.Rows[rowCount]["TDS"].ToString();


                if (rowCount % 2 == 0) //For ODD row
                {
                    sbr.Append("<tr>");
                    sbr.Append("<td>" + MRNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CASH + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CHEQUE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + TDS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' href='../../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
                else // For Even Row
                {
                    sbr.Append("<tr style='background-color:#99CCFF;'>");
                    sbr.Append("<td>" + MRNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CASH + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CHEQUE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + TDS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' href='../../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
            }

            sbr.Append("</table>");

            dvMoneyReceived.InnerHtml = sbr.ToString();

            mpeMoneyReceivedDetail.Show();

        }

        protected void ShowCreditNoteAmt(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor a = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            headerTest.InnerText = "Credit Note Amount Details";

            GridViewRow Row = (GridViewRow)a.NamingContainer;
            HiddenField hdnInvID = (HiddenField)Row.FindControl("hdnInvID");

            DataTable dtDoc = oImportBLL.GetCNAmtBreakup(Convert.ToInt64(hdnInvID.Value));

            StringBuilder sbr = new StringBuilder();
            sbr.Append("<table style='width: 100%; border: none;' cellpadding='0' cellspacing='0'>");
            sbr.Append("<tr style='background-color:#328DC4;color:White; font-weight:bold;'>");
            sbr.Append("<td style='width: 170px;padding-left:2px;'>CRNNo.</td>");
            sbr.Append("<td style='width: 60px;'>Date</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>Gross</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>STax</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>Cess</td>");
            sbr.Append("<td style='width: 70px;text-align:right;'>ACess</td>");
            sbr.Append("<td style='width: 50px;text-align:center;'>Print</td>");
            sbr.Append("</tr>");

            for (int rowCount = 0; rowCount < dtDoc.Rows.Count; rowCount++)
            {
                string CRNID = dtDoc.Rows[rowCount]["CRNID"].ToString();
                string GROSS = dtDoc.Rows[rowCount]["GROSS"].ToString();
                string STAX = dtDoc.Rows[rowCount]["STAX"].ToString();
                string DATE = Convert.ToDateTime(dtDoc.Rows[rowCount]["DATE"].ToString()).ToString("dd/MM/yyyy");
                string CRNNO = dtDoc.Rows[rowCount]["CRNNO"].ToString();
                string CESS = dtDoc.Rows[rowCount]["CESS"].ToString();
                string ACESS = dtDoc.Rows[rowCount]["ACESS"].ToString();

                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                string ss = string.Format("ReportPrint2('{0}','{1}','{2}','{3}','{4}','{5}');",
                "reportName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("CreditNoteExport"),
                "&LineBLNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(lblJobNumber.Text),
                "&Location=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(lblOps.Text),
                "&LoginUserName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(user.FirstName + " " + user.LastName),
                "&InvoiceId=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(hdnInvID.Value),
                "&CreditNoteNo=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(CRNID));

                if (rowCount % 2 == 0) //For ODD row
                {
                    sbr.Append("<tr>");
                    sbr.Append("<td>" + CRNNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + GROSS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + STAX + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CESS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + ACESS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    //sbr.Append("<td><a target='_blank' href='../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/Print.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' onclick=" + ss + "><img src='../../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
                else // For Even Row
                {
                    sbr.Append("<tr style='background-color:#99CCFF;'>");
                    sbr.Append("<td>" + CRNNO + "</td>");
                    sbr.Append("<td>" + DATE + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + GROSS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + STAX + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + CESS + "</td>");
                    sbr.Append("<td style='text-align:right;'>" + ACESS + "</td>");
                    //sbr.Append("<td><a href='AddEditMoneyReceipts.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/edit.png' /></a></td>");
                    //sbr.Append("<td><a target='_blank' href='../Reports/MoneyRcpt.aspx?mrid=" + GeneralFunctions.EncryptQueryString(MRID) + "'><img src='../Images/Print.png' /></a></td>");
                    sbr.Append("<td><a target='_blank' onclick=" + ss + "><img src='../../Images/Print.png' /></a></td>");

                    sbr.Append("</tr>");
                }
            }

            sbr.Append("</table>");

            dvMoneyReceived.InnerHtml = sbr.ToString();

            mpeMoneyReceivedDetail.Show();

        }

        //private void SendConfirmationMail(int uId, string JobNo)
        //{
        //    IUser user = new UserBLL().GetUser(uId);

        //    if (!ReferenceEquals(user, null))
        //    {
        //        if (!string.IsNullOrEmpty(user.ManagerEmailID))
        //        {
        //            string url = Convert.ToString(ConfigurationManager.AppSettings["ApplicationUrl"]);
        //            string msgBody = "Hello " + user.UserFullName + "<br/>Your Approval is Required on Proforma Job No " + JobNo + ". <br/>Please click on following link to Login Application<br/><a href='" + url + "'>" + url + "</a>";

        //            try
        //            {
        //                CommonBLL.SendMail(user.EmailId, user.UserFullName, user.ManagerEmailID, string.Empty, "Job Confirmation " + JobNo, msgBody, Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserAccount"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserPwd"]));
        //                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00071") + "');</script>", false);
        //            }
        //            catch (Exception ex)
        //            {
        //                CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
        //            }
        //        }
        //    }
        //}
    }
}