using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Collections.Specialized;
using EMS.Utilities.Cryptography;

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


    public class ReportUtil {
        private ReportParameter[] BindParameter(Dictionary<string,string> reportParma)
        {  ReportParameter[] rptParameters = null;
            if(reportParma!=null && reportParma.Count>1){
                int i = 0;
           rptParameters = new ReportParameter[reportParma.Count-1];
           foreach (string v in reportParma.Keys)
           {
                if (v.ToLower() != "reportname")
                {
                    rptParameters[i++] = new ReportParameter(v,GeneralFunctions.DecryptQueryString( reportParma[v]));
                }
            }
            }            
            return rptParameters;
        }
        private ReportParameter[] BindParameter(NameValueCollection reportParma)
        {
            ReportParameter[] rptParameters = null;
            if (reportParma != null && reportParma.Count >1)
            {
                rptParameters = new ReportParameter[reportParma.Count-1];
                int i = 0;
                foreach (string v in reportParma)
                {
                    if (v.ToLower() != "reportname")
                    {
                        rptParameters[i++] = new ReportParameter(v, GeneralFunctions.DecryptQueryString(reportParma[v]));
                        //rptParameters[i++] = new ReportParameter(v,reportParma[v]);
                    }
                }
            }
            return rptParameters;
        }
        public Dictionary<string,string> GetQueryString(NameValueCollection nameValue) { 
             Dictionary<string,string> dic=null;
            if(nameValue.Count<2) throw new Exception("insufficent Params");
            dic=new Dictionary<string,string>();
            foreach(string str in nameValue.AllKeys){
                if (str.ToLower() != "reportname")
                {
                     dic[str]= GeneralFunctions.DecryptQueryString(nameValue[str]);               
                   // dic[str] = nameValue[str];
                }
            }
        return dic;
        }
        public  void LoadReport(Microsoft.Reporting.WebForms.ReportViewer rptViewer,NameValueCollection  reportParma){
            string ReportName = reportParma["ReportName"];
            Load(rptViewer, GeneralFunctions.DecryptQueryString(ReportName),BindParameter(reportParma)); 
        }

        private void Load(Microsoft.Reporting.WebForms.ReportViewer rptViewer, string reportType, ReportParameter[] reportParma)
        {
            if (string.IsNullOrEmpty(reportType)) throw new Exception("Null ReportParameter Or Null Report Name");
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportURL"]);
            rptViewer.ServerReport.ReportPath = "/EMS.Report/" + reportType;
            rptViewer.ServerReport.SetParameters(reportParma);
            rptViewer.ServerReport.DisplayName = reportType + "_" + DateTime.Now.Ticks.ToString();
            rptViewer.ShowCredentialPrompts = false;
            rptViewer.ShowPrintButton = true;
            rptViewer.ShowParameterPrompts = false;
            rptViewer.ShowPromptAreaButton = false;
            rptViewer.ShowToolBar = true;
            rptViewer.ShowPrintButton = true;            
            rptViewer.ShowReportBody = true;
            rptViewer.ServerReport.Refresh();
        }
       public  void LoadReport(Microsoft.Reporting.WebForms.ReportViewer rptViewer,string reportType,Dictionary<string,string> reportParma)
        {
             string ReportName = reportParma["ReportName"];
           Load(rptViewer,reportType,BindParameter(reportParma));                    

        }
    }

    public partial class ShowReport : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            strReportName = Request.QueryString["reportName"];
            if (string.IsNullOrEmpty(strReportName)) {
                throw new Exception("EmptyParameters");
            }
            ViewState["strReportName"] = strReportName; 
            trInvoice.Visible = false;
            switch (strReportName.ToLower())
            {

                case "cargoarrivalnotice":
                    ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged1;                   
                    break;
                default:
                    lblLine.Text = "BL No.";
                    ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged;                   
                    trCar.Visible = false;
                    trCar1.Visible = false;
                    trInvoice.Visible = false;
                    if (strReportName.ToLower() == "invoicedeveloper")
                    {
                        trInvoice.Visible = true;
                        ddlLocation.SelectedIndexChanged += ddlLocation_SelectedIndexChanged_Invoice;    
                    }
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
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "gang":
                    rptParameters = new ReportParameter[4];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("Shift", ddlShift.SelectedValue);
                    rptParameters[3] = new ReportParameter("GangDate", txtGangDate.Text);
                    break;
                case "edeliveryorder":
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("invBLHeader", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "deliveryorder":
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("invBLHeader", ddlLocation.SelectedValue);
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
                    rptParameters = new ReportParameter[4];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedItem.Text);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("LoginUserName", txtPrintedBy.Text);
                    rptParameters[3] = new ReportParameter("InvoiceId", ddlInvoice.SelectedValue);
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
                Filler.FillData(ddlLocation, CommonBLL.GetBLHeaderByBLNo(lng), "ImpLineBLNo", "ImpLineBLNo", "Bl. No.");
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
        //Get invoice
        protected void ddlLocation_SelectedIndexChanged_Invoice(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedIndex > 0)
            {
                Filler.FillData(ddlInvoice, CommonBLL.GetInvoiceByBLNo(ddlLocation.SelectedValue), "InvoiceNo", "InvoiceID", "Invoice");
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