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

        public bool ChangePassword(IUser user)
        {
            return UserDAL.ChangePassword(user);
        }

        public void DeleteUser(int userId, int modifiedBy)
        {
            UserDAL.DeleteUser(userId, modifiedBy);
        }

        public void ResetPassword(IUser user, int modifiedBy)
        {
            user.Password = GetDefaultPassword();
            UserDAL.ResetPassword(user, modifiedBy);
        }
    }
}
