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
using System.Text;
using System.Data;
using EMS.Utilities.ReportManager;
namespace EMS.WebApp.Reports
{
    public partial class ExpBLPrint : System.Web.UI.Page
    {
        public IList<BLPrintModel> Model { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                btnReport_Click(sender, e);
                Filler.FillData<ILocation>(ddlLine, new CommonBLL().GetActiveLocation(), "Name", "Id", "Location");
            }
        }
        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLine.SelectedIndex > 0)
            {

                Filler.FillData(ddlLocation, CommonBLL.GetLine(ddlLine.SelectedValue), "ProspectName", "ProspectID", "Line");
            }
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lng = ddlLine.SelectedValue.ToLong();
            if (lng > 0)
            {
                Filler.FillData(ddlBlNo, CommonBLL.GetBLHeaderByBLNo(lng), "ImpLineBLNo", "ImpLineBLNo", "Bl. No.");
            }
        }

        //private void fillFakData(DataTable dt){
        //    int j = 0;
        //    for (int i = 0; i < 10;i++ )
        //    {
        //        var dr = dt.NewRow();
        //        dr.ItemArray=new object[]{"Con"+j.ToString(),j,"Type"+j.ToString(),j,j};
        //        dt.Rows.Add(dr);
        //    }
        //}
        protected void btnReport_Click(object sender, EventArgs e)
        {
            DownLoadPDF();
            return;
            ISearchCriteria iSearchCriteria = new SearchCriteria();
            iSearchCriteria.StringParams = new List<string>() {
            //    ddlBlNo.SelectedValue 
            "MUM/UA/13-14/000001"
            };
            /*Report Viewer PDF on aspx*/
            // iSearchCriteria = null;
            // var temp = ExpBLPrintingBLL.GetxpBLPrinting(iSearchCriteria);

            // Model = temp==null?(List<BLPrintModel>)null:new List<BLPrintModel>() {temp  };
            var temp = ExpBLPrintingBLL.GetxpBLPrintingDS(iSearchCriteria);
            DataSet dsBLPrinting = new DataSet();
            DataTable dtBLPrintingCons = null;
            DataTable dtBLPrintingCons5 = null; 
            int i = 0;
            if (temp != null && temp.Tables.Count > 0)
            {
                
               //fillFakData(temp.Tables[1]);
                var top5=temp.Tables[1].AsEnumerable().Take(5);
                var Next5=temp.Tables[1].AsEnumerable().Skip(5);
                dtBLPrintingCons = temp.Tables[1].Clone();
                dtBLPrintingCons5 = temp.Tables[1].Clone();
                foreach (DataRow dr in top5){   
                    dtBLPrintingCons5.ImportRow(dr);
                }
                foreach (DataRow dr in Next5){   
                    dtBLPrintingCons.ImportRow(dr);
                }
                LocalReportManager reportManager = new LocalReportManager(rptViewer, "BLPrint", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
                string rptName = "BLPrint.rdlc";

                rptViewer.Reset();
                rptViewer.LocalReport.Dispose();
                rptViewer.LocalReport.DataSources.Clear();
                rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBLPrinting", temp.Tables[0]));
                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsBLPrintingContainerTopFive", dtBLPrintingCons5));
                rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsBLPrintingContainer", dtBLPrintingCons));
                rptViewer.LocalReport.SetParameters(new ReportParameter("ImageShow", "false"));
                rptViewer.LocalReport.Refresh();
                
                 
            }
        }
        public int StartCount = 0;
        public int EndCount = 0;
        public void DownLoadPDF()
        {
            ISearchCriteria iSearchCriteria = new SearchCriteria();
            iSearchCriteria.StringParams = new List<string>() {
                ddlBlNo.SelectedValue 
            //"MUM/UA/13-14/000001"
            };
           
            var temp = ExpBLPrintingBLL.GetxpBLPrintingDS(iSearchCriteria);
            DataSet dsBLPrinting = new DataSet();
            DataTable dtBLPrintingCons = null;
            DataTable dtBLPrintingCons5 = null;
            int i = 0;
            if (temp != null && temp.Tables.Count > 0)
            {

                //fillFakData(temp.Tables[1]);
                var top5 = temp.Tables[1].AsEnumerable().Take(5);
                var Next5 = temp.Tables[1].AsEnumerable().Skip(5);
                dtBLPrintingCons = temp.Tables[1].Clone();
                dtBLPrintingCons5 = temp.Tables[1].Clone();
                foreach (DataRow dr in top5)
                {
                    dtBLPrintingCons5.ImportRow(dr);
                }
                foreach (DataRow dr in Next5)
                {
                    dtBLPrintingCons.ImportRow(dr);
                }


                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath =  this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + "BLPrint.rdlc";
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DSBLPrinting", temp.Tables[0]));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("dsBLPrintingContainerTopFive", dtBLPrintingCons5));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("dsBLPrintingContainer", dtBLPrintingCons));
                viewer.LocalReport.SetParameters(new ReportParameter("ImageShow", "true"));
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=TEst." + extension);
                Response.BinaryWrite(bytes); // create the file
                Response.Flush(); // send it to the client to download
            }
        }
        public string GetTable(IList<ItemDetail> model)
        {
            StringBuilder sb = new StringBuilder();
            if (model != null)
            {
                if (StartCount == 0) { EndCount = model.Count > 5 ? 5 : model.Count; }
                sb.Append(@" <table style='width: 100%; margin-top: 20px' border='1' cellpadding='0' cellspacing='0'>");
                sb.Append(@"<tr><th style='width: 20%'>Container No</th>");
                sb.Append(@"<th style='width: 20%'>Size / Type</th>");
                sb.Append(@" <th style='width: 20%'>Seal No</th>");
                sb.Append(@"<th style='width: 20%'>Package</th>");
                sb.Append(@"<th style='width: 20%'>Gross Weight</th></tr>");
                for (int cnt = StartCount; cnt < EndCount; cnt++)
                {
                    sb.AppendFormat(@"<tr><td>{0} </td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", model[cnt].ContainerNo, model[cnt].Size, model[cnt].SealNo, model[cnt].Package, model[cnt].GrossWeight);
                }
                sb.Append(@"</table>");
                if (model.Count > 5)
                {
                    EndCount = model.Count;
                    StartCount = 5;
                }

            } return model != null ? sb.ToString() : string.Empty;
        }
    }
}