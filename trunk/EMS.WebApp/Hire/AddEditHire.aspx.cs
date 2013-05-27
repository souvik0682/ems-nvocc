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

namespace EMS.WebApp.Hire
{
    public partial class AddEditHire : System.Web.UI.Page
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
                SetDefault();
                try
                {

                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        ViewState["HireId"] = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]).LongRequired();
                        ViewState["Edit"] = "Edit";
                        SetEditValue(new BLL.OnHireBLL().GetOnHire(new SearchCriteria { StringOption4 = ViewState["HireId"].ToString() }));
                        rdTransactionType.Enabled = false;
                        Hid = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]).LongRequired();
                    }
                    else
                    {
                        Hid = 0;
                    }
                }
                catch { }
            }
            CheckUserAccess(Hid);
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
            Filler.FillData<ILocation>(ddlLocation, new CommonBLL().GetActiveLocation(), "Name", "Id", "---Select---");
            Filler.FillData(ddlLineCode, new DBInteraction().GetNVOCCLine(-1, "").Tables[0], "NVOCCName", "pk_NVOCCID", "---Select---");
            Filler.GridDataBind(new List<IEqpOnHireContainer>(), gvwHire);
            rdTransactionType_SelectedIndexChanged(null, null);

            if (rdTransactionType.SelectedValue == "F" )
                {
                    txtContainerNo.TextChanged+=txtContainerNo_TextChanged;
                ddlSize.Enabled=false;
                ddlType.Enabled=false;
                }
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

        public bool ReturnAt() {
            if (rdTransactionType.SelectedValue == "F") { 
            //chk valid ReturnAt            
                var t=txtReturn.Text.Trim();
                DataTable dt = ImportHaulageBLL.GetAllPort(t);
                    if(dt.AsEnumerable().FirstOrDefault(e=>e.Field<String>("PortName") != t)!=null ) 
                    {
                        return true;
                    }
                   return false;
                    
            }
            return true;
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
            //txtHireReference.Text = string.Empty;
            txtLGNo.Text = string.Empty;
            txtIGMNo.Text = string.Empty;
            //ddlLocation.SelectedValue = "0";
            //ddlLineCode.SelectedValue = "0";
            txtIGMDate.Text = string.Empty;
            txtOnHireDate.Text = string.Empty;
        }
        public string GetTypeData(string val)
        {

            if (ddlType.SelectedIndex > 0) return ddlType.Items.FindByValue(val).Text;
            return "";
        }

        protected bool IsValidContainerNo()
        {
            bool IsValid = true;
            int total = 0;
            int step = 10;
            string containerNo = txtContainerNo.Text.ToUpper();

            Dictionary<char, int> AlphabetCodes = new Dictionary<char, int>();
            List<int> PowerOfMultipliers = new List<int>();
            for (int i = 65; i < 91; i++)
            {
                char c = (char)i;
                int pos = i - 65 + step;
                if (c == 'A' || c == 'K' || c == 'U') //omit multiples of 11.
                    step += 1;
                AlphabetCodes.Add(c, pos); //add to dictionary
            }
            for (int i = 0; i < 10; i++)
            {
                int result = (int)Math.Pow(2, i); //power of 2 calculation.
                PowerOfMultipliers.Add(result); //add to list.
            }
            if (containerNo.Length == 11) //container numbers must be 11 characters long.
            {
                for (int i = 0; i < 10; i++) //loop through the first 10 characters (the 11th is the check digit!).
                {
                    if (AlphabetCodes.ContainsKey(containerNo[i])) //if the current character is in the dictionary.
                        total += (AlphabetCodes[containerNo[i]] * PowerOfMultipliers[i]); //add it's value to the total.
                    else
                    {
                        int serialNumber = (int)containerNo[i] - 48; //it must be a number, so get the number from the char ascii value.
                        total += (serialNumber * PowerOfMultipliers[i]); //and add it to the total.
                    }
                }
                int checkDigit = (int)total % 11; //this should give you the check digit
                //The check digit shouldn't equal 10 according to ISO best practice - BUT there are containers out there that do, so we'll
                //double check and set the check digit to 0...again according to ISO best practice.
                if (checkDigit == 10)
                    checkDigit = 0;
                if (checkDigit != (int)containerNo[10] - 48) //check digit should equal the last character in the textbox.
                {
                    //errContainer.Text = "Container Number NOT Valid";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "container", "<script>javascript:void alert('Container Number NOT Valid!');</script>", false);
                    IsValid = false;
                }
                else
                {
                    //errContainer.Text = "Container Number Valid";
                    IsValid = true;
                }
            }
            else
            {
                //errContainer.Text = "Container Number must be 11 characters in length";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "container", "<script>javascript:void alert('Container Number must be 11 characters in length!');</script>", false);
                IsValid = false;
            }
            return IsValid;
        }
        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            counter = 1;

            var g = DateTime.Now;
            if (!IsValidContainerNo())
            {
                return;
            }
            else {
                if (rdTransactionType.SelectedValue == "F" &&  OnHireBLL.ValidateContainerStatus(txtContainerNo.Text)) {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), string.Format("alert('Please check the container {0}');", txtContainerNo.Text), true);
                }
            }
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
                    temp.ContainerNo = txtContainerNo.Text.JToUpper();
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
            if (!CheckContainerInList(lstEqpOnHireContainer, txtContainerNo.Text.Trim().JToUpper().StringRequired()) && !new OnHireBLL().ValidateOnHire(txtContainerNo.Text.Trim().JToUpper(), rdTransactionType.SelectedValue))// Check using proc
            {
                lstEqpOnHireContainer.Add(new EqpOnHireContainer
                 {
                     ContainerNo = txtContainerNo.Text.Trim().JToUpper(),
                     LGNo = txtLGNo.Text.JToUpper(),
                     IGMNo = txtIGMNo.Text.ToNullLong(),
                     CntrSize = (ddlSize.SelectedValue.Equals("0") ? "" : ddlSize.SelectedValue).StringRequired(),
                     ContainerTypeID = ddlType.SelectedValue.Trim().ToNullInt(),
                     IGMDate = txtIGMDate.Text.ToNullDateTime(),
                     ActualOnHireDate = txtOnHireDate.Text.ToNullDateTime(),
                     AddedOn = DateTime.Now,
                     EditedOn = DateTime.Now,
                     MovementOptID = rdTransactionType.SelectedValue=="N"?7:17,
                     UserAdded = user.Id,
                     UserLastEdited = user.Id
                 });
                ClearFieldUpper();
                Filler.GridDataBind(lstEqpOnHireContainer, gvwHire);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.Ticks.ToString(), string.Format("alert('{0} already exist in Table');", txtContainerNo.Text.Trim().JToUpper()), true);
            return;
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
            if (!ReturnAt()) {
                    ScriptManager.RegisterStartupScript(this,this.GetType(),DateTime.Now.Ticks.ToString(),string.Format("alert('ReturnAt is not valid');"),true);
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
                
                var lst = GetEqpOnHireContainers;
                //foreach (var t in lst)
                //{
                //if(new OnHireBLL().ValidateOnHire(t.ContainerNo, rdTransactionType.SelectedValue)){
                //    ScriptManager.RegisterStartupScript(this,this.GetType(),DateTime.Now.Ticks.ToString(),string.Format("alert('{0} already exist in Table');",t.ContainerNo),true);
                //return;
                //}
                //}
                var feu = CountFEU();
                IEqpOnHire onHire = new EqpOnHire
                {
                    AddedOn = DateTime.Now,
                    CompanyID = 1,//                    user.CompanyId
                    EditedOn = DateTime.Now,
                    FEUs = feu,
                    HireReference = txtHireReference.Text.Trim().JToUpper(),
                    HireReferenceDate = txtReferenceDate.Text.Trim().ToNullDateTime(),
                    LstEqpOnHireContainer = lst,
                    LocationID = ddlLocation.SelectedValue.ToInt(),
                    NVOCCID = ddlLineCode.SelectedValue.ToInt(),
                    Narration = txtNarration.Text.Trim().JToUpper(),
                    OnOffHire = rdTransactionType.SelectedValue.ToCharArray()[0],
                    ReturnedPortID = hdnReturn.Value.ToInt(),
                    ReleaseRefDate = txtReleaseDate.Text.ToNullDateTime(),
                    ReleaseRefNo = txtReleaseRefNo.Text.Trim().JToUpper(),
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Saved successfully.');", true);
                    Response.Redirect("Hire.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),"alert('Error! Please try again.')", true);
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
                    ddlSize.Enabled = true;
                    ddlType.Enabled = true;
                }
                else
                {

                    rfvValidTill.Enabled = false;
                    rfvLocation.Enabled = false;
                    lblValid.Visible = false;
                    lblStock.Visible = false;

                    rfvReturn.Enabled = true;
                    lblReturn.Visible = true;
                   // txtContainerNo.TextChanged+=txtContainerNo_TextChanged;
                    ddlSize.Enabled=false;
                    ddlType.Enabled=false;
                }
            }
            catch { }
        }

        protected void txtContainerNo_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidContainerNo())
            {
                return;
            }
            else
            {
                if (rdTransactionType.SelectedValue == "F" )
                {
                    var dt = OnHireBLL.GetContainerInfo(txtContainerNo.Text);
                     foreach(DataRow dr in dt.Rows) {
                        ddlType.SelectedValue= dr["fk_ContainerTypeID"].ToString();
                        ddlSize.SelectedValue= dr["CntrSize"].ToString();
                         break;
                     }
                }
                 
            }
        }
    }
}