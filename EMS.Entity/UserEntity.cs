﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class UserEntity : IUser
    {
        #region IUser Members

        public string Password
        {
            get;
            set;
        }

        public string NewPassword
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string UserFullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public string EmailId
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public bool AllowMutipleLocation
        {
            get;
            set;
        }

        #endregion

        #region IRole Members

        public IRole UserRole
        {
            get;
            set;
        }

        #endregion

        #region ILocation Members

        public ILocation UserLocation
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

        #region ICommon Members

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public UserEntity()
        {
            this.UserRole = new RoleEntity();
            this.UserLocation = new LocationEntity();
        }

        public UserEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["UserId"]);
            this.Name = Convert.ToString(reader["UserName"]);
            this.FirstName = Convert.ToString(reader["FirstName"]);
            this.LastName = Convert.ToString(reader["LastName"]);
            this.UserRole = new RoleEntity(reader);
            this.UserLocation = new LocationEntity();
            this.UserLocation.Id = Convert.ToInt32(reader["LocId"]);
            this.UserLocation.Name = Convert.ToString(reader["LocName"]);

            if (reader["EmailId"] != DBNull.Value)
                this.EmailId = Convert.ToString(reader["EmailId"]);

            this.IsActive = Convert.ToBoolean(reader["Active"]);
            this.AllowMutipleLocation = Convert.ToBoolean(reader["AllowMutipleLocation"]);
        }

        #endregion
    }
}
