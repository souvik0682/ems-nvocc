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
using System.Data;

namespace EMS.WebApp.Forwarding.Transaction
{
    public partial class AddEditAdvanceAdjustment : System.Web.UI.Page
    {
        #region Private Member Variables


        private int _userId = 0;
        private string countryId = "";
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;

        private int AdvAdjInvID { get { if (ViewState["Id"] != null) { return Convert.ToInt32(ViewState["Id"]); } return 0; } set { ViewState["Id"] = value; } }
        private string Mode { get { if (ViewState["Id"] != null) { return "E"; } return "A"; } }

        bool IsEmptyGrid { get; set; }
        #endregion


        #region Protected Event Handler

        protected void grvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var dll = (DropDownList)e.Row.FindControl("ddlInvoiceOrAdvNo");
                if (ViewState["ddlInvoiceOrAdvNo"] == null)
                {
                    var inv = new AdvAdjustmentBLL().GetInvoiceFromJob(ddlJobNo.SelectedValue.IntRequired(), 'c');
                    ViewState["ddlInvoiceOrAdvNo"] = inv;
                }
                DataSet ds = (DataSet)ViewState["ddlInvoiceOrAdvNo"];
                dll.DataSource = ds;
                dll.DataTextField = "InvoiceNo";
                dll.DataValueField = "pk_InvoiceID";
                dll.DataBind();
                dll.Items.Insert(0, new ListItem("--Select--", "0"));
                List<InvoiceJobAdjustment> lstInvoiceJobAdjustment = (List<InvoiceJobAdjustment>)ViewState["InvoiceJobAdjustment"];
                if (lstInvoiceJobAdjustment != null && lstInvoiceJobAdjustment.Count > 0)
                {

                    foreach (var v in lstInvoiceJobAdjustment)
                    {
                        try
                        {
                            dll.Items.Remove(dll.Items.FindByValue(v.InvoiceOrAdvNo));
                        }
                        catch { }
                    }

                }

            }
            else if (e.Row.RowType == DataControlRowType.DataRow && IsEmptyGrid)
            {
                e.Row.Visible = false;
                IsEmptyGrid = false;

            }
        }

        protected void grvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            var invoiceJobAdjustment = new InvoiceJobAdjustment();
            List<InvoiceJobAdjustment> lstInvoiceJobAdjustment = null;
            if (ViewState["InvoiceJobAdjustment"] != null)
            {
                lstInvoiceJobAdjustment = (List<InvoiceJobAdjustment>)ViewState["InvoiceJobAdjustment"];
            }
            else
            {
                lstInvoiceJobAdjustment = new List<InvoiceJobAdjustment>();
            }

            if (e.CommandName == "Add")
            {
                if (grvInvoice.FooterRow != null)
                {
                    invoiceJobAdjustment.InvoiceOrAdvDate = Convert.ToDateTime(((TextBox)grvInvoice.FooterRow.FindControl("txtInvoiceOrAdvDate")).Text);
                    var drp = ((DropDownList)grvInvoice.FooterRow.FindControl("ddlInvoiceOrAdvNo"));
                    if (drp != null)
                    {
                        invoiceJobAdjustment.InvoiceOrAdvNo = drp.SelectedValue;
                    }
                    invoiceJobAdjustment.DrAmount = Convert.ToDouble(((TextBox)grvInvoice.FooterRow.FindControl("txtDrAmount")).Text);
                    invoiceJobAdjustment.CrAmount = Convert.ToDouble(((TextBox)grvInvoice.FooterRow.FindControl("txtCrAmount")).Text);
                    try
                    {
                        invoiceJobAdjustment.InvoiceJobAdjustmentPk = lstInvoiceJobAdjustment.Max(x => x.InvoiceJobAdjustmentPk) + 1;
                    }
                    catch { invoiceJobAdjustment.InvoiceJobAdjustmentPk = 1; }
                    try
                    {

                        DataSet ds = (DataSet)ViewState["ddlInvoiceOrAdvNo"];
                        DataRow row = ds.Tables[0].AsEnumerable().Where(x => Convert.ToString(x["InvoiceNo"]).Equals(invoiceJobAdjustment.InvoiceOrAdvNo)).FirstOrDefault();
                        if (row != null)
                        {
                            invoiceJobAdjustment.InvOrAdv = Convert.ToString(row["Type"]);
                        }
                    }
                    catch { }
                    lstInvoiceJobAdjustment.Add(invoiceJobAdjustment);

                }
            }
            else if (e.CommandName == "Remove")
            {
                lstInvoiceJobAdjustment.Remove(lstInvoiceJobAdjustment.FindAll(f => f.InvoiceJobAdjustmentPk.ToString().Equals(e.CommandArgument)).FirstOrDefault());
            }

            if (lstInvoiceJobAdjustment.Count == 0)
            {
                SetEmptyGrid();
            }
            else
            {
                ViewState["InvoiceJobAdjustment"] = lstInvoiceJobAdjustment;
                grvInvoice.DataSource = lstInvoiceJobAdjustment;
                grvInvoice.DataBind();
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {

            RetriveParameters();
            if (!IsPostBack)
            {
                LoadDefault();
                if (Request.QueryString["AdvAdjId"] != string.Empty)
                {

                    try
                    {
                        var id = GeneralFunctions.DecryptQueryString(Request.QueryString["AdvAdjId"]);
                        var pid = Convert.ToInt32(id);
                        if (pid > 0)
                        {
                            AdvAdjInvID = pid;
                            LoadData(pid);
                        }
                    }
                    catch { }
                }
            }

            //   CheckUserAccess(countryId);
        }

        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            //Get user permission.
            //  EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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

        protected void ddlJobNo_SelectIndexChange(object sender, EventArgs e)
        {
            ddlDrCrName.Enabled = false;
            rdoDbCr.Enabled = true;
            foreach (ListItem itms in rdoDbCr.Items)
            {
                itms.Selected = false;
            }
            ddlDrCrName.DataSource = null;
            ddlDrCrName.Items.Clear();
            ddlDrCrName.Items.Insert(0, new ListItem("--Select--", "0"));
            var temp = new List<InvoiceJobAdjustment>();
            grvInvoice.DataSource = temp;
            grvInvoice.DataBind();
            ViewState["InvoiceJobAdjustment"] = temp;
            //grvInvoice.ShowFooter = false;

            DataSet ds = (DataSet)ViewState["Jobs"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].AsEnumerable().Where(x => Convert.ToString(x["pk_JobID"]).Equals(ddlJobNo.SelectedValue)).FirstOrDefault();
                if (row != null)
                {

                    try
                    {
                        lblHBLNo.Text = Convert.ToString(row["fwdBLNo"]);
                    }
                    catch { lblHBLNo.Text = ""; }

                    try
                    {
                        lblJobDate.Text = Convert.ToString(row["JobDate"]);
                        lblJobDate.Text = Convert.ToDateTime(lblJobDate.Text).ToShortDateString();
                    }
                    catch { lblJobDate.Text = ""; }
                }
            }
        }

        protected void ddlInvoiceOrAdvNo_SelectIndexChange(object sender, EventArgs e)
        {
            if (grvInvoice.FooterRow != null)
            {
                var ddl = ((DropDownList)grvInvoice.FooterRow.FindControl("ddlInvoiceOrAdvNo"));
                if (ddl != null)
                {
                    if (ddl.SelectedIndex != 0)
                    {
                        var txt = ((TextBox)grvInvoice.FooterRow.FindControl("txtInvoiceOrAdvDate"));
                        var txtDr = ((TextBox)grvInvoice.FooterRow.FindControl("txtDrAmount"));
                        var txtCr = ((TextBox)grvInvoice.FooterRow.FindControl("txtCrAmount"));

                        if (txt != null) { txt.Text = ""; }
                        if (txtDr != null) { txtDr.Text = "0"; }
                        if (txtCr != null) { txtCr.Text = "0"; }

                        if (ViewState["ddlInvoiceOrAdvNo"] != null)
                        {

                            DataSet ds = (DataSet)ViewState["ddlInvoiceOrAdvNo"];
                            DataRow row = ds.Tables[0].AsEnumerable().Where(x => Convert.ToString(x["InvoiceNo"]).Equals(ddl.SelectedValue)).FirstOrDefault();
                            if (row != null)
                            {

                                try
                                {
                                    var type = Convert.ToString(row["Type"]);
                                    if (!string.IsNullOrEmpty(type) && type == "I")
                                    {
                                        txtDr.Enabled = false;
                                        txtCr.Enabled = true;
                                        var rfvtxtDrAmount = ((RequiredFieldValidator)grvInvoice.FooterRow.FindControl("rfvtxtDrAmount"));
                                        if (rfvtxtDrAmount != null)
                                        {
                                            rfvtxtDrAmount.Enabled = false;
                                            rfvtxtDrAmount.ValidationGroup = "NoRequired";
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(type) && type == "A")
                                    {
                                        txtCr.Enabled = false;
                                        txtDr.Enabled = true;
                                        var rfvtxtCrAmount = ((RequiredFieldValidator)grvInvoice.FooterRow.FindControl("rfvtxtCrAmount"));
                                        if (rfvtxtCrAmount != null)
                                        {
                                            rfvtxtCrAmount.Enabled = false;
                                            rfvtxtCrAmount.ValidationGroup = "NoRequired";
                                        }
                                    }
                                    try
                                    {
                                        txt.Text = Convert.ToDateTime(Convert.ToString(row["InvoiceDate"])).ToShortDateString();
                                        txt.Enabled = false;
                                    }
                                    catch { txt.Text = ""; txt.Enabled = true; }
                                }
                                catch { }
                            }

                        }

                    }
                    else
                    {
                        var txt = ((TextBox)grvInvoice.FooterRow.FindControl("txtInvoiceOrAdvDate"));
                        var txtDr = ((TextBox)grvInvoice.FooterRow.FindControl("txtDrAmount"));
                        var txtCr = ((TextBox)grvInvoice.FooterRow.FindControl("txtCrAmount"));
                        if (txt != null) { txt.Text = ""; }
                        if (txtDr != null) { txtDr.Text = "0"; }
                        if (txtCr != null) { txtCr.Text = "0"; }

                    }
                }
            }
        }

        protected void ddlDrCrName_SelectIndexChange(object sender, EventArgs e)
        {
            SetEmptyGrid();
        }

        protected void rdoDbCr_SelectIndexChange(object sender, EventArgs e)
        {
            ddlDrCrName.Enabled = true;
            //fill the Bebitor and creditor Data
            ddlDrCrName.DataSource = new AdvAdjustmentBLL().GetDetailFromJob(ddlJobNo.SelectedValue.IntRequired(), rdoDbCr.SelectedValue[0]);
            ddlDrCrName.DataTextField = "CustName";
            ddlDrCrName.DataValueField = "pk_CustID";
            ddlDrCrName.DataBind();
            ddlDrCrName.Items.Insert(0, new ListItem("--Select--", "0"));
            var temp = new List<InvoiceJobAdjustment>();
            grvInvoice.DataSource = temp;
            grvInvoice.DataBind();
            ViewState["InvoiceJobAdjustment"] = temp;
            //grvInvoice.ShowFooter = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveAdjustmentModel();

        }

        #endregion

        #region Private Methods

        private bool IsValid(List<InvoiceJobAdjustment> invoiceJobAdjustment)
        {
            if (invoiceJobAdjustment != null)
            {
                var sumOfDr = invoiceJobAdjustment.Sum(x => x.DrAmount);
                var sumOfCr = invoiceJobAdjustment.Sum(x => x.CrAmount);
                return sumOfDr == sumOfCr;

            }
            return false;
        }
        
        private void LoadDefault()
        {
            var jobs = new AdvAdjustmentBLL().GetDetailFromJob();
            ddlJobNo.DataSource = jobs;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "pk_JobID";
            ddlJobNo.DataBind();
            ddlJobNo.Items.Insert(0, new ListItem("--Select--", "0"));
            ViewState["Jobs"] = jobs;
            var temp = new List<InvoiceJobAdjustment>();
            grvInvoice.DataSource = temp;
            grvInvoice.DataBind();
            ViewState["InvoiceJobAdjustment"] = temp;
            //grvInvoice.ShowFooter = false;
            ddlDrCrName.Enabled = false;
            txtAdjustmentDate.Text = DateTime.Now.ToShortDateString();
            lblAdjustmentId.Text = "Auto Generated Id";
            lblHBLNo.Text = "";
            lblJobDate.Text = "";
            rdoDbCr.Enabled = false;
        }

        private void LoadData(int id)
        {
            var src = new AdvAdjustmentBLL().GetAdjustmentModel(new SearchCriteria { StringParams = new List<string>() { id.ToString(), "", "", "", "" } });
            if (src != null && src.Count() > 0)
            {
                var model = src.FirstOrDefault();
                ddlJobNo.SelectedValue = model.JobNo;
                ddlJobNo_SelectIndexChange(null, null);
                txtAdjustmentDate.Text = model.AdjustmentDate.ToShortDateString();
                rdoDbCr.SelectedValue = model.DOrC;
                rdoDbCr_SelectIndexChange(null, null);
                ddlDrCrName.SelectedValue = model.DebtorOrCreditorName;
                lblAdjustmentId.Text = model.AdjustmentNo.ToString();
                txtAdjustmentDate.Enabled = false;
                rdoDbCr.Enabled = false;
                ddlJobNo.Enabled = false;
                ddlDrCrName.Enabled = false;

                var inv = new AdvAdjustmentBLL().GetInvoiceFromJob(ddlJobNo.SelectedValue.IntRequired(), model.DOrC[0]);
                ViewState["ddlInvoiceOrAdvNo"] = inv;

                if (model.LstInvoiceJobAdjustment != null && model.LstInvoiceJobAdjustment.Count > 0) {
                    if (ViewState["ddlInvoiceOrAdvNo"] != null)
                    {

                        DataSet ds = (DataSet)ViewState["ddlInvoiceOrAdvNo"];
                        foreach (var v in model.LstInvoiceJobAdjustment)
                        {
                            DataRow row = ds.Tables[0].AsEnumerable().Where(x => Convert.ToString(x["InvoiceNo"]).Equals(v.InvoiceOrAdvNo)).FirstOrDefault();
                            if (row != null)
                            {
                                v.InvoiceOrAdvDate = Convert.ToDateTime(Convert.ToString(row["InvoiceDate"]));
                            }
                        }
                    }
                }
                ViewState["InvoiceJobAdjustment"] = model.LstInvoiceJobAdjustment;
                grvInvoice.DataSource = model.LstInvoiceJobAdjustment;
                grvInvoice.DataBind();
            }
        }



        private AdjustmentModel ExtractData()
        {
            //var party = 

            return new AdjustmentModel
            {
                JobNo = ddlJobNo.SelectedValue,//
                AdjustmentDate = Convert.ToDateTime(txtAdjustmentDate.Text),//
                DOrC = rdoDbCr.SelectedValue,//
                DebtorOrCreditorName = ddlDrCrName.SelectedValue,
                LstInvoiceJobAdjustment = (List<InvoiceJobAdjustment>)ViewState["InvoiceJobAdjustment"],
                CompanyID = 1,
                UserID = _userId,
                AdjustmentPk = AdvAdjInvID
            };

        }
        
        private void SaveAdjustmentModel()
        {
            var data = ExtractData();
            if (IsValid(data.LstInvoiceJobAdjustment))
            {

                var result = new AdvAdjustmentBLL().SaveAdjustmentModel(data, Mode);
                if (result > 0)
                {
                  //  AdvAdjInvID = result;
                    Response.Redirect("~/Forwarding/Transaction/ManageAdvanceAdjustment.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), "alert('Error Occured');", true);
                }
            }
            else
            {
               ScriptManager.RegisterStartupScript(this,this.GetType(),DateTime.Now.Ticks.ToString(), "alert('Avance and Invoice are not properly adjusted.');",true);
            }
        }

        private void SetEmptyGrid()
        {
            grvInvoice.ShowFooter = true;
            IsEmptyGrid = true;
            List<InvoiceJobAdjustment> dr = new List<InvoiceJobAdjustment>() { new InvoiceJobAdjustment { } };
            grvInvoice.DataSource = dr;
            grvInvoice.DataBind();
            ViewState["InvoiceJobAdjustment"] = new List<InvoiceJobAdjustment>();
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/ManageAdvanceAdjustment.aspx");
        }
    }
}