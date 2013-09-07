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

namespace EMS.WebApp.MasterModule
{
    public partial class ExpSlotCostList : System.Web.UI.Page
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
                //RetriveParameters();
                //CheckUserAccess();
                Filler.FillData(ddlLine, new DBInteraction().GetNVOCCLine(-1, "").Tables[0], "NVOCCName", "pk_NVOCCID", "--LINE--");
                SetDeafaultSetting();
                
                FillData(); 
            }
        }

        private void SetDeafaultSetting()
        {
            FillSearchCriteria();
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            searchCriteria.PageIndex = 0;
            searchCriteria.PageSize = 10;
            searchCriteria.SortDirection = "";
            searchCriteria.SortExpression = "";
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
            if (SearchCriteriaProp.PageSize == 0) {SearchCriteriaProp.PageSize=Convert.ToInt32(ddlPaging.SelectedValue); }
            gvwSlotCost.PageSize = SearchCriteriaProp.PageSize;
            gvwSlotCost.DataSource = BLL.ExpSlotCostBLL.GetSlotCost(SearchCriteriaProp);
            gvwSlotCost.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetDeafaultSetting();
            FillData();
        }
        private void FillSearchCriteria()
        {
            
            ISearchCriteria searchCriteria = new SearchCriteria();
            if (searchCriteria == null) { searchCriteria = new SearchCriteria(); }
            searchCriteria.StringParams.Add(ddlLine.SelectedValue=="0"?String.Empty:ddlLine.SelectedItem.Text);
            searchCriteria.StringParams.Add(txtSlotOperator.Text);
            searchCriteria.StringParams.Add(txtLoadPort.Text);
            searchCriteria.StringParams.Add(txtDestinationPort.Text);
            searchCriteria.StringParams.Add(string.IsNullOrEmpty(txtEffectiveDate.Text)?string.Empty:Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"));
            searchCriteria.StringParams.Add("1");//CompanyId
            searchCriteria.StringParams.Add("0");//SlotID
            SearchCriteriaProp = searchCriteria;
        }
        protected void gvwSlotCost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwSlotCost.PageIndex = e.NewPageIndex;
            FillSearchCriteria();
            ISearchCriteria searchCriteria = SearchCriteriaProp;
            SearchCriteriaProp.PageIndex = newIndex;
            SearchCriteriaProp = searchCriteria;
            FillData();
        }

        protected void gvwSlotCost_RowCommand(object sender, GridViewCommandEventArgs e)
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
              
            }
            else if (e.CommandName == "Remove")
            {
                try{
                var tempId=GeneralFunctions.DecryptQueryString(e.CommandArgument.ToString());
                BLL.ExpSlotCostBLL.DeleteSlotCost(tempId.LongRequired());
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
            FillSearchCriteria();
            searchCriteria.PageSize = newPageSize;
            searchCriteria.PageIndex = 0;
            SearchCriteriaProp = searchCriteria;
            gvwSlotCost.PageSize = newPageSize;
            FillData();


        }

        protected void gvwSlotCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            
        }
    }
}