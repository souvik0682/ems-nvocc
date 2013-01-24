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

namespace EMS.WebApp.Hire
{
    public partial class AddEditHire : System.Web.UI.Page
    {

        IUser user = null;
        public int counter = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (IUser)Session[Constants.SESSION_USER_INFO];
            if (!IsPostBack)
            {
                SetDefault();
                try
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        ViewState["HireId"] = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]).LongRequired();
                        ViewState["Edit"] = "Edit";
                        SetEditValue(new BLL.OnHireBLL().GetOnHire(new SearchCriteria { StringOption4 = ViewState["HireId"].ToString() }));
                        rdTransactionType.Enabled = false;
                    }
                }
                catch { }
            }
        }

        private void SetDefault()
        {
            Filler.FillData<IContainerType>(ddlType, CommonBLL.GetContainerType(), "CotainerDesc", "ContainerTypeID", "---Type---");
            Filler.FillData<ILocation>(ddlLocation, new CommonBLL().GetActiveLocation(), "Name", "Id", "---Select---");
            Filler.FillData(ddlLineCode, new DBInteraction().GetNVOCCLine(1, "").Tables[0], "NVOCCName", "pk_NVOCCID", "---Select---");
            Filler.GridDataBind(new List<IEqpOnHireContainer>(), gvwHire);
            rdTransactionType_SelectedIndexChanged(null, null);
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
                        txtHireReference.Text = rw["HireReference"].DataToValue<String>();
                        txtReferenceDate.Text = rw["HireReferenceDate"].DataToValue<DateTime>();
                        txtReleaseDate.Text = rw["ReleaseRefDate"].DataToValue<DateTime>();
                        txtReleaseRefNo.Text = rw["ReleaseRefNo"].DataToValue<String>();
                        hdnReturn.Value = rw["ReturnedPortID"].DataToValue<String>();
                        rdTransactionType.SelectedValue = rw["OnOffHire"].ToString();
                        txtNarration.Text = rw["Narration"].DataToValue<String>();
                        ddlLocation.SelectedValue = rw["LocationID"].DataToValue<String>();
                        ddlLineCode.SelectedValue = rw["NVOCCID"].DataToValue<String>();
                        txtReturn.Text = rw["PortName"].DataToValue<String>();
                        txtValidTill.Text = rw["ValidTill"].DataToValue<DateTime>();
                        break;
                    }
                }
                IList<IEqpOnHireContainer> lstEqpOnHireContainer = new EqpOnHireContainer().GetEqpOnHireContainerList(dt);
                GetEqpOnHireContainers = lstEqpOnHireContainer;
                Filler.GridDataBind(lstEqpOnHireContainer, gvwHire);
            }
        }
        private void ClearFieldLower()
        {
            txtContainerNo.Text = string.Empty;
            txtLGNo.Text = string.Empty;
            txtIGMNo.Text = string.Empty;
            ddlSize.SelectedValue = "0";
            ddlType.SelectedValue = "0";
            txtIGMDate.Text = string.Empty;
            txtOnHireDate.Text = string.Empty;
        }

        private void ClearFieldUpper()
        {
            txtHireReference.Text = string.Empty;
            txtLGNo.Text = string.Empty;
            txtIGMNo.Text = string.Empty;
            ddlLocation.SelectedValue = "0";
            ddlLineCode.SelectedValue = "0";
            txtIGMDate.Text = string.Empty;
            txtOnHireDate.Text = string.Empty;
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
            IList<IEqpOnHireContainer> lstEqpOnHireContainer = GetEqpOnHireContainers;

            if (lstEqpOnHireContainer == null)
            {
                lstEqpOnHireContainer = new List<IEqpOnHireContainer>();
                GetEqpOnHireContainers = lstEqpOnHireContainer;
            }
            if (ViewState["Edit"] != null)
            {
                txtContainerNo.Enabled = true;
                var temp = lstEqpOnHireContainer.FirstOrDefault(f => f.ContainerNo == txtContainerNo.Text.Trim().StringRequired());
                if (temp != null)
                {
                    temp.ContainerNo = txtContainerNo.Text;
                    temp.LGNo = txtLGNo.Text;
                    temp.IGMNo = txtIGMNo.Text.ToNullLong();
                    temp.CntrSize = (ddlSize.SelectedValue.Equals("0") ? "" : ddlSize.SelectedValue).StringRequired();
                    temp.ContainerTypeID = ddlType.SelectedValue.Trim().ToNullInt();
                    temp.IGMDate = txtIGMDate.Text.ToNullDateTime();
                    temp.ActualOnHireDate = txtOnHireDate.Text.ToNullDateTime();

                    ClearFieldUpper();
                    Filler.GridDataBind(lstEqpOnHireContainer, gvwHire);
                    return;
                }
            }
            if (!CheckContainerInList(lstEqpOnHireContainer, txtContainerNo.Text.Trim().StringRequired()) && !new OnHireBLL().ValidateOnHire(txtContainerNo.Text.Trim(), rdTransactionType.SelectedValue))// Check using proc
            {
                lstEqpOnHireContainer.Add(new EqpOnHireContainer
                 {
                     ContainerNo = txtContainerNo.Text.Trim(),
                     LGNo = txtLGNo.Text,
                     IGMNo = txtIGMNo.Text.ToNullLong(),
                     CntrSize = (ddlSize.SelectedValue.Equals("0") ? "" : ddlSize.SelectedValue).StringRequired(),
                     ContainerTypeID = ddlType.SelectedValue.Trim().ToNullInt(),
                     IGMDate = txtIGMDate.Text.ToNullDateTime(),
                     ActualOnHireDate = txtOnHireDate.Text.ToNullDateTime(),
                     AddedOn = DateTime.Now,
                     MovementOptID = 9,
                     UserAdded = user.Id,
                     UserLastEdited = user.Id
                 });
                ClearFieldUpper();
                Filler.GridDataBind(lstEqpOnHireContainer, gvwHire);
            }
        }
        protected void gvwHire_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }
        protected void gvwHire_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            // throw new NotImplementedException();
        }
        protected void gvwHire_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {


            switch (e.CommandName)
            {
                case "Remove":
                    RemoveContainerInList(GetEqpOnHireContainers, e.CommandArgument.ToString());
                    break;
                case "Edit":
                    ViewState["Edit"] = "Edit";
                    txtContainerNo.Enabled = false;
                    SetCorVal(GetEqpOnHireContainers, e.CommandArgument.ToString());
                    break;
            }
        }


        private bool SetCorVal(IList<IEqpOnHireContainer> lstEqpOnHireContainer, string containerNo)
        {
            if (string.IsNullOrEmpty(containerNo) && containerNo.Length == 0)
                throw new Exception("Container No. can't Null Or Empty.");
            if (lstEqpOnHireContainer != null)
            {
                var temp = lstEqpOnHireContainer.FirstOrDefault(e => e.ContainerNo == containerNo);
                if (temp != null)
                {
                    txtContainerNo.Text = temp.ContainerNo;
                    txtLGNo.Text = temp.LGNo;
                    txtIGMNo.Text = temp.IGMNo.ToString();
                    ddlSize.SelectedValue = temp.CntrSize;
                    ddlType.SelectedValue = temp.ContainerTypeID.ToString();
                    txtIGMDate.Text = temp.IGMDate.HasValue ? temp.IGMDate.Value.ToNullDateTimeToString() : "";
                    txtOnHireDate.Text = temp.ActualOnHireDate.HasValue ? temp.ActualOnHireDate.Value.ToNullDateTimeToString() : "";
                }
            }
            return false;
        }
        private bool CheckContainerInList(IList<IEqpOnHireContainer> lstEqpOnHireContainer, string containerNo)
        {
            if (string.IsNullOrEmpty(containerNo) && containerNo.Length == 0)
                throw new Exception("Container No. can't Null Or Empty.");
            if (lstEqpOnHireContainer != null)
            {
                return lstEqpOnHireContainer.Any(e => e.ContainerNo == containerNo);
            }
            return false;
        }
        private void RemoveContainerInList(IList<IEqpOnHireContainer> lstEqpOnHireContainer, string containerNo)
        {
            if (string.IsNullOrEmpty(containerNo) && containerNo.Length == 0)
                throw new Exception("Container No. can't Null Or Empty.");
            if (lstEqpOnHireContainer != null)
            {
                try
                {
                    lstEqpOnHireContainer.Remove(lstEqpOnHireContainer.FirstOrDefault(e => e.ContainerNo == containerNo));
                    Filler.GridDataBind(lstEqpOnHireContainer, gvwHire);
                }
                catch { }

            }
        }
        public IEqpOnHireContainer List { get; set; }
        private bool Validate()
        {
            var temCont = GetEqpOnHireContainers;
            if (temCont == null || temCont.Count < 0)
            {
                //Mes="Please add one or more On Hire Containers"
                return false;
            }
            return true;
        }
        private int CountFEU()
        {
            var temp = GetEqpOnHireContainers;
            try
            {
                return temp.Count(e => e.CntrSize == "40");
            }
            catch { }
            return 0;
        }
        private IList<IEqpOnHireContainer> GetEqpOnHireContainers
        {
            get
            {
                IList<IEqpOnHireContainer> lstEqpOnHireContainer = null;
                try
                {
                    if (Session["IEqpOnHireContainer"] != null)
                        lstEqpOnHireContainer = (List<IEqpOnHireContainer>)Session["IEqpOnHireContainer"];
                }
                catch { }
                return lstEqpOnHireContainer;
            }
            set { Session["IEqpOnHireContainer"] = value; }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Validate())
            {
                var feu = CountFEU();
                var lst = GetEqpOnHireContainers;
                IEqpOnHire onHire = new EqpOnHire
                {
                    AddedOn = DateTime.Now,
                    CompanyID = 1,//                    user.CompanyId
                    EditedOn = DateTime.Now,
                    FEUs = feu,
                    HireReference = txtHireReference.Text.Trim(),
                    HireReferenceDate = txtReferenceDate.Text.Trim().ToNullDateTime(),
                    LstEqpOnHireContainer = lst,
                    LocationID = ddlLocation.SelectedValue.ToInt(),
                    NVOCCID = ddlLineCode.SelectedValue.ToInt(),
                    Narration = txtNarration.Text.Trim(),
                    OnOffHire = rdTransactionType.SelectedValue.ToCharArray()[0],
                    ReturnedPortID = hdnReturn.Value.ToInt(),
                    ReleaseRefDate = txtReleaseDate.Text.ToNullDateTime(),
                    ReleaseRefNo = txtReleaseRefNo.Text.Trim(),
                    TEUs = lst.Count - feu,
                    UserAdded = user.Id,
                    UserLastEdited = user.Id,
                    ValidTill = txtValidTill.Text.Trim().ToNullDateTime()
                };
                var retrunVal = 0;
                if (ViewState["HireId"] != null)
                {
                    onHire.HireID = ViewState["HireId"].ToLong();
                    retrunVal = new OnHireBLL().UpdateOnHire(onHire);
                }
                else
                {

                    retrunVal = new OnHireBLL().SaveOnHire(onHire);

                }
                if (retrunVal > 0)
                {
                    Session.Remove("IEqpOnHireContainer");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Messenger.SendMessage("Saved successfully.", "1", "Add/Edit OnHire", "Hire.aspx"), true);
                    //Response.Redirect("Hire.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Messenger.SendMessage("Error! Please try again.", "4", "Add/Edit OnHire", ""), true);
                }
            }


        }

        protected void rdTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetEqpOnHireContainers = null;
                Filler.GridDataBind(new List<IEqpOnHireContainer>(), gvwHire);
                if (rdTransactionType.SelectedValue == "N")
                {
                    rfvReturn.Enabled = false;
                    lblReturn.Visible = false;

                    rfvValidTill.Enabled = true;
                    rfvLocation.Enabled = true;
                    lblValid.Visible = true;
                    lblStock.Visible = true;
                }
                else
                {

                    rfvValidTill.Enabled = false;
                    rfvLocation.Enabled = false;
                    lblValid.Visible = false;
                    lblStock.Visible = false;

                    rfvReturn.Enabled = true;
                    lblReturn.Visible = true;
                }
            }
            catch { }
        }
    }
}