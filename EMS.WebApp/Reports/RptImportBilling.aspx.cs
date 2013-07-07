using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Utilities.ReportManager;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using EMS.Common;
using EMS.Utilities;

namespace EMS.WebApp.Reports
{
    public partial class RptImportBilling : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;

        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        private int _userLocation = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            //CheckUserAccess();
            //IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            //_userId = user == null ? 0 : user.Id;
            if (!IsPostBack)
            {
                autoComplete1.ContextKey = "0|0";
                FillDropDown();
              
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
        protected void LocationLine_Changed(object sender, EventArgs e)
        {
            autoComplete1.ContextKey = ddlLocation.SelectedValue + "|" + ddlLine.SelectedValue;
            if (ddlLocation.SelectedIndex > 0)
                ddlLine.Enabled = true;
            else
            {
                ddlLine.Enabled = false;
                ddlLine.SelectedIndex = 0;
            }

            if (ddlLine.SelectedIndex > 0)
                txtBlNo.Enabled = true;
            else
            {
                txtBlNo.Enabled = false;
                txtBlNo.Text = string.Empty;
               
            }
        }

        private void GenerateReport()
        {
            lblMsg.Text = "";
            ReportBLL cls = new ReportBLL();

            LocalReportManager reportManager = new LocalReportManager(rptViewer, "ImpBilling", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            //string rptName = "RptimpBill.rdlc";
            //string rptName = "RptImpBillAnnex.rdlc";
            string rptName = string.Empty;

            if (rbRpt.SelectedValue == "1")
            {
                rptName = "RptimpBill.rdlc";
                string BlRefNo = ((TextBox)autoComplete1.FindControl("txtBlNo")).Text;
                DataSet ds = EMS.BLL.BLLReport.GetImpBill(hdnBLId.Value, txtdtBill.Text);
                decimal gamt1 = 0;
                decimal.TryParse(Convert.ToString(ds.Tables[0].Compute("Sum(col1)", "")), out gamt1);
                decimal MRamt = 0;
                decimal.TryParse(Convert.ToString(ds.Tables[0].Compute("Sum(col2)", "")), out MRamt);
                decimal famt = gamt1 - MRamt;
                string finalAmt = string.Empty;
                if (famt > 0)
                    finalAmt = "Amount to Collect   Rs.   " + famt;
                else if (famt < 0)
                    finalAmt = "Amount to Refund   Rs.   " + famt * (-1);
                else finalAmt = "Amount to Refund      NIL";
                famt = Math.Abs(famt);
                string numToWords = EMS.BLL.BLLReport.GetNumToWords(Convert.ToInt64(famt));

                try
                {
                    string compname = Convert.ToString(ConfigurationManager.AppSettings["CompanyName"]);
                    rptViewer.Reset();
                    rptViewer.LocalReport.Dispose();
                    rptViewer.LocalReport.DataSources.Clear();
                    rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                    rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", compname));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("Address", EMS.BLL.BLLReport.GetAddByCompName(compname)));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("tillDate", txtdtBill.Text));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("BL_Ref", txtBlNo.Text));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("finalAmt", finalAmt));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("NumToWords", numToWords));
                    rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                    rptViewer.LocalReport.Refresh();

                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;

                }

                lblMsg.Text = rptViewer.LocalReport.ReportPath + "dgfdfgdf";
            }

            else if (rbRpt.SelectedValue == "2")
            {
                rptName = "RptImpBillAnnex.rdlc";
                string BlRefNo = ((TextBox)autoComplete1.FindControl("txtBlNo")).Text;
                DataSet ds = EMS.BLL.BLLReport.GetImpBillAnne(hdnBLId.Value, txtdtBill.Text);
                try
                {
                    string compname = Convert.ToString(ConfigurationManager.AppSettings["CompanyName"]);
                    rptViewer.Reset();
                    rptViewer.LocalReport.Dispose();
                    rptViewer.LocalReport.DataSources.Clear();
                    rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                    rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", compname));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("Address", EMS.BLL.BLLReport.GetAddByCompName(compname)));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("tillDate", txtdtBill.Text));
                    rptViewer.LocalReport.SetParameters(new ReportParameter("BL_Ref", txtBlNo.Text));
                    rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                    rptViewer.LocalReport.Refresh();

                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;

                }
            }
            else
                return;

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {

        }

        void FillDropDown()
        {
            ListItem Li = null;
          

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Location, ddlLocation, 0, 0);
            ddlLocation.Items.Insert(0, Li);

            Li = new ListItem("Select", "0");
            PopulateDropDown((int)Enums.DropDownPopulationFor.Line, ddlLine, 0, 0);
            ddlLine.Items.Insert(0, Li);

            if (_LocationSpecific)
            {
                ddlLocation.SelectedValue = Convert.ToString(_userLocation);
                ddlLocation.Enabled = false;
                ddlLine.Enabled = true;
            }
        }

        void PopulateDropDown(int Number, DropDownList ddl, int? Filter1, int? Filter2)
        {
            CommonBLL.PopulateDropdown(Number, ddl, Filter1, Filter2);
        }

       
    }
}