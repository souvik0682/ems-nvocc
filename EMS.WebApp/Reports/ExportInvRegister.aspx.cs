using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Entity.Report;
using EMS.Utilities;
using EMS.Utilities.ReportManager;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Reports
{
    public partial class ExportInvRegister : System.Web.UI.Page
    {
        #region Private Member Variables

        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            //CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string message = string.Empty;

                if (ValidateData(out message))
                {
                    GenerateReport();
                }
                else
                {
                    GeneralFunctions.RegisterAlertScript(this, message);
                }
            }
            catch (Exception ex)
            {
                GeneralFunctions.RegisterErrorAlertScript(this, ex.Message);
            }
        }

        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string message = string.Empty;

            //    if (ValidateData(out message))
            //    {
            //        PrintInvoice();
            //    }
            //    else
            //    {
            //        GeneralFunctions.RegisterAlertScript(this, message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    GeneralFunctions.RegisterErrorAlertScript(this, ex.Message);
            //}
        }
        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                cbeFromDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                cbeToDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
            _LocationSpecific = UserBLL.GetUserLocationSpecific();
            _userLocation = UserBLL.GetUserLocation();
        }

        private void CheckUserAccess()
        {
            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void PopulateControls()
        {
            PopulateLocation();
            if (_LocationSpecific)
            {
                ddlLoc.SelectedValue = Convert.ToString(_userLocation);
                ddlLoc.Enabled = false;
            }
            PopulateLine();
            PopulateBillType();
        }

        private void PopulateLocation()
        {
            List<ILocation> lstLoc = new CommonBLL().GetActiveLocation();

            ddlLoc.DataValueField = "Id";
            ddlLoc.DataTextField = "Name";
            ddlLoc.DataSource = lstLoc;
            ddlLoc.DataBind();
            ddlLoc.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void PopulateLine()
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = dbinteract.GetNVOCCLine(-1, string.Empty);
            ddlLine.DataValueField = "pk_NVOCCID";
            ddlLine.DataTextField = "NVOCCName";
            ddlLine.DataSource = ds;
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void PopulateBillType()
        {
            List<InvoiceTypeEntity> lstLoc = new MoneyReceiptsBLL().GetExpInvoiceTypes();

            ddlType.DataValueField = "InvoiceTypeId";
            ddlType.DataTextField = "InvoiceTypeName";
            ddlType.DataSource = lstLoc;
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private bool ValidateData(out string message)
        {
            bool isValid = true;
            int slNo = 1;
            message = GeneralFunctions.FormatAlertMessage("Please correct the following errors:");

            if (string.IsNullOrEmpty(txtFromDt.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please enter from date");
                slNo++;
            }

            if (string.IsNullOrEmpty(txtToDt.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please enter to date");
                slNo++;
            }

            if (!isValid)
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }

            return isValid;
        }


        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();
            DateTime dtFrom = Convert.ToDateTime(txtFromDt.Text, _culture);
            DateTime dtTo = Convert.ToDateTime(txtToDt.Text, _culture);
            List<ImpInvRegisterEntity> lstReg = ReportBLL.GetExportInvoiceRegister(Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlLoc.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), dtFrom, dtTo);

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ExportInvRegister", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "ExportInvRegister.rdlc";

            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("RptDataSet", lstReg));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Line", ddlLine.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("BillType", ddlType.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate", txtFromDt.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ToDate", txtToDt.Text));
            rptViewer.LocalReport.Refresh();
        }

        private void GenerateNavigationLink()
        {
            if (string.IsNullOrEmpty(txtFromDt.Text))
                txtFromDt.Text = "1900-01-01";

            if (string.IsNullOrEmpty(txtToDt.Text))
                txtToDt.Text = "1900-01-01";

            //if (!string.IsNullOrEmpty(txtFromDt.Text) && !string.IsNullOrEmpty(txtToDt.Text))
            //{
            IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            string username = user.Name;

            string ss = string.Format("ReportPrint2('{0}')",
              "reportName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString("MIDExp") +
              "&line=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(ddlLine.SelectedValue) +
              "&Location=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(ddlLoc.SelectedValue) +
              "&InvoiceTypeID=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(ddlType.SelectedValue) +
              "&ToDate=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(Convert.ToDateTime(txtToDt.Text).ToString("yyyy-MM-dd")) +
              "&FromDate=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(Convert.ToDateTime(txtFromDt.Text).ToString("yyyy-MM-dd")) +
              "&LoginUserName=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(username));



            btnInvoice.Attributes.Add("onclick", ss);
            //}

        }

        #endregion
        # region Record Selection
        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateNavigationLink();
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateNavigationLink();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateNavigationLink();
        }

        protected void txtFromDt_TextChanged(object sender, EventArgs e)
        {
            GenerateNavigationLink();
        }

        protected void txtToDt_TextChanged(object sender, EventArgs e)
        {
            GenerateNavigationLink();
        }
        # endregion
    }
}