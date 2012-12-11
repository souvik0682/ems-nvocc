using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;

namespace EMS.Entity
{
    public class RoleMenuEntity : IRoleMenu
    {
        #region IRoleMenu Members

        public int MenuAccessID
        {
            get;
            set;
        }

        public int CompanyID
        {
            get;
            set;
        }

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

        #region IMenu Members

        public int MenuID
        {
            get;
            set;
        }

        public int MainID
        {
            get;
            set;
        }

        public int SubID
        {
            get;
            set;
        }

        public int SubSubID
        {
            get;
            set;
        }

        public string MenuName
        {
            get;
            set;
        }

        #endregion

        #region IRole Members

        public bool? LocationSpecific
        {
            get;
            set;
        }

        public bool? RoleStatus
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        #endregion

        #region IBase<int> Members

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public RoleMenuEntity()
        {

        }

        public RoleMenuEntity(DataTableReader reader)
        {
            this.MenuAccessID = Convert.ToInt32(reader["MenuAccessID"]);
            this.CompanyID = Convert.ToInt32(reader["CompanyID"]);
            this.Id = Convert.ToInt32(reader["RoleID"]);
            this.MenuID = Convert.ToInt32(reader["MenuID"]);
            this.MainID = Convert.ToInt32(reader["MainID"]);
            this.SubID = Convert.ToInt32(reader["SubID"]);
            this.SubSubID = Convert.ToInt32(reader["SubSubID"]);
            this.CanAdd = Convert.ToBoolean(reader["CanAdd"]);
            this.CanEdit = Convert.ToBoolean(reader["CanEdit"]);
            this.CanDelete = Convert.ToBoolean(reader["CanDelete"]);
            this.CanView = Convert.ToBoolean(reader["CanView"]);
            this.MenuName = Convert.ToString(reader["MenuName"]);
        }

        #endregion
    }
}
