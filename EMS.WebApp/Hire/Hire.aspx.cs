using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Entity;
using EMS.Common;
using EMS.BLL;

namespace EMS.WebApp.Hire
{
    public partial class Hire : System.Web.UI.Page
    {
        #region Private Member Variables

        IUser user = null;

        public ISearchCriteria SearchCriteriaProp
        {
            get { return (ISearchCriteria)ViewState["SearchCriteria"]; }
            set { ViewState["SearchCriteria"] = value; }
        }
        private bool _hasEditAccess = true;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userId = 0;

        #endregion
        public int counter = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RetriveParameters();
                CheckUserAccess();
                SetDeafaultSetting();
                FillData(); 
            }
        }

        private void SetDeafaultSetting()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                PageIndex = 0,
                PageSize = 10,
                SortDirection = "",
                SortExpression = "",
                StringOption4="0"
            };
            ViewState["SearchCriteria"] = searchCriteria;

        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
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
            gvwHire.DataSource = new BLL.OnHireBLL().GetOnHire(SearchCriteriaProp);
            gvwHire.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            searchCriteria.StringOption1 = txtContainerNo.Text.Trim();
            searchCriteria.StringOption2 = txtReferenceNo.Text.Trim();
            searchCriteria.StringOption3 = txtRefDate.Text.Trim();
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
                //a.OnOffHire,a.HireReference,a.HireReferenceDate,a.ValidTill,a.ReleaseRefNo,e.PortName
                ISearchCriteria searchCriteria = SearchCriteriaProp;

                if (searchCriteria.SortExpression == e.CommandArgument.ToString()) {
                    if (searchCriteria.SortDirection == "Asc")
                    {
                        searchCriteria.SortDirection = "Desc";
                    }
                    else { searchCriteria.SortDirection = "Asc"; }
                }
                searchCriteria.SortExpression = e.CommandArgument.ToString();
                SearchCriteriaProp = searchCriteria;
                FillData();
                
            }
            else if (e.CommandName == "Edit")
            {
                
              //  HttpContext.Current.Items.Add("HireId",e.CommandArgument);
                //Server.Transfer("AddEditHire.aspx");
            }
            else if (e.CommandName == "Remove")
            {
                try{
                var tempId=GeneralFunctions.DecryptQueryString(e.CommandArgument.ToString());
                new BLL.OnHireBLL().DeleteOnHire(tempId.LongRequired());
                FillData();
                }catch{
                throw ;
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
            
            if(e.Row.RowType==DataControlRowType.DataRow )
            {
                var t = ((System.Data.DataRowView)e.Row.DataItem).Row.ItemArray;
                var m = t[3].ToString();
                var m1 = t[29].ToString();
                if (m == "On Hire" && m1 == "RCVE")
                {
                    var btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                    var btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    if (btnEdit != null) { btnEdit.Visible = false; }
                    if (btnRemove != null) { btnRemove.Visible = false; }
                }
                 if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    
                    btnRemove.Visible = true;
                    //btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                    //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));

                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }

            }
        }
    }
}