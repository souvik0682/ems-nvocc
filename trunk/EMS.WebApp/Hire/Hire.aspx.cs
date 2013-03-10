using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Entity;
using EMS.Common;

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

        #endregion
        public int counter = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
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
            
            if(e.Row.RowType==DataControlRowType.DataRow ){
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

            }
        }
    }
}