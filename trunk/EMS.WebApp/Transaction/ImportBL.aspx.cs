using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using EMS.Common;
using EMS.BLL;
using System.Configuration;
using EMS.Entity;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Transaction
{
    public partial class ImportBL : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        //private int _locId = 0;
        //private bool _hasEditAccess = true;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadImportBL();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadImportBL();
            upBL.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtIGMBLNo.Text = "";
            txtLineBLNo.Text = ""; 
            txtPOD.Text = "";
            txtPOL.Text = "";
            txtVessel.Text = "";
            txtVoyageNo.Text = "";
            txtSLine.Text = "";
            txtSLocation.Text = "";

            SaveNewPageIndex(0);
            LoadImportBL();
            upBL.Update();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadImportBL();
            upBL.Update();
        }

        protected void gvImportBL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                if (ViewState[Constants.SORT_EXPRESSION] == null)
                {
                    ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    ViewState[Constants.SORT_DIRECTION] = "ASC";
                }
                else
                {
                    if (ViewState[Constants.SORT_EXPRESSION].ToString() == e.CommandArgument.ToString())
                    {
                        if (ViewState[Constants.SORT_DIRECTION].ToString() == "ASC")
                            ViewState[Constants.SORT_DIRECTION] = "DESC";
                        else
                            ViewState[Constants.SORT_DIRECTION] = "ASC";
                    }
                    else
                    {
                        ViewState[Constants.SORT_DIRECTION] = "ASC";
                        ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    }
                }

                LoadImportBL();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteImportBL(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvImportBL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 6);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                //e.Row.Cells[0].Text = ((gvImportBL.PageSize * gvImportBL.PageIndex) + e.Row.RowIndex + 1).ToString();

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ImpLineBLNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ImpLineBLNo"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselName"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VoyageNo"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PortOfLoading"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PortOfDischarge"));

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00070");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));
            }
        }

        protected void gvImportBL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvImportBL.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadImportBL();
        }


        #region Private Methods

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                //if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager && user.UserRole.Id != (int)UserRole.SalesExecutive)
                //{
                //    Response.Redirect("~/Unauthorized.aspx");
                //}
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    _roleId = user.UserRole.Id;
                }

                //if (!ReferenceEquals(user.UserLocation, null))
                //{
                //    _locId = user.UserLocation.Id;
                //}
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                //txtWMELoc.WatermarkText = ResourceManager.GetStringWithoutName("ERR00031");
                //txtWMECust.WatermarkText = ResourceManager.GetStringWithoutName("ERR00035");
                //txtWMEGr.WatermarkText = ResourceManager.GetStringWithoutName("ERR00034");
                //txtWMEExec.WatermarkText = ResourceManager.GetStringWithoutName("ERR00069");
                ////gvwCust.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

                gvImportBL.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void LoadImportBL()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    
                    gvImportBL.PageIndex = searchCriteria.PageIndex;

                    if (searchCriteria.PageSize > 0) gvImportBL.PageSize = searchCriteria.PageSize;

                    //if (_roleId == (int)UserRole.Management)
                    //    gvImportBL.DataSource = commonBll.GetActiveCustomer(searchCriteria);
                    //else
                    //    gvImportBL.DataSource = commonBll.GetAllCustomer(searchCriteria);

                    gvImportBL.DataSource = new ImportBLL().GetImportBL(searchCriteria);
                    gvImportBL.DataBind();
                }
            }
        }

        private void DeleteImportBL(int blId)
        {
            //CommonBLL commonBll = new CommonBLL();
            //commonBll.DeleteCustomer(custId, _userId);
            LoadImportBL();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Transaction/ManageImportBL.aspx?BLId=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            int roleId = UserBLL.GetLoggedInUserRoleId();

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }
            else
            {
                sortExpression = "LineBLNo";
                sortDirection = "ASC";
            }

            //criteria.PageIndex = gvwCust.PageIndex;
            criteria.UserId = _userId;
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.LineBLNo = (txtLineBLNo.Text == "") ? string.Empty : txtLineBLNo.Text.Trim();
            criteria.IGMBLNo = (txtIGMBLNo.Text == "") ? string.Empty : txtIGMBLNo.Text.Trim();
            //criteria.ContainerNo = (txtContainerNo.Text == "") ? string.Empty : txtContainerNo.Text.Trim();
            criteria.POD = (txtPOD.Text == "") ? string.Empty : txtPOD.Text.Trim();
            criteria.POL = (txtPOL.Text == "") ? string.Empty : txtPOL.Text.Trim();
            criteria.Vessel = (txtVessel.Text == "") ? string.Empty : txtVessel.Text.Trim();
            criteria.Voyage = (txtVoyageNo.Text == "") ? string.Empty : txtVoyageNo.Text.Trim();
            criteria.LineName = (txtSLine.Text == "") ? string.Empty : txtSLine.Text.Trim();
            criteria.Location = (txtSLocation.Text == "") ? string.Empty : txtSLocation.Text.Trim();

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void RetrieveSearchCriteria()
        {
            bool isCriteriaExists = false;

            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    if (criteria.CurrentPage != PageName.ImportBL)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        //txtContainerNo.Text = criteria.ContainerNo;
                        txtIGMBLNo.Text = criteria.IGMBLNo;
                        txtLineBLNo.Text = criteria.LineBLNo;
                        txtPOD.Text = criteria.POD;
                        txtPOL.Text = criteria.POL;
                        txtVessel.Text = criteria.Vessel;
                        txtVoyageNo.Text = criteria.Voyage;
                        txtSLocation.Text = criteria.Location;
                        txtSLine.Text = criteria.LineName;

                        gvImportBL.PageIndex = criteria.PageIndex;
                        gvImportBL.PageSize = criteria.PageSize;
                        ddlPaging.SelectedValue = criteria.PageSize.ToString();
                        isCriteriaExists = true;
                    }
                }
            }

            if (!isCriteriaExists)
            {
                SearchCriteria newcriteria = new SearchCriteria();
                SetDefaultSearchCriteria(newcriteria);
            }
        }

        private void SetDefaultSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = "LineBLNo";
            string sortDirection = "ASC";

            criteria.CurrentPage = PageName.ImportBL;
            criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void SaveNewPageIndex(int newIndex)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    criteria.PageIndex = newIndex;
                }
            }
        }

        private void SaveNewPageSize(int newPageSize)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    criteria.PageSize = newPageSize;
                }
            }
        }

        #endregion

        
    }
}