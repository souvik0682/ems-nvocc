using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.View
{
    public partial class AddEditRole : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private enum MainMenuItem
        {
            Master = 1,
            Import = 2,
            Finance = 3,
            Logistic = 4,
            Export = 5
        };

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void gvwMst_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwMst, e.Row);
            }
        }

        protected void gvwImp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwImp, e.Row);
            }
        }

        protected void gvwFin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwFin, e.Row);
            }
        }

        protected void gvwLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwLog, e.Row);
            }
        }

        protected void gvwExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopulateControlsForGridView(gvwExp, e.Row);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRole();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageRole.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                rfvRole.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00052");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _roleId);
            }
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

                //if (user.UserRole.Id != (int)UserRole.Admin)
                //{
                //    Response.Redirect("~/Unauthorized.aspx");
                //}
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (_roleId == 0)
                Response.Redirect("~/View/ManageRole.aspx");
        }

        private void PopulateControlsForGridView(GridView gvw, GridViewRow row)
        {
            GeneralFunctions.ApplyGridViewAlternateItemStyle(row, 6);

            //ScriptManager sManager = ScriptManager.GetCurrent(this);

            row.Cells[0].Text = ((gvw.PageSize * gvw.PageIndex) + row.RowIndex + 1).ToString();
            row.Cells[1].Text = Convert.ToString(DataBinder.Eval(row.DataItem, "MenuName"));

            //Add
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanAdd")))
                ((CheckBox)row.FindControl("chkAdd")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkAdd")).Checked = false;

            //Edit
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanEdit")))
                ((CheckBox)row.FindControl("chkEdit")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkEdit")).Checked = false;

            //Delete
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanDelete")))
                ((CheckBox)row.FindControl("chkDel")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkDel")).Checked = false;

            //View
            if (Convert.ToBoolean(DataBinder.Eval(row.DataItem, "CanView")))
                ((CheckBox)row.FindControl("chkView")).Checked = true;
            else
                ((CheckBox)row.FindControl("chkView")).Checked = false;
        }

        private void LoadData()
        {
            UserBLL userBll = new UserBLL();

            gvwMst.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Master);
            gvwMst.DataBind();

            gvwImp.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Import);
            gvwImp.DataBind();

            gvwFin.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Finance);
            gvwFin.DataBind();

            gvwLog.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Logistic);
            gvwLog.DataBind();

            gvwExp.DataSource = userBll.GetMenuByRole(_roleId, (int)MainMenuItem.Export);
            gvwExp.DataBind();
        }

        private void SaveRole()
        {
            CommonBLL commonBll = new CommonBLL();
            ILocation loc = new LocationEntity();
            string message = string.Empty;
            BuildLocationEntity(loc);
            commonBll.SaveLocation(loc, _userId);
            Response.Redirect("~/View/ManageRole.aspx");
        }

        private void BuildLocationEntity(ILocation loc)
        {

        }

        #endregion
    }
}