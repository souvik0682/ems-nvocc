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
        #region User

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

        #endregion

        #region Role

        private void SetDefaultSearchCriteriaForRole(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Role";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IRole> GetAllRole(SearchCriteria searchCriteria)
        {
            return UserDAL.GetRole(false, searchCriteria);
        }

        public List<IRole> GetActiveRole()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForRole(searchCriteria);
            return UserDAL.GetRole(true, searchCriteria);
        }

        public IRole GetRole(int roleId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForRole(searchCriteria);
            return UserDAL.GetRole(roleId, false, searchCriteria);
        }

        public string SaveRole(List<RoleMenuEntity> lstRoleMenu, IRole role, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            string xmlDoc = GeneralFunctions.Serialize(lstRoleMenu);
            result = UserDAL.SaveRole(role, Constants.DEFAULT_COMPANY_ID, xmlDoc, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00072");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteRole(int roleId, int modifiedBy)
        {
            UserDAL.DeleteRole(roleId, modifiedBy);
        }

        public void ChangeRoleStatus(int roleId, bool status, int modifiedBy)
        {
            UserDAL.ChangeRoleStatus(roleId, status, modifiedBy);
        }

        public List<IRoleMenu> GetMenuByRole(int roleId, int mainId)
        {
            return UserDAL.GetMenuByRole(roleId, mainId);
        }

        #endregion
    }
}
