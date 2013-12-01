using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Entity.Report;
using EMS.Utilities;
using EMS.Utilities.ReportManager;
using EMS.Utilities.ResourceManager;
using Microsoft.Reporting.WebForms;

namespace EMS.WebApp.Export
{
    public partial class DOList : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadDeliveryOrder();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadDeliveryOrder();
            upList.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBookingNo.Text = string.Empty;
            txtBookingRef.Text = string.Empty;
            txtDONo.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtLine.Text = string.Empty;
            SaveNewPageIndex(0);
            LoadDeliveryOrder();
            upList.Update();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(0);
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadDeliveryOrder();
            upList.Update();
        }

        protected void gvwList_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadDeliveryOrder();
            }
            else if (e.CommandName == "EditData")
            {
                RedirecToAddEditPage(Convert.ToInt64(e.CommandArgument));
            }
            else if (e.CommandName == "RemoveData")
            {
                DeleteDeliveryOrder(Convert.ToInt64(e.CommandArgument));
            }
            else if (e.CommandName == "PrintData")
            {
                GenerateReport(Convert.ToInt64(e.CommandArgument));
            }
        }

        protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 10);
                e.Row.Cells[0].Text = ((gvwList.PageSize * gvwList.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocationName"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "NVOCCName"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingNumber"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingRef"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DeliveryOrderNumber"));
                e.Row.Cells[6].Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DeliveryOrderDate"), _culture).ToString(Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Containers"));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DeliveryOrderId"));

                // Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");

                if (_canDelete)
                {
                    btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                    btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DeliveryOrderId"));
                }
                else
                {
                    btnRemove.Style["display"] = "none";
                }

                // Print link

                ImageButton btnPrint = (ImageButton)e.Row.FindControl("btnPrint");
                ScriptManager sManager = ScriptManager.GetCurrent(this);
                if (!ReferenceEquals(sManager, null)) sManager.RegisterPostBackControl(btnPrint);

                btnPrint.ToolTip = ResourceManager.GetStringWithoutName("ERR00083");
                btnPrint.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DeliveryOrderId"));
            }
        }

        protected void gvwList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwList.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadDeliveryOrder();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                gvwList.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                gvwList.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
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
                    if (!_canView)
                    {
                        Response.Redirect("~/Unauthorized.aspx");
                    }

                    if (!_canAdd)
                    {
                        btnAdd.Visible = false;
                    }
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

        private void RedirecToAddEditPage(Int64 doId)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(doId.ToString());
            Response.Redirect("~/Export/DOEdit.aspx?id=" + encryptedId);
        }

        private void LoadDeliveryOrder()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    CommonBLL commonBll = new CommonBLL();

                    gvwList.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwList.PageSize = searchCriteria.PageSize;

                    gvwList.DataSource = DOBLL.GetDeliveryOrder(searchCriteria, _userId);
                    gvwList.DataBind();
                }
            }
        }

        private void DeleteDeliveryOrder(Int64 doId)
        {
            string message = DOBLL.DeleteDeliveryOrder(doId);
            LoadDeliveryOrder();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + message + "');</script>", false);
        }

        private void GenerateReport(Int64 doId)
        {
            LocalReportManager reportManager = new LocalReportManager("DeliveryOrder", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            ReportBLL cls = new ReportBLL();

            List<DOPrintEntity> lstDO = ReportBLL.GetDeliveryOrder(doId);
            List<DOPrintEntity> lstDOCntr = ReportBLL.GetDeliveryOrderContainer(doId);
            ReportDataSource dsDO = new ReportDataSource("dsDeliveryOrder", lstDO);
            ReportDataSource dsDOCntr = new ReportDataSource("dsDeliveryOrderContainer", lstDOCntr);
            reportManager.ReportFormat = ReportFormat.PDF;
            reportManager.AddDataSource(dsDO);
            reportManager.AddDataSource(dsDOCntr);
            reportManager.Export();
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }
            else
            {
                sortExpression = "DONo";
                sortDirection = "DESC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.BookingNo = txtBookingNo.Text.Trim();
            criteria.DONumber = txtDONo.Text.Trim();
            criteria.LineName = txtLine.Text.Trim();
            criteria.Location = txtLocation.Text.Trim();
            criteria.BookingRef = txtBookingRef.Text.Trim();

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
                    if (criteria.CurrentPage != PageName.DeliveryOrder)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        txtBookingNo.Text = criteria.BookingNo;
                        txtDONo.Text = criteria.DONumber;
                        txtLine.Text = criteria.LineName;
                        txtLocation.Text = criteria.Location;
                        txtBookingRef.Text = criteria.BookingRef;
                        gvwList.PageIndex = criteria.PageIndex;
                        gvwList.PageSize = criteria.PageSize;
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
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.CurrentPage = PageName.DeliveryOrder;
            criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
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