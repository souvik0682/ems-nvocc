using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;
using EMS.Entity;
using EMS.WebApp.CustomControls;

namespace EMS.WebApp.Forwarding.Master
{
    public partial class AddEditParty : System.Web.UI.Page
    {
        PartyEntity oPartyEntity;
        #region Private Member Variables


        private int _userId = 0;
        private string countryId = "";
        //private bool _canAdd = false;
        //private bool _canEdit = false;
        //private bool _canDelete = false;
        //private bool _canView = false;
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;

        private int PartyId { get { if (ViewState["Id"] != null) { return Convert.ToInt32(ViewState["Id"]); } return 0; } set { ViewState["Id"] = value; } }
        private string Mode { get { if (ViewState["Id"] != null) { return "E"; } return "A"; }  }
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {

            RetriveParameters();
            if (!IsPostBack)
            {
                LoadDefault();
                if (Request.QueryString["PartyId"] != string.Empty)
                {

                    try
                    {
                        var id = GeneralFunctions.DecryptQueryString(Request.QueryString["PartyId"]);
                        var pid = Convert.ToInt32(id);
                        if (pid > 0)
                        {
                            PartyId = pid;
                            LoadData(pid);
                        }
                    }
                    catch { }
                }
            }
           
         //   CheckUserAccess(countryId);
        }

        private void RetriveParameters()
        {
            _userId = EMS.BLL.UserBLL.GetLoggedInUserId();

            //Get user permission.
          //  EMS.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess(string xID)
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

                    if (!_canEdit && xID != "-1")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "-1")
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
            SaveParty();

        }

        #endregion

        #region Private Methods

        private void LoadDefault()
        {
            var line = new CommonBLL().GetfwLineByType(new SearchCriteria { StringOption1 = "S,A" });
            var principal = new CommonBLL().GetfwLineByType(new SearchCriteria { StringOption1 = "O" });
            ddlLine.DataSource = line;
            ddlLine.DataTextField = "LineName";
            ddlLine.DataValueField = "LineID";
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPrincipal.DataSource = principal;
            ddlPrincipal.DataTextField = "LineName";
            ddlPrincipal.DataValueField = "LineID";
            ddlPrincipal.DataBind();
            ddlPrincipal.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void LoadData(int id)
        {
            var src = new PartyBLL().GetParty(new SearchCriteria() { PartyID = id, StringParams = new List<string>() { "",""} });
            if (src != null && src.Count() > 0)
            {
                var party = src.FirstOrDefault();
                txtEmailID.Text = party.emailID;
                txtContactPerson.Text = party.ContactPerson;
                txtFAX.Text = party.FAX;
                txtPAN.Text = party.PAN;
                txtPartyName.Text = party.PartyName;
                txtPhone.Text = party.Phone;
                txtTAN.Text = party.TAN;
                ddlLine.SelectedValue = party.fLineID.ToString();
                ddlPartyType.SelectedValue = party.PartyType;
                ddlPrincipal.SelectedValue = party.PrincipalID.ToString();
                AutoCompleteCountry1.CountryId = party.CountryID.ToString();
                AutoCompleteCountry1.CountryName = party.CountryName;
            }
        }
        private void ClearText()
        {
            txtEmailID.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtFAX.Text = string.Empty;
            txtPAN.Text = string.Empty;
            txtPartyName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtTAN.Text = string.Empty;
            ddlLine.SelectedValue = "0";
            ddlPartyType.SelectedValue = "0";
            ddlPrincipal.SelectedValue = "0";


        }

        private PartyEntity ExtractData()
        {
            //var party = 

            //oPartyEntity = new PartyEntity();
            //oPartyEntity.emailID = txtEmailID.Text;
            //oPartyEntity.ContactPerson = txtContactPerson.Text;
            //oPartyEntity.FAX = txtFAX.Text;
            //oPartyEntity.PAN = txtPAN.Text;
            //oPartyEntity.PartyName = txtPartyName.Text;
            //oPartyEntity.Phone = txtPhone.Text;
            //oPartyEntity.TAN = txtTAN.Text;
            //oPartyEntity.fLineID = Convert.ToInt32(ddlLine.SelectedValue);
            //oPartyEntity.PartyType = ddlPartyType.SelectedValue;
            //oPartyEntity.PrincipalID = Convert.ToInt32(ddlPrincipal.SelectedValue);
            //oPartyEntity.FwPartyID = PartyId;
            //oPartyEntity.PartyAddress = "";
            //oPartyEntity.CountryID = AutoCompleteCountry1.CountryId.IntRequired();
            //oPartyEntity.UserID = _userId;
            //oPartyEntity.LocID = 4;

            //return oPartyEntity;


            return new PartyEntity
            {
                emailID = txtEmailID.Text,//
                ContactPerson = txtContactPerson.Text,//
                FAX = txtFAX.Text,//

                PAN = txtPAN.Text,//
                PartyName = txtPartyName.Text,//
                Phone = txtPhone.Text,//

                TAN = txtTAN.Text,//
                fLineID = Convert.ToInt32(ddlLine.SelectedValue),
                PartyType = ddlPartyType.SelectedValue,//

                PrincipalID = Convert.ToInt32(ddlPrincipal.SelectedValue),//
                FwPartyID = PartyId,//
                PartyAddress = "",//

                CompanyID = 1,//

                CountryID = AutoCompleteCountry1.CountryId.IntRequired(),
                LocID = 4,//
                UserID = _userId//


            };

        }
        private void SaveParty()
        {
          
            var result = new PartyBLL().SaveParty(ExtractData(), Mode);     
            if (result > 0)
            {
                PartyId = result;
                Response.Redirect("~/Forwarding/Master/ManageParties.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}