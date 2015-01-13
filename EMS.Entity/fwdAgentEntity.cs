using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class fwdAgentEntity : IfwdAgent
    {
        public int AgentID
        {
            get;
            set;
        }

        public bool Action
        {
            get;
            set;
        }

        public string NVOCC
        {
            get;
            set;
        }

        public string FPOD
        {
            get;
            set;
        }

        public int LinerID
        {
            get;
            set;
        }
        public string FPODID
        {
            get;
            set;
        }

        public string AgentName
        {
            get;
            set;
        }

        public string ContactPerson
        {
            get;
            set;
        }

        public string AgentAddress
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string FAX
        {
            get;
            set;
        }

        public string email
        {
            get;
            set;
        }

        public string PAN
        {
            get;
            set;
        }

        public bool AgentStatus
        {
            get;
            set;
        }

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

        public fwdAgentEntity()
        {

        }


        public fwdAgentEntity(DataTableReader reader)
        {
            this.AgentID = Convert.ToInt32(reader["pk_AgentID"]);
            this.AgentName = Convert.ToString(reader["AgentName"]);
            this.AgentAddress = Convert.ToString(reader["AgentAddress"]);
            this.LinerID = Convert.ToInt32(reader["fk_LIneID"]);
            this.ContactPerson = Convert.ToString(reader["ContactPerson"]);
            this.AgentStatus = Convert.ToBoolean(reader["AgentStatus"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["email"])))
                this.email = Convert.ToString(reader["email"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["Phone"])))
                this.Phone = Convert.ToString(reader["Phone"]);
            if (!String.IsNullOrEmpty(Convert.ToString(reader["FAX"])))
                this.FAX = Convert.ToString(reader["FAX"]);
            this.NVOCC = Convert.ToString(reader["NVOCC"]);
            this.FPOD = Convert.ToString(reader["PortName"]);
            this.FPODID = Convert.ToString(reader["FK_FPOD"]);
        }
    }
}
