using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.BLL;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlTypes;
using EMS.Common;
using EMS.Entity;

namespace EMS.WebApp.Transaction
{
    public partial class ManageSettlement : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;
        private int _CompanyId = 1;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            _userId = user == null ? 0 : user.Id;
            //_userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();
            _userLocation = UserBLL.GetUserLocation();

            if (!Page.IsPostBack)
            {
                autoComplete1.ContextKey = "0|0";
               
                //chkFreightToCollect.Enabled = true;
                //DisableAllServiceControls();
                RetriveParameters();
            }
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

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void RetriveParameters()
        {
            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                hdnSettlementID.Value = GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString());
                txtSettlementAmount.Enabled = false;
                txtBlNo.Enabled = false;
                DataSet BLDataSet = new DataSet();
                SettlementBLL oSettlementBLL = new SettlementBLL();
                BLDataSet = oSettlementBLL.GetSettlementWithSettlment(hdnSettlementID.Value.ToInt());
                hdnBLId.Value = BLDataSet.Tables[0].Rows[0]["BLID"].ToString();
                txtSettlementNo.Text = BLDataSet.Tables[0].Rows[0]["SettlementNo"].ToString();
                txtPayToRcvdFrom.Text = BLDataSet.Tables[0].Rows[0]["PayRcv"].ToString();
                txtChequeDetail.Text = BLDataSet.Tables[0].Rows[0]["ChequeDetail"].ToString();
                txtBankName.Text = BLDataSet.Tables[0].Rows[0]["BankName"].ToString();
                fillBLDetail(BLDataSet.Tables[0]);
                BLDataSet = oSettlementBLL.GetSettlementWithBL(hdnBLId.Value.ToInt());
                LoadBLStatus(BLDataSet.Tables[1]);
                txtOutstanding.Text = Convert.ToString(Math.Abs(BLDataSet.Tables[2].Rows[0]["TotCr"].ToDecimal() - BLDataSet.Tables[2].Rows[0]["TotDr"].ToDecimal()) + txtSettlementAmount.Text.ToDecimal());
                //PopulateAllData();
            }
            else
            {
                hdnSettlementID.Value = "0";
                txtSettlementAmount.Enabled = true;
            }
        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
            if (txtBlNo.Text != string.Empty)
            {
                gvwInvoice.Visible = true;
                PopulateAllData();
                UpdatePanel2.Update();
            }
            else
            {
                ClearAll();
            }
        }

        private void PopulateAllData()
        {
            ClearAll();
            DataSet BLDataSet = new DataSet();
            SettlementBLL oSettlementBLL = new SettlementBLL();
            BLDataSet = oSettlementBLL.GetSettlementWithBL(hdnBLId.Value.ToInt());
            txtBlNo.Text = string.Empty;

            if (BLDataSet.Tables[0].Rows.Count > 0)
            {
                fillBLDetail(BLDataSet.Tables[0]);
                LoadBLStatus(BLDataSet.Tables[1]);
                if (hdnSettlementID.Value == "0")
                    fillTotal(BLDataSet.Tables[2]);
            }
        }

        void ClearAll()
        {
            txtBlDate.Text = string.Empty;
            txtBlNo.Text = string.Empty;
            txtLine.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtOutstanding.Text = string.Empty;
            txtSettlementAmount.Text = string.Empty;
            txtSettlementDate.Text = string.Empty;
            txtTransactionType.Text = string.Empty;

            DataSet BLDataSet = new DataSet();
            SettlementBLL oSettlementBLL = new SettlementBLL();
            BLDataSet = oSettlementBLL.GetSettlementWithBL(0);
            gvwInvoice.DataSource = BLDataSet.Tables[0];
            gvwInvoice.DataBind();
        }

        void fillBLDetail(DataTable dtDetail)
        {
            //hdnBLId.Value = dtDetail.Rows[0]["BLID"].ToString();
            txtBlNo.Text = dtDetail.Rows[0]["BLNO"].ToString();
            txtBlDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["BLDate"]).ToString("dd/MM/yyyy");
          
            txtLocation.Text = dtDetail.Rows[0]["LocName"].ToString();
            txtLine.Text = dtDetail.Rows[0]["Line"].ToString();
            //txtPayToRcvdFrom.Text = dtDetail.Rows[0]["PayRcv"].ToString();
            //txtChequeDetail.Text = dtDetail.Rows[0]["ChequeDetail"].ToString();
            //txtBankName.Text = dtDetail.Rows[0]["BankName"].ToString();

            if (hdnSettlementID.Value == "0")
                txtSettlementDate.Text = DateTime.Now.ToShortDateString();
            else
            {
                txtSettlementDate.Text = Convert.ToDateTime(dtDetail.Rows[0]["SettlementDate"]).ToString("dd/MM/yyyy");
                txtSettlementAmount.Text = dtDetail.Rows[0]["SettlementAmount"].ToString();
                if (dtDetail.Rows[0]["PorR"].ToString() == "R")
                {
                    txtTransactionType.Text = "Rcvble";
                    lblPaymentRcpt.Text = "Rcvble Amount";
                    lblPayToRcvdFrom.Text = "Received From";
                }
                else
                {
                    txtTransactionType.Text = "Payable";
                    lblPaymentRcpt.Text = "Payable Amount";
                    lblPayToRcvdFrom.Text = "Paid To";
                }
            }
        }

        private void LoadBLStatus(DataTable dtDetail)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    gvwInvoice.PageIndex = searchCriteria.PageIndex;

                    gvwInvoice.DataSource = dtDetail;
                    gvwInvoice.DataBind();
                }
            }
        }

        protected void fillTotal(DataTable dtTot)
        {
            txtOutstanding.Text = Convert.ToString(Math.Abs(dtTot.Rows[0]["TotCr"].ToDecimal() - dtTot.Rows[0]["TotDr"].ToDecimal()));
            txtSettlementAmount.Text = Convert.ToString(Math.Abs(dtTot.Rows[0]["TotCr"].ToDecimal() - dtTot.Rows[0]["TotDr"].ToDecimal()));
            if (dtTot.Rows[0]["TotCr"].ToDecimal() - dtTot.Rows[0]["TotDr"].ToDecimal() > 0)
            {
                txtTransactionType.Text = "Rcvble";
                lblPaymentRcpt.Text = "Rcvble Amount";
            }
            else
            {
                txtTransactionType.Text = "Payable";
                lblPaymentRcpt.Text = "Payable Amount";
            }
        }

        protected void gvwInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 5);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocType"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocNo"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocDate")).Split(' ')[0];
                //e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DocDate"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CrAmount"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DrAmount"));
                //e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PorR"));
                //e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SettlementAmount"));

                //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");

                //btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_SettlementID"));

            }
        }

   
        private void BuildSettlementEntity(ISettlement Settlement)
        {
            Settlement.CreatedBy = _userId;
            Settlement.CreatedOn = DateTime.Now;
            Settlement.CompanyID = _CompanyId;
            Settlement.BLID = hdnBLId.Value.ToInt();
            Settlement.SettlementDate = txtSettlementDate.Text.ToDateTime();
            Settlement.SettlementNo = txtSettlementNo.Text;
            Settlement.PorR = (txtTransactionType.Text) == "Payable" ? "P" : "R";
            Settlement.SettlementAmount = txtSettlementAmount.Text.ToDecimal();
            Settlement.OutstandingAmount = txtOutstanding.Text.ToDecimal();
            Settlement.BankName = txtBankName.Text;
            Settlement.ChequeDetail = txtChequeDetail.Text;
            Settlement.PayRcvd = txtPayToRcvdFrom.Text;
            Settlement.pk_SettlementID = hdnSettlementID.Value.ToInt();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //string misc = string.Empty;
                ISettlement Settlement = new SettlementEntity();
                BuildSettlementEntity(Settlement);
                //if (voyage.POL == 0)
                //{
                //    GeneralFunctions.RegisterAlertScript(this, "Please provide Port of Loading");
                //    return;
                //}

                //if (voyage.NextPortID == 0)
                //{
                //    GeneralFunctions.RegisterAlertScript(this, "Please provide Next Port Call");
                //    return;
                //}

                //if (voyage.ETA > voyage.ETD)
                //{
                //    GeneralFunctions.RegisterAlertScript(this, "ETA should be less than Equals ETD");
                //    return;
                //}

                //if (voyage.ETD > voyage.ETANextPort)
                //{
                //    GeneralFunctions.RegisterAlertScript(this, "ETD should be less than ETA next port");
                //    return;
                //}

                //if (voyage.VesselCutOffDate.ToString() != "" && voyage.ETD < voyage.VesselCutOffDate)
                //{
                //    GeneralFunctions.RegisterAlertScript(this, "ETD should be greater than Vessel Cut off Date");
                //    return;
                //}

                //bool isedit = false;
                //long qrystrvoyageid = long.Parse(GeneralFunctions.DecryptQueryString(Request.QueryString["VoyageID"].ToString()) != "" ?
                //    GeneralFunctions.DecryptQueryString(Request.QueryString["VoyageID"].ToString()) : "0");
                ////Add-Update
                ////false for insert and true for update
                //if (qrystrvoyageid.Equals(-1)) isedit = false;
                //else isedit = true;
                long Settlementid = new SettlementBLL().SaveSettlement(Settlement);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully!');</script>", false);
                if (!ReferenceEquals(Request.QueryString["id"], null))
                    Response.Redirect("~/Transaction/Settlement1.aspx");
                else
                    Response.Redirect("~/Transaction/Settlement1.aspx");
                    //ClearAll();
                
                //if (isedit == true)
                //    Response.Redirect("~/Export/Voyage.aspx");
                //else
                //    clearPage();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record could not be saved due to ! " + ex.Message.ToString() + "');</script>", false);
            }
        }

        private void clearPage()
        {

            txtLine.Text = string.Empty;
            txtLine.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtOutstanding.Text = string.Empty;
            txtSettlementAmount.Text = string.Empty;
            txtSettlementDate.Text = string.Empty;
            txtSettlementNo.Text = string.Empty;
            txtTransactionType.Text = string.Empty;
            txtBlNo.Text = string.Empty;
            txtBlDate.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtChequeDetail.Text = string.Empty;
            txtPayToRcvdFrom.Text= string.Empty;
            gvwInvoice.DataSource = null;
            gvwInvoice.DataBind();
            gvwInvoice.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/Settlement1.aspx");
        }
     
    }
}