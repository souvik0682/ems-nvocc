using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;
using System.IO;

namespace EMS.WebApp.Export
{
    public partial class EDI_Export : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _hasEditAccess = true;
        BLL.DBInteraction dbinteract = new BLL.DBInteraction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //PopulateLine();
                PopulateVessel();
                PopulateLocation();
                //LoadData();
            }
        }
        private void PopulateLine()
        {
            //BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            //DataSet ds = dbinteract.GetNVOCCLine(-1, string.Empty);
            //ddlLine.DataValueField = "pk_NVOCCID";
            //ddlLine.DataTextField = "NVOCCName";
            //ddlLine.DataSource = ds;
            //ddlLine.DataBind();
            //ddlLine.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
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
        private void PopulateVessel()
        {
            DataTable dt = new expVoyageBLL().GetVessels(); // Convert.ToInt64(ddlLocation.SelectedValue));

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["pk_VesselID"] = "0";
                dr["VesselName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                ddlVessel.DataValueField = "pk_VesselID";
                ddlVessel.DataTextField = "VesselName";
                ddlVessel.DataSource = dt;
                ddlVessel.DataBind();
            }

            //DataTable dt = new DataTable();
            //dt = new CommonBLL().GetVesselList();
            //ddlVessel.DataValueField = "VesselID";
            //ddlVessel.DataTextField = "VesselName";
            //ddlVessel.DataSource = dt;
            //ddlVessel.DataBind();
            //ddlVessel.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateVoyage();
        }
        private void PopulateVoyage()
        {
            DataSet ds = BookingBLL.GetExportVoyages(ddlVessel.SelectedValue.ToInt(), ddlLoc.SelectedValue.ToInt());
            ddlVoyage.DataValueField = "VoyageID";
            ddlVoyage.DataTextField = "VoyageNo";
            ddlVoyage.DataSource = ds;
            ddlVoyage.DataBind();

            //DataTable dt = new DataTable();
            //dt = new CommonBLL().GetVoyageList(Convert.ToInt64(ddlVessel.SelectedValue));
            //ddlVoyage.DataValueField = "VoyageID";
            //ddlVoyage.DataTextField = "VoyageNo";
            //ddlVoyage.DataSource = dt;
            //ddlVoyage.DataBind();
            ddlVoyage.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateLoadingPort();
        }
        private void PopulateLoadingPort()
        {
            DataTable dt = new DataTable();
            dt = new ExportEDIBLL().GetLoadPort(Convert.ToInt32(ddlVoyage.SelectedValue));

            //dt = new CommonBLL().GetLoadPort(Convert.ToInt64(ddlVoyage.SelectedValue));
            ddlLoadingPort.DataValueField = "pk_PortID";
            ddlLoadingPort.DataTextField = "PortName";
            ddlLoadingPort.DataSource = dt;
            ddlLoadingPort.DataBind();
            ddlLoadingPort.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            string FileName = EDI_TXT();
            DownLoadFile(FileName);
            File.Delete(FileName);
            //GenerateReport();
        }
        private void GenerateReport()
        {
            ExportEDIBLL cls = new ExportEDIBLL();
            try
            {

                DataSet dtExcel = new DataSet();
                //dt1 = Convert.ToDateTime(txtStartDt.Text.Trim());
                //dt2 = Convert.ToDateTime(txtEndDt.Text.Trim());
                // dtExcel = cls.GetExportEDI(Convert.ToInt64(txtRotationNo.Text));
                //dtExcel.Columns.Remove("fk_NVOCCID");
                //dtExcel.Columns.Remove("fk_MainLineVesselID");                
                //dtExcel.Columns.Remove("fk_MainLineVoyageID");
                //dtExcel.Columns.Remove("ContainerAbbr");               
                //dtExcel.Columns.Remove("Commodity");                
                ExporttoExcel(dtExcel);
            }
            catch (Exception)
            {
                //Response.Write(ex.Message.ToString());
            }

        }
        private void ExporttoExcel(DataSet datasettables)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Export_Performance_Report.xls");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            foreach (DataTable table in datasettables.Tables)
            {
                HttpContext.Current.Response.Write("<BR><BR><BR>");
                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                  "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                  "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
                foreach (DataRow row in table.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }

                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            try
            {

                HttpContext.Current.Response.End();
                //Response.End();
            }
            catch (System.Threading.ThreadAbortException)
            {
                //throw ex;

            }



        }
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {

            //EDI_TXT();
        }
        private void DownLoadFile(string FileName)
        {
            FileInfo file = new FileInfo(FileName);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AppendHeader("Content-Disposition", "attachment; filename = EXP_EDI.IGM");
            Response.AppendHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/download";
            Response.WriteFile(file.FullName);
            Response.Flush();
            Response.Close();
            Response.End();

        }
        public string EDI_TXT()
        {

            string uniqueFileName = Guid.NewGuid().ToString();
            //File.Create(Server.MapPath(@"~/Import/EDIFile/") + uniqueFileName + "_IMP_EDI.IGM");
            string FileName = Server.MapPath(@"~/Import/EDIFile/") + uniqueFileName + "_IMP_EDI.IGM";
            string ICEGateLogin = Convert.ToString(new DBInteraction().GetShipLine_Tax(_userId).Tables[0].Rows[0]["ICEGateLogin"]);


            ExportEDIBLL cls = new ExportEDIBLL();
            DataSet dataset = new DataSet();
            //dt1 = Convert.ToDateTime(txtStartDt.Text.Trim());
            //dt2 = Convert.ToDateTime(txtEndDt.Text.Trim());
            dataset = cls.GetExportEDI(Convert.ToInt32(ddlLoc.SelectedValue),Convert.ToInt64(ddlVessel.SelectedValue),Convert.ToInt64(ddlVoyage.SelectedValue),Convert.ToInt32(ddlLoadingPort.SelectedValue));

            //first table values
            DataTable dt = new DataTable();
            dt = dataset.Tables[0];
            string ICEGateLoginD = (from DataRow dr in dt.Rows
                                    select (string)dr["icegateLoginD"]).FirstOrDefault();
            string CustomHouseCode = (from DataRow dr in dt.Rows
                                      select (string)dr["CustomHouseCode"]).FirstOrDefault();
            string SailDate = (from DataRow dr in dt.Rows
                               select (string)dr["SailDate"]).ToString();
            //second table
            DataTable dt1 = new DataTable();
            dt1 = dataset.Tables[1];
            string EGMNo = (from DataRow dr in dt1.Rows
                            select (string)dr["EGMNo"]).FirstOrDefault();
            string EGMDate = (from DataRow dr in dt1.Rows
                              select (string)dr["EGMDate"]).FirstOrDefault();
            string ShippingBillNo = (from DataRow dr in dt1.Rows
                                     select (string)dr["ShippingBillNo"]).FirstOrDefault();
            string ShippingBillDate = (from DataRow dr in dt1.Rows
                                       select (string)dr["ShippingBillDate"]).FirstOrDefault();
            string BLIssuePort = (from DataRow dr in dt1.Rows
                                  select (string)dr["BLIssuePort"]).FirstOrDefault();
            string DestinationPort = (from DataRow dr in dt1.Rows
                                      select (string)dr["DestinationPort"]).FirstOrDefault();
            string GateWayPort = (from DataRow dr in dt1.Rows
                                  select (string)dr["GateWayPort"]).FirstOrDefault();
            string WEIGHT = (from DataRow dr in dt1.Rows
                             select (string)dr["WEIGHT"]).FirstOrDefault();
            string UNITCODE = (from DataRow dr in dt1.Rows
                               select (string)dr["UNITCODE"]).FirstOrDefault();
            string PACKAGE = (from DataRow dr in dt1.Rows
                              select (string)dr["PACKAGE"]).FirstOrDefault();
            //third table
            DataTable dt2 = new DataTable();
            dt2 = dataset.Tables[2];
            string EGMNo1 = (from DataRow dr in dt2.Rows
                             select (string)dr["EGMNo"]).FirstOrDefault();
            string EGMDate1 = (from DataRow dr in dt2.Rows
                               select (string)dr["EGMDate"]).FirstOrDefault();
            string ShippingBillNo1 = (from DataRow dr in dt2.Rows
                                      select (string)dr["ShippingBillNo"]).FirstOrDefault();
            string ShippingBillDate1 = (from DataRow dr in dt2.Rows
                                        select (string)dr["ShippingBillDate"]).FirstOrDefault();
            string CONTAINERNO = (from DataRow dr in dt2.Rows
                                  select (string)dr["CONTAINERNO"]).FirstOrDefault();
            string CONTSTATUS = (from DataRow dr in dt2.Rows
                                 select (string)dr["CONTSTATUS"]).FirstOrDefault();

            StreamWriter writer = new StreamWriter(FileName);
            //  ("myfile.txt")

            writer.WriteLine(("HREC" + ('' + ("ZZ" + (''
                            + (ICEGateLogin + ('' + ("ZZ" + (''
                            + (CustomHouseCode + ('' + ("ICES1_5" + ('' + ("P" + ('' + ('' + ("SACHI01" + ('' + ("786" + (''
                            + (SailDate)))))))))))))))))))));
            writer.WriteLine("<manifest>");
            writer.WriteLine("<vesinfo>");
            writer.WriteLine(("F" + (''
                            + (EGMNo + (''
                            + (EGMDate + (''
                            + (ShippingBillNo + (''
                            + (ShippingBillDate + (''
                            + (BLIssuePort + (''
                            + (DestinationPort + (''
                            + (GateWayPort + (''
                            + (WEIGHT + (''
                            + (UNITCODE + (''
                            + (PACKAGE + (''
                            + (EGMNo1 + (''
                            + (EGMDate1 + (''
                            + (ShippingBillNo1 + (''
                            + (ShippingBillDate1 + (''
                            + (CONTAINERNO + (''
                            + (CONTSTATUS + (''
                             )))))))))))))))))))))))))))))))))));

            writer.WriteLine("<END-vesinfo>");
            writer.WriteLine("<cargo>");
            string sss = writer.ToNewString();
            //int PortCode = dbinteract.GetId("Port", pod);

            //DataSet Ds = EDIBLL.GetEDICargoInfo(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue), dbinteract.GetId("Port", pod));
            ////             TextShippingCode.Text = Ds.Tables(0).Rows(0).Item("ShippingLineCode")
            //DataTable Dt = Ds.Tables[0];
            //int Srl = 0;
            //string BlDate = ""; string Con1 = string.Empty; string Con2 = string.Empty; string Con3 = string.Empty; string Con4 = string.Empty;
            //string Not1 = string.Empty; string Not2 = string.Empty; string Not3 = string.Empty; string Not4 = string.Empty;
            //string Destport = string.Empty; string BLno = string.Empty; string DischargePort = string.Empty;


            //foreach (DataRow Dr in Dt.Rows)
            //{
            //    Srl = (Srl + 1);
            //    BlDate = Convert.ToDateTime(Dr["INFDATE"]).ToString("ddMMyyyy");
            //    int conLen = 35, notLen = 35;
            //    string conOri = Dr["ConsigneeInformation"].ToString();
            //    string notOri = Dr["NotifyPartyInformation"].ToString();

            //    if (conOri.Length > 0)
            //        Con1 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
            //    else
            //        Con1 = ".";

            //    conOri = conOri.Replace(Con1, "");
            //    if (conOri.Length > 0)
            //        Con2 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
            //    else
            //        Con2 = ".";

            //    conOri = conOri.Replace(Con2, "");
            //    if (conOri.Length > 0)
            //        Con3 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
            //    else
            //        Con3 = ".";

            //    conOri = conOri.Replace(Con3, "");
            //    if (conOri.Length > 0)
            //        Con4 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
            //    else
            //        Con4 = ".";


            //    if (notOri.Length > 0)
            //        Not1 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
            //    else
            //        Not1 = ".";

            //    notOri = notOri.Replace(Not1, "");
            //    if (notOri.Length > 0)
            //        Not2 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
            //    else
            //        Not2 = ".";

            //    notOri = notOri.Replace(Not2, "");
            //    if (notOri.Length > 0)
            //        Not3 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
            //    else
            //        Not3 = ".";

            //    notOri = notOri.Replace(Not3, "");
            //    if (notOri.Length > 0)
            //        Not4 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
            //    else
            //        Not4 = ".";


            //    Destport = ((Dr["DischargeIG"].ToString().Substring(0, 2) == "IN") ? ddlCustomHouse.SelectedItem.Text : Dr["DischargeIG"].ToString());
            //    BLno = System.Text.RegularExpressions.Regex.Replace(Dr["BLNUMBER"].ToString(), "[^\\w\\ ]", "").TrimEnd().Replace(" ", "").Replace(" ", "20");
            //    DischargePort = Dr["DischargeIG"].ToString().Split(',')[1].Trim();

            //    writer.WriteLine(("F" + (''
            //                    + (custHouse + (''
            //                    + (txtIMONo.Text + (''
            //                    + (txtCallSign.Text + (''
            //                    + (ddlVoyage.SelectedItem.Text + (''
            //                    + (txtIGMNo.Text + (''
            //                    + (txtDtIGM.Text.Replace(@"/", "") + (''
            //                    + (Dr["VgLineNo"].ToString() + ('' + ("0" + (''
            //                    + (BLno.Length >= 20 ? BLno.Substring(0, 20) : BLno + (''
            //                    + (BlDate + (''
            //                    + (Dr["LOADINGIG"].ToString().Split(',')[1].Trim() + (''
            //                    + (Destport.Split(',')[1].Trim() + ('' + ('' + (''
            //                    + (Con1 + (''
            //                    + (Con2 + (''
            //                    + (Con3 + (''
            //                    + (Con4 + (''
            //                    + (Not1 + (''
            //                    + (Not2 + (''
            //                    + (Not3 + (''
            //                    + (Not4 + (''
            //                    + (Dr["CARGONATURE"].ToString() + (''
            //                    + (Dr["ITEMTYPE"].ToString() + (''
            //                    + (Dr["CargoMovementCode"].ToString() + (''

            //                    //+ ((( (DischargePort.Length>=6 ? DischargePort.Substring(0, 6):DischargePort) == "INHAL1") ? DischargePort : string.Empty) + (''
            //        //+ ((Dr["CargoMovementCode"].ToString()=="LC" ? Dr["CFSCode"].ToString() : "") + (''
            //                    + ((Dr["CargoMovementCode"].ToString() == "LC" ? Dr["CFSCode"].ToString() : "") + (''
            //        //=======
            //        //                    //+ ((( (DischargePort.Length>=6 ? DischargePort.Substring(0, 6):DischargePort) == "INHAL1") ? DischargePort : string.Empty) + (''
            //        //                                + ((Dr["CargoMovementCode"].ToString() == "LC" ? Dr["CFSCode"].ToString() : "") + (''
            //        //>>>>>>> .r912
            //        //+ ((( (DischargePort.Length>=6 ? DischargePort.Substring(0, 6):DischargePort) == "INHAL1") ? DischargePort : string.Empty) + (''
            //        //+ ((Dr["CargoMovementCode"].ToString() == "LC" ? Dr["CFSCode"].ToString() : "") + (''
            //                    + (Dr["NumberPackage"].ToString() + (''
            //                    + (Dr["PackingUnit"].ToString() + (''
            //                    + (((Convert.ToInt32(Dr["GrossWeight"]) > 0) ? (Dr["GrossWeight"].ToString() + ('' + (Dr["WTUnit"].ToString() + ''))) : (string.Empty + '' + string.Empty))
            //                    + (((Convert.ToInt32(Dr["Volume"]) > 0) ? (Dr["Volume"].ToString() + ('' + Dr["VolUnit"].ToString())) : (string.Empty + '' + string.Empty)) + (''
            //                    + ((Dr["MarksNumbers"].ToString().Length > 300 ? Dr["MarksNumbers"].ToString().Substring(0, 300) : Dr["MarksNumbers"].ToString()) + (''
            //                    + ((Dr["GoodDescription"].ToString().Length > 250 ? Dr["GoodDescription"].ToString().Substring(0, 250) : Dr["GoodDescription"].ToString()) + (''
            //                    + (Dr["UNOCode"].ToString() + (''
            //                    + (Dr["IMO_IMDGCode"].ToString() + (''
            //                    + (Dr["TPBondNo"].ToString() + (''
            //                    + (Dr["CACode"].ToString() + (''
            //                    + (Dr["TransportMode"].ToString() + ('' + Dr["MLOCode"].ToString()))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))));
            //    //+ ((Dr["CargoMovementCode"].ToString() == "LC" ? "" : Dr["TransportMode"].ToString()) + ('' + Dr["MLOCode"].ToString()))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))));
            //    //
            //}
            //writer.WriteLine("<END-cargo>");


            //writer.WriteLine("<contain>");

            //DataSet DsContain = EDIBLL.GetEDIContainerInfo(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue));
            ////             TextShippingCode.Text = Ds.Tables(0).Rows(0).Item("ShippingLineCode")
            //DataTable DtContain = DsContain.Tables[0];
            //int Srl1 = 0;


            //foreach (DataRow Dr in DtContain.Rows)
            //{
            //    Srl1 = (Srl1 + 1);
            //    writer.WriteLine(("F" + (''
            //                + (custHouse + (''
            //                + (txtIMONo.Text + (''
            //                + (txtCallSign.Text + (''
            //                + (ddlVoyage.SelectedItem.Text + (''
            //                + (txtIGMNo.Text + (''
            //                + (txtDtIGM.Text.Replace(@"/", "") + (''
            //                + (Dr["VgLineNo"].ToString() + ('' + ("0" + (''
            //        //+ (txtCallSign.Text + (''
            //        //+ (ddlVoyage.SelectedItem.Text + (''
            //        //+ (txtShipCode.Text + (''
            //        //+ (txtPAN.Text + (''
            //        //+ (txtMaster.Text + (''
            //        //+ (custHouse + (''
            //    + (Dr["ContainerNo"].ToString() + (''
            //    + (Dr["SealNo"].ToString() + (''
            //    + (Dr["AgentCode"].ToString().Trim() + (''
            //    + (Dr["cstatus"].ToString() + (''
            //    + (Dr["NoofPackages"].ToString() + (''
            //    + (Dr["CargoWtTon"].ToString() + (''
            //    + (Dr["ISOCode"].ToString() + (''
            //    + (Dr["SOC"].ToString()))))))))))))))))))))))))))))))))));
            //}
            //writer.WriteLine("<END-contain>");
            //writer.WriteLine("<END-manifest>");
            //writer.WriteLine("TREC786");
            ////EndUsing;

            writer.Close();
            writer.Dispose();

            return FileName;
        }
    }
}