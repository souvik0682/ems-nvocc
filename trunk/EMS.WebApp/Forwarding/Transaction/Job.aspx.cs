using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using System.Data;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Forwarding.Transaction
{
    public partial class Job : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        List<IBookingContainer> Containers = new List<IBookingContainer>();
        JobBLL oJobBll;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            

            if (!IsPostBack)
            {
                FillDebtors();
                FillJobType();
                FillShipmentMode();
                FillContainerType();

                //BLL.DBInteraction dbinteract = new BLL.DBInteraction();
                //GeneralFunctions.PopulateDropDownList(ddlCntrType, dbinteract.PopulateDDLDS("mstContainerType", "pk_ContainerTypeID", "ContainerAbbr"));

                if (!ReferenceEquals(Request.QueryString["JobId"], null))
                {
                    int JobId = 0;
                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["JobId"].ToString()), out JobId);

                    if (JobId > 0)
                    {
                        ViewState["JOBID"] = JobId;
                        ViewState["ISEDIT"] = "True";
                        //DataSet ds = new DataSet();
                        //BookingBLL oBookChgBLL = new BookingBLL();

                        //ds = oBookChgBLL.GetBookingChargesList(JobId);
                        //if (ds.Tables[0].Rows.Count > 0)
                        //    ViewState["ISEDIT"] = "True";
                        //else
                        //    ViewState["ISEDIT"] = "False";
                        ddlJobType.Enabled = false;
                        LoadJob();
                        FillJobContainer(JobId);
                    }
                    else
                    {
                        ViewState["JOBID"] = null;
                        ddlHblFormat.Enabled = false;
                        chkPrintHBL.Checked = false;
                        //ddlHblFormat.Enabled = false;
                        ddlCompany.SelectedValue = "1";
                        ViewState["ISEDIT"] = "False";
                        lnkContainerDtls.Enabled = false;
                    }
                }
                else
                {
                    ViewState["JOBID"] = null;
                    ddlHblFormat.Enabled = false;
                    chkPrintHBL.Checked = false;
                    //ddlHblFormat.Enabled = false;
                    ddlCompany.SelectedValue = "1";
                    lnkContainerDtls.Enabled = false;
                }
            }

            AC_Port2.TextChanged += new EventHandler(AC_Port2_TextChanged);
            AC_Port3.TextChanged += new EventHandler(AC_Port3_TextChanged);
        }

        void FillJobType()
        {
            DataTable JobType = new CommonBLL().GetfwdJobType();
            ddlJobType.DataSource = JobType;
            ddlJobType.DataTextField = "JobType";
            ddlJobType.DataValueField = "pk_JobTypeID";
            ddlJobType.DataBind();
            ddlJobType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        void FillDebtors()
        {
            DataTable principal = new CommonBLL().GetfwdPartyByType("D");
            ddlCustomer.DataSource = principal;
            ddlCustomer.DataTextField = "LineName";
            ddlCustomer.DataValueField = "LineID";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        void FillShipmentMode()
        {
            DataTable principal = new CommonBLL().GetfwdShipmentMode();
            ddlShipmentMode.DataSource = principal;
            ddlShipmentMode.DataTextField = "ModeName";
            ddlShipmentMode.DataValueField = "ModeId";
            ddlShipmentMode.DataBind();
            ddlShipmentMode.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        void FillContainerType()
        {
            DataTable JobType = new CommonBLL().GetfwdContainerType();
            ddlCntrType.DataSource = JobType;
            ddlCntrType.DataTextField = "UnitName";
            ddlCntrType.DataValueField = "pk_UnitTypeID";
            ddlCntrType.DataBind();
            ddlCntrType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        // PortOfDischarge

        void AC_Port3_TextChanged(object sender, EventArgs e)
        {
            string dischargePort = ((TextBox)AC_Port3.FindControl("txtPort")).Text;

            if (dischargePort != string.Empty)
            {
                if (dischargePort.Split('|').Length > 1)
                {

                    string portCode = dischargePort.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState["PORTOFDISCHARGEID"] = portId;

                    hdnPortDischarge.Value = dischargePort.Split('|')[0].Trim();
                }
                else
                {
                    ViewState["PORTOFDISCHARGEID"] = null;
                }
            }
            else
            {
                ViewState["PORTOFDISCHARGEID"] = null;
            }
        }

        //PortOfLoading
        void AC_Port2_TextChanged(object sender, EventArgs e)
        {
            string loadingPort = ((TextBox)AC_Port2.FindControl("txtPort")).Text;

            if (loadingPort != string.Empty)
            {
                if (loadingPort.Split('|').Length > 1)
                {
                    string portCode = loadingPort.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState["PORTOFLOADINGID"] = portId;

                    hdnPortLoading.Value = loadingPort.Split('|')[0].Trim();
                }
                else
                {
                    ViewState["PORTOFLOADINGID"] = null;
                }
            }
            else
            {
                ViewState["PORTOFLOADINGID"] = null;
            }
        }

        private void LoadJob()
        {
            SearchCriteria newcriteria = new SearchCriteria();
            SetDefaultSearchCriteria(newcriteria);

            IJob job = JobBLL.GetJobs(newcriteria, Convert.ToInt32(ViewState["JOBID"]), null).SingleOrDefault();
            DisplayJobType(job.JobTypeID);
            if (job.JobTypeID == 1 || job.JobTypeID == 2)
                lnkContainerDtls.Enabled = true;
            else
                lnkContainerDtls.Enabled = false;
            ddlJobType.SelectedValue = job.JobTypeID.ToString();
            ddlJobScope.SelectedValue = job.JobScopeID.ToString();
            txtJobDate.Text = job.JobDate.ToShortDateString();
            txtJobNo.Text = job.JobNo;
            ddlOpsControlled.SelectedValue = job.OpsLocID.ToString();
            ddlDocControlled.SelectedValue = job.jobLocID.ToString();
            ddlSalesControlled.SelectedValue = job.SalesmanID.ToString();
            ddlShipmentMode.SelectedValue = job.SmodeID.ToString();
            ddlShipmentMode_SelectedIndexChanged(null, null);
            ddlPrimeDocs.SelectedValue = job.PrDocID.ToString();

            txtGrossWeight.Text = job.grwt.ToString();
            txtVolumeWeight.Text = job.VolWt.ToString();
            txtMTWeight.Text = job.weightMT.ToString();
            txtCBMVolume.Text = job.volCBM.ToString();
            txtRevenue.Text = job.RevTon.ToString();
            txtPlaceReciept.Text = job.PlaceOfReceipt;
            txtCreditDays.Text = job.CreditDays.ToString();
            txtDocumentNo.Text = job.DocumentNo.ToString();
            txtJobNote1.Text = job.JobNote1.ToString();
            txtJobNote2.Text = job.JobNote2.ToString();
            txtVoyageNo.Text = job.Voyage.ToString();
            chkPrintHBL.Checked = job.PrintHBL;
            ddlHblFormat.SelectedValue = job.HBLFormatID.ToString();
            //Port of Discharge
            string dischargePort = new ImportBLL().GetPortNameById(job.fk_DportID);
            hdnPortDischarge.Value = dischargePort.Split('|')[0].Trim();
            ((TextBox)AC_Port3.FindControl("txtPort")).Text = dischargePort;

            //Port of Loading
            string loadingPort = new ImportBLL().GetPortNameById(job.fk_LportID);
            hdnPortLoading.Value = loadingPort.Split('|')[0].Trim();
            ((TextBox)AC_Port2.FindControl("txtPort")).Text = loadingPort;

            ViewState["PORTOFDISCHARGEID"] = job.fk_DportID;
            ViewState["PORTOFLOADINGID"] = job.fk_LportID;

            txtDelivery.Text = job.PlaceOfDelivery;
            ddlShippingLine.SelectedValue = job.FLineID.ToString();
            ddlCustomer.SelectedValue = job.fk_CustID.ToString();
            ddlCustomsAgent.SelectedValue = job.fk_CustAgentID.ToString();
            ddlTransporter.SelectedValue = job.fk_TransID.ToString();
            ddlOverseasAgent.SelectedValue = job.fk_OSID.ToString();
            ddlCargoSource.SelectedValue = job.CargoSource.ToString();
            ddlCompany.SelectedValue = job.CompID.ToString();
            if (job.JobActive.ToString() == "C")
                btnSave.Visible = false;

        }

        private void SetDefaultSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = "JobNo";
            string sortDirection = "ASC";

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.StringOption1 = "B";

        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
                }
            }
        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager)
                {

                    if (_canView == false)
                    {
                        Response.Redirect("~/Unauthorized.aspx");
                    }

                    if (_canAdd == false)
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

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (ddlJobType.SelectedIndex == 1 || ddlJobType.SelectedIndex == 2)
            {
                if (!string.IsNullOrEmpty(txtMTWeight.Text))
                {
                    if (Convert.ToDecimal(txtMTWeight.Text) == 0)
                    {
                        lblError.Text = "Weight in MT is compulsory";
                        return;
                    }
                    else
                    {
                        lblError.Text = "";
                    }
                }
                else
                {
                    lblError.Text = "Weight in MT is compulsory";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtGrossWeight.Text))
                {
                    if (Convert.ToDecimal(txtGrossWeight.Text) == 0)
                    {
                        lblError.Text = "Weight in KG is compulsory";
                        return;
                    }
                }
                else
                {
                    lblError.Text = "Weight in KG is compulsory";
                    return;
                }
                }
            IJob job = new JobEntity();

            if (ViewState["JOBID"] != null)
            {
                job.JobID = Convert.ToInt32(ViewState["JOBID"]);
                job.JobActive = 'E';
                job.JobNo = txtJobNo.Text;
            }
            else
            {
                job.JobActive = 'A';
            }

            job.JobTypeID = ddlJobType.SelectedValue.ToInt();
            job.JobScopeID = ddlJobScope.SelectedValue.ToInt();
            job.JobDate = Convert.ToDateTime(txtJobDate.Text);
            job.OpsLocID = ddlOpsControlled.SelectedValue.ToInt();
            job.jobLocID = ddlDocControlled.SelectedValue.ToInt();
            //job. = ddlDocControlled.SelectedValue.ToInt();
            job.SalesmanID = ddlSalesControlled.SelectedValue.ToInt();
            job.SmodeID = ddlShipmentMode.SelectedValue.ToInt();
            job.PrDocID = ddlPrimeDocs.SelectedValue.ToInt();
            job.PrintHBL = chkPrintHBL.Checked;
            job.HBLFormatID = ddlHblFormat.SelectedValue.ToInt();
            if (ddlJobType.SelectedIndex == 1 || ddlJobType.SelectedIndex == 2)
            {
                //job.ttl20 = Convert.ToInt32(txtTTL20.Text);
                //job.ttl40 = Convert.ToInt32(txtTTL40.Text);
                if (!string.IsNullOrEmpty(txtMTWeight.Text))
                    job.weightMT = Convert.ToDecimal(txtMTWeight.Text);
                else
                    job.weightMT = 0;

                if (!string.IsNullOrEmpty(txtCBMVolume.Text))
                    job.volCBM = Convert.ToDecimal(txtCBMVolume.Text);
                else
                    job.volCBM = 0;
                //job.RevTon = Convert.ToDecimal(txtRevenue.Text);
            }
            //else if (ddlJobType.SelectedIndex == 3 || ddlJobType.SelectedIndex == 5)
            //{
            //    if (!string.IsNullOrEmpty(txtMTWeight.Text))
            //        job.weightMT = Convert.ToDecimal(txtMTWeight.Text);
            //    else
            //        job.weightMT = 0;

            //    if (!string.IsNullOrEmpty(txtCBMVolume.Text))
            //        job.volCBM = Convert.ToDecimal(txtCBMVolume.Text);
            //    else
            //        job.volCBM = 0;

            //    if (!string.IsNullOrEmpty(txtRevenue.Text))
            //        job.RevTon = Convert.ToDecimal(txtRevenue.Text);
            //    else
            //        job.RevTon = 0;
            //}
            else if (ddlJobType.SelectedIndex == 3 || ddlJobType.SelectedIndex == 4)
            {
                if (!string.IsNullOrEmpty(txtGrossWeight.Text))
                    job.grwt = Convert.ToDecimal(txtGrossWeight.Text);
                else
                    job.grwt = 0;

                if (!string.IsNullOrEmpty(txtVolumeWeight.Text))
                    job.VolWt = Convert.ToDecimal(txtVolumeWeight.Text);
                else
                    job.VolWt = 0;
                //job.RevTon = Convert.ToDecimal(txtRevenue.Text);
            }

            job.PlaceOfReceipt = txtPlaceReciept.Text;
            //job.fk_LportID = ddlPortLoading.SelectedValue.ToInt();
            //job.fk_DportID = ddlPortDischarge.SelectedValue.ToInt();
            job.fk_DportID = Convert.ToInt32(ViewState["PORTOFDISCHARGEID"]);
            job.fk_LportID = Convert.ToInt32(ViewState["PORTOFLOADINGID"]);
            job.PlaceOfDelivery = txtDelivery.Text;
            job.FLineID = ddlShippingLine.SelectedValue.ToInt();
            job.fk_CustID = ddlCustomer.SelectedValue.ToInt();
            job.fk_CustAgentID = ddlCustomsAgent.SelectedValue.ToInt();
            job.fk_TransID = ddlTransporter.SelectedValue.ToInt();
            job.fk_OSID = ddlOverseasAgent.SelectedValue.ToInt();
            job.CargoSource = Convert.ToChar(ddlCargoSource.SelectedValue);
            job.CompID = ddlCompany.SelectedValue.ToInt();
            if (!string.IsNullOrEmpty(txtCreditDays.Text))
                job.CreditDays = Convert.ToInt32(txtCreditDays.Text);
            else
                job.CreditDays = 0;
            //job.CreditDays = Convert.ToInt32(txtCreditDays.Text);
            job.Voyage = txtVoyageNo.Text;
            //job.Vessel = txtVesselName.Text;
            job.DocumentNo = txtDocumentNo.Text;
            job.JobNote1 = txtJobNote1.Text;
            job.JobNote2 = txtJobNote2.Text;
            job.CreatedBy = _userId;

            int CompanyId = 1;
            int outJobId = 0;

            int Status = JobBLL.AddEditJob(job, CompanyId, ref outJobId);
            if (Status == 1)
            {
                oJobBll = new JobBLL();
                oJobBll.DeactivateAllContainersAgainstJobId(outJobId);
                AddContainers(outJobId);

            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Record saved successfully!'); window.location='" + string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.ServerVariables["HTTP_HOST"], (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath) + "/Forwarding/Transaction/JobList.aspx';", true);
        }

        private int AddContainers(int JobId)
        {
            if (ViewState["BookingCntr"] != null)
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];

            if (Containers.Count > 0)
            {
                foreach (BookingContainerEntity obj in Containers)
                {
                    oJobBll = new JobBLL();
                    //oBookingContainerEntity = new BookingContainerEntity();

                    obj.BookingID = JobId;
                    //oBookingContainerEntity.BookingContainerID = Convert.ToInt32(dt.Rows[i]["BookingContainerID"].ToString());
                    //oBookingContainerEntity.ContainerTypeID = Convert.ToInt32(dt.Rows[i]["ContainerTypeID"].ToString());
                    //oBookingContainerEntity.CntrSize = dt.Rows[i]["CntrSize"].ToString();
                    //oBookingContainerEntity.NoofContainers = Convert.ToInt32(dt.Rows[i]["NoofContainers"].ToString());
                    //oBookingContainerEntity.wtPerCntr = Convert.ToDecimal(dt.Rows[i]["wtPerCntr"].ToString());

                    int res = oJobBll.AddEditJobContainer(obj);
                    if (res != 1)
                        return res;
                }
                return 1;
            }
            return 1;
        }
        private bool ValidateSave()
        {
            bool IsValid = true;
            errPortOfLoading.Text = "";
            errPortOfDischarge.Text = "";

            if (Convert.ToString(ViewState["PORTOFLOADINGID"]) == string.Empty || Convert.ToString(ViewState["PORTOFLOADINGID"]) == "0")
            {
                IsValid = false;
                errPortOfLoading.Text = "This field is required";
            }

            if (Convert.ToString(ViewState["PORTOFDISCHARGEID"]) == string.Empty || Convert.ToString(ViewState["PORTOFDISCHARGEID"]) == "0")
            {
                IsValid = false;
                errPortOfDischarge.Text = "This field is required";
            }

            return IsValid;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Transaction/JobList.aspx");
        }

        protected void ddlOpsControlled_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkPrintHBL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrintHBL.Checked)
            {
                ddlHblFormat.Enabled = true;
                ListItem i = ddlHblFormat.Items.FindByValue("0");
                //i.Attributes.Add("style", "color:gray;");
                i.Attributes.Add("disabled", "true");
                i.Value = "-1";
                ddlHblFormat.SelectedIndex = 1;
            }
            else
            {
                ListItem i = ddlHblFormat.Items.FindByValue("-1");
                //i.Attributes.Add("style", "color:gray;");
                i.Attributes.Add("disabled", "false");
                i.Value = "0";
                ddlHblFormat.SelectedIndex = 0;
                ddlHblFormat.Enabled = false;
            }
        }

        void DisplayJobType(int typeID)
        {
            if (typeID == 1 || typeID == 2)
            {
                txtGrossWeight.Enabled = false;
                txtVolumeWeight.Enabled = false;
                rfvGrossWeight.Enabled = false;
                rfvVolumeWeight.Enabled = false;
                txtGrossWeight.Text = "";
                txtVolumeWeight.Text = "";

                txtMTWeight.Enabled = true;
                rfvMTWeight.Enabled = true;
                txtCBMVolume.Enabled = true;
                //rfvCBMVolume.Enabled = true;
                txtRevenue.Enabled = true;
                rfvRevenue.Enabled = true;
                lnkContainerDtls.Enabled = true;

                //txtTTL20.Enabled = true;
                //txtTTL40.Enabled = true;
                ddlShipmentMode.Enabled = true;
                ddlShipmentMode.Items[0].Enabled = true;
                ddlShipmentMode.Items[1].Enabled = true;
                ddlShipmentMode.Items[2].Enabled = true;
                ddlShipmentMode.Items[3].Enabled = true;
                ddlShipmentMode.Items[4].Enabled = false;
                ddlShipmentMode.Items[5].Enabled = true;

                //var principal = new CommonBLL().GetfwLineByType(new SearchCriteria { StringOption1 = "O" });
                DataTable principal = new CommonBLL().GetfwdPartyByTypeid(4);

                ddlShippingLine.DataSource = principal;
                ddlShippingLine.DataTextField = "LineName";
                ddlShippingLine.DataValueField = "LineID";
                ddlShippingLine.DataBind();
                ddlShippingLine.Items.Insert(0, new ListItem("--Select--", "0"));


                DataTable forwarder = new CommonBLL().GetfwdPartyByTypeid(22);

                ddlTransporter.DataSource = forwarder;
                ddlTransporter.DataTextField = "LineName";
                ddlTransporter.DataValueField = "LineID";
                ddlTransporter.DataBind();
                ddlTransporter.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                txtGrossWeight.Enabled = true;
                txtVolumeWeight.Enabled = true;
                rfvGrossWeight.Enabled = true;
                rfvVolumeWeight.Enabled = true;

                txtMTWeight.Enabled = false;
                rfvMTWeight.Enabled = false;
                txtCBMVolume.Enabled = false;
                //rfvCBMVolume.Enabled = false;
                txtRevenue.Enabled = false;
                rfvRevenue.Enabled = false;
                txtMTWeight.Text = "";
                txtCBMVolume.Text = "";
                txtCBMVolume.Text = "";
                lnkContainerDtls.Enabled = false;

                ddlShipmentMode.Items[0].Enabled = false;
                ddlShipmentMode.Items[1].Enabled = false;
                ddlShipmentMode.Items[2].Enabled = false;
                ddlShipmentMode.Items[3].Enabled = false;
                ddlShipmentMode.Items[4].Enabled = true;
                ddlShipmentMode.Items[5].Enabled = false;
                //ddlShipmentMode.SelectedIndex = 6;
                ddlShipmentMode.Enabled = false;

                DataTable principal = new CommonBLL().GetfwdPartyByTypeid(20);

                ddlShippingLine.DataSource = principal;
                ddlShippingLine.DataTextField = "LineName";
                ddlShippingLine.DataValueField = "LineID";
                ddlShippingLine.DataBind();
                ddlShippingLine.Items.Insert(0, new ListItem("--Select--", "0"));

                DataTable IATAAgent = new CommonBLL().GetfwdPartyByTypeid(19);

                ddlTransporter.DataSource = IATAAgent;
                ddlTransporter.DataTextField = "LineName";
                ddlTransporter.DataValueField = "LineID";
                ddlTransporter.DataBind();
                ddlTransporter.Items.Insert(0, new ListItem("--Select--", "0"));

                gvContainer.DataSource = null;


            }
        }

        protected void txtDelivery_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayJobType(ddlJobType.SelectedIndex);
            //if (ddlJobType.SelectedIndex == 1 || ddlJobType.SelectedIndex == 2)
            //{
            //    txtGrossWeight.Enabled = false;
            //    txtVolumeWeight.Enabled = false;
            //    rfvGrossWeight.Enabled = false;
            //    rfvVolumeWeight.Enabled = false;
            //    txtGrossWeight.Text = "";
            //    txtVolumeWeight.Text = "";

            //    txtMTWeight.Enabled = true;
            //    rfvMTWeight.Enabled = true;
            //    txtCBMVolume.Enabled = true;
            //    rfvCBMVolume.Enabled = true;
            //    txtRevenue.Enabled = true;
            //    rfvRevenue.Enabled = true;

            //    txtTTL20.Enabled = true;
            //    txtTTL40.Enabled = true;
            //    ddlShipmentMode.Enabled = true;
            //    ddlShipmentMode.Items[6].Enabled = false;
            //    ddlShipmentMode.Items[0].Enabled = true;
            //    ddlShipmentMode.Items[1].Enabled = true;
            //    ddlShipmentMode.Items[2].Enabled = true;
            //    ddlShipmentMode.Items[3].Enabled = true;
            //    ddlShipmentMode.Items[4].Enabled = true;
            //    ddlShipmentMode.Items[5].Enabled = true;

            //    var principal = new CommonBLL().GetfwLineByType(new SearchCriteria { StringOption1 = "O" });

            //    ddlShippingLine.DataSource = principal;
            //    ddlShippingLine.DataTextField = "LineName";
            //    ddlShippingLine.DataValueField = "LineID";
            //    ddlShippingLine.DataBind();
            //    ddlShippingLine.Items.Insert(0, new ListItem("--Select--", "0"));


            //}
            //else
            //{
            //    txtGrossWeight.Enabled = true;
            //    txtVolumeWeight.Enabled = true;
            //    rfvGrossWeight.Enabled = true;
            //    rfvVolumeWeight.Enabled = true;

            //    txtMTWeight.Enabled = false;
            //    rfvMTWeight.Enabled = false;
            //    txtCBMVolume.Enabled = false;
            //    rfvCBMVolume.Enabled = false;
            //    txtRevenue.Enabled = false;
            //    rfvRevenue.Enabled = false;
            //    txtMTWeight.Text = "";
            //    txtCBMVolume.Text = "";
            //    txtCBMVolume.Text = "";

            //    txtTTL20.Text = "";
            //    txtTTL40.Text = "";
            //    txtTTL20.Enabled = false;
            //    txtTTL40.Enabled = false;
            //    ddlShipmentMode.Items[0].Enabled = false;
            //    ddlShipmentMode.Items[1].Enabled = false;
            //    ddlShipmentMode.Items[2].Enabled = false;
            //    ddlShipmentMode.Items[3].Enabled = false;
            //    ddlShipmentMode.Items[4].Enabled = false;
            //    ddlShipmentMode.Items[5].Enabled = false;
            //    ddlShipmentMode.Items[6].Enabled = true;
            //    //ddlShipmentMode.SelectedIndex = 6;
            //    ddlShipmentMode.Enabled = false;

            //    DataTable principal = new CommonBLL().GetfwdPartyByType("A");

            //    ddlShippingLine.DataSource = principal;
            //    ddlShippingLine.DataTextField = "LineName";
            //    ddlShippingLine.DataValueField = "LineID";
            //    ddlShippingLine.DataBind();
            //    ddlShippingLine.Items.Insert(0, new ListItem("--Select--", "0"));



            //}
        }

        private void ResetContainer()
        {
            hdnBookingContainerID.Value = "0";
            hdnIndex.Value = string.Empty;
            ddlCntrType.SelectedIndex = 0;
            ddlSize.SelectedIndex = 0;
            txtNos.Text = string.Empty;
        }

        protected void gvContainer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RowIndex = row.RowIndex;

            if (e.CommandName == "EditGrid")
            {
                HiddenField gvhdnBookingContainerID = (HiddenField)gvContainer.Rows[RowIndex].FindControl("gvhdnBookingContainerID");
                HiddenField gvhdnContainerTypeId = (HiddenField)gvContainer.Rows[RowIndex].FindControl("gvhdnContainerTypeId");
                Label lblContainerSize = (Label)gvContainer.Rows[RowIndex].FindControl("lblContainerSize");
                Label lblUnit = (Label)gvContainer.Rows[RowIndex].FindControl("lblUnit");
                Label lblwtPerCont = (Label)gvContainer.Rows[RowIndex].FindControl("lblwtPerCont");

                hdnIndex.Value = RowIndex.ToString();
                hdnBookingContainerID.Value = gvhdnBookingContainerID.Value;
                ddlCntrType.SelectedValue = gvhdnContainerTypeId.Value;
                ddlSize.SelectedValue = lblContainerSize.Text;
                txtNos.Text = lblUnit.Text;
                //txtWtPerCntr.Text = lblwtPerCont.Text;
            }
            else if (e.CommandName == "Remove")
            {
                if (ViewState["BookingCntr"] != null)
                    Containers = (List<IBookingContainer>)ViewState["BookingCntr"];

                if (Containers.Count > 0)
                {
                    Containers.RemoveAt(RowIndex);

                    //foreach (BookingContainerEntity obj in Containers)
                    //{
                    //    _TotWeight = _TotWeight + obj.NoofContainers.ToDecimal() * obj.wtPerCntr.ToDecimal();
                    //}

                    //txtGrossWeight.Text = _TotWeight.ToString();

                    gvContainer.DataSource = Containers;
                    gvContainer.DataBind();

                    ViewState["BookingCntr"] = gvContainer.DataSource = Containers;

                }
            }
        }

        protected void gvContainer_DataBound(object sender, EventArgs e)
        {
            if (ViewState["BookingCntr"] != null)
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];

            txtContainerDtls.Text = string.Empty;

            if (Containers.Count > 0)
            {
                foreach (BookingContainerEntity obj in Containers)
                {
                    if (string.IsNullOrEmpty(txtContainerDtls.Text))
                        txtContainerDtls.Text = obj.NoofContainers.ToString() + " X " + obj.CntrSize + "'" + obj.ContainerType;
                    else
                        txtContainerDtls.Text += "," + obj.NoofContainers.ToString() + " X " + obj.CntrSize + "'" + obj.ContainerType;
                }
            }
        }

        protected void gvContainer_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");

                if (BookingBLL.GetBookingChargeExists(Convert.ToInt32(hdnJobID.Value)) != string.Empty)
                {
                    btnEdit.Visible = false;
                    btnRemove.Visible = false;
                }
            }
        }

        protected void lnkContainerDtls_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void btnimgSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["BookingCntr"] != null)
                Containers = (List<IBookingContainer>)ViewState["BookingCntr"];
            //IEnumerable<IBookingContainer> Rts = from IBookingContainer rt in Containers
            //                                     where rt.BkCntrStatus == true
            //                                     select rt;
            lblMessage.Text = string.Empty;

            IBookingContainer obj = new BookingContainerEntity();
            if (!string.IsNullOrEmpty(hdnIndex.Value))
                obj = Containers.ElementAt(Convert.ToInt32(hdnIndex.Value));
            else
            {
                foreach (BookingContainerEntity objBookingContainer in Containers)
                {
                    if (objBookingContainer.ContainerTypeID.ToString() == ddlCntrType.SelectedValue && objBookingContainer.CntrSize == ddlSize.SelectedValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00076") + "');</script>", false);
                        return;
                    }

                }
            }

            obj.BookingContainerID = Convert.ToInt32(hdnBookingContainerID.Value);
            obj.ContainerTypeID = Convert.ToInt32(ddlCntrType.SelectedValue);
            obj.ContainerType = ddlCntrType.SelectedItem.Text;
            obj.CntrSize = ddlSize.SelectedValue;
            obj.NoofContainers = Convert.ToInt32(txtNos.Text);

            if (string.IsNullOrEmpty(hdnIndex.Value))
                Containers.Add(obj);

            gvContainer.DataSource = Containers;
            gvContainer.DataBind();

            ViewState["BookingCntr"] = Containers;
            ResetContainer();
            //ModalPopupExtender1.Show();
        }

        protected void btnimgReset_Click(object sender, ImageClickEventArgs e)
        {

        }

        void FillJobContainer(Int32 JobID)
        {
            //oChargeRates = new List<ChargeRateEntity>();
            //oChargeRates = new List<ChargeRateEntity>();
            oJobBll = new JobBLL();
            Containers = new List<IBookingContainer>();
            Containers = oJobBll.GetJobContainers(JobID);

            ViewState["BookingCntr"] = Containers;

            gvContainer.DataSource = Containers;
            gvContainer.DataBind();
        }

        protected void ddlShipmentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShipmentMode.SelectedItem.ToString() == "FCL")
            {
                txtGrossWeight.Enabled = false;
                txtVolumeWeight.Enabled = false;
                txtMTWeight.Enabled = true;
                txtCBMVolume.Enabled = true;
                txtRevenue.Enabled = false;
                lnkContainerDtls.Enabled = true;
                rfvGrossWeight.Enabled = false;
                rfvMTWeight.Enabled = true;
                //rfvCBMVolume.Enabled = true;
                rfvVolumeWeight.Enabled = false;
                rfvRevenue.Enabled = false;
            }
            else if (ddlShipmentMode.SelectedItem.ToString() == "LCL")
            {
                txtGrossWeight.Enabled = false;
                txtVolumeWeight.Enabled = false;
                txtMTWeight.Enabled = true;
                txtCBMVolume.Enabled = true;
                txtRevenue.Enabled = false;
                lnkContainerDtls.Enabled = false;
                rfvGrossWeight.Enabled = false;
                rfvMTWeight.Enabled = true;
                //rfvCBMVolume.Enabled = true;
                rfvVolumeWeight.Enabled = false;
                rfvRevenue.Enabled = false;
            }
            else if (ddlShipmentMode.SelectedItem.ToString() == "AIR CONSOLE")
            {
                txtGrossWeight.Enabled = true;
                txtVolumeWeight.Enabled = true;
                txtMTWeight.Enabled = false;
                txtCBMVolume.Enabled = false;
                txtRevenue.Enabled = false;
                lnkContainerDtls.Enabled = false;
                rfvGrossWeight.Enabled = true;
                rfvMTWeight.Enabled = false;
                //rfvCBMVolume.Enabled = false;
                rfvVolumeWeight.Enabled = true;
                rfvRevenue.Enabled = false;
            }
            else if (ddlShipmentMode.SelectedItem.ToString() == "BREAK-BULK")
            {
                txtGrossWeight.Enabled = false;
                txtVolumeWeight.Enabled = false;
                txtMTWeight.Enabled = true;
                txtCBMVolume.Enabled = true;
                txtRevenue.Enabled = false;
                lnkContainerDtls.Enabled = false;
                rfvGrossWeight.Enabled = false;
                rfvMTWeight.Enabled = true;
                //rfvCBMVolume.Enabled = true;
                rfvVolumeWeight.Enabled = false;
                rfvRevenue.Enabled = false;
            }
            else if (ddlShipmentMode.SelectedItem.ToString() == "PROJECT")
            {
                txtGrossWeight.Enabled = false;
                txtVolumeWeight.Enabled = false;
                txtMTWeight.Enabled = true;
                txtCBMVolume.Enabled = true;
                txtRevenue.Enabled = false;
                lnkContainerDtls.Enabled = true;
                rfvGrossWeight.Enabled = false;
                rfvMTWeight.Enabled = true;
                //rfvCBMVolume.Enabled = true;
                rfvVolumeWeight.Enabled = false;
                rfvRevenue.Enabled = false;
            }
        }

        protected void txtMTWeight_TextChanged(object sender, EventArgs e)
        {
            if (ddlShipmentMode.Text == "BREAK-BULK" || ddlShipmentMode.Text == "LCL")
            {
                if (txtMTWeight.Text.ToDecimal() > txtCBMVolume.Text.ToDecimal())
                    txtRevenue.Text = txtMTWeight.Text;
                else
                    txtRevenue.Text = txtVolumeWeight.Text;
            }
        }

        protected void txtCBMVolume_TextChanged(object sender, EventArgs e)
        {
            if (ddlShipmentMode.Text == "BREAK-BULK" || ddlShipmentMode.Text == "LCL")
            {
                if (txtMTWeight.Text.ToDecimal() > txtCBMVolume.Text.ToDecimal())
                    txtRevenue.Text = txtMTWeight.Text;
                else
                    txtRevenue.Text = txtVolumeWeight.Text;
            }

        }

    }
}