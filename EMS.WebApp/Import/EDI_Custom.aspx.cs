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

namespace EMS.WebApp.Import
{
    public partial class EDI_Custom : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _hasEditAccess = true;
        BLL.DBInteraction dbinteract = new BLL.DBInteraction();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                GeneralFunctions.PopulateDropDownList(ddlVessel, dbinteract.PopulateDDLDS("trnVessel", "pk_VesselID", "VesselName", " order by VesselName"));
                GeneralFunctions.PopulateDropDownList(ddlCustomHouse, EDIBLL.GetCustomHouse());

                //GeneralFunctions.PopulateDropDownList(ddlTerminalOperator, dbinteract.PopulateDDLDS("mstTerminal", "pk_TerminalID", "TerminalName"));
                //TextBox txtPort = ((TextBox)AutoCompletepPort1.FindControl("txtPort"));
                //txtPort.Attributes.Add("onblur", "document.getElementById('form1').submit();");
            }

            ((TextBox)AutoCompletepPort2.FindControl("txtPort")).ReadOnly = true;

        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CLearAll();
            GeneralFunctions.PopulateDropDownList(ddlVoyage, dbinteract.PopulateDDLDS("trnVoyage", "pk_VoyageID", "VoyageNo", "where fk_VesselID=" + ddlVessel.SelectedValue));
        }

        protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            if (pod.Trim() == "")
            {
                GeneralFunctions.RegisterAlertScript(this, "Port of Discharge cannot be left blank");
                ddlVoyage.SelectedIndex = 0;
                return;
            }
            LoadData();
            LoadEDIList();
        }

        private void LoadSurveyorDDL(DropDownList ddlSurveyor,int LocationId)
        {
            DataTable dt;
            if (ViewState["Surveyor"] == null)
            {
                dt = new ImportBLL().GetSurveyor(LocationId);
                DataRow dr = dt.NewRow();
                dr["fk_AddressID"] = "0";
                dr["AddrName"] = "--Select--";
                dt.Rows.InsertAt(dr, 0);

                ViewState["Surveyor"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Surveyor"];
            }

            ddlSurveyor.DataValueField = "fk_AddressID";
            ddlSurveyor.DataTextField = "AddrName";
            ddlSurveyor.DataSource = dt;
            ddlSurveyor.DataBind();
        }

        void LoadEDIList()
        {
            DataTable dt = EDIBLL.GetItemNoForEDI(Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(hdnPortId.Value));

            //if (dt.Rows.Count > 0)
            //{
            rpEDI.DataSource = dt;
            rpEDI.DataBind();

            //string ss = dt.Rows[0]["NOALLOTED"].ToString();
            //if (Convert.ToBoolean(ss))
            //{
            //    lnkLines.Visible = true;
            //}
            //else
            //    lnkLines.Visible = true;
            //}

        }

        private void LoadData()
        {
            string pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            Int32 podid = 0;
            pod = pod.Contains(',') ? pod.Split(',')[1] : "";
            List<IVesselVoyageEDI> lstVesselVoyageEDI = EDIBLL.VesselVoyageEDI(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue), dbinteract.GetId("Port", pod));
            if (lstVesselVoyageEDI.Count == 0)
            {
                CLearAll();
                return;
            }
            IVesselVoyageEDI vesselVoyageEDI = lstVesselVoyageEDI.First<IVesselVoyageEDI>();
            txtCallSign.Text = vesselVoyageEDI.CallSign;
            txtCargoDesc.Text = vesselVoyageEDI.CargoDesc;
            txtIGMNo.Text = vesselVoyageEDI.IGMNo;
            txtIMONo.Text = vesselVoyageEDI.IMONumber;
            string Lastport = ((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text = vesselVoyageEDI.LastPortCalled;
            pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text = vesselVoyageEDI.pod;
            //txtLastPort.Text = vesselVoyageEDI.LastPortCalled;
            txtLightHouse.Text = vesselVoyageEDI.LightHouseDue.ToString();
            txtMaster.Text = vesselVoyageEDI.MasterName;
            txtPAN.Text = vesselVoyageEDI.PANNo;
            txtShipCode.Text = vesselVoyageEDI.ShippingLineCode;
            txtTotLine.Text = vesselVoyageEDI.TotalLines;
            txtVesselFlag.Text = vesselVoyageEDI.VesselFlag;
            txtdtArrival.Text = vesselVoyageEDI.LandingDate.ToString();
            txtdtArrival.Text = txtdtArrival.Text.Contains(' ') ? txtdtArrival.Text.Split(' ')[0] : "";
            txtDtIGM.Text = vesselVoyageEDI.IGMDate == null ? "" : Convert.ToDateTime(vesselVoyageEDI.IGMDate).ToShortDateString();

            ddlCrewEffList.SelectedValue = vesselVoyageEDI.CrewEffectList.ToString();
            ddlCrewList.SelectedValue = vesselVoyageEDI.CrewList.ToString();
            ddlMaritime.Text = vesselVoyageEDI.MaritimeList.ToString();
            ddlPessengerList.SelectedValue = vesselVoyageEDI.PassengerList.ToString();
            ddlSameButton.SelectedValue = vesselVoyageEDI.SameButtonCargo.ToString();
            ddlShipStoreSubmitted.SelectedValue = vesselVoyageEDI.ShipStoreSubmitted.ToString();
            ddlVesselType.SelectedValue = vesselVoyageEDI.VesselType;
            podid = Convert.ToInt32(vesselVoyageEDI.podid);
            GeneralFunctions.PopulateDropDownList(ddlTerminalOperator, EDIBLL.GetTerminalOperator(Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), podid));
            // txtArriveTime.Text = DateTime.Now.ToString("hh:mm");
            if (ddlTerminalOperator.Items.Count > 1)
            {
                ddlTerminalOperator.SelectedIndex = 1;
            }

        }

        private void CLearAll()
        {
            txtCallSign.Text = "";
            txtCargoDesc.Text = "";
            txtIGMNo.Text = "";
            txtIMONo.Text = "";
            //txtLastPort.Text = "";
            txtLightHouse.Text = "";
            txtMaster.Text = "";
            txtPAN.Text = "";
            txtShipCode.Text = "";
            txtTotLine.Text = "";
            txtVesselFlag.Text = "";
            txtdtArrival.Text = "";
            txtDtIGM.Text = "";



        }

        //protected void ddlCustomHouse_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GeneralFunctions.PopulateDropDownList(ddlTerminalOperator, dbinteract.PopulateDDLDS("mstTerminal", "TerminalName", "pk_TerminalID", "where TerminalName like '" + ddlCustomHouse.SelectedItem.Text + "%'"));
        //}

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            string pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            if (pod.Trim() == "")
            {
                GeneralFunctions.RegisterAlertScript(this, "Port of Discharge cannot be left blank");
                return;
            }
            if (ddlTerminalOperator.SelectedIndex == 0)
            {
                GeneralFunctions.RegisterAlertScript(this, "Terminal Operator cannot be left blank");
                return;
            }
            string FileName = EDI_TXT();
            DownLoadFile(FileName);
            File.Delete(FileName);
        }

        private void DownLoadFile(string FileName)
        {
            FileInfo file = new FileInfo(FileName);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AppendHeader("Content-Disposition", "attachment; filename = IMP_EDI.IGM");
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
            string ArrDate = txtdtArrival.Text.Trim() == "" ? "" : Convert.ToDateTime(txtdtArrival.Text).ToString("yyyyMMdd");
            string ArrTime = txtArriveTime.Text.Replace(":", ""); //Convert.ToDateTime(txtdtArrival.Text).ToString("hhmm");
            if (ArrTime.Length > 4) ArrTime = ArrTime.Substring(0, 4);
            ArrTime = string.IsNullOrEmpty(ArrTime.Trim()) ? "1200" : ArrTime;
            string ArrDate1 = (txtdtArrival.Text.Trim()) == "" ? "" : Convert.ToDateTime(txtdtArrival.Text).ToString("ddMMyyyy hh:mm");//.Replace(" ","");
            string lighthsdue = txtLightHouse.Text;// (txtLightHouse.Text.Trim() == "" || txtLightHouse.Text == "0") ? "+(''+" : txtLightHouse.Text;
            //string custHouse =  ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            // string custHouse = !((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text.Contains(',') ? "" : ((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text.Split(',')[1].Trim();
            string custHouse = ddlCustomHouse.SelectedItem.Text.Trim();
            string port1 = ((TextBox)AutoCompletepPort2.FindControl("txtPort")).Text;
            string port2 = ((TextBox)AutoCompletepPort3.FindControl("txtPort")).Text;
            string port3 = ((TextBox)AutoCompletepPort4.FindControl("txtPort")).Text;
            string pod = ((TextBox)AutoCompletepPort1.FindControl("txtPort")).Text;
            pod = pod.Contains(',') ? pod.Split(',')[1] : "";
            StreamWriter writer = new StreamWriter(FileName);
            //  ("myfile.txt")
            writer.WriteLine(("HREC" + ('' + ("ZZ" + (''
                            + (ICEGateLogin + ('' + ("ZZ" + (''
                            + (custHouse + ('' + ("ICES1_5" + ('' + ("P" + ('' + ('' + ("SACHI01" + ('' + ("786" + (''
                            + (ArrDate + ('' + ArrTime))))))))))))))))))))));
            writer.WriteLine("<manifest>");
            writer.WriteLine("<vesinfo>");
            writer.WriteLine(("F" + (''
                            + (custHouse + (''
                            + (txtIGMNo.Text + (''
                            + (txtDtIGM.Text.Replace(@"/", "") + (''
                            + (txtIMONo.Text + (''
                            + (txtCallSign.Text + (''
                            + (ddlVoyage.SelectedItem.Text + (''
                            + (txtShipCode.Text + (''
                            + (txtPAN.Text.Trim() + (''
                            + (txtMaster.Text.Trim() + (''
                            + (custHouse.Trim() + (''
                            + ((!port1.Contains(',') ? string.Empty : port1.Split(',')[1].Trim()) + (''
                            + ((!port2.Contains(',') ? string.Empty : port2.Split(',')[1].Trim()) + (''
                            + ((!port3.Contains(',') ? string.Empty : port3.Split(',')[1].Trim()) + (''
                            + (ddlVesselType.Text.Substring(0, 1) + (''
                            + (txtTotLine.Text + (''
                            + (txtCargoDesc.Text + (''
                            + (ArrDate1 + (''
                //+ (((double.Parse(txtLightHouse.Text) == 0) ? "" : double.Parse(txtLightHouse.Text).ToString()) + (''
                            //+ ((txtLightHouse.Text == "0" || txtLightHouse.Text.Trim() == "" ? (string.Empty + '' + string.Empty) : txtLightHouse.Text) + (''
                            + ((txtLightHouse.Text == "0" || txtLightHouse.Text.Trim() == "" ? (string.Empty) : txtLightHouse.Text) + (''
                            + (ddlSameButton.SelectedValue == "1" ? "Y" : "N") + (''
                            + (ddlShipStoreSubmitted.SelectedValue == "1" ? "Y" : "N") + (''
                            + (ddlCrewList.SelectedValue == "1" ? "Y" : "N") + (''
                            + (ddlPessengerList.SelectedValue == "1" ? "Y" : "N") + (''
                            + (ddlCrewEffList.SelectedValue == "1" ? "Y" : "N") + (''
                            + (ddlMaritime.SelectedValue == "1" ? "Y" : "N") + (''
                            + (ddlTerminalOperator.SelectedIndex == 0 ? "" : ddlTerminalOperator.SelectedItem.Text.Split('(')[0].Trim()))))))))))))))))))))))))))))))))))))))))))))));

            writer.WriteLine("<END-vesinfo>");
            writer.WriteLine("<cargo>");
            string sss = writer.ToNewString();
            int PortCode = dbinteract.GetId("Port", pod);

            DataSet Ds = EDIBLL.GetEDICargoInfo(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue), dbinteract.GetId("Port", pod));
            //             TextShippingCode.Text = Ds.Tables(0).Rows(0).Item("ShippingLineCode")
            DataTable Dt = Ds.Tables[0];
            int Srl = 0;
            string BlDate = ""; string Con1 = string.Empty; string Con2 = string.Empty; string Con3 = string.Empty; string Con4 = string.Empty;
            string Not1 = string.Empty; string Not2 = string.Empty; string Not3 = string.Empty; string Not4 = string.Empty;
            string Destport = string.Empty; string BLno = string.Empty; string DischargePort = string.Empty;


            foreach (DataRow Dr in Dt.Rows)
            {
                Srl = (Srl + 1);
                BlDate = Convert.ToDateTime(Dr["INFDATE"]).ToString("ddMMyyyy");
                int conLen = 35, notLen = 35;
                string conOri = Dr["ConsigneeInformation"].ToString();
                string notOri = Dr["NotifyPartyInformation"].ToString();

                if (conOri.Length > 0)
                    Con1 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
                else
                    Con1 = ".";

                conOri = conOri.Replace(Con1, "");
                if (conOri.Length > 0)
                    Con2 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
                else
                    Con2 = ".";

                conOri = conOri.Replace(Con2, "");
                if (conOri.Length > 0)
                    Con3 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
                else
                    Con3 = ".";

                conOri = conOri.Replace(Con3, "");
                if (conOri.Length > 0)
                    Con4 = ((conOri.Length >= conLen) ? conOri.Substring(0, 35) : conOri);
                else
                    Con4 = ".";


                if (notOri.Length > 0)
                    Not1 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
                else
                    Not1 = ".";

                notOri = notOri.Replace(Not1, "");
                if (notOri.Length > 0)
                    Not2 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
                else
                    Not2 = ".";

                notOri = notOri.Replace(Not2, "");
                if (notOri.Length > 0)
                    Not3 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
                else
                    Not3 = ".";

                notOri = notOri.Replace(Not3, "");
                if (notOri.Length > 0)
                    Not4 = ((notOri.Length >= notLen) ? notOri.Substring(0, 35) : notOri);
                else
                    Not4 = ".";


                Destport = ((Dr["DischargeIG"].ToString().Substring(0, 2) == "IN") ? ddlCustomHouse.SelectedItem.Text : Dr["DischargeIG"].ToString());
                BLno = System.Text.RegularExpressions.Regex.Replace(Dr["BLNUMBER"].ToString(), "[^\\w\\ ]", "").TrimEnd().Replace(" ", "").Replace(" ", "20");
                DischargePort = Dr["DischargeIG"].ToString().Split(',')[1].Trim();

                writer.WriteLine(("F" + (''
                                + (custHouse + (''
                                + (txtIMONo.Text + (''
                                + (txtCallSign.Text + (''
                                + (ddlVoyage.SelectedItem.Text + (''
                                + (txtIGMNo.Text + (''
                                + (txtDtIGM.Text.Replace(@"/", "") + (''
                                + (Dr["VgLineNo"].ToString() + ('' + ("0" + (''
                                + (BLno.Length >= 20 ? BLno.Substring(0, 20) : BLno + (''
                                + (BlDate + (''
                                + (Dr["LOADINGIG"].ToString().Split(',')[1].Trim() + (''
                                + (Destport.Split(',')[1].Trim() + ('' + ('' + (''
                                + (Con1 + (''
                                + (Con2 + (''
                                + (Con3 + (''
                                + (Con4 + (''
                                + (Not1 + (''
                                + (Not2 + (''
                                + (Not3 + (''
                                + (Not4 + (''
                                + (Dr["CARGONATURE"].ToString() + (''
                                + (Dr["ITEMTYPE"].ToString() + (''
                                + (Dr["CargoMovementCode"].ToString() + (''
    
                                //+ ((( (DischargePort.Length>=6 ? DischargePort.Substring(0, 6):DischargePort) == "INHAL1") ? DischargePort : string.Empty) + (''
                                //+ ((Dr["CargoMovementCode"].ToString()=="LC" ? Dr["CFSCode"].ToString() : "") + (''
                                + ((Dr["CargoMovementCode"].ToString()=="LC" ? Dr["CFSCode"].ToString() : "") + (''
//=======
//                    //+ ((( (DischargePort.Length>=6 ? DischargePort.Substring(0, 6):DischargePort) == "INHAL1") ? DischargePort : string.Empty) + (''
//                                + ((Dr["CargoMovementCode"].ToString() == "LC" ? Dr["CFSCode"].ToString() : "") + (''
//>>>>>>> .r912
                    //+ ((( (DischargePort.Length>=6 ? DischargePort.Substring(0, 6):DischargePort) == "INHAL1") ? DischargePort : string.Empty) + (''
                                //+ ((Dr["CargoMovementCode"].ToString() == "LC" ? Dr["CFSCode"].ToString() : "") + (''
                                + (Dr["NumberPackage"].ToString() + (''
                                + (Dr["PackingUnit"].ToString() + (''
                                + (((Convert.ToInt32(Dr["GrossWeight"]) > 0) ? (Dr["GrossWeight"].ToString() + ('' + (Dr["WTUnit"].ToString() + ''))) : (string.Empty + '' + string.Empty))
                                + (((Convert.ToInt32(Dr["Volume"]) > 0) ? (Dr["Volume"].ToString() + ('' + Dr["VolUnit"].ToString())) : (string.Empty + '' + string.Empty)) + (''
                                + ((Dr["MarksNumbers"].ToString().Length > 300 ? Dr["MarksNumbers"].ToString().Substring(0, 300) : Dr["MarksNumbers"].ToString()) + (''
                                + ((Dr["GoodDescription"].ToString().Length > 250 ? Dr["GoodDescription"].ToString().Substring(0, 250) : Dr["GoodDescription"].ToString()) + (''
                                + (Dr["UNOCode"].ToString() + (''
                                + (Dr["IMO_IMDGCode"].ToString() + (''
                                + (Dr["TPBondNo"].ToString() + (''
                                + (Dr["CACode"].ToString() + (''
                                + (Dr["TransportMode"].ToString() + ('' + Dr["MLOCode"].ToString()))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))));
                                //+ ((Dr["CargoMovementCode"].ToString() == "LC" ? "" : Dr["TransportMode"].ToString()) + ('' + Dr["MLOCode"].ToString()))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))));
                //
            }
            writer.WriteLine("<END-cargo>");


            writer.WriteLine("<contain>");

            DataSet DsContain = EDIBLL.GetEDIContainerInfo(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVoyage.SelectedValue));
            //             TextShippingCode.Text = Ds.Tables(0).Rows(0).Item("ShippingLineCode")
            DataTable DtContain = DsContain.Tables[0];
            int Srl1 = 0;


            foreach (DataRow Dr in DtContain.Rows)
            {
                Srl1 = (Srl1 + 1);
                writer.WriteLine(("F" + (''
                            + (custHouse + (''
                            + (txtIMONo.Text + (''
                            + (txtCallSign.Text + (''
                            + (ddlVoyage.SelectedItem.Text + (''
                            + (txtIGMNo.Text + (''
                            + (txtDtIGM.Text.Replace(@"/", "") + (''
                            + (Dr["VgLineNo"].ToString() + ('' + ("0" + (''
                    //+ (txtCallSign.Text + (''
                    //+ (ddlVoyage.SelectedItem.Text + (''
                    //+ (txtShipCode.Text + (''
                    //+ (txtPAN.Text + (''
                    //+ (txtMaster.Text + (''
                    //+ (custHouse + (''
                + (Dr["ContainerNo"].ToString() + (''
                + (Dr["SealNo"].ToString() + (''
                + (Dr["AgentCode"].ToString().Trim() + (''
                + (Dr["cstatus"].ToString() + (''
                + (Dr["NoofPackages"].ToString() + (''
                + (Dr["CargoWtTon"].ToString() + (''
                + (Dr["ISOCode"].ToString() + (''
                + (Dr["SOC"].ToString()))))))))))))))))))))))))))))))))));
            }
            writer.WriteLine("<END-contain>");
            writer.WriteLine("<END-manifest>");
            writer.WriteLine("TREC786");
            //EndUsing;

            writer.Close();
            writer.Dispose();

            return FileName;
        }

        protected void lnkLines_Click(object sender, EventArgs e)
        {
            mpeLine.Show();
        }
        
        protected void btnFillEdi_Click(object sender, EventArgs e)
        {
            LoadEDIList();
        }

        protected void SaveEDI(object sender, EventArgs e)
        {
            foreach (RepeaterItem Itm in rpEDI.Items)
            {
                if (Itm.ItemType == ListItemType.Item || Itm.ItemType == ListItemType.AlternatingItem)
                {
                    //EDIBLL.SaveEDINo();

                    HiddenField hdnLineId = (HiddenField)Itm.FindControl("hdnLineId");
                    TextBox txtTotalLine = (TextBox)Itm.FindControl("txtTotalLine");
                    DropDownList ddlSurveyor = (DropDownList)Itm.FindControl("ddlSurveyor");

                    if (String.IsNullOrEmpty(txtTotalLine.Text))
                        txtTotalLine.Text = "0";
                    EDIBLL.SaveEDINo(Convert.ToInt32(ddlVoyage.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(hdnPortId.Value), Convert.ToInt32(hdnLineId.Value), Convert.ToInt32(txtTotalLine.Text), Convert.ToInt32(ddlSurveyor.SelectedValue));
                }
            }
        }

        protected void rpEDI_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlSurveyor = (DropDownList)e.Item.FindControl("ddlSurveyor");
                HiddenField hdnSurveyor = (HiddenField)e.Item.FindControl("hdnSurveyor");
                LoadSurveyorDDL(ddlSurveyor, Convert.ToInt32(hdnSurveyor.Value));

                DataRowView oRow = (DataRowView)e.Item.DataItem;
                ddlSurveyor.SelectedValue = oRow["SURVEYORID"].ToString();
            }
        }
    }
}