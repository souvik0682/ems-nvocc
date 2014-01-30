using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.BLL;
using EMS.Common;
using System.Data;
using EMS.Entity;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Export
{
    public partial class BookingCharges : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            RetriveParameters();
            CheckUserAccess();
            */
            RetriveParameters();
            if (!IsPostBack)
            {
                if (!ReferenceEquals(Request.QueryString["BookingId"], null))
                {
                    int BookingId = 0;
                    Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["BookingId"].ToString()), out BookingId);
                    CheckUserAccess(BookingId);
                    //BookingId = 1; //For testing

                    if (BookingId > 0)
                    {
                        ViewState["BOOKINGID"] = BookingId;

                        DataSet ds = new DataSet();
                        BookingBLL oBookChgBLL = new BookingBLL();

                        ds = oBookChgBLL.GetBookingChargesList(BookingId);
                        if (ds.Tables[0].Rows.Count > 0)
                            ViewState["ISEDIT"] = "True";
                        else
                            ViewState["ISEDIT"] = "False";


                        (txtBrokeragePayableTo.FindControl("txtBrockerage") as TextBox).Enabled = false;
                        ((TextBox)txtRefundPayableTo.FindControl("txtRefund")).Enabled = false;

                        LoadSlotOperatorsDDL();
                       
                        LoadBookingHeader();
                        LoadChargeDetails();
                    }
                    else
                    {
                        ViewState["BOOKINGID"] = null;
                        ((TextBox)txtRefundPayableTo.FindControl("txtRefund")).Enabled = false;
                    }
                }
            }
            txtBrokeragePayableTo.TextChanged += new EventHandler(txtBrokeragePayableTo_TextChanged);
            txtRefundPayableTo.TextChanged += new EventHandler(txtRefundPayableTo_TextChanged);
            txtFreightPayableAt.TextChanged += new EventHandler(txtFreightPayableAt_TextChanged);
        }

        void txtFreightPayableAt_TextChanged(object sender, EventArgs e)
        {
            string frtPayable = ((TextBox)txtFreightPayableAt.FindControl("txtPort")).Text;

            if (frtPayable != string.Empty)
            {
                if (frtPayable.Split('|').Length > 1)
                {
                    string portCode = frtPayable.Split('|')[1].Trim();
                    int portId = new ImportBLL().GetPortId(portCode);
                    ViewState["FRIEGHTPAYABLEATID"] = portId;
                }
                else
                {
                    ViewState["FRIEGHTPAYABLEATID"] = null;
                }
            }
            else
            {
                ViewState["FRIEGHTPAYABLEATID"] = null;
            }
        }

        void txtRefundPayableTo_TextChanged(object sender, EventArgs e)
        {
            string payableTo = ((TextBox)txtRefundPayableTo.FindControl("txtRefund")).Text.Trim();

            if (payableTo != string.Empty)
            {
                string payableToId = BookingBLL.GetBrokeragePayableId(payableTo);

                if (!ReferenceEquals(payableToId, null))
                    ViewState["REFUNDPAYABLEID"] = payableToId;
                else
                    ViewState["REFUNDPAYABLEID"] = null;
            }
            else
            {
                ViewState["REFUNDPAYABLEID"] = null;
            }
        }

        void txtBrokeragePayableTo_TextChanged(object sender, EventArgs e)
        {
            string payableTo = ((TextBox)txtBrokeragePayableTo.FindControl("txtBrockerage")).Text.Trim();

            if (payableTo != string.Empty)
            {
                string payableToId = BookingBLL.GetBrokeragePayableId(payableTo);

                if (!ReferenceEquals(payableToId, null))
                    ViewState["BROKERAGEPAYABLEID"] = payableToId;
                else
                    ViewState["BROKERAGEPAYABLEID"] = null;
            }
            else
            {
                ViewState["BROKERAGEPAYABLEID"] = null;
            }
        }

        protected void rdblRefundPayable_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IBookingCharges> lstData = new List<IBookingCharges>();
            int totalRows = gvwCharges.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwCharges.Rows[r];
                HiddenField hdnBookingChargeId = (HiddenField)thisGridViewRow.FindControl("hdnBookingChargeId");

                //DropDownList ddlApplicable = (DropDownList)thisGridViewRow.FindControl("ddlApplicable");
                TextBox txtCharged = (TextBox)thisGridViewRow.FindControl("txtCharged");
                TextBox txtRefund = (TextBox)thisGridViewRow.FindControl("txtRefund");
                TextBox txtBrokerageBasic = (TextBox)thisGridViewRow.FindControl("txtBrokerageBasic");
                TextBox txtManifest = (TextBox)thisGridViewRow.FindControl("txtManifest");

                lstData = ViewState["DataSource"] as List<IBookingCharges>;
                lstData.Where(d => d.BookingChargeId == Convert.ToInt32(hdnBookingChargeId.Value))
                    .Select(d =>
                    {
                        //d.ChargeApplicable = Convert.ToBoolean(ddlApplicable.SelectedValue);
                        d.ManifestRate = Convert.ToDecimal(txtManifest.Text);
                        d.ActualRate = Convert.ToDecimal(txtCharged.Text);
                        d.RefundAmount = 0;
                        d.BrokerageBasic = Convert.ToDecimal(txtBrokerageBasic.Text);
                        return d;
                    }).ToList();
            }
            ViewState["DataSource"] = lstData;

            if (rdblRefundPayable.SelectedValue == "True")
            {
                ((TextBox)txtRefundPayableTo.FindControl("txtRefund")).Enabled = true;
                LoadChargeDetails();
            }
            else
            {
                ((TextBox)txtRefundPayableTo.FindControl("txtRefund")).Enabled = false;
                LoadChargeDetails();
            }
        }

        protected void rdblBorkerage_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<IBookingCharges> lstData = new List<IBookingCharges>();
            int totalRows = gvwCharges.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwCharges.Rows[r];
                HiddenField hdnBookingChargeId = (HiddenField)thisGridViewRow.FindControl("hdnBookingChargeId");

                //DropDownList ddlApplicable = (DropDownList)thisGridViewRow.FindControl("ddlApplicable");
                TextBox txtCharged = (TextBox)thisGridViewRow.FindControl("txtCharged");
                TextBox txtRefund = (TextBox)thisGridViewRow.FindControl("txtRefund");
                TextBox txtBrokerageBasic = (TextBox)thisGridViewRow.FindControl("txtBrokerageBasic");
                TextBox txtManifest = (TextBox)thisGridViewRow.FindControl("txtManifest");

                lstData = ViewState["DataSource"] as List<IBookingCharges>;
                lstData.Where(d => d.BookingChargeId == Convert.ToInt32(hdnBookingChargeId.Value))
                    .Select(d =>
                    {
                        //d.ChargeApplicable = Convert.ToBoolean(ddlApplicable.SelectedValue);
                        d.ManifestRate = Convert.ToDecimal(txtManifest.Text);
                        d.ActualRate = Convert.ToDecimal(txtCharged.Text);
                        d.RefundAmount = Convert.ToDecimal(txtRefund.Text); ;
                        d.BrokerageBasic = 0;
                        return d;
                    }).ToList();
            }
            ViewState["DataSource"] = lstData;

            if (rdblBorkerage.SelectedValue == "True")
            {
                (txtBrokeragePayableTo.FindControl("txtBrockerage") as TextBox).Enabled = true;
                txtBrokeragePercent.Enabled = true;
                LoadChargeDetails();
            }
            else
            {
                (txtBrokeragePayableTo.FindControl("txtBrockerage") as TextBox).Enabled = false;
                txtBrokeragePercent.Enabled = false;
                LoadChargeDetails();
            }
        }

        //Do Not Remove this section. May require later.
        //protected void btnRemove_Click(object sender, EventArgs e)
        //{
        //    ImageButton txtRemove = (ImageButton)sender;
        //    GridViewRow gvChargesRow = (GridViewRow)txtRemove.Parent.Parent;
        //    HiddenField hdnBookingChargeId = gvChargesRow.FindControl("hdnBookingChargeId") as HiddenField;

        //    List<IBookingCharges> lstData = ViewState["DataSource"] as List<IBookingCharges>;

        //    lstData.Where(d => d.BookingChargeId == Convert.ToInt64(hdnBookingChargeId.Value))
        //            .Select(d =>
        //            {
        //                d.ChargeStatus = false;
        //                return d;
        //            }).ToList();


        //    ViewState["DataSource"] = lstData;
        //    LoadChargeDetails();
        //}
        

        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;

            if (thisTextBox.ID == "txtCharged")
            {
            }
            else if (thisTextBox.ID == "txtRefund")
            {
            }
            else
            {
                //Brokerage Basic
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            //UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess(Int32 BookingID)
        {
            // check for invoice existance
            if (BookingBLL.GetExpInvoiceExists(BookingID) == true)
            {
                btnSave.Visible = false;
                btnDel.Visible = false;
            }
            else
            {
                btnSave.Visible = true;
                btnDel.Visible = true;
            }

            //if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            //{
            //    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

            //    if (ReferenceEquals(user, null) || user.Id == 0)
            //    {
            //        Response.Redirect("~/Login.aspx");
            //    }

            //    if (_canView == false)
            //    {
            //        Response.Redirect("~/Unauthorized.aspx");
            //    }

            //    if (user.UserRole.Id != (int)UserRole.Admin)
            //    {
            //        //ddlLocation.Enabled = false;
            //    }
            //    else
            //    {
            //        //ddlLocation.Enabled = true;
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Login.aspx");
            //}
        }

        private void SaveBookingHeaderDetails(long BookingId)
        {
            IBooking objBooking = new BookingEntity();
            if (Convert.ToInt32(ViewState["FRIEGHTPAYABLEATID"]) == 0)
            {
                lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00085");
                return;
            }
            objBooking.BookingID = BookingId;
            objBooking.FreightPayableId = Convert.ToInt32(ViewState["FRIEGHTPAYABLEATID"]);
            objBooking.BrokeragePayable = Convert.ToBoolean(rdblBorkerage.SelectedValue);
            objBooking.BrokeragePercentage = Convert.ToDecimal(txtBrokeragePercent.Text);
            objBooking.BrokeragePayableId = Convert.ToInt32(ViewState["BROKERAGEPAYABLEID"]);
            objBooking.RefundPayable = Convert.ToBoolean(rdblRefundPayable.SelectedValue);
            objBooking.RefundPayableId = Convert.ToInt32(ViewState["REFUNDPAYABLEID"]);
            objBooking.ExportRemarks = txtRemarks.Text.Trim();
            objBooking.RateReference = txtRateReference.Text.Trim();
            objBooking.RateType = ddlRateType.SelectedValue;
            objBooking.UploadPath = hdnFilePath.Value;
            objBooking.SlotOperatorId = Convert.ToInt32(ddlSlot.SelectedValue);
            objBooking.Shipper = txtShipper.Text.Trim();
            objBooking.ModifiedBy = _userId;
            objBooking.PpCc = ddlPpCc.SelectedValue;
            objBooking.ModifiedOn = DateTime.Now;

            BookingBLL.UpdateBooking(objBooking);
        }

        private void SaveChargesDetails(long BookingId)
        {
            List<IBookingCharges> lstData = new List<IBookingCharges>();

            int totalRows = gvwCharges.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwCharges.Rows[r];
                HiddenField hdnBookingChargeId = (HiddenField)thisGridViewRow.FindControl("hdnBookingChargeId");

                //DropDownList ddlApplicable = (DropDownList)thisGridViewRow.FindControl("ddlApplicable");
                TextBox txtCharged = (TextBox)thisGridViewRow.FindControl("txtCharged");
                TextBox txtRefund = (TextBox)thisGridViewRow.FindControl("txtRefund");
                TextBox txtBrokerageBasic = (TextBox)thisGridViewRow.FindControl("txtBrokerageBasic");
                TextBox txtManifest = (TextBox)thisGridViewRow.FindControl("txtManifest");

                lstData = ViewState["DataSource"] as List<IBookingCharges>;
                lstData.Where(d => d.BookingChargeId == Convert.ToInt32(hdnBookingChargeId.Value))
                    .Select(d =>
                    {
                        d.BookingId = BookingId;
                        //d.ChargeApplicable = Convert.ToBoolean(ddlApplicable.SelectedValue);
                        d.ManifestRate = Convert.ToDecimal(txtManifest.Text);
                        d.ActualRate = Convert.ToDecimal(txtCharged.Text);
                        d.RefundAmount = Convert.ToDecimal(txtRefund.Text);
                        d.BrokerageBasic = Convert.ToDecimal(txtBrokerageBasic.Text);
                        return d;
                    }).ToList();
            }

            if (!Convert.ToBoolean(ViewState["ISEDIT"]))
                BookingBLL.InsertBookingCharges(lstData);
            else
                BookingBLL.UpdateBookingCharges(lstData);

            ViewState["DataSource"] = lstData;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(ViewState["BOOKINGID"], null))
            {
                int BookingId = Convert.ToInt32(ViewState["BOOKINGID"]);
              
                if (BookingId > 0)
                {
                    if (CheckValidity())
                    {
                        SaveBookingHeaderDetails(BookingId);
                        SaveChargesDetails(BookingId);
                    }
                    else
                    {
                        //lblMessage.Text = "Check Amout. Should have a positive Value";
                        return;
                    }
                }
                Response.Redirect("Booking.aspx");
            }

            //Save Booking Charges
            List<IBookingCharges> lstData = ViewState["DataSource"] as List<IBookingCharges>;

        }

        private void LoadBookingHeader()
        {
            if (!ReferenceEquals(ViewState["BOOKINGID"], null))
            {
                int bookingId = Convert.ToInt32(ViewState["BOOKINGID"]);

                if (bookingId > 0)
                {
                    //Get Booking Header
                    rdblRefundPayable.SelectedIndexChanged -= this.rdblRefundPayable_SelectedIndexChanged; // new EventHandler(rdblRefundPayable_SelectedIndexChanged);
                    rdblBorkerage.SelectedIndexChanged -= this.rdblBorkerage_SelectedIndexChanged; // new EventHandler(rdblRefundPayable_SelectedIndexChanged);
                    IBooking objBooking = BookingBLL.GetBookingByBookingId(bookingId);

                    txtBookingNo.Text = objBooking.BookingNo;
                    txtBookingDate.Text = Convert.ToString(objBooking.BookingDate.ToString("dd-MM-yyyy"));
                    txtPOL.Text = objBooking.POL;
                    txtPOD.Text = objBooking.POD;
                    txtFPOD.Text = objBooking.FPOD;
                    txtContainers.Text = objBooking.Containers;

                    //Additional
                    ((TextBox)txtFreightPayableAt.FindControl("txtPort")).Text = objBooking.FreightPayableName;
                    ViewState["FRIEGHTPAYABLEATID"] = objBooking.FreightPayableId;

                    rdblBorkerage.SelectedValue = Convert.ToString(objBooking.BrokeragePayable);
                    //rdblBorkerage_SelectedIndexChanged(rdblBorkerage, new EventArgs());
                    txtBrokeragePercent.Text = Convert.ToString(objBooking.BrokeragePercentage);

                    if (!ReferenceEquals(ViewState["ISEDIT"], false))
                    {
                        ((TextBox)txtBrokeragePayableTo.FindControl("txtBrockerage")).Text = objBooking.BookingParty;
                        ViewState["BROKERAGEPAYABLEID"] = objBooking.CustID;
                    }
                    else
                    {
                        ((TextBox)txtBrokeragePayableTo.FindControl("txtBrockerage")).Text = objBooking.BrokeragePayableName;
                        ViewState["BROKERAGEPAYABLEID"] = objBooking.BrokeragePayableId;
                    }

                    rdblRefundPayable.SelectedValue = Convert.ToString(objBooking.RefundPayable);
                    //rdblRefundPayable_SelectedIndexChanged(rdblRefundPayable, new EventArgs());


                    if (!ReferenceEquals(ViewState["ISEDIT"], false))
                    {
                        ((TextBox)txtRefundPayableTo.FindControl("txtRefund")).Text = objBooking.BookingParty;
                        ViewState["REFUNDPAYABLEID"] = objBooking.CustID;
                    }
                    else
                    {
                        ((TextBox)txtRefundPayableTo.FindControl("txtRefund")).Text = objBooking.RefundPayableName;
                        ViewState["REFUNDPAYABLEID"] = objBooking.RefundPayableId;
                    }

                    txtRemarks.Text = objBooking.ExportRemarks;
                    txtShipper.Text = objBooking.Shipper;
                    txtRateReference.Text = objBooking.RateReference;
                    ddlRateType.SelectedValue = objBooking.RateType;
                    hdnFilePath.Value = objBooking.UploadPath;
                    ddlSlot.SelectedValue = Convert.ToString(objBooking.SlotOperatorId);
                    ddlPpCc.SelectedValue = Convert.ToString(objBooking.PpCc);
                }
            }
        }

        private void LoadChargeDetails()
        {
            int bookingId = Convert.ToInt32(ViewState["BOOKINGID"]);
            bool isEdit = Convert.ToBoolean(ViewState["ISEDIT"]);

            if (bookingId > 0)
            {
                List<IBookingCharges> objData = new List<IBookingCharges>();

                if (!ReferenceEquals(ViewState["DataSource"], null) && ((List<IBookingCharges>)(ViewState["DataSource"])).Count > 0)
                {
                    objData = ViewState["DataSource"] as List<IBookingCharges>;
                }
                else
                {
                    if (!isEdit)
                    {
                        objData = BookingBLL.GetBookingChargesForAdd(bookingId);
                        btnDel.Visible = false;
                    }
                    else
                    {
                        objData = BookingBLL.GetBookingChargesForEdit(bookingId);
                        if (BookingBLL.GetExpInvoiceExists(bookingId) == true)
                        {
                            btnDel.Visible = false;
                        }
                        else
                        {
                            btnDel.Visible = true;
                        }

                        //btnDel.Visible = true;
                    }
                }

                //objData = objData.Where(d => d.ChargeStatus == true).ToList();
                

                if (rdblBorkerage.SelectedValue == "True")
                    objData = objData.Where(d => d.ChargeStatus == true).Select(d => { d.BrokerageEditable = true; return d; }).ToList();
                else
                    objData = objData.Where(d => d.ChargeStatus == true).Select(d => { d.BrokerageEditable = false; return d; }).ToList();

                if (rdblRefundPayable.SelectedValue == "True")
                    objData = objData.Where(d => d.ChargeStatus == true).Select(d => { d.RefundEditable = true; return d; }).ToList();
                else
                    objData = objData.Where(d => d.ChargeStatus == true).Select(d => { d.RefundEditable = false; return d; }).ToList();

                ViewState["DataSource"] = objData;

                gvwCharges.DataSource = objData;
                gvwCharges.DataBind();
            }
        }

        private void LoadSlotOperatorsDDL()
        {
            DataTable dtSlotOperators = BookingBLL.GetSlotOperators();

            if (dtSlotOperators != null && dtSlotOperators.Rows.Count > 0)
            {
                DataRow dr = dtSlotOperators.NewRow();
                dr["SlotOperatorID"] = "0";
                dr["SlotOperatorName"] = "--Select--";
                dtSlotOperators.Rows.InsertAt(dr, 0);

                ddlSlot.DataValueField = "SlotOperatorID";
                ddlSlot.DataTextField = "SlotOperatorName";
                ddlSlot.DataSource = dtSlotOperators;
                ddlSlot.DataBind();
            }
        }

        protected void gvwContainers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //List<ChargeApplicable> lstApplicable = new List<ChargeApplicable>();
                //lstApplicable.Add(new ChargeApplicable { Text = "Yes", Value = "True" });
                //lstApplicable.Add(new ChargeApplicable { Text = "No", Value = "False" });

                //// Bind drop down to Applicable
                //DropDownList ddl = (DropDownList)e.Row.FindControl("ddlApplicable");
                //ddl.DataTextField = "Text";
                //ddl.DataValueField = "Value";
                //ddl.DataSource = lstApplicable;
                //ddl.DataBind();

                //IBookingCharges charge = e.Row.DataItem as IBookingCharges;
                //ddl.SelectedValue = charge.ChargeApplicable.ToString();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Booking.aspx");
        }

        protected bool CheckValidity()
        {
            bool IsValid = true;
            int totalRows = gvwCharges.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwCharges.Rows[r];
                HiddenField hdnBookingChargeId = (HiddenField)thisGridViewRow.FindControl("hdnBookingChargeId");
                HiddenField hdnDocumentType = (HiddenField)thisGridViewRow.FindControl("hdnDocumentType");
                 
                //DropDownList ddlApplicable = (DropDownList)thisGridViewRow.FindControl("ddlApplicable");
                TextBox txtCharged = (TextBox)thisGridViewRow.FindControl("txtCharged");
                TextBox txtRefund = (TextBox)thisGridViewRow.FindControl("txtRefund");
                TextBox txtBrokerageBasic = (TextBox)thisGridViewRow.FindControl("txtBrokerageBasic");
                TextBox txtManifest = (TextBox)thisGridViewRow.FindControl("txtManifest");
                lblMessage.Text = "";

                errFreightPayableAt.Text = "";
                if (Convert.ToString(ViewState["FRIEGHTPAYABLEATID"]) == string.Empty || Convert.ToString(ViewState["FRIEGHTPAYABLEATID"]) == "0")
                {
                    errFreightPayableAt.Text = "Freight Payable at is compulsory";
                    return false;

                }


                if (hdnDocumentType.Value.ToInt() == 1)
                {
                    if (txtCharged.Text.ToDecimal() == 0)
                    {
                        IsValid = false;
                        lblMessage.Text = "Invalid Charged Amount";
                        return IsValid;
                    }

                    if (txtManifest.Text.ToDecimal() == 0)
                    {
                        IsValid = false;
                        lblMessage.Text = "Invalid Manifest Amount";
                        return IsValid;

                    }

                    if (txtCharged.Text.ToDecimal() < txtManifest.Text.ToDecimal())
                    {
                        IsValid = false;
                        lblMessage.Text = "Charged amount < Manifest Amount";
                        return IsValid;
                    }

                    if (rdblBorkerage.SelectedValue == "True" && txtBrokerageBasic.Text.ToDecimal() == 0)
                    {
                        IsValid = false;
                        lblMessage.Text = "Invalid Brokerage Basic Amount";
                        return IsValid;
                    }

                    if (rdblRefundPayable.SelectedValue == "True" && txtRefund.Text.ToDecimal() == 0)
                    {
                        IsValid = false;
                        lblMessage.Text = "Invalid Refund Payable Amount";
                        return IsValid;
                    }

                    if (txtBrokerageBasic.Text.ToDecimal() > txtCharged.Text.ToDecimal() - txtRefund.Text.ToDecimal())
                    {
                        IsValid = false;
                        lblMessage.Text = "Brokerage Basic <= Charged - Refund amount";
                        return IsValid;
                    }

                    if (txtCharged.Text.ToDecimal() < txtManifest.Text.ToDecimal() + txtRefund.Text.ToDecimal())
                    {
                        IsValid = false;
                        lblMessage.Text = "Charged Freight < Manifest + Refund";
                        return IsValid;
                    }
                }

            }
            return IsValid;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(ViewState["BOOKINGID"], null))
            {
                int BookingId = Convert.ToInt32(ViewState["BOOKINGID"]);

                if (BookingId > 0)
                {

                    BookingBLL.DeleteBookingCharges(BookingId);
                    Response.Redirect("Booking.aspx");
                  
                }
            }

            //Save Booking Charges
            List<IBookingCharges> lstData = ViewState["DataSource"] as List<IBookingCharges>;

        }
    }

    //public class ChargeApplicable
    //{
    //    public string Text { get; set; }
    //    public string Value { get; set; }
    //}


}