using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.BLL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using EMS.Utilities.ResourceManager;

namespace EMS.WebApp.Transaction
{
    public partial class ManageMoneyReceipt : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            //RetriveParameters();
            //CheckUserAccess();
            //SetAttributes();

            if (!IsPostBack)
            {
                this.RetrieveSearchCriteria();
                this.LoadMoneyReceipt();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadMoneyReceipt();
            //upUser.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //txtUserName.Text = string.Empty;
            //txtFName.Text = string.Empty;
        }

        protected void gvwMoneyReceipt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwMoneyReceipts.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadMoneyReceipt();
        }
        protected void gvwMoneyReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadMoneyReceipt();
            }
            else if (e.CommandName == "Edit")
            {
                MoneyReceiptEntity mrSession = (Session["MROBJECT"] as List<MoneyReceiptEntity>).FirstOrDefault(mr=>mr.MRNo == e.CommandArgument.ToString());
                mrSession.IsEdited = 1;
                Session["MROBJECT"] = mrSession;
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteMoneyReceipt(e.CommandArgument.ToString());
            }
        }

        protected void gvwMoneyReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 8);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwMoneyReceipts.PageSize * gvwMoneyReceipts.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MRNo"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MRDate"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocationName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLNo"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MRAmount"));
               
                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MRNo"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MRNo"));

                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MRNo")) == "1")
                {
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00059") + "');return false;";
                }
                else
                {
                    if (_canDelete)
                    {
                        btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                    }
                    else
                    {
                        //btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                        btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                    }
                }
            }
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            //SaveNewPageSize(newPageSize);
            //LoadMoneyReceipt();
            //upMoneyReceipt.Update();
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            ////Get user permission.
            //UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess()
        {
            //if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            //{
            //    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

            //    if (ReferenceEquals(user, null) || user.Id == 0)
            //    {
            //        Response.Redirect("~/Login.aspx");
            //    }

            //    if (user.UserRole.Id != (int)UserRole.Admin)
            //    {
            //        Response.Redirect("~/Unauthorized.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Login.aspx");
            //}

            //if (!_canView)
            //{
            //    Response.Redirect("~/Unauthorized.aspx");
            //}
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                //txtWMEUserName.WatermarkText = ResourceManager.GetStringWithoutName("ERR00032");
                //txtWMEFName.WatermarkText = ResourceManager.GetStringWithoutName("ERR00033");
                //gvwMoneyReceipt.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                //gvwMoneyReceipt.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void DeleteMoneyReceipt(string mrNo)
        {
            MoneyReceiptsBLL mrBLL = new MoneyReceiptsBLL();
            mrBLL.DeleteMoneyReceipts(mrNo);
            //UserBLL userBll = new UserBLL();
            //userBll.DeleteUser(userId, _userId);
            //LoadUser();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
        }

        public List<MoneyReceiptEntity> GetMoneyReceiptsRearranged(List<MoneyReceiptEntity> mrs)
        {
            List<MoneyReceiptEntity> lstMoneyReceipts = mrs;
            List<MoneyReceiptEntity> lstMrs = null;
            if (lstMoneyReceipts != null)
            {
                //Get distinct MR nos.
                lstMrs = lstMoneyReceipts.GroupBy(lmr =>new {lmr.MRNo,lmr.BLNo}).Select(lmr => lmr.FirstOrDefault()).ToList();

                //Prepare charge details for each MR no.
                foreach (MoneyReceiptEntity mrNo in lstMrs)
                {
                  List<ChargeDetails> mrFiltered =  mrs.Where(mr => (mr.MRNo == mrNo.MRNo && mr.BLNo == mrNo.BLNo)).Select(mr =>
                      
                           new ChargeDetails
                           {
                               InvoiceNo = mr.InvoiceNo,
                               InvoiceTypeId = mr.InvoiceTypeId,
                               InvoiceDate = mr.InvoiceDate,
                               InvoiceAmount = mr.InvoiceAmount,
                               MRNo = mr.MRNo,
                               TDS = mr.TdsDeducted,
                               CashAmount =mr.CashPayment,
                               ChequeAmount = mr.ChequePayment,
                               ChequeDetails = mr.ChequeBank,
                               CurrentReceivedAmount = mr.CashPayment + mr.ChequePayment + mr.TdsDeducted,
                         
                           }).ToList();
                  mrNo.ChargeDetails = new List<ChargeDetails>(mrFiltered);
                  mrNo.ChargeDetails.ForEach(cdd=>cdd.ReceivedAmount =  mrNo.ChargeDetails.Sum(cd=>cd.CurrentReceivedAmount));
                }
            }

            return lstMrs;


        }

        private void LoadMoneyReceipt()
        {
             //List<MoneyReceiptEntity> moneyReceipts = new List<MoneyReceiptEntity>();
             //   MoneyReceiptEntity mr = new MoneyReceiptEntity(null);
             //   MoneyReceiptEntity moneyReceipt1 = new MoneyReceiptEntity(null);
             //   MoneyReceiptEntity moneyReceipt2 = new MoneyReceiptEntity(null);

             //   MoneyReceiptEntity mr1 = new MoneyReceiptEntity(null);
             //   mr1.BLNo = "XYZ";

             //   MoneyReceiptEntity moneyReceipt11 = new MoneyReceiptEntity(null);
             //   moneyReceipt11.BLNo = "XYZ";

             //   MoneyReceiptEntity moneyReceipt22 = new MoneyReceiptEntity(null);
             //   moneyReceipt22.MRNo = "TG";
             //   moneyReceipt22.BLNo = "XYZ";
             //       moneyReceipts.Add(mr);
             //       moneyReceipts.Add(moneyReceipt1);
             //       moneyReceipts.Add(moneyReceipt2);

             //       moneyReceipts.Add(mr1);
             //       moneyReceipts.Add(moneyReceipt11);
             //       moneyReceipts.Add(moneyReceipt22);

             //       moneyReceipts.Add(mr1);
             //       moneyReceipts.Add(moneyReceipt11);
             //       moneyReceipts.Add(moneyReceipt22);
                    //GetMoneyReceiptsRearranged(moneyReceipts);
                  //  gvwMoneyReceipts.DataSource = GetMoneyReceiptsRearranged(moneyReceipts);
                  //  gvwMoneyReceipts.DataBind();
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    MoneyReceiptsBLL mrBll = new MoneyReceiptsBLL();

                    gvwMoneyReceipts.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwMoneyReceipts.PageSize = searchCriteria.PageSize;
                    List<MoneyReceiptEntity> lstMRMaster = GetMoneyReceiptsRearranged(mrBll.GetMoneyReceipts(searchCriteria));
                    gvwMoneyReceipts.DataSource = lstMRMaster;
                    gvwMoneyReceipts.DataBind();
                    Session["MROBJECT"] = lstMRMaster;
                }
            }
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Transaction/AddEditMoneyReceipts.aspx?id=" + encryptedId);
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
                sortExpression = "MRNo";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.StringOption1 = this.txtInvoiceNo.Text.Trim(); //(txtUserName.Text == ResourceManager.GetStringWithoutName("ERR00032")) ? string.Empty : txtUserName.Text.Trim();
            criteria.StringOption2 = this.txtBLNo.Text.Trim(); //(txtFName.Text == ResourceManager.GetStringWithoutName("ERR00033")) ? string.Empty : txtFName.Text.Trim();
            criteria.StringOption3 = this.txtMoneyReceipt.Text.Trim();
            criteria.IntegerOption1 = this.drpDwnExportImport.SelectedItem.Text == "Export" ? 1 : 0;
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
                    if (criteria.CurrentPage != PageName.UserMaster)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        this.txtInvoiceNo.Text = criteria.StringOption1;
                        this.txtBLNo.Text = criteria.StringOption2;
                        this.txtMoneyReceipt.Text = criteria.StringOption3;
                        this.drpDwnExportImport.SelectedItem.Text = criteria.IntegerOption1.ToString();

                        gvwMoneyReceipts.PageIndex = criteria.PageIndex;
                        gvwMoneyReceipts.PageSize = criteria.PageSize;
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
            criteria.CurrentPage = PageName.UserMaster;
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

        private void ResetUserPassword(int uId)
        {
            //IUser user = new UserEntity();
            //user.Id = uId;
            //new UserBLL().ResetPassword(user, _userId);
            //SendResetPwdEmail(uId);
        }

        private void SendResetPwdEmail(int uId)
        {
        //    IUser user = new UserBLL().GetUser(uId);

        //    if (!ReferenceEquals(user, null))
        //    {
        //        string url = Convert.ToString(ConfigurationManager.AppSettings["ApplicationUrl"]) + "/Security/ChangePassword.aspx?id=" + GeneralFunctions.EncryptQueryString(uId.ToString());
        //        string msgBody = "Hello " + user.UserFullName + "<br/>We have received new password request for your account " + user.Name + ". Your temporary password is:" + Constants.DEFAULT_PASSWORD + " <br/>If this request was initiated by you, please click on following link and change your password:<br/><a href='" + url + "'>" + url + "</a>";

        //        try
        //        {
        //            CommonBLL.SendMail(Convert.ToString(ConfigurationManager.AppSettings["Sender"]), Convert.ToString(ConfigurationManager.AppSettings["DisplayName"]), user.EmailId, string.Empty, "Request for change password", msgBody, Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserAccount"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserPwd"]));
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00023") + "');</script>", false);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
        //        }
        //    }
        }

        #endregion
    }
}