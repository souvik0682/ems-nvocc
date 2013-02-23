using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.Utilities;

namespace EMS.Entity
{
    [Serializable]
    public class SearchCriteria : ISearchCriteria
    {
        #region Public Properties

        public string StringOption1 { get; set; }
        public string StringOption2 { get; set; }
        public string StringOption3 { get; set; }
        public string StringOption4 { get; set; }
        public string StringOption5 { get; set; }

        public int IntegerOption1 { get; set; }
        public int IntegerOption2 { get; set; }
        public int IntegerOption3 { get; set; }

        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string ContainerSize { get; set; }
        public string RoleName { get; set; }
        public DateTime? Date { get; set; }

        public string ChargeName { get; set; }
        public char ChargeType { get; set; }
        public string LineName { get; set; }

        public string IGMBLNo { get; set; }
        public string LineBLNo { get; set; }
        public DateTime? BLDate { get; set; }
        public string ContainerNo { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }


        public string LocAbbr
        {
            get;
            set;
        }

        public string LocName
        {
            get;
            set;
        }

        public string AreaName
        {
            get;
            set;
        }

        public string GroupName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string ExecutiveName
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public string SortExpression
        {
            get;
            set;
        }

        public string SortDirection
        {
            get;
            set;
        }

        public PageName CurrentPage
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public SearchCriteria()
        {

        }

        #endregion

        #region Public Methods

        public void Clear()
        {
            this.RoleName = string.Empty;
            this.AreaName = string.Empty;
            this.CustomerName = string.Empty;
            this.ExecutiveName = string.Empty;
            this.FirstName = string.Empty;
            this.GroupName = string.Empty;
            this.LocAbbr = string.Empty;
            this.LocName = string.Empty;
            this.SortDirection = string.Empty;
            this.SortExpression = string.Empty;
            this.UserId = 0;
            this.UserName = string.Empty;
            this.CurrentPage = 0;
            this.PageIndex = 0;
            this.PageSize = 0;
            this.BLDate = null;
            this.ContainerNo = string.Empty;
            this.IGMBLNo = string.Empty;
            this.LineBLNo = string.Empty;
            this.POD = string.Empty;
            this.POL = string.Empty;
            this.Vessel = string.Empty;
            this.Voyage = string.Empty;
        }

        #endregion
    }
}
