using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Utilities;
using EMS.Entity;
using EMS.Utilities.ResourceManager;
using EMS.Common;
using System.Data;
using System.Text;

namespace EMS.WebApp.Export
{
    public partial class ContainerTranshipmentEntry : System.Web.UI.Page
    {
        private int _userId = 0;
        DataTable dtFilteredContainer = new DataTable();
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            if (!Page.IsPostBack)
            {
                CheckUserAccess();
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

                btnSave.Visible = true;

                if (!_canAdd && !_canEdit)
                    btnSave.Visible = false;
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void txtExpBL_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnExpBL.Value.Trim()))
            {
                DataSet ds = new TranshipmentDetailsBLL().GetTranshipmentHeader(Convert.ToInt32(hdnExpBL.Value.Trim()));

                lblBLDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpBLDate"]).ToString("dd/MM/yyyy");
                hdnBookingId.Value = ds.Tables[0].Rows[0]["fk_BookingID"].ToString();
                lblBookingNo.Text = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                hdnVessel.Value = ds.Tables[0].Rows[0]["fk_VesselID"].ToString();
                txtVessel.Text = ds.Tables[0].Rows[0]["VesselName"].ToString();
                //hdnVoyageId.Value = ds.Tables[0].Rows[0]["fk_VoyageID"].ToString();
                //txtVoyage.Text = ds.Tables[0].Rows[0]["VoyageNo"].ToString();
                PopulateVoyage(Convert.ToInt32(hdnVessel.Value.Trim()));
                ddlVoyage.SelectedValue = ds.Tables[0].Rows[0]["fk_VoyageID"].ToString();

                PopulatePort(Convert.ToInt32(hdnBookingId.Value.Trim()));
            }
        }

        protected void txtVessel_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVessel.Text.Trim()))
                ddlVoyage.Items.Add(new ListItem("--Select--", "0"));
            else
                PopulateVoyage(Convert.ToInt32(hdnVessel.Value.Trim()));
        }

        private void PopulateVoyage(int vesselID)
        {
            //BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            DataSet ds = TranshipmentDetailsBLL.GetExportVoyages(vesselID);
            ddlVoyage.DataValueField = "VoyageID";
            ddlVoyage.DataTextField = "VoyageNo";
            ddlVoyage.DataSource = ds;
            ddlVoyage.DataBind();
            //ddlLoadingVoyage.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        private void PopulatePort(int bookingID)
        {
            TranshipmentDetailsBLL objBLL = new TranshipmentDetailsBLL();
            DataSet ds = objBLL.GetBookingTranshipment(bookingID);
            ddlPortName.DataValueField = "fk_PortID";
            ddlPortName.DataTextField = "PortName";
            ddlPortName.DataSource = ds;
            ddlPortName.DataBind();
            ddlPortName.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            TranshipmentDetailsBLL objBLL = new TranshipmentDetailsBLL();
            DataSet ds = objBLL.GetContainerTranshipment(Convert.ToInt32(ddlPortName.SelectedValue.Trim()), Convert.ToInt32(hdnExpBL.Value.Trim()), Convert.ToInt32(hdnBookingId.Value.Trim()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = "";
                gvContainer.DataSource = ds;
                gvContainer.DataBind();
            }
            else
            {
                lblMessage.Text = "Invalid Port code";
                return;
            }

            List<TranshipmentDetails> objTrans = new List<TranshipmentDetails>();
            TranshipmentDetails objTran = new TranshipmentDetails();
            objTran.PortId = Convert.ToInt64(ddlPortName.SelectedValue.Trim());
            objTran.ExportBLId = Convert.ToInt64(hdnExpBL.Value.Trim());
            objTran.ActualArrival = Convert.ToDateTime(txtDateofArrival.Text.Trim());
            objTran.ActualDeparture = Convert.ToDateTime(txtDateofDeparture.Text.Trim());

            objTrans.Add(objTran);
            ViewState["Data"] = objTrans;

            ModalPopupExtender1.Show();
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<TranshipmentDetails> obj = (List<TranshipmentDetails>)ViewState["Data"];
            foreach (TranshipmentDetails td in obj)
            {
                TranshipmentDetailsBLL objBL = new TranshipmentDetailsBLL();
                objBL.SaveContainerTranshipment(td);
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');</script>", false);
            ResetControls();
        }

        private void ResetControls()
        {
            hdnExpBL.Value = string.Empty;
            txtExpBL.Text = string.Empty;
            lblBLDate.Text = string.Empty;
            ddlPortName.Items.Clear();
            ddlPortName.Items.Add(new ListItem("--Select--", "0"));
            hdnBookingId.Value = string.Empty;
            lblBookingNo.Text = string.Empty;
            hdnVessel.Value = string.Empty;
            txtVessel.Text = string.Empty;
            ddlVoyage.Items.Clear();
            ddlVoyage.Items.Add(new ListItem("--Select--", "0"));
            txtDateofArrival.Text = string.Empty;
            txtDateofDeparture.Text = string.Empty;
            List<TranshipmentDetails> objTrans = new List<TranshipmentDetails>();
            ViewState["Data"] = objTrans;
            gvContainer.DataSource = objTrans;
            gvContainer.DataBind();
            gvSelectedContainer.DataSource = objTrans;
            gvSelectedContainer.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Export/ContainerTranshipment.aspx");
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            List<TranshipmentDetails> objTrans = new List<TranshipmentDetails>();
            foreach (GridViewRow gvr in gvContainer.Rows)
            {
                HiddenField hdnImpFooterId = (HiddenField)gvr.FindControl("hdnImpFooterId");
                HiddenField hdnContainerId = (HiddenField)gvr.FindControl("hdnContainerId");
                HiddenField hdnTranshipmentId = (HiddenField)gvr.FindControl("hdnTranshipmentId");
                HiddenField hdnExpBLContainerID = (HiddenField)gvr.FindControl("hdnExpBLContainerID");
                Label lblContainerNo = (Label)gvr.FindControl("lblContainerNo");
                Label lblContainerSize = (Label)gvr.FindControl("lblContainerSize");
                Label lblContainerType = (Label)gvr.FindControl("lblContainerType");
                CheckBox chkContainer = (CheckBox)gvr.FindControl("chkContainer");

                if (chkContainer.Checked)
                {
                    TranshipmentDetails objTran = new TranshipmentDetails();
                    objTran.PortId = Convert.ToInt64(ddlPortName.SelectedValue.Trim());
                    objTran.ExportBLId = Convert.ToInt64(hdnExpBL.Value.Trim());
                    objTran.ImportBLFooterId = Convert.ToInt64(hdnImpFooterId.Value.Trim());
                    objTran.HireContainerId = Convert.ToInt64(hdnContainerId.Value.Trim());
                    objTran.TranshipmentId = Convert.ToInt64(hdnTranshipmentId.Value.Trim());
                    objTran.ActualArrival = Convert.ToDateTime(txtDateofArrival.Text.Trim());
                    objTran.ActualDeparture = Convert.ToDateTime(txtDateofDeparture.Text.Trim());
                    objTran.ContainerNo = lblContainerNo.Text.Trim();
                    objTran.ContainerType = lblContainerType.Text.Trim();
                    objTran.Size = lblContainerSize.Text.Trim();
                    objTran.ExpBLContainerID = Convert.ToInt64(hdnExpBLContainerID.Value.Trim());

                    objTrans.Add(objTran);
                }
            }
            ViewState["Data"] = objTrans;
            gvSelectedContainer.DataSource = objTrans;
            gvSelectedContainer.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void gvContainer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkContainer = (CheckBox)e.Row.FindControl("chkContainer");
                HiddenField hdnContainerId = (HiddenField)e.Row.FindControl("hdnContainerId");

                foreach (GridViewRow gvr in gvSelectedContainer.Rows)
                {
                    HiddenField hdnContainerId1 = (HiddenField)gvr.FindControl("hdnContainerId");
                    if (hdnContainerId.Value == hdnContainerId1.Value)
                        chkContainer.Checked = true;
                }
            }
        }
    }
}