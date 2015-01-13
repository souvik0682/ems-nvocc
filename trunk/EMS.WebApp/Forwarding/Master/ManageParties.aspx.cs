using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using EMS.Utilities.ResourceManager;
using EMS.Utilities;
using EMS.Entity;
using EMS.Common;
using EMS.BLL;

namespace EMS.WebApp.Forwarding.Master
{
    public partial class ManageParties : System.Web.UI.Page
    {
        #region Private Member Variables

        IUser user = null;

        public ISearchCriteria SearchCriteriaProp
        {
            get { return (ISearchCriteria)Session["SearchCriteria"]; }
            set { Session["SearchCriteria"] = value; }
        }
        private bool _hasEditAccess = true;
        //private bool _canAdd = false;
        //private bool _canEdit = false;
        //private bool _canDelete = false;
        //private bool _canView = false;
        private bool _canAdd = true;
        private bool _canEdit = true;
        private bool _canDelete = true;
        private bool _canView = true;
        private int _userId = 0;

        #endregion
        public int counter = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            if (!IsPostBack)
            {
               
               // CheckUserAccess();
                SetDeafaultSetting();
                FillData();
            }
        }

        private void SetDeafaultSetting()
        {
            DataTable PartyType = new CommonBLL().GetfwdPartyType();
            ddlPartyType.DataSource = PartyType;
            ddlPartyType.DataTextField = "PartyType";
            ddlPartyType.DataValueField = "pk_PartyTypeID";
            ddlPartyType.DataBind();
            ddlPartyType.Items.Insert(0, new ListItem("--Select--", "0"));


            SearchCriteria searchCriteria = new SearchCriteria
            {
                PageIndex = 0,
                PageSize = 10,
                SortDirection = "",
                SortExpression = "",
                StringOption4 = "0",
                PartyID = 0,
                LocAbbr = "",
                PartyName = txtPartyName.Text,
                StringParams = new List<string>() { ddlPartyType.SelectedValue == "0" ? "" : ddlPartyType.SelectedValue, txtFullName.Text }
            
            };
            Session["SearchCriteria"] = searchCriteria;

        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
        //    UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    if (_canView == false)
                    {
                        Response.Redirect("~/Unauthorized.aspx");
                    }

                    if (_canAdd == false)
                    {
                        btnAdd.Visible = false;
                    }
                    //Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void FillData()
        {
            counter = 1;
            gvwHire.PageSize = SearchCriteriaProp.PageSize;
            gvwHire.DataSource = new BLL.PartyBLL().GetParty(SearchCriteriaProp).ToList();
            gvwHire.DataBind();
            upLoc.Update();
        }

        protected void gvwHire_OnSorting(object sender, GridViewSortEventArgs e)
        {
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            string partyName = string.IsNullOrEmpty(txtPartyName.Text) ? "" : txtPartyName.Text.Trim();
            string FullName = string.IsNullOrEmpty(txtFullName.Text) ? "" : txtFullName.Text.Trim();
            string partyType = ddlPartyType.SelectedValue == "0" ? "" : ddlPartyType.SelectedValue;
            searchCriteria.PartyID = 0;
            searchCriteria.PartyName = partyName;
            searchCriteria.StringParams = new List<string>() { partyType, FullName };
            SearchCriteriaProp = searchCriteria;
            FillData();
        }

        protected void gvwHire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwHire.PageIndex = e.NewPageIndex;
            FillData();
        }

        protected void gvwHire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                
                ISearchCriteria searchCriteria = SearchCriteriaProp;

                if (searchCriteria.SortExpression == e.CommandArgument.ToString())
                {
                    if (searchCriteria.SortDirection == "Asc")
                    {
                        searchCriteria.SortDirection = "Desc";
                    }
                    else { searchCriteria.SortDirection = "Asc"; }
                }
                else { searchCriteria.SortDirection = "Asc"; }
                searchCriteria.SortExpression = e.CommandArgument.ToString();
                SearchCriteriaProp = searchCriteria;
                FillData();

            }
            else if (e.CommandName == "Edit")
            {
               
            }
            else if (e.CommandName == "Remove")
            {
                try
                {
                    var tempId = GeneralFunctions.DecryptQueryString(e.CommandArgument.ToString());
                    var companyId = 1;
                    new BLL.PartyBLL().DeleteParty(tempId.IntRequired(), _userId,companyId);
                    FillData();
                }
                catch
                {
                    throw;
                }
            }
        }



        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            searchCriteria.PageSize = newPageSize;
            searchCriteria.PageIndex = 0;
            SearchCriteriaProp = searchCriteria;
            gvwHire.PageSize = newPageSize;
            FillData();


        }

        protected void gvwHire_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");

                    btnRemove.Visible = true; //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";

                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forwarding/Master/AddEditParty.aspx");
        }
    }
}