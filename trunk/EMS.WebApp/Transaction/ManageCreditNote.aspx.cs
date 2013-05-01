using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Utilities;
using EMS.BLL;
using System.Configuration;
using EMS.Utilities.ResourceManager;
using EMS.Entity;
using EMS.Common;
using System.Globalization;

namespace EMS.WebApp.Transaction
{
    public partial class ManageCreditNote : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();

            if (!Page.IsPostBack)
            {
                if (!ReferenceEquals(Request.QueryString["CrnId"], null))
                {
                    long CrnId = 0;
                    Int64.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["CrnId"].ToString()), out CrnId);

                    if (CrnId > 0)
                        LoadForView(CrnId);

                    btnSave.Enabled = false;
                    btnAdd.Enabled = false;
                }

                if (!ReferenceEquals(Request.QueryString["InvoiceId"], null))
                {
                    int LineId = 0;
                    int LocationId = 0;
                    int InvoiceId = 0;

                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["InvoiceId"].ToString()), out InvoiceId);
                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["LocationId"].ToString()), out LocationId);
                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["LineId"].ToString()), out LineId);

                    LoadCreditNote(LineId, LocationId, InvoiceId);
                    LoadChargesDDL();

                    ViewState["INVOICE_ID"] = InvoiceId;
                    ViewState["LOCATION_ID"] = LocationId;
                    ViewState["NVOCC_ID"] = LineId;

                    btnSave.Enabled = true;
                    btnAdd.Enabled = true;
                }

                //List<ICreditNoteCharge> cnCharges = new List<ICreditNoteCharge>();

                //gvwCreditNote.DataSource = cnCharges;
                //gvwCreditNote.DataBind();
            }
        }

        protected void ddlChargeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ICreditNoteCharge cnCharge = new CreditNoteBLL().GetChargeDetails(Convert.ToInt32(ddlFChargeName.SelectedValue), txtInvoiceRef.Text);

            if (cnCharge != null)
            {
                txtChargeInvoice.Text = cnCharge.GrossCRNAmount.ToString();
                txtChargeServiceTax.Text = cnCharge.ServiceTaxAmount.ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            decimal chargeAmount = Convert.ToDecimal(txtChargeInvoice.Text);
            decimal cnAmount = Convert.ToDecimal(txtCNAmount.Text);

            if (cnAmount > 0)
            {
                if (chargeAmount > cnAmount)
                {
                    AddCreditNoteCharge();
                }
                else
                {
                    lblMessage.Text = "Credit Note Amount should be less than Charged in Invoice Amount";
                }
            }
            else
            {
                lblMessage.Text = "Credit Note Amount should be greater than zero";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ICreditNote creditNote = new CreditNoteEntity();
            BuildCreditNoteEntity(creditNote);

            List<ICreditNoteCharge> cnCharges = ViewState["CN_CHARGE"] as List<ICreditNoteCharge>;
            creditNote.CreditNoteCharges = cnCharges;

            long creditNoteId = new CreditNoteBLL().SaveCreditNote(creditNote);

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Record saved successfully!');</script>", false);

            Response.Redirect("~/Transaction/BL-Query.aspx?BlNo=" + GeneralFunctions.EncryptQueryString(txtBLRef.Text));
        }

        protected void gvwCreditNote_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                RemoveCreditNoteCharge(Convert.ToInt64(e.CommandArgument));
            }
            else if (e.CommandName == "EditRow")
            {
                EditCreditNoteCharge(Convert.ToInt64(e.CommandArgument));
            }

        }

        protected void gvwCreditNote_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeName"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeAmount"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ChargeServiceTax"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GrossCRNAmount"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TotalServiceTax"));

                if (!ReferenceEquals(Request.QueryString["CrnId"], null))
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");

                    btnRemove.Visible = false;
                    btnEdit.Visible = false;
                }
                else
                {
                    //Delete link
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    //btnRemove.Visible = true;
                    btnRemove.ToolTip = "Remove";
                    btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CRNChargeID"));


                    //Edit link
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                    //btnEdit.Visible = true;
                    btnEdit.ToolTip = "Edit";
                    btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CRNChargeID"));

                    btnRemove.OnClientClick = "javascript:return confirm('Are you sure about delete?');";
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/BL-Query.aspx?BlNo=" + GeneralFunctions.EncryptQueryString(txtBLRef.Text));
        }

        protected void txtCNAmount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtCNAmount.Text) > 0)
                CalculateServiceTax();
        }


        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void LoadCreditNote(int LineId, int LocationId, int InvoiceId)
        {
            ICreditNote creditNote = new CreditNoteBLL().GetHeaderInformation(LineId, LocationId, InvoiceId);

            if (creditNote != null)
            {
                txtLocation.Text = creditNote.LocationName;
                txtLine.Text = creditNote.NVOCCName;
                txtInvoiceType.Text = creditNote.InvoiceTypeName;
                txtInvoiceRef.Text = creditNote.InvoiceNumber;
                txtContainers.Text = creditNote.Containers;
                txtInvoiceDate.Text = creditNote.InvoiceDate.ToShortDateString();
                txtBLRef.Text = creditNote.BLNumber;
                ViewState["INVOICETYPE_ID"] = creditNote.InvoiceTypeID;
            }
        }

        private void LoadChargesDDL()
        {
            DataTable dt = new CreditNoteBLL().GetAllCharges(txtInvoiceRef.Text);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["fk_ChargeID"] = "0";
                dr["ChargeDescr"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                ddlFChargeName.DataValueField = "fk_ChargeID";
                ddlFChargeName.DataTextField = "ChargeDescr";
                ddlFChargeName.DataSource = dt;
                ddlFChargeName.DataBind();
            }
        }

        private void CalculateServiceTax()
        {
            decimal TaxPer = 0;
            decimal TaxCess = 0;
            decimal TaxAddCess = 0;
            decimal serviceTax = 0;
            decimal cessAmount = 0;
            decimal addCess = 0;

            decimal grossAmount = Convert.ToDecimal(txtCNAmount.Text);

            DataTable dtSTax = new InvoiceBLL().GetServiceTax(Convert.ToDateTime(txtInvoiceDate.Text));

            if (dtSTax != null && dtSTax.Rows.Count > 0)
            {
                TaxPer = Convert.ToDecimal(dtSTax.Rows[0]["TaxPer"].ToString());
                TaxCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxCess"].ToString());
                TaxAddCess = Convert.ToDecimal(dtSTax.Rows[0]["TaxAddCess"].ToString());
            }

            if (Convert.ToDecimal(txtChargeServiceTax.Text) > 0)
            {
                serviceTax = Math.Round((grossAmount * TaxPer) / 100, 0);
                cessAmount = Math.Round((serviceTax * TaxCess) / 100, 0);
                addCess = Math.Round((serviceTax * TaxAddCess) / 100, 0);
            }

            txtCNServiceTax.Text = (serviceTax + cessAmount + addCess).ToString();

            ViewState["CESSAMOUNT"] = cessAmount;
            ViewState["ADDCESS"] = addCess;
            ViewState["STAX"] = serviceTax;
        }

        private void AddCreditNoteCharge()
        {
            List<ICreditNoteCharge> cnCharges = null;

            ICreditNoteCharge cnCharge = new CreditNoteChargeEntity();
            BuildCreditNoteCharge(cnCharge);

            if (ViewState["CN_CHARGE"] != null)
                cnCharges = ViewState["CN_CHARGE"] as List<ICreditNoteCharge>;
            else
                cnCharges = new List<ICreditNoteCharge>();

            cnCharges.Add(cnCharge);

            gvwCreditNote.DataSource = cnCharges;
            gvwCreditNote.DataBind();

            ViewState["CN_CHARGE"] = cnCharges;
            ClearCreditNoteCharge();
        }

        private void BuildCreditNoteCharge(ICreditNoteCharge cnCharge)
        {
            if (ViewState["EDIT_CN_CHARGEID"] == null)
            {
                if (ViewState["CN_CHARGEID"] == null)
                    cnCharge.CRNChargeID = -1;
                else
                    cnCharge.CRNChargeID = Convert.ToInt32(ViewState["CN_CHARGEID"]) - 1;

                ViewState["CN_CHARGEID"] = cnCharge.CRNChargeID;
            }
            else
            {
                List<ICreditNoteCharge> cnCharges = null;

                if (ViewState["CN_CHARGE"] != null)
                    cnCharges = ViewState["CN_CHARGE"] as List<ICreditNoteCharge>;

                cnCharge.CRNChargeID = Convert.ToInt64(ViewState["EDIT_CN_CHARGEID"]);

                ICreditNoteCharge cnChargeTemp = cnCharges.Single(c => c.CRNChargeID == Convert.ToInt64(ViewState["EDIT_CN_CHARGEID"]));
                cnCharges.Remove(cnChargeTemp);

                ViewState["CN_CHARGE"] = cnCharges;
                ViewState["EDIT_CN_CHARGEID"] = null;
            }

            cnCharge.ChargeId = Convert.ToInt64(ddlFChargeName.SelectedValue);
            cnCharge.ChargeName = ddlFChargeName.SelectedItem.Text;
            cnCharge.ChargeAmount = Convert.ToDecimal(txtChargeInvoice.Text);
            cnCharge.ChargeServiceTax = Convert.ToDecimal(txtChargeServiceTax.Text);
            //cnCharge.CRNChargeID = Convert.ToInt64(ddlFChargeName.SelectedValue);
            cnCharge.GrossCRNAmount = Convert.ToDecimal(txtCNAmount.Text);
            cnCharge.TotalServiceTax = Convert.ToDecimal(txtCNServiceTax.Text);
            cnCharge.ServiceTaxACess = Convert.ToDecimal(ViewState["ADDCESS"]);
            cnCharge.ServiceTaxAmount = Convert.ToDecimal(ViewState["STAX"]);
            cnCharge.ServiceTaxCessAmount = Convert.ToDecimal(ViewState["CESSAMOUNT"]);

            cnCharge.CRNAmount = (cnCharge.GrossCRNAmount + cnCharge.TotalServiceTax);
        }

        private void ClearCreditNoteCharge()
        {
            ddlFChargeName.SelectedValue = "0";
            txtCNAmount.Text = "0.00";
            txtChargeInvoice.Text = "0.00";
            txtChargeServiceTax.Text = "0.00";
            txtCNServiceTax.Text = "0.00";
        }

        private void EditCreditNoteCharge(long cnChargeId)
        {
            List<ICreditNoteCharge> cnCharges = null;

            if (ViewState["CN_CHARGE"] != null)
                cnCharges = ViewState["CN_CHARGE"] as List<ICreditNoteCharge>;

            ICreditNoteCharge cnCharge = cnCharges.Single(c => c.CRNChargeID == cnChargeId);

            ddlFChargeName.SelectedValue = cnCharge.ChargeId.ToString();
            txtChargeInvoice.Text = Convert.ToString(cnCharge.ChargeAmount);
            txtChargeServiceTax.Text = Convert.ToString(cnCharge.ChargeServiceTax);
            txtCNAmount.Text = Convert.ToString(cnCharge.GrossCRNAmount);
            txtCNServiceTax.Text = Convert.ToString(cnCharge.TotalServiceTax);

            ViewState["ADDCESS"] = cnCharge.ServiceTaxACess;
            ViewState["STAX"] = cnCharge.ServiceTaxAmount;
            ViewState["CESSAMOUNT"] = cnCharge.ServiceTaxCessAmount;

            ViewState["EDIT_CN_CHARGEID"] = cnCharge.CRNChargeID;
        }

        private void RemoveCreditNoteCharge(long cnChargeId)
        {
            List<ICreditNoteCharge> cnCharges = null;

            if (ViewState["CN_CHARGE"] != null)
                cnCharges = ViewState["CN_CHARGE"] as List<ICreditNoteCharge>;

            ICreditNoteCharge cnCharge = cnCharges.Single(c => c.CRNChargeID == cnChargeId);
            cnCharges.Remove(cnCharge);

            ViewState["CN_CHARGE"] = cnCharges;
            RefreshGridView();
        }

        private void RefreshGridView()
        {
            List<ICreditNoteCharge> cnCharges = null;

            if (ViewState["CN_CHARGE"] != null)
                cnCharges = ViewState["CN_CHARGE"] as List<ICreditNoteCharge>;

            gvwCreditNote.DataSource = cnCharges;
            gvwCreditNote.DataBind();
        }

        private void BuildCreditNoteEntity(ICreditNote creditNote)
        {
            creditNote.CrnDate = Convert.ToDateTime(txtCNDate.Text);
            creditNote.InvoiceID = Convert.ToInt64(ViewState["INVOICE_ID"]);
            creditNote.InvoiceTypeID = Convert.ToInt32(ViewState["INVOICETYPE_ID"]);
            creditNote.LocationID = Convert.ToInt32(ViewState["LOCATION_ID"]);
            creditNote.NVOCCID = Convert.ToInt32(ViewState["NVOCC_ID"]);
            creditNote.UserAdded = _userId;
        }

        private void LoadForView(long CreditNoteId)
        {
            ICreditNote creditNote = new CreditNoteBLL().GetCreditNoteForView(CreditNoteId);
            List<ICreditNoteCharge> cnCharges = creditNote.CreditNoteCharges;

            if (creditNote != null)
            {
                txtLocation.Text = creditNote.LocationName;
                txtLine.Text = creditNote.NVOCCName;
                txtInvoiceType.Text = creditNote.InvoiceTypeName;
                txtInvoiceRef.Text = creditNote.InvoiceNumber;
                txtContainers.Text = creditNote.Containers;
                txtInvoiceDate.Text = creditNote.InvoiceDate.ToShortDateString();
                txtBLRef.Text = creditNote.BLNumber;
                txtCreditNoteNo.Text = creditNote.CrnNo;
                txtCNDate.Text = creditNote.CrnDate.ToShortDateString();
            }

            LoadChargesDDL();
            ViewState["CN_CHARGE"] = cnCharges;

            gvwCreditNote.DataSource = cnCharges;
            gvwCreditNote.DataBind();
        }
    }
}