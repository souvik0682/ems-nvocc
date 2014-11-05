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

namespace EMS.WebApp.Forwarding.Transaction
{
    public partial class Job : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();

            if (!IsPostBack)
            {
                if (!ReferenceEquals(Request.QueryString["JobId"], null))
                {
                    int JobId = 0;
                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["JobId"].ToString()), out JobId);

                    if (JobId > 0)
                    {
                        ViewState["JOBID"] = JobId;

                        DataSet ds = new DataSet();
                        BookingBLL oBookChgBLL = new BookingBLL();

                        ds = oBookChgBLL.GetBookingChargesList(JobId);
                        if (ds.Tables[0].Rows.Count > 0)
                            ViewState["ISEDIT"] = "True";
                        else
                            ViewState["ISEDIT"] = "False";

                        LoadJob();
                    }
                    else
                    {
                        ViewState["JOBID"] = null;
                        ddlHblFormat.Enabled = false;
                    }
                }
            }

            AC_Port2.TextChanged += new EventHandler(AC_Port2_TextChanged);
            AC_Port3.TextChanged += new EventHandler(AC_Port3_TextChanged);
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
            IJob job = JobBLL.GetJobs(null, Convert.ToInt32(ViewState["JOBID"]), null).SingleOrDefault();
            ddlJobType.SelectedValue = job.JobTypeID.ToString();
            ddlJobScope.SelectedValue = job.JobScopeID.ToString();
            txtJobDate.Text = job.JobDate.ToShortDateString();
            txtJobNo.Text = job.JobNo;
            ddlOpsControlled.SelectedValue = job.OpsLocID.ToString();
            //ddlDocControlled.SelectedValue = job.
            ddlSalesControlled.SelectedValue = job.SalesmanID.ToString();
            ddlShipmentMode.SelectedValue = job.SmodeID.ToString();
            ddlPrimeDocs.SelectedValue = job.PrDocID.ToString();
            txtTTL20.Text = job.ttl20.ToString();
            txtTTL40.Text = job.ttl40.ToString();
            txtGrossWeight.Text = job.grwt.ToString();
            txtVolumeWeight.Text = job.VolWt.ToString();
            txtMTWeight.Text = job.weightMT.ToString();
            txtCBMVolume.Text = job.volCBM.ToString();
            txtRevenue.Text = job.RevTon.ToString();
            txtPlaceReciept.Text = job.PlaceOfReceipt;
            //ddlPortLoading.SelectedValue = job.fk_LportID.ToString();
            //ddlPortDischarge.SelectedValue = job.fk_DportID.ToString();

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
            IJob job = new JobEntity();

            if (ViewState["ISEDIT"] == "True")
            {
                job.JobID = Convert.ToInt32(ViewState["JOBID"]);
                job.JobActive = 'E';
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
            if (ddlJobType.SelectedIndex == 0 || ddlJobType.SelectedIndex == 1)
            {
                job.ttl20 = Convert.ToInt32(txtTTL20.Text);
                job.ttl40 = Convert.ToInt32(txtTTL40.Text);
                job.weightMT = Convert.ToDecimal(txtMTWeight.Text);
                job.volCBM = Convert.ToDecimal(txtCBMVolume.Text);
                job.RevTon = Convert.ToDecimal(txtRevenue.Text);
            }
            else
            {
                job.grwt = Convert.ToDecimal(txtGrossWeight.Text);
                job.VolWt = Convert.ToDecimal(txtVolumeWeight.Text);
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
            job.CreditDays = Convert.ToInt32(txtCreditDays.Text);

            int CompanyId = 1;
            int jobId = JobBLL.AddEditJob(job, CompanyId);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Record saved successfully!'); window.location='" + string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.ServerVariables["HTTP_HOST"], (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath) + "/Forwarding/Transaction/JobList.aspx';", true);
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

        protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        protected void ddlJobType_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlJobType.SelectedIndex == 0 || ddlJobType.SelectedIndex == 1)
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
                rfvCBMVolume.Enabled = true;
                txtRevenue.Enabled = true;
                rfvRevenue.Enabled = true;

                txtTTL20.Enabled = true;
                txtTTL40.Enabled = true;

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
                rfvCBMVolume.Enabled = false;
                txtRevenue.Enabled = false;
                rfvRevenue.Enabled = false;
                txtMTWeight.Text = "";
                txtCBMVolume.Text = "";
                txtCBMVolume.Text = "";

                txtTTL20.Text = "";
                txtTTL40.Text = "";
                txtTTL20.Enabled = false;
                txtTTL40.Enabled = false;
            }
        }
    }
}