using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Common;
using EMS.Entity;
using System.Xml.Linq;
using EMS.Utilities;
using EMS.BLL;
using EMS.WebApp.Transaction;

namespace EMS.WebApp.MasterModule
{
    public partial class ExpAddEditSlotCost : System.Web.UI.Page
    {

        IUser user = null;
        public int counter = 1;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userId = 0;
        private long Hid;

        protected void Page_Load(object sender, EventArgs e)
        {
             RetriveParameters();
            user = (IUser)Session[Constants.SESSION_USER_INFO];
            if (!IsPostBack)
            {

                try
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        ViewState["SlotCostId"] = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]).LongRequired();
                        Hid = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]).LongRequired();
                        //GetValue
                       
                    }
                    else
                    {
                        Hid = 0;
                    }
                    SetDefault();
                }
                catch { }
            }
           //  CheckUserAccess(Hid);
        }

        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();
            EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess(long xID)
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
                else
                {

                    if (!_canEdit && xID != 0)
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == 0)
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

        private void SetDefault()
        {
            Filler.FillData<IContainerType>(ddlContainerType, CommonBLL.GetContainerType(), "ContainerAbbr", "ContainerTypeID", "--Container Type--");

            Filler.FillData(ddlOperator, ExpSlotCostBLL.GetSlotOperatorList(new SearchCriteria() { StringParams = new List<string>() { "0", "", "", "" } }), "SlotOperatorName", "pk_SlotOperatorID", "--Operator--");//
            Filler.FillData(ddlMovOrigin, ExpSlotCostBLL.GetMovementType(), "MovTypeName", "pk_MovTypeID", "--Movement Origin--");
            //Filler.FillData(ddlMovDestination, ExpSlotCostBLL.GetMovementType(), "MovTypeName", "pk_MovTypeID", "--Movement Destination--");
            Filler.FillData(ddlLineCode, new DBInteraction().GetNVOCCLine(-1, "").Tables[0], "NVOCCName", "pk_NVOCCID", "--Line--");
            IList<SlotCost> lst=null;
            if (ViewState["SlotCostId"] != null) { 
             var temp= ExpSlotCostBLL.GetSlotCost(Hid);
                if(temp!=null){
                
            txtLoadPort.Text = string.Empty;
            txtDestinationPort.Text = string.Empty;
            hdnLoadPort.Value=temp.Slot.PORTLOADING.ToString();
            hdnDestinationPort.Value=temp.Slot.PORTDISCHARGE.ToString();
            txtEffectiveDate.Text = temp.Slot.EFFECTIVEDATE.Value.ToShortDateString();
            txtPodTerminal.Text = temp.Slot.PODTERMINAL;
            ddlOperator.SelectedValue = temp.Slot.OPERATOR.ToString();
            ddlMovOrigin.SelectedValue = temp.Slot.MOVORIGIN.ToString();
            //ddlMovDestination.SelectedValue = temp.Slot.MOVDESTINATION.ToString();
            ddlLineCode.SelectedValue =temp.Slot.LINE.ToString();
            txtLoadPort.Text = temp.Slot.PORTLOADINGNAME;
            txtDestinationPort.Text =temp.Slot.PORTDISCHARGENAME;
                    GetSlotCost=temp.SlotCostList;
                    lst=temp.SlotCostList;
                }

            }else{ lst=new List<SlotCost>();}

            Filler.GridDataBind(lst, gvwSlotCost);
           
        }

        private void ClearFieldLower()
        {
            txtAmount.Text = string.Empty;
            ddlSize.SelectedValue = "0";
            ddlType.SelectedValue = "0";
            ddlContainerType.SelectedValue = "0";
            txtRevTon.Text = string.Empty;
            ddlCargo.SelectedValue = "0";
        }

        private void ClearFieldUpper()
        {
            txtLoadPort.Text = string.Empty;
            txtDestinationPort.Text = string.Empty;
            txtEffectiveDate.Text = string.Empty;
            txtPodTerminal.Text = string.Empty;
            ddlOperator.SelectedValue = "0";
            ddlMovOrigin.SelectedValue = "0";
            //ddlMovDestination.SelectedValue = "0";
            ddlLineCode.SelectedValue = "0";
        }

        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            counter = 1;

            var g = DateTime.Now;
            IList<SlotCost> lstSLOTCOST = GetSlotCost;

            if (lstSLOTCOST == null)
            {
                lstSLOTCOST = new List<SlotCost>();

            }
            if (ViewState["Edit"] != null)
            {
                var temp = lstSLOTCOST.FirstOrDefault(f => f.SLOTCOSTID == Convert.ToInt64(ViewState["EditId"]));
                if (temp != null)
                {
                    temp.AMOUNT = Convert.ToDecimal(txtAmount.Text.StringRequired());
                    temp.REVTON = Convert.ToDecimal(txtRevTon.Text.StringRequired());
                    temp.SIZE = (ddlSize.SelectedValue.Equals("0") ? "" : ddlSize.SelectedValue).StringRequired();
                    temp.CARGO = (ddlCargo.SelectedValue.Equals("0") ? "" : ddlCargo.SelectedValue).StringRequired()[0];
                    temp.CONTAINERTYPE = Convert.ToInt32((ddlContainerType.SelectedValue.Equals("0") ? "" : ddlContainerType.SelectedValue).StringRequired());
                    temp.TYPE = (ddlType.SelectedValue.Equals("0") ? "" : ddlType.SelectedValue).StringRequired()[0];

                    ViewState["EditId"] = null;
                    ViewState["Edit"] = null;
                }
            }
            else
            {
                lstSLOTCOST.Add(new SlotCost
                 {
                     SLOTCOSTID = DateTime.Now.Ticks,
                     AMOUNT = Convert.ToDecimal(txtAmount.Text.StringRequired()),
                     REVTON = Convert.ToDecimal(txtRevTon.Text.StringRequired()),
                     SIZE = (ddlSize.SelectedValue.Equals("0") ? "" : ddlSize.SelectedValue).StringRequired(),
                     CARGO = (ddlCargo.SelectedValue.Equals("0") ? "" : ddlCargo.SelectedValue).StringRequired()[0],
                     CONTAINERTYPE = Convert.ToInt32((ddlContainerType.SelectedValue.Equals("0") ? "" : ddlContainerType.SelectedValue).StringRequired()),
                     TYPE = (ddlType.SelectedValue.Equals("0") ? "" : ddlType.SelectedValue).StringRequired()[0]
                 });
                
            }
            GetSlotCost = lstSLOTCOST;
            ClearFieldLower();
            Filler.GridDataBind(lstSLOTCOST, gvwSlotCost);
            upSLOT.Update();

        }
        public string GetItemFromValue(object obj, string ControlName)
        {
            if (obj != null)
            {
                var ctrl = upSLOT.FindControl(ControlName);
                if (ctrl != null)
                {
                    return ((DropDownList)ctrl).Items.FindByValue(obj.ToString()).Text;
                }
            }
            return "0";
        }
        protected void gvwSlotCost_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            e.Cancel = true;
        }
        protected void gvwSlotCost_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
            // throw new NotImplementedException();
        }
        protected void gvwSlotCost_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {


            switch (e.CommandName)
            {
                case "Remove":
                    RemoveSlotCostList(GetSlotCost, e.CommandArgument.ToString());
                    break;
                case "Edit":
                    ViewState["Edit"] = "Edit";
                    SetCorVal(GetSlotCost, e.CommandArgument.ToString());
                    break;
            }
        }

        private bool SetCorVal(IList<SlotCost> lstslotcost, string slotcostid)
        {
            if (string.IsNullOrEmpty(slotcostid) && slotcostid.Length == 0)
                throw new Exception("SlotCost can't be Null Or Empty.");
            if (lstslotcost != null)
            {
                var temp = lstslotcost.FirstOrDefault(e => e.SLOTCOSTID == Convert.ToInt64(slotcostid));
                if (temp != null)
                {

                    txtAmount.Text = string.Format(Convert.ToString(temp.AMOUNT), "{0:0.00}");
                    ddlSize.SelectedValue = temp.SIZE.ToString();
                    ddlType.SelectedValue = temp.TYPE.ToString();
                    ddlContainerType.SelectedValue = Convert.ToString(temp.CONTAINERTYPE);
                    txtRevTon.Text = string.Format(Convert.ToString(temp.REVTON), "{0:0.00}");
                    ddlCargo.SelectedValue = temp.CARGO.ToString();
                    ViewState["EditId"] = slotcostid;
                }
            }
            return false;
        }
        private bool CheckSlotCost(IList<SlotCost> lstslotcost, string slotcostid)
        {
            if (string.IsNullOrEmpty(slotcostid) && slotcostid.Length == 0)
                throw new Exception("SlotCost can't be  Null Or Empty.");
            if (lstslotcost != null)
            {
                return lstslotcost.Any(e => e.SLOTCOSTID == slotcostid.To<long>());
            }
            return false;
        }
        private void RemoveSlotCostList(IList<SlotCost> lstslotcost, string slotcostid)
        {
            if (string.IsNullOrEmpty(slotcostid) && slotcostid.Length == 0)
                throw new Exception("SlotCost can't be Null Or Empty.");
            if (lstslotcost != null)
            {
                try
                {
                    lstslotcost.Remove(lstslotcost.FirstOrDefault(e => e.SLOTCOSTID == slotcostid.To<long>()));
                    GetSlotCost = lstslotcost;
                    Filler.GridDataBind(lstslotcost, gvwSlotCost);
                   
                }
                catch { }

            }
        }

        private bool Validate()
        {
            var temCont = GetSlotCost;
            if (temCont == null || temCont.Count < 0)
            {
                //Mes="Please add one or more On Hire Containers"
                return false;
            }
            return true;
        }

        private IList<SlotCost> GetSlotCost
        {
            get
            {
                IList<SlotCost> lstslotcost = null;
                try
                {
                    if (Session["lstslotcost"] != null)
                        lstslotcost = (List<SlotCost>)Session["lstslotcost"];
                }
                catch { }
                return lstslotcost;
            }
            set {
                try { TextBox1.Text = value.Count().ToString(); }
                catch { TextBox1.Text = "0"; }
                Session["lstslotcost"] = value; }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var lst = GetSlotCost;
            SlotCostModel slotCostModel = new SlotCostModel
            {
                CompanyID = 1,
                UserId = user.Id,
                Slot = new Slot
                {
                    EFFECTIVEDATE = Convert.ToDateTime(txtEffectiveDate.Text.Trim()),
                    LINE = Convert.ToInt64(ddlLineCode.SelectedValue),
                    //MOVDESTINATION = Convert.ToInt32(ddlMovDestination.SelectedValue),
                    MOVORIGIN = Convert.ToInt32(ddlMovOrigin.SelectedValue),
                    OPERATOR = Convert.ToInt32(ddlOperator.SelectedValue),
                    PODTERMINAL = txtPodTerminal.Text,
                    PORTDISCHARGE = Convert.ToInt64(hdnDestinationPort.Value),
                    PORTLOADING = Convert.ToInt64(hdnLoadPort.Value),
                    SLOTID = 0
                },
                SlotCostList = GetSlotCost
            };
            var retrunVal = 0;
            if (ViewState["SlotCostId"] != null)
            {
                slotCostModel.Slot.SLOTID =Convert.ToInt64( ViewState["SlotCostId"]);
                retrunVal = ExpSlotCostBLL.UpdateSlotCost(slotCostModel);
            }
            else
            {
                retrunVal = ExpSlotCostBLL.SaveSlotCost(slotCostModel);
            }
            if (retrunVal > 0)
            { 
                Session.Remove("lstslotcost");
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Saved successfully.');", true);
                Response.Redirect("~/MasterModule/ExpSlotCostList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Error! Please try again.')", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session.Remove("lstslotcost");
            Response.Redirect("~/MasterModule/ExpSlotCostList.aspx");
        }


    }
}