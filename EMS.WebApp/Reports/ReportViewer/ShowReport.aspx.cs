using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using EMS.Utilities;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;

namespace EMS.WebApp.Reports.ReportViewer
{
    [Serializable]
    public class ReportCredentials : IReportServerCredentials
    {
        // Fields
        private string _domain;
        private string _password;
        private string _userName;

        // Methods
        public ReportCredentials(string userName, string password, string domain) {
            _userName = userName;
            _password = password;
            _domain = domain;
        }

        public bool GetFormsCredentials(out Cookie authCoki, out string userName, out string password, out string authority)
        {
            userName = this._userName;
            password = this._password;
            authority = this._domain;
            authCoki = new Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain");
            return true;
        }


        // Properties
        public WindowsIdentity ImpersonationUser { get; set; }
        public ICredentials NetworkCredentials { get; set; }
    }


    public partial class ShowReport : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            strReportName = Request.QueryString["reportName"];
            ViewState["strReportName"] = strReportName;
            switch (strReportName.ToLower())
            {
                case "cargoarrivalnotice":
                    ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged1;                   
                    break;
                default:
                    ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged;
                    trCar.Visible = false;
                    trCar1.Visible = false;
                    break;
            }

            trgang2.Visible = false;
        }
       
        IUser user = null;
        ReportParameter[] reportParameter = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (IUser)Session[Constants.SESSION_USER_INFO];
            if (!IsPostBack) {
              FillData();              
              SetDefault();
            }
            
        }
        private void FillData(){
            Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            if (ViewState["strReportName"].ToString().ToLower() != "cargoarrivalnotice")
            {
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            }
            if (user.UserRole.Id != 2)
            {
                ddlLine.SelectedValue = user.UserLocation.Id.ToString();
            }            
            //Filler.FillData<IContainerType>(ddlEmptyYard, );
            
        }
        public void SetDefault() {
            if (strReportName.ToLower() != "gang") {
                trgang1.Visible = false; trgang2.Visible = false;
            }
        
        }
        
        private ReportParameter[] BindParameter(string reportType) {
            ReportParameter[] rptParameters = null;
            switch (reportType.ToLower()) {
                case "custom":
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlBlNo.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "gang":
                    rptParameters = new ReportParameter[4];
                   rptParameters[0] = new ReportParameter("LineBLNo", ddlBlNo.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("Shift", ddlShift.SelectedValue);
                    rptParameters[3] = new ReportParameter("GangDate", txtGangDate.Text);
                    break;
                case "edeliveryorder":
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("invBLHeader", ddlBlNo.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "deliveryorder":
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("invBLHeader", ddlBlNo.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "cargoarrivalnotice":
                    rptParameters = new ReportParameter[6];
                    rptParameters[0] = new ReportParameter("blno", ddlBlNo.SelectedValue);
                    rptParameters[1] = new ReportParameter("line", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("Location", ddlLocation.SelectedValue);
                    rptParameters[3] = new ReportParameter("Vessel", ddlVessel.SelectedValue);
                    rptParameters[4] = new ReportParameter("voyage", ddlVoyage.SelectedValue);
                    rptParameters[5] = new ReportParameter("ETA", txtETA.Text.Trim());
                    break;
                case "invoicedeveloper":
                    rptParameters = new ReportParameter[3];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlBlNo.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("LoginUserName", ddlLine.SelectedValue);
                    break;
                default: strReportName = string.Empty; break;
            }
            return rptParameters;
        }
        
        private void LoadReport()
        {
            
            if (string.IsNullOrEmpty(strReportName)) return;            
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportURL"]);
           // rptViewer.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"], "");
            if (strReportName.Equals(string.Empty))
            {
                rptViewer.ServerReport.ReportPath = "";
            }
            else
            {
                rptViewer.ServerReport.ReportPath = "/EMS.Report/" + strReportName;
            }
            rptViewer.ServerReport.SetParameters(reportParameter);           
            rptViewer.ServerReport.DisplayName = this.strFileName + "_" + DateTime.Now.Ticks.ToString();
            rptViewer.ShowCredentialPrompts = false;
            rptViewer.ShowPrintButton = true;
            rptViewer.ShowParameterPrompts = false;
            rptViewer.ShowPromptAreaButton = false;
            rptViewer.ShowToolBar = true;
            rptViewer.ShowReportBody = true;
            rptViewer.ServerReport.Refresh();

        }
        
        public string strFileName { get; set; }

        public string strReportName { get; set; }

        public string parameter1 { get; set; }

        public string parameter2 { get; set; }
        protected void ddlLine_SelectedIndexChanged1(object sender, EventArgs e)
        {            
            if (ddlLine.SelectedIndex>0)
            {
                Filler.FillData(ddlLocation, CommonBLL.GetLine(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
            }
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lng = ddlLine.SelectedValue.ToLong();
            if (lng> 0)
            {
                Filler.FillData(ddlBlNo, CommonBLL.GetBLHeaderByBLNo(lng), "ImpLineBLNo", "ImpLineBLNo", "Bl. No.");
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            strReportName = ViewState["strReportName"].ToString();
            if (!string.IsNullOrEmpty(strReportName))
            {
                reportParameter = BindParameter(strReportName);
                LoadReport();
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedIndex > 0)
            {
                Filler.FillData(ddlVessel, CommonBLL.GetVessels(ddlLocation.SelectedValue), "VesselName", "VesselID", "Vessel");
            }
        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVessel.SelectedIndex > 0)
            {
                Filler.FillData(ddlVoyage, CommonBLL.GetVoyages(ddlVessel.SelectedValue), "VoyageNo", "VoyageID", "Voyage");
            }
        }

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVoyage.SelectedIndex > 0)
            {
                Filler.FillData(ddlBlNo, CommonBLL.GetBLNo(ddlLocation.SelectedValue, ddlVessel.SelectedValue, ddlVoyage.SelectedValue), "LineBLNo", "LineBLNo", "Bl. No.");
            }
        }
    }
}