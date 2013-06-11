//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using EMS.Utilities;
//using EMS.BLL;
//using System.Configuration;
//using System.Data;
//using System.Text;
//using EMS.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EMS.Utilities;
using System.Data;
using EMS.Common;
using EMS.Entity;
using EMS.BLL;
using System.Text;

namespace EMS.WebApp.Reports
{
    public partial class COPRAR : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _hasEditAccess = true;
        BLL.DBInteraction dbinteract = new BLL.DBInteraction();

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlLine, EMS.BLL.EquipmentBLL.DDLGetLine());
                GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName"), true);
                GeneralFunctions.PopulateDropDownList(ddlLoc, dbinteract.PopulateDDLDS("DSR.dbo.mstLocation", "pk_LocID", "LocName", true), true);
                // GenerateReport();
            }

        }

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateDDLVoyage();
        }

        private void GenerateReport()
        {
            string pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            pod = pod.Contains(',') ? pod.Split(',')[1] : "";
            if (pod.Trim() == "")
            {
                lblError.Attributes.Add("Style", "display:block");
                return;
            }
            else
                lblError.Attributes.Add("Style", "display:none");

            //ReportBLL cls = new ReportBLL();

            //LocalReportManager reportManager = new LocalReportManager(rptViewer, "IGMCargoDesc", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            //string rptName = "IGMCargoDesc.rdlc";
            //int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            //int voyageId = ddlVoyage.SelectedIndex > 0 ? Convert.ToInt32(ddlVoyage.SelectedValue) : 0;
            //DBInteraction dbinteract = new DBInteraction();
            //DataSet ds = EMS.BLL.IGMReportBLL.GetRptCargoDesc(vesselId, voyageId, dbinteract.GetId("Port", pod));
            //try
            //{
            //    rptViewer.Reset();
            //    rptViewer.LocalReport.Dispose();
            //    rptViewer.LocalReport.DataSources.Clear();
            //    rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;

            //    rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));

            //    rptViewer.LocalReport.Refresh();
            //}
            //catch
            //{


            //}

            string FileName = COPRAR_TXT();
            DownLoadFile(FileName);
            File.Delete(FileName);

        }
        private void GenerateDDLVoyage()
        {
            int vesselId = Convert.ToInt32(ddlVessel.SelectedValue);
            int LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
            int nvoccID = Convert.ToInt32(ddlLine.SelectedValue);
            GeneralFunctions.PopulateDropDownList(ddlVoyage, EMS.BLL.VoyageBLL.GetVoyageAccVessel_Loc(LocationId, vesselId, nvoccID));
        }

        private void DownLoadFile(string FileName)
        {
            FileInfo file = new FileInfo(FileName);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AppendHeader("Content-Disposition", "attachment; filename = COPRAR.TXT");
            Response.AppendHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/download";
            Response.WriteFile(file.FullName);
            Response.Flush();
            Response.Close();
            Response.End();

        }
        public string COPRAR_TXT()
        {
            string rpd;
            int LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
            int LineID = Convert.ToInt32(ddlLine.SelectedValue);
            int VesselID = Convert.ToInt32(ddlVessel.SelectedValue);
            int VoyageID = Convert.ToInt32(ddlVoyage.SelectedValue);

            string VCN;
            string CallSign;
            string IMONo;
            string VoyageNo;
            string Port;
            string Country;
            string Containers;

            string uniqueFileName = Guid.NewGuid().ToString();
            //File.Create(Server.MapPath(@"~/Import/COPRARFILE/") + uniqueFileName + "_IMP_EDI.IGM");
            string FileName = Server.MapPath(@"~/Import/EDIFile/") + uniqueFileName + "_COPRAR.TXT";
            string PCSLogin = Convert.ToString(new DBInteraction().GetPCSLogin(LocationId).Tables[0].Rows[0]["PCSLoginID"]);
            string CurDate = DateTime.Now.ToString("yyyyMMdd")+"00000001";
 
            //string ArrDate = txtdtArrival.Text.Trim() == "" ? "" : Convert.ToDateTime(txtdtArrival.Text).ToString("yyyyMMdd");
            //string ArrTime = txtArriveTime.Text.Replace(":", ""); //Convert.ToDateTime(txtdtArrival.Text).ToString("hhmm");
            //if (ArrTime.Length > 4) ArrTime = ArrTime.Substring(0, 4);
            //ArrTime = string.IsNullOrEmpty(ArrTime.Trim()) ? "1200" : ArrTime;
            //string ArrDate1 = (txtdtArrival.Text.Trim()) == "" ? "" : Convert.ToDateTime(txtdtArrival.Text).ToString("ddMMyyyy hh:mm");//.Replace(" ","");
            //string lighthsdue = txtLightHouse.Text;// (txtLightHouse.Text.Trim() == "" || txtLightHouse.Text == "0") ? "+(''+" : txtLightHouse.Text;
            //string custHouse =  ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            // string custHouse = !((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text.Contains(',') ? "" : ((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text.Split(',')[1].Trim();
            //string custHouse = ddlCustomHouse.SelectedItem.Text.Trim();
            //string port1 = ((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text;
            //string port2 = ((TextBox)AutoCompletepPort3.FindControl("txtPort")).Text;
            //string port3 = ((TextBox)AutoCompletepPort4.FindControl("txtPort")).Text;
            string pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            pod = pod.Contains(',') ? pod.Split(',')[1] : "";

            if(pod=="INCCU1")
                rpd="kopt001";
            else
                rpd="haldia001";

            int PortCode = dbinteract.GetId("Port", pod);

            StreamWriter writer = new StreamWriter(FileName);
            //  ("myfile.txt")

            writer.WriteLine(("UNH" + ('' + ("COPRAR" + (''
                            + ("Advance Container List" + ('' + (CurDate + (''
                            + (CurDate + ('' + ("9" + ('' + (PCSLogin))))))))))))));

            writer.WriteLine(("RPD" + ('' + (rpd))));
             
            //System.Data.DataTable dt = EquipmentBLL.GetOMHinformation(LocationId, LineID, VesselID, VoyageID, PortCode);

            System.Data.DataSet ds = EquipmentBLL.GetOMHinformation(LocationId, LineID, VesselID, VoyageID, PortCode);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
                VCN = ds.Tables[0].Rows[0]["VCN"].ToString();
                CallSign = ds.Tables[0].Rows[0]["CallSign"].ToString();
                IMONo = ds.Tables[0].Rows[0]["IMONumber"].ToString();
                VoyageNo = ds.Tables[0].Rows[0]["VoyageNo"].ToString();
                Port = ds.Tables[0].Rows[0]["PortCode"].ToString();
                Country = ds.Tables[0].Rows[0]["CountryAbbr"].ToString();
                Containers = ds.Tables[0].Rows[0]["TotContainers"].ToString();
            //}


            writer.WriteLine(("OMH" + (''
                            + (VCN + (''
                            + (CallSign + (''
                            + (IMONo + (''
                            + (VoyageNo + (''
                            + (Port + (''
                            + (Country + (''
                            + ("BLA" + ('' + ('' + ('' + (''  + ('' 
                            + (Containers))))))))))))))))))))));

            DataSet DsContain = EquipmentBLL.GetCOPRARContainerInfo(VesselID, VoyageID, PortCode);
                //.GetCOPRARContainerInfo(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue));
            //             TextShippingCode.Text = Ds.Tables(0).Rows(0).Item("ShippingLineCode")
            DataTable DtContain = DsContain.Tables[0];
            int Srl1 = 0;


            foreach (DataRow Dr in DtContain.Rows)
            {
                Srl1 = (Srl1 + 1);
                writer.WriteLine(("OMD" + (''
                            + ("3" + (''
                            + (Dr["ContainerNo"].ToString() + (''
                            + (Dr["cstatus"].ToString() + (''
                            + (Dr["ISOCode"].ToString() + (''
                            + (Dr["TareWeight"].ToString().Trim() + (''
                            + (Dr["CargoWtTon"].ToString() + ('' + ('' 
                            + (Dr["issueport"].ToString() + (''
                            + (Dr["loadport"].ToString() + (''
                            + (Dr["discport"].ToString() + ('' + ('' + ('' + ('' + ('' 
                            + ("12" + (''
                            + ("BLA" + ('' + ('' + ('' 
                            + (Dr["discport"].ToString() + (''
                            + (Dr["finalDestination"].ToString() + (''
                            + (Dr["TMODE"].ToString() + (''
                            + (Dr["ICD"].ToString() + ('' + ('' + ('' + ('' + ('' + ('' + ('' + ('' + (''
                ))))))))))))))))))))))))))))))))))))))))))))))));
            }
            writer.Close();
            writer.Dispose();

            return FileName;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GenerateDDLVoyage();
        }
    }
}