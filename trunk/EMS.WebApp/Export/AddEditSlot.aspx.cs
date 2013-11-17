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

namespace EMS.WebApp.Export
{
    public partial class AddEditSlot : System.Web.UI.Page
    {

        IUser user = null;
        public int counter = 1;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userId = 0;
        private long Sid;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            user = (IUser)Session[Constants.SESSION_USER_INFO];
            if (!IsPostBack)
            {
                SetDefault();
                try
                {

                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        ViewState["SlotId"] = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]).LongRequired();
                        ViewState["Edit"] = "Edit";
                        SetEditValue(new BLL.SlotBLL().GetSlot(new SearchCriteria { StringOption4 = ViewState["SlotId"].ToString() }));
                        Sid = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]).LongRequired();
                    }
                    else
                    {
                        Sid = 0;
                    }
                }
                catch { }
            }
            CheckUserAccess(Sid);
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
            Filler.FillData<IContainerType>(ddlType, CommonBLL.GetContainerType(), "ContainerAbbr", "ContainerTypeID", "---Type---");

            Filler.FillData(ddlTerm1, new DBInteraction().GetActiveOperator().Tables[0], "SlotOperatorName", "pk_SlotOperatorID", "---Select---");
            Filler.FillData(ddlLineCode, new DBInteraction().GetNVOCCLine(-1, "").Tables[0], "NVOCCName", "pk_NVOCCID", "---Select---");
            Filler.FillData(ddlTerm1, new DBInteraction().GetMovType().Tables[0], "MovTypeName", "pk_MovTypeID", "---Select---");
            Filler.FillData(ddlTerm2, new DBInteraction().GetMovType().Tables[0], "MovTypeName", "pk_MovTypeID", "---Select---");
            Filler.GridDataBind(new List<IEqpOnHireContainer>(), gvwSlot);
            //rdTransactionType_SelectedIndexChanged(null, null);

            //if (rdTransactionType.SelectedValue == "F")
            //{
            //    txtContainerNo.TextChanged += txtContainerNo_TextChanged;
            //    ddlSize.Enabled = false;
            //    ddlType.Enabled = false;
            //}
        }

        private void SetEditValue(DataTable dt)
        {
            if (dt != null)
            {
                int i = 0;
                foreach (DataRow rw in dt.Rows)
                {
                    if (i++ == 0)
                    {
                        ddlOperator.SelectedValue= rw["fk_SlotOperatorID"].DataToValue<String>();
                        ddlLineCode.SelectedValue = rw["fk_LineID"].DataToValue<String>();
                        txtPOL.Text = rw["LoadPort"].DataToValue<String>();
                        txtPOD.Text = rw["DischargePort"].DataToValue<String>();
                        hdnPOD.Value = rw["fk_POD"].DataToValue<String>();
                        hdnPOL.Value = rw["fk_POL"].DataToValue<String>();
                        ddlTerm1.SelectedValue = rw["fk_MovOrigin"].DataToValue<String>();
                        ddlTerm2.SelectedValue = rw["fk_MovDest"].DataToValue<String>();
                        txtEffectDate.Text = rw["effDate"].DataToValue<DateTime>();
                        txtContainerRate.Text = rw["ContainerRate"].DataToValue<String>();
                        hdnPOD.Value = rw["ReturnedPortID"].DataToValue<String>();
                        hdnPOL.Value = rw["ReturnedPortID"].DataToValue<String>();
                        txtRatePerTon.Text = rw["RatePerTon"].DataToValue<String>();
                        txtRatePerCBM.Text = rw["RatePerCBM"].DataToValue<String>();
                        break;
                    }
                }
                //IList<IEqpOnHireContainer> lstEqpOnHireContainer = new EqpOnHireContainer().GetEqpOnHireContainerList(dt);
                IList<ISlotCost> lstSlotCost = new SlotCostEntity().GetSlotCost(dt);
                //GetEqpOnHireContainers = lstEqpOnHireContainer;
                GetSlotCosts = lstSlotCost;
                Filler.GridDataBind(lstSlotCost, gvwSlot);
            }
        }

        public bool CheckPort(string PortName)
        {
           
            var t = PortName.Trim();
            DataTable dt = ImportHaulageBLL.GetAllPort(t);
            if (dt.AsEnumerable().FirstOrDefault(e => e.Field<String>("PortName") != t) != null)
            {
                return true;
            }
            return false;

        }



        private void ClearFieldLower()
        {
            ddlSpecialType.SelectedValue = "0";
            ddlCargoType.SelectedValue = "0";
            ddlSize.SelectedValue = "0";
            ddlType.SelectedValue = "0";
            txtContainerRate.Text = string.Empty;
            txtRatePerCBM.Text = string.Empty;
            txtRatePerTon.Text = string.Empty;
        }

        private void ClearFieldUpper()
        {
            //txtHireReference.Text = string.Empty;
            txtEffectDate.Text = string.Empty;
            txtPOL.Text = string.Empty;
            txtPOD.Text = string.Empty;
            txtPODTerminal.Text = string.Empty;
            ddlOperator.SelectedValue = "0";
            ddlTerm1.SelectedValue = "0";
            ddlTerm2.SelectedValue = "0";
            ddlLineCode.SelectedValue = "0";
        }
        public string GetTypeData(string val)
        {

            if (ddlType.SelectedIndex > 0) return ddlType.Items.FindByValue(val).Text;
            return "";
        }

        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            counter = 1;

            var g = DateTime.Now;
            //if (!IsValidContainerNo())
            //{
            //    return;
            //}
            //else
            //{
            //    if (rdTransactionType.SelectedValue == "F" && OnHireBLL.ValidateContainerStatus(txtContainerNo.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), string.Format("alert('Please check the container {0}');", txtContainerNo.Text), true);
            //    }
            //}
            IList<ISlotCost> lstSlotCost = GetSlotCosts;

            if (lstSlotCost == null)
            {
                lstSlotCost = new List<ISlotCost>();
                GetSlotCosts = lstSlotCost;
            }
            if (ViewState["Edit"] != null && ddlCargoType.SelectedIndex != 2)
            {
                //txtContainerNo.Enabled = true;
                var temp = lstSlotCost.FirstOrDefault(f => f.CargoType == ddlCargoType.Text.Trim().StringRequired());
                var temp1 = lstSlotCost.FirstOrDefault(f => f.CntrSize == ddlSize.Text.Trim().StringRequired());
                var temp2 = lstSlotCost.FirstOrDefault(f => f.ContainerTypeID == Convert.ToInt32(ddlType.Text.Trim().StringRequired()));
                var temp3 = lstSlotCost.FirstOrDefault(f => f.SpecialType == ddlSpecialType.Text.Trim().StringRequired());
                if (temp != null && temp1 != null && temp2 != null && temp3 != null)
                {
                   
                    temp.CargoType = (ddlCargoType.SelectedValue.Equals("0") ? "" : ddlCargoType.SelectedValue).StringRequired();
                    temp.SpecialType = (ddlSpecialType.SelectedValue.Equals("0") ? "" : ddlSpecialType.SelectedValue).StringRequired();
                    temp.CntrSize = (ddlSize.SelectedValue.Equals("0") ? "" : ddlSize.SelectedValue).StringRequired();
                    temp.ContainerTypeID = ddlType.SelectedValue.Trim().ToNullInt();
                    temp.ContainerRate = Convert.ToDecimal(txtContainerRate.Text.Trim());
                    temp.RatePerTon = Convert.ToDecimal(txtRatePerTon.Text.Trim());
                    temp.RetePerCBM = Convert.ToDecimal(txtRatePerCBM.Text.Trim());
                    ClearFieldUpper();
                    Filler.GridDataBind(lstSlotCost, gvwSlot);
                    return;
                }
            }
            
            //if (!CheckDuplicateEntry)
            //{
                lstSlotCost.Add(new SlotCostEntity
                {
                    CargoType = (ddlCargoType.SelectedValue.Equals("0") ? "" : ddlCargoType.SelectedValue).StringRequired(),
                    SpecialType = (ddlSpecialType.SelectedValue.Equals("0") ? "" : ddlSpecialType.SelectedValue).StringRequired(),
                    CntrSize = (ddlSize.SelectedValue.Equals("0") ? "" : ddlSize.SelectedValue).StringRequired(),
                    ContainerTypeID = ddlType.SelectedValue.Trim().ToNullInt(),
                    ContainerRate = Convert.ToDecimal(txtContainerRate.Text.Trim()),
                    RatePerTon = Convert.ToDecimal(txtRatePerTon.Text.Trim()),
                    RatePerCBM = Convert.ToDecimal(txtRatePerCBM.Text.Trim()),
                    AddedOn = DateTime.Now,
                    EditedOn = DateTime.Now,
                    UserAdded = user.Id,
                    UserLastEdited = user.Id
                });
                ClearFieldUpper();
                Filler.GridDataBind(lstSlotCost, gvwSlot);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), string.Format("alert('{0} already exist in Table');", txtContainerNo.Text.Trim().JToUpper()), true);
            //    return;
            //}
        }
        protected void gvwSlot_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }
        protected void gvwSlot_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            // throw new NotImplementedException();
        }
        protected void gvwSlot_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {


            //switch (e.CommandName)
            //{
            //    case "Remove":
            //        RemoveContainerInList(GetSlotCosts, e.CommandArgument.ToString());
            //        break;
            //    case "Edit":
            //        ViewState["Edit"] = "Edit";
            //        SetCorVal(GetSlotCosts, e.CommandArgument.ToString());
            //        break;
            //}
        }


        private bool SetCorVal(IList<ISlotCost> lstSlotCost)
        {
            //if (string.IsNullOrEmpty(containerNo) && containerNo.Length == 0)
            //    throw new Exception("Container No. can't Null Or Empty.");
            if (lstSlotCost != null)
            {
                var temp = lstSlotCost.FirstOrDefault(f => f.CargoType == ddlCargoType.Text.Trim().StringRequired());
                var temp1 = lstSlotCost.FirstOrDefault(f => f.CntrSize == ddlSize.Text.Trim().StringRequired());
                var temp2 = lstSlotCost.FirstOrDefault(f => f.ContainerTypeID == Convert.ToInt32(ddlType.Text.Trim().StringRequired()));
                var temp3 = lstSlotCost.FirstOrDefault(f => f.SpecialType == ddlSpecialType.Text.Trim().StringRequired());
                if (temp != null && temp1 != null && temp2 != null && temp3 != null)
                //var temp = lstEqpOnHireContainer.FirstOrDefault(e => e.ContainerNo == containerNo);
                if (temp != null)
                {
                    ddlCargoType.SelectedValue=temp.CargoType;
                    ddlSpecialType.SelectedValue = temp.SpecialType;
                    ddlSize.SelectedValue = temp.CntrSize;
                    ddlType.SelectedValue = temp.ContainerTypeID.ToString();
                    txtContainerRate.Text = temp.ContainerRate.ToString();
                    txtRatePerTon.Text = temp.RatePerTon.ToString();
                    txtRatePerCBM.Text = temp.RetePerCBM.ToString();
                    //txtIGMDate.Text = temp.IGMDate.HasValue ? temp.IGMDate.Value.ToNullDateTimeToString() : "";
                    //txtOnHireDate.Text = temp.ActualOnHireDate.HasValue ? temp.ActualOnHireDate.Value.ToNullDateTimeToString() : "";
                }
            }
            return false;
        }
        //private bool CheckTheList(IList<ISlotCost> lstSlotCost)
        //{
        //    if (string.IsNullOrEmpty(containerNo) && containerNo.Length == 0)
        //        throw new Exception("Container No. can't Null Or Empty.");
        //    if (lstEqpOnHireContainer != null)
        //    {
        //        return lstEqpOnHireContainer.Any(e => e.ContainerNo == containerNo);
        //    }
        //    return false;
        //}
        //private void RemoveContainerInList(IList<ISlotCost> lstSlotCost, string containerNo)
        //{
        //    if (string.IsNullOrEmpty(containerNo) && containerNo.Length == 0)
        //        throw new Exception("Container No. can't Null Or Empty.");
        //    if (lstEqpOnHireContainer != null)
        //    {
        //        try
        //        {
        //            lstEqpOnHireContainer.Remove(lstEqpOnHireContainer.FirstOrDefault(e => e.ContainerNo == containerNo));
        //            Filler.GridDataBind(lstEqpOnHireContainer, gvwSlot);
        //        }
        //        catch { }

        //    }
        //}
        public ISlotCost List { get; set; }
        private bool ValidateSlot()
        {
            var temCont = GetSlotCosts;
            if (temCont == null || temCont.Count < 0)
            {
                //Mes="Please add one or more On Hire Containers"
                return false;
            }
            if (!CheckPort(txtPOD.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), string.Format("alert('POD is not valid');"), true);
            }
            if (!CheckPort(txtPOL.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), string.Format("alert('POL is not valid');"), true);
            }
            return true;
        }
        //private int CountFEU()
        //{
        //    var temp = GetSlotCosts;
        //    try
        //    {
        //        return temp.Count(e => e.CntrSize == "40");
        //    }
        //    catch { }
        //    return 0;
        //}
        private IList<ISlotCost> GetSlotCosts
        {
            get
            {
                IList<ISlotCost> lstSlotCost = null;
                try
                {
                    if (Session["ISlotCost"] != null)
                        lstSlotCost = (List<ISlotCost>)Session["ISlotCost"];
                }
                catch { }
                return lstSlotCost;
            }
            set { Session["ISlotCost"] = value; }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (ValidateSlot())
            {

                var lst = GetSlotCosts;
                //foreach (var t in lst)
                //{
                //if(new OnHireBLL().ValidateSlotOnHire(t.ContainerNo, rdTransactionType.SelectedValue)){
                //    ScriptManager.RegisterStartupScript(this,this.GetType(),DateTime.Now.Ticks.ToString(),string.Format("alert('{0} already exist in Table');",t.ContainerNo),true);
                //return;
                //}
                //}
                //var feu = CountFEU();
                ISlot slots = new SlotEntity
              
                {
                    CreatedOn = DateTime.Now,
                    CompanyID = 1,//                    user.CompanyId
                    ModifiedOn = DateTime.Now,
                    SlotOperatorID = ddlOperator.SelectedValue.ToInt(),
                    LineID = ddlLineCode.SelectedValue.ToInt(),
                    PODID = hdnPOD.Value.ToInt(),
                    POLID = hdnPOL.Value.ToInt(),
                    MovOrigin = ddlTerm1.SelectedValue.ToInt(),
                    MovDestination = ddlTerm2.SelectedValue.ToInt(),
                    EffectDt = txtEffectDate.Text.ToDateTime(),
                    CreatedBy = user.Id,
                    ModifiedBy = user.Id,
                   
                };
                var retrunVal = 0;
                if (ViewState["SlotId"] != null)
                {
                    slots.SlotID = ViewState["SlotId"].ToInt();
                    retrunVal = new SlotBLL().SaveSlot(slots, 1);
                }
                else
                {

                    retrunVal = new SlotBLL().SaveSlot(slots, 0);

                }
                if (retrunVal > 0)
                {
                    Session.Remove("ISlotCost");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Saved successfully.');", true);
                    Response.Redirect("ManageSlotCost.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Error! Please try again.')", true);
                }
            }


        }


        protected void ddlCargoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCargoType.SelectedIndex == 0 || ddlCargoType.SelectedIndex == 1)
            {
                ddlSize.Enabled = true;
                ddlType.Enabled = true;
                ddlSpecialType.Enabled = true;
                txtContainerRate.Enabled = true;
                txtRatePerCBM.Enabled = false;
                txtRatePerTon.Enabled = false;

            }
            else
            {
                ddlSize.Enabled = false;
                ddlType.Enabled = false;
                ddlSpecialType.Enabled = false;
                txtContainerRate.Enabled = false;
                txtRatePerCBM.Enabled = true;
                txtRatePerTon.Enabled = true;
            }

        }
    }
}