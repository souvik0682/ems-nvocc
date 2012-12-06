using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.DAL;
using EMS.Common;
using EMS.Entity;
using EMS.Utilities;
using System.Web;
using EMS.Utilities.ResourceManager;
using EMS.Utilities.Cryptography;

namespace EMS.BLL
{
    public class UserBLL
    {
        public static string GetDefaultPassword()
        {
            return Encryption.Encrypt(Constants.DEFAULT_PASSWORD);
        }

        public bool ValidateUser(IUser user)
        {
            UserDAL.ValidateUser(user);
            return (user.Id > 0) ? true : false;
        }

        public static int GetLoggedInUserId()
        {
            int userId = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null))
                {
                    userId = user.Id;
                }
            }

            return userId;
        }

        public static int GetLoggedInUserRoleId()
        {
            int roleId = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null) && user.Id > 0)
                {
                    if (!ReferenceEquals(user.UserRole, null))
                    {
                        roleId = user.UserRole.Id;
                    }
                }
            }

            return roleId;
        }

        private void SetDefaultSearchCriteriaForUser(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "UserName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IUser> GetAllUserList(SearchCriteria searchCriteria)
        {
            return UserDAL.GetUserList(false, searchCriteria);
        }

        public List<IUser> GetActiveUserList()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForUser(searchCriteria);
            return UserDAL.GetUserList(true, searchCriteria);
        }

        public IUser GetUser(int userId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForUser(searchCriteria);
            return UserDAL.GetUser(userId, false, searchCriteria);
        }

        public string SaveUser(IUser user, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = UserDAL.SaveUser(user, Constants.DEFAULT_COMPANY_ID, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00060");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteUser(int userId, int modifiedBy)
        {
            UserDAL.DeleteUser(userId, modifiedBy);
        }

        public bool ChangePassword(IUser user)
        {
            return UserDAL.ChangePassword(user);
        }

        public void ResetPassword(IUser user, int modifiedBy)
        {
            user.Password = GetDefaultPassword();
            UserDAL.ResetPassword(user, modifiedBy);
        }

        #region Role

        public List<IRole> GetRole()
        {
            return UserDAL.GetRole();
        }

        public IRole GetRole(int roleId)
        {
            return UserDAL.GetRole(roleId);
        }

        #endregion
    }
}
