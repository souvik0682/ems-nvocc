using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class UserPermission:IUserPermission
    {
        #region IUserPermission Members

        public bool CanAdd
        {
            get;
            set;
        }

        public bool CanEdit
        {
            get;
            set;
        }

        public bool CanDelete
        {
            get;
            set;
        }

        public bool CanView
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        #endregion
    }
}
