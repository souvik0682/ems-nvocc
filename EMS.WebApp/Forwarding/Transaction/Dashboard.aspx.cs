using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Utilities;
using System.Data;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Farwarding.Transaction
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
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
                if (dtJob.Rows[0]["JobDate"] != DBNull.Value)
                    lblJobDate.Text = Convert.ToString(dtJob.Rows[0]["JobDate"]);
                if (dtJob.Rows[0]["JobNo"] != DBNull.Value)
                    lblJobNumber.Text = Convert.ToString(dtJob.Rows[0]["JobNo"]);
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
                if (dtJob.Rows[0]["ttl20"] != DBNull.Value)
                    lblTTL20.Text = Convert.ToString(dtJob.Rows[0]["ttl20"]);
                if (dtJob.Rows[0]["ttl40"] != DBNull.Value)
                    lblTTL40.Text = Convert.ToString(dtJob.Rows[0]["ttl40"]);
                if (dtJob.Rows[0]["grwt"] != DBNull.Value)
                    lblWeight.Text = Convert.ToString(dtJob.Rows[0]["grwt"]);
                if (dtJob.Rows[0]["RevTon"] != DBNull.Value)
                    lblRevenue.Text = Convert.ToString(dtJob.Rows[0]["RevTon"]);
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
                if (dtJob.Rows[0]["TotPaid"] != DBNull.Value)
                    lblTotalPaid.Text = Convert.ToString(dtJob.Rows[0]["TotPaid"]);
                if (dtJob.Rows[0]["TotReceived"] != DBNull.Value)
                    lblTotalReceived.Text = Convert.ToString(dtJob.Rows[0]["TotReceived"]);
                if (dtJob.Rows[0]["EstReceivable"] != DBNull.Value)
                    lblTotalEstimateReceiveable.Text = Convert.ToString(dtJob.Rows[0]["EstReceivable"]);
                if (dtJob.Rows[0]["EstREceivable"] != DBNull.Value && dtJob.Rows[0]["EstPayable"] != DBNull.Value)
                    lblProjectedGrossProfit.Text = (Convert.ToDecimal(dtJob.Rows[0]["EstREceivable"]) - Convert.ToDecimal(dtJob.Rows[0]["EstPayable"])).ToString();
                if (dtJob.Rows[0]["TotReceived"] != DBNull.Value && dtJob.Rows[0]["TotPaid"] != DBNull.Value)
                    lblArchievedGrossProfit.Text = (Convert.ToDecimal(dtJob.Rows[0]["TotReceived"]) - Convert.ToDecimal(dtJob.Rows[0]["TotPaid"])).ToString();
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

                if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager)
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

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UnitName"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Qty"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BillFrom"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CurrencyName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChgAmt"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ROE"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PaymentBy"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmount"));

                string EstimateId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_EstimateID"));

                ImageButton btnDashboard = (ImageButton)e.Row.FindControl("btnBillReceipt");
                btnDashboard.ToolTip = "Bill Reciept";
                btnDashboard.CommandArgument = EstimateId;

                ImageButton btnUpload = (ImageButton)e.Row.FindControl("btnUpload");
                btnUpload.ToolTip = "Upload";
                btnUpload.CommandArgument = EstimateId;
                btnUpload.Attributes.Add("onclick", "javascript:popWin('" + EstimateId + "');");

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
            }
        }
        protected void gvEstimatePayable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
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
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:popWin('" + e.CommandArgument.ToString() + "');</script>", false);
            }
        }

        protected void gvEstimateReceivable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UnitName"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Qty"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BillFrom"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CurrencyName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChgAmt"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ROE"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PaymentBy"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmount"));

                string EstimateId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmount"));

                ImageButton btnDashboard = (ImageButton)e.Row.FindControl("btnGenInv");
                btnDashboard.ToolTip = "Generate Invoice";
                btnDashboard.CommandArgument = EstimateId;

                ImageButton btnUpload = (ImageButton)e.Row.FindControl("btnUpload");
                btnUpload.ToolTip = "Upload";
                btnUpload.CommandArgument = EstimateId;
                btnUpload.Attributes.Add("onclick", "javascript:popWin('" + EstimateId + "');");

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
            }
        }
        protected void gvEstimateReceivable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                JobBLL.DeleteDashBoardData(Convert.ToInt32(e.CommandArgument), "Estimate");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert3", "<script>javascript:void alert('Record deleted successfully!');</script>", false);
            }
            else if (e.CommandName == "GenInv")
            {
            }
            else if (e.CommandName == "Upload")
            {
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
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Payable"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLARefNo"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLARefDate"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChgAmt"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ROE"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "INRAmt"));

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnPayment");
                btnEdit.ToolTip = "Payment";
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CInvoiceID"));

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
            }
        }
        protected void gvCreditors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Payment")
            {
                //RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                JobBLL.DeleteDashBoardData(Convert.ToInt32(e.CommandArgument), "Credit");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert2", "<script>javascript:void alert('Record deleted successfully!');</script>", false);
            }
        }


        protected void gvDebtors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceType"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceNo"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceDate"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Amount"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ReceivedAmt"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CNAmt"));
                //e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "")); //balance amt missing

                ImageButton btnPrint = (ImageButton)e.Row.FindControl("btnPrint");
                btnPrint.ToolTip = "Print";
                btnPrint.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceID"));

                ImageButton btnAddMR = (ImageButton)e.Row.FindControl("btnAddMR");
                btnAddMR.ToolTip = "Add MR";
                btnAddMR.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceID"));

                ImageButton btnAddCRN = (ImageButton)e.Row.FindControl("btnAddCRN");
                btnAddCRN.ToolTip = "Add CRN";
                btnAddCRN.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InvoiceID"));
            }
        }
        protected void gvDebtors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "AddMR")
            {

            }
            else if (e.CommandName == "AddCRN")
            {

            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            JobBLL.UpdateJobStatus(Convert.ToInt32(ViewState["JOBID"]), "O", _userId);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert100", "<script>javascript:void alert('Approved successfully!');</script>", false);
            LoadDashboard();
        }

        protected void btnCloseJob_Click(object sender, EventArgs e)
        {
            JobBLL.UpdateJobStatus(Convert.ToInt32(ViewState["JOBID"]), "C", _userId);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert101", "<script>javascript:void alert('Closed successfully!');</script>", false);
            LoadDashboard();
        }

        protected void btnAdvPayment_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddPayable_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdvanceReceipt_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddRecovery_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddInvoiceCred_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddInvoiceDebt_Click(object sender, EventArgs e)
        {

        }
    }
}