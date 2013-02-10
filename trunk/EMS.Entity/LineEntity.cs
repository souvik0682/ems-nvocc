using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class LineEntity : ILine
    {
        public int NVOCCID { get; set; }

        public string NVOCCName { get; set; }

        public bool NVOCCActive { get; set; }

        public int DefaultFreeDays { get; set; }

        public string ContAgentCode { get; set; }

        public int UserAdded { get; set; }

        public int UserLastEdited { get; set; }

        public DateTime? AddedOn { get; set; }

        public DateTime? EditedOn { get; set; }

        public string LogoPath { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public LineEntity()
        {
        }

        public LineEntity(DataTableReader reader)
        {
            if (reader["DefaultFeeDays"] != DBNull.Value)
                this.DefaultFreeDays = Convert.ToInt32(reader["DefaultFeeDays"]);
            this.LogoPath = Convert.ToString(reader["LogoPath"]);
            this.NVOCCID = Convert.ToInt32(reader["pk_ProspectID"]);
            this.NVOCCName = Convert.ToString(reader["ProspectName"]);
        }
    }
}
