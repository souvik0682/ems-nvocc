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
        public ReportCredentials(string userName, string password, string domain)
        {
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


    public class ReportUtil
    {
        //private void CreatePDF(string fileName)
        //{
        //    // Setup DataSet
        //    MyDataSetTableAdapters.YourTableAdapterHere ds = new MyDataSetTableAdapters.YourTableAdapterHere();


        //    // Create Report DataSource
        //    ReportDataSource rds = new ReportDataSource("MyDataSourceName", ds.GetData());


        //    // Variables
        //    Warning[] warnings;
        //    string[] streamIds;
        //    string mimeType = string.Empty;
        //    string encoding = string.Empty;
        //    string extension = string.Empty;


        //    // Setup the report viewer object and get the array of bytes
        //    ReportViewer viewer = new ReportViewer();
        //    viewer.ProcessingMode = ProcessingMode.Local;
        //    viewer.LocalReport.ReportPath = "YourReportHere.rdlc";
        //    viewer.LocalReport.DataSources.Add(rds); // Add datasource here


        //    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


        //    // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        //    Response.Buffer = true;
        //    Response.Clear();
        //    Response.ContentType = mimeType;
        //    Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
        //    Response.BinaryWrite(bytes); // create the file
        //    Response.Flush(); // send it to the client to download
        //}
        public string GetUrl(string reportName, NameValueCollection reportParma)
        {

            string str = string.Empty;
            if (reportParma != null && reportParma.Count > 1)
            {
                foreach (string v in reportParma)
                {
                    if (v.ToLower() != "reportname")
                    {
                        str += string.Format("&{0}={1}", v, GeneralFunctions.DecryptQueryString(reportParma[v]));
                        // str += string.Format("&{0}={1}", v, reportParma[v]);
                    }
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = ConfigurationManager.AppSettings["ReportURL"] + string.Format("?%2fEMS.Report%2f{0}&rs:Command=Render{1}&rs:Format=PDF", reportName, str);
            }
            return str;
        }
        private ReportParameter[] BindParameter(Dictionary<string, string> reportParma)
        {
            ReportParameter[] rptParameters = null;
            if (reportParma != null && reportParma.Count > 1)
            {
                int i = 0;
                rptParameters = new ReportParameter[reportParma.Count - 1];
                foreach (string v in reportParma.Keys)
                {
                    if (v.ToLower() != "reportname")
                    {
                        rptParameters[i++] = new ReportParameter(v, GeneralFunctions.DecryptQueryString(reportParma[v]));
                    }
                }
            }
            return rptParameters;
        }
        private ReportParameter[] BindParameter(NameValueCollection reportParma)
        {
            ReportParameter[] rptParameters = null;
            if (reportParma != null && reportParma.Count > 1)
            {
                rptParameters = new ReportParameter[reportParma.Count - 1];
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
        public Dictionary<string, string> GetQueryString(NameValueCollection nameValue)
        {
            Dictionary<string, string> dic = null;
            if (nameValue.Count < 2) throw new Exception("insufficent Params");
            dic = new Dictionary<string, string>();
            foreach (string str in nameValue.AllKeys)
            {
                if (str.ToLower() != "reportname")
                {
                    dic[str] = GeneralFunctions.DecryptQueryString(nameValue[str]);
                    // dic[str] = nameValue[str];
                }
            }
            return dic;
        }
        string path = string.Empty;
        void myWebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (!string.IsNullOrEmpty(path))
            {
                FileUtil ft = new FileUtil();
                ft.Dst = path;
                ft.Download(HttpContext.Current.Response);
            }
        }
        public void LoadReport(Microsoft.Reporting.WebForms.ReportViewer rptViewer, NameValueCollection reportParma)
        {
            string ReportName = GeneralFunctions.DecryptQueryString(reportParma["ReportName"]);
            // string ReportName = reportParma["ReportName"];//;
            if (!string.IsNullOrEmpty(ReportName))
            {
                if (ReportName.ToLower().Equals("invoicedeveloper"))
                {
                    //HttpContext.Current.Response.Redirect(GetUrl(ReportName, reportParma));
                    using (WebClient myWebClient = new WebClient())
                    {
                        // Download the Web resource and save it into the current filesystem folder.
                        myWebClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(myWebClient_DownloadDataCompleted);
                        myWebClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                        path = HttpContext.Current.Server.MapPath("~/Download/" + "Invoice_" + DateTime.Now.Ticks.ToString() + ".pdf");
                        myWebClient.DownloadFile(GetUrl(ReportName, reportParma).Replace(ConfigurationManager.AppSettings["ReplaceString"], "http://localhost"), path);
                        if (!string.IsNullOrEmpty(path))
                        {
                            var fileInfo = new System.IO.FileInfo(path);
                            HttpContext.Current.Response.ContentType = "Application/pdf";
                            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", fileInfo.Name));
                            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                            HttpContext.Current.Response.WriteFile(path);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                else if (ReportName.ToLower().Equals("deliveryorder"))
                {
                    using (WebClient myWebClient = new WebClient())
                    {
                        myWebClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(myWebClient_DownloadDataCompleted);
                        myWebClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                        path = HttpContext.Current.Server.MapPath("~/Download/" + "DO_" + DateTime.Now.Ticks.ToString() + ".pdf");
                        myWebClient.DownloadFile(GetUrl(ReportName, reportParma).Replace(ConfigurationManager.AppSettings["ReplaceString"], "http://localhost"), path);
                        if (!string.IsNullOrEmpty(path))
                        {
                            var fileInfo = new System.IO.FileInfo(path);
                            HttpContext.Current.Response.ContentType = "Application/pdf";
                            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", fileInfo.Name));
                            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                            HttpContext.Current.Response.WriteFile(path);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                else if (ReportName.ToLower().Equals("creditnote"))
                {
                    using (WebClient myWebClient = new WebClient())
                    {
                        myWebClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(myWebClient_DownloadDataCompleted);
                        myWebClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                        path = HttpContext.Current.Server.MapPath("~/Download/" + "creditnote_" + DateTime.Now.Ticks.ToString() + ".pdf");
                        myWebClient.DownloadFile(GetUrl(ReportName, reportParma).Replace(ConfigurationManager.AppSettings["ReplaceString"], "http://localhost"), path);
                        if (!string.IsNullOrEmpty(path))
                        {
                            var fileInfo = new System.IO.FileInfo(path);
                            HttpContext.Current.Response.ContentType = "Application/pdf";
                            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", fileInfo.Name));
                            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                            HttpContext.Current.Response.WriteFile(path);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                else
                {
                    Load(rptViewer, ReportName, BindParameter(reportParma));
                }
            }
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
        public void LoadReport(Microsoft.Reporting.WebForms.ReportViewer rptViewer, string reportType, Dictionary<string, string> reportParma)
        {
            string ReportName = reportParma["ReportName"];
            Load(rptViewer, reportType, BindParameter(reportParma));

        }
    }

    public partial class ShowReport : System.Web.UI.Page
    {
      
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            strReportName = Request.QueryString["reportName"];
            if (string.IsNullOrEmpty(strReportName))
            {
                throw new Exception("EmptyParameters");
            }
            ViewState["strReportName"] = strReportName;

            lblLine.Text = "BL No.";
            ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged;
            trCar.Visible = false;
            trCar1.Visible = false;
            trInvoice.Visible = false;

            trInvoice.Visible = false;
            TrHire.Visible = false;
            TrHire1.Visible = false;
            trPendingDO.Visible = false;
            btnPrint.Visible = false;
            //PendingDelivaryOrder
            switch (strReportName.ToLower())
            {
                    

                case "cargoarrivalnotice":
                    ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged1;
                    trCar.Visible = true;
                    trCar1.Visible = true;
                    lblLine.Text = "Line";
                    break;
                case "invoicedeveloper":
                    trInvoice.Visible = true;
                    btnPrint.Visible = true;
                    ddlLocation.SelectedIndexChanged += ddlLocation_SelectedIndexChanged_Invoice;
                    break;
                case "onoffhire":
                    TrHire.Visible = true;
                    TrHire1.Visible = true;
                    lblLine.Text = "Line";
                    ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged1;
                    break;
                case "pendingdelivaryorder":
                    trPendingDO.Visible = true;
                    ddlLine.SelectedIndexChanged += ddlLine_SelectedIndexChanged1;
                    lblLine.Text = "Line";
                    break;
                case "creditnote":
                    trInvoice.Visible = true;
                    btnPrint.Visible = true;
                    ddlLocation.SelectedIndexChanged += ddlLocation_SelectedIndexChanged_Invoice;
                    break;
                default:
                    break;
            }

            trgang2.Visible = false;

        }

        IUser user = null;
        ReportParameter[] reportParameter = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (IUser)Session[Constants.SESSION_USER_INFO];
            if (!IsPostBack)
            {
                FillData();
                SetDefault();
            }

        }
        private void FillData()
        {
            Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            if (ViewState["strReportName"].ToString().ToLower() != "cargoarrivalnotice")
            {
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            }


            switch (strReportName.ToLower())
            {
                case "custom":
                    litHeader.Text = "CUSTOM LETTER";
                    break;
                case "dropoff":
                    litHeader.Text = " EMPTY DROP-OFF LETTER";
                    break;
                case "gang":
                    litHeader.Text = "GANG LETTER";
                    break;
                case "surveyor":
                    litHeader.Text = "SURVEYOR LETTER";
                    break;
                case "edeliveryorder":
                    litHeader.Text = "EXAMINATION DELIVERY ORDER";
                    break;
                case "deliveryorder":
                    litHeader.Text = "DELIVERY ORDER";
                    break;
                case "cargoarrivalnotice":
                    litHeader.Text = "CARGO ARRIVAL NOTICE";
                    break;
                case "invoicedeveloper":
                    litHeader.Text = "INVOICE";
                    break;
                case "creditnote":
                    litHeader.Text = "CREDIT NOTE";
                    break;
                case "onoffhire":
                    litHeader.Text = "ON/OFF REGISTER FROM";
                    break;
                case "pendingdelivaryorder":
                    litHeader.Text = "PENDING DELIVERY ORDER";
                    break;
                //PendingDelivaryOrder
                default: strReportName = string.Empty; break;
            }
            if (user.UserRole.Id != 2)
            {
                ddlLine.SelectedValue = user.UserLocation.Id.ToString();
                ddlLine.Enabled = false;
                strReportName = ViewState["strReportName"].ToString();
                switch (strReportName.ToLower())
                {

                    case "cargoarrivalnotice":
                        ddlLine_SelectedIndexChanged1(null, null);
                        break;
                    case "invoicedeveloper":
                        ddlLocation_SelectedIndexChanged_Invoice(null, null);
                        break;
                    case "onoffhire":
                        ddlLine_SelectedIndexChanged1(null, null);
                        break;
                    case "creditnote":
                        ddlLocation_SelectedIndexChanged_Invoice(null, null);
                        break;

                    default:
                        ddlLine_SelectedIndexChanged(null, null);
                        break;
                }
            }
            //Filler.FillData<IContainerType>(ddlEmptyYard, );

        }
        public void SetDefault()
        {
            if (strReportName.ToLower() != "gang")
            {
                trgang1.Visible = false; trgang2.Visible = false;
            }
            //else if (strReportName.ToLower() != "surveyor")
            //{

            //}

        }

        private ReportParameter[] BindParameter(string reportType)
        {
            ReportParameter[] rptParameters = null;
            switch (reportType.ToLower())
            {
                case "custom":
                    //litHeader.Text = "CUSTOM LETTER";
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "dropoff":
                    //litHeader.Text = " EMPTY DROP-OFF LETTER";
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "gang":
                    //litHeader.Text = "GANG LETTER";
                    rptParameters = new ReportParameter[3];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("Shift", ddlShift.SelectedValue);
                    //rptParameters[3] = new ReportParameter("GangDate", txtGangDate.Text);
                    break;
                case "surveyor":
                    //litHeader.Text = "SURVEYOR LETTER";
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    //rptParameters[2] = new ReportParameter("Shift", ddlShift.SelectedValue);
                    //rptParameters[3] = new ReportParameter("GangDate", txtGangDate.Text);
                    break;
                case "edeliveryorder":
                    //litHeader.Text = "EXAMINATION DELIVERY ORDER";
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("invBLHeader", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "deliveryorder":
                    //litHeader.Text = "DELIVERY ORDER";
                    rptParameters = new ReportParameter[2];
                    rptParameters[0] = new ReportParameter("invBLHeader", ddlLocation.SelectedValue);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    break;
                case "cargoarrivalnotice":
                    //litHeader.Text = "CARGO ARRIVAL NOTICE";
                    //rptParameters = new ReportParameter[6];
                    //rptParameters[0] = new ReportParameter("blno", ddlBlNo.SelectedValue);
                    //rptParameters[1] = new ReportParameter("line", ddlLocation.SelectedValue);
                    //rptParameters[2] = new ReportParameter("Location", ddlLine.SelectedValue);
                    //rptParameters[3] = new ReportParameter("Vessel", ddlVessel.SelectedValue);
                    //rptParameters[4] = new ReportParameter("voyage", ddlVoyage.SelectedValue);
                    //rptParameters[5] = new ReportParameter("ETA", txtETA.Text );
                    rptParameters = new ReportParameter[6];
                    rptParameters[0] = new ReportParameter("blno", ddlBlNo.SelectedValue);
                    rptParameters[1] = new ReportParameter("line", ddlLocation.SelectedValue);
                    rptParameters[2] = new ReportParameter("location", ddlLine.SelectedValue);
                    rptParameters[3] = new ReportParameter("Vessel", ddlVessel.SelectedValue);
                    rptParameters[4] = new ReportParameter("voyage", ddlVoyage.SelectedValue);
                    rptParameters[5] = new ReportParameter("eta", txtETA.Text);
                    break;
                case "invoicedeveloper":
                    //litHeader.Text = "INVOICE DEVELOPER";
                    rptParameters = new ReportParameter[4];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedItem.Text);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("LoginUserName", txtPrintedBy.Text);
                    rptParameters[3] = new ReportParameter("InvoiceId", ddlInvoice.SelectedValue);
                    break;
                case "creditnote":
                    //litHeader.Text = "INVOICE DEVELOPER";
                    rptParameters = new ReportParameter[5];
                    rptParameters[0] = new ReportParameter("LineBLNo", ddlLocation.SelectedItem.Text);
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[2] = new ReportParameter("LoginUserName", txtPrintedBy.Text);
                    rptParameters[3] = new ReportParameter("InvoiceId", ddlInvoice.SelectedValue);
                    rptParameters[4] = new ReportParameter("CrnId", ddlInvoice.SelectedValue);
                    break;
                case "onoffhire":
                    //litHeader.Text = "ON/OFF REGISTER FROM";
                    rptParameters = new ReportParameter[5];
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[0] = new ReportParameter("line", ddlLocation.SelectedValue);
                    rptParameters[2] = new ReportParameter("refDateS", txtSDate.Text);
                    rptParameters[3] = new ReportParameter("refDateE", txtEDate.Text);
                    rptParameters[4] = new ReportParameter("onOffhire", ddlHire.SelectedValue);
                    break;
                case "pendingdelivaryorder":
                    //litHeader.Text = "PENDING DELIVERY ORDER";
                    rptParameters = new ReportParameter[2];
                    rptParameters[1] = new ReportParameter("Location", ddlLine.SelectedValue);
                    rptParameters[0] = new ReportParameter("line", ddlLocation.SelectedValue);
                    //rptParameters[2] = new ReportParameter("DOFinalBill", txtDoFinal.Text);
                    break;
                //PendingDelivaryOrder
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
                if (ViewState["strReportName"].ToString().ToLower() == "cargoarrivalnotice")
                {
                    rptViewer.ServerReport.ReportPath = "/EMS.Report/MCAN";
                }
                else
                {
                    rptViewer.ServerReport.ReportPath = "/EMS.Report/" + strReportName;
                }
            }
            rptViewer.ServerReport.SetParameters(reportParameter);
            rptViewer.ServerReport.DisplayName = this.strFileName + "_" + DateTime.Now.Ticks.ToString();
            rptViewer.ShowCredentialPrompts = false;
            rptViewer.ShowPrintButton = true;
            rptViewer.ShowParameterPrompts = false;
            rptViewer.ShowPromptAreaButton = false;
            rptViewer.ShowToolBar = true;
            rptViewer.ZoomPercent = 300;
            if (ViewState["strReportName"].ToString().ToLower() == "cargoarrivalnotice")
            {

            }
            rptViewer.ShowReportBody = true;
            rptViewer.Visible = true;
            rptViewer.ServerReport.Refresh();

        }

        public string strFileName { get; set; }

        public string strReportName { get; set; }

        public string parameter1 { get; set; }

        public string parameter2 { get; set; }
        protected void ddlLine_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlLine.SelectedIndex > 0)
            {
                if (ViewState["strReportName"].ToString().ToLower() == "onoffhire")
                {
                    Filler.FillData(ddlLocation, CommonBLL.GetLineForHire(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
                }
                else
                {
                    Filler.FillData(ddlLocation, CommonBLL.GetLine(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
                }
            }
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lng = ddlLine.SelectedValue.ToLong();
            if (lng > 0)
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
                Filler.FillData(ddlVoyage, CommonBLL.GetVoyages(ddlVessel.SelectedValue, ddlLocation.SelectedValue), "VoyageNo", "VoyageID", "Voyage");
            }
        }

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVoyage.SelectedIndex > 0)
            {
                Filler.FillData(ddlBlNo, CommonBLL.GetBLNo(ddlLocation.SelectedValue, ddlVessel.SelectedValue, ddlVoyage.SelectedValue), "LineBLNo", "LineBLNo", "Bl. No.");
                if (ViewState["strReportName"].ToString().ToLower() == "cargoarrivalnotice")
                {
                    ddlBlNo.Items.Insert(1, new ListItem("All", "All"));
                }
            }
        }
    }
}