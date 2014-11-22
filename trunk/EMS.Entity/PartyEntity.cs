
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using System.Data;

namespace EMS.Entity
{
    public class PartyEntity : IParty
    {
        string _partyType = string.Empty;
        public int FwPartyID { get; set; }
        public int CompanyID { get; set; }
        public int LocID { get; set; }
        public string LocName { get; set; }
        public string PartyName { get; set; }
        public int PartyType { get; set;}
        public string PartyTypeName {get; set;}
       
        public string PartyAddress { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public int fLineID { get; set; }
        public string LineName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string FAX { get; set; }
        public string emailID { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public int PrincipalID { get; set; }
        public int UserID { get; set; }
        public bool PartyStatus { get; set; }
        //public string PartyTypeName { get; set; }
        //Dictionary<string, string> PartyTypeDic = null;
        //private void Initalize()
        //{
        //    PartyTypeDic = new Dictionary<string, string>();
        //    PartyTypeDic.Add("A", "Custome Agent");
        //    PartyTypeDic.Add("T", "Transporter");
        //    PartyTypeDic.Add("O", "Overseas Agent");
        //    PartyTypeDic.Add("D", "Debtors");
        //    PartyTypeDic.Add("C", "Creditors");
        //}
        public PartyEntity()
        {
            //Initalize();
        }
        public PartyEntity(DataTableReader reader)
        {
            //Initalize();
            if (ColumnExists(reader, "CustID"))
                if (reader["CustID"] != DBNull.Value)
                    FwPartyID = Convert.ToInt32(reader["CustID"]);

            if (ColumnExists(reader, "fk_CompanyID"))
                if (reader["fk_CompanyID"] != DBNull.Value)
                    CompanyID = Convert.ToInt32(reader["fk_CompanyID"]);

            if (ColumnExists(reader, "fk_LocID"))
                if (reader["fk_LocID"] != DBNull.Value)
                    LocID = Convert.ToInt32(reader["fk_LocID"]);

            if (ColumnExists(reader, "CountryName"))
                if (reader["CountryName"] != DBNull.Value)
                    CountryName = Convert.ToString(reader["CountryName"]);

            if (ColumnExists(reader, "LocName"))
                if (reader["LocName"] != DBNull.Value)
                    LocName = Convert.ToString(reader["LocName"]);
            if (ColumnExists(reader, "PartyName"))
                if (reader["PartyName"] != DBNull.Value)
                    PartyName = Convert.ToString(reader["PartyName"]);

            if (ColumnExists(reader, "PartyType"))
                if (reader["PartyType"] != DBNull.Value)
                    PartyType = Convert.ToInt32(reader["PartyType"]);

            if (ColumnExists(reader, "PartyTypeName"))
                if (reader["PartyTypeName"] != DBNull.Value)
                    PartyTypeName = Convert.ToString(reader["PartyTypeName"]);

            if (ColumnExists(reader, "PartyAddress"))
                if (reader["PartyAddress"] != DBNull.Value)
                    PartyAddress = Convert.ToString(reader["PartyAddress"]);

            if (ColumnExists(reader, "fk_CountryID"))
                if (reader["fk_CountryID"] != DBNull.Value)
                    CountryID = Convert.ToInt32(reader["fk_CountryID"]);

            if (ColumnExists(reader, "fk_fLineID"))
                if (reader["fk_fLineID"] != DBNull.Value)
                    fLineID = Convert.ToInt32(reader["fk_fLineID"]);

            if (ColumnExists(reader, "LineName"))
                if (reader["LineName"] != DBNull.Value)
                    LineName = Convert.ToString(reader["LineName"]);

            if (ColumnExists(reader, "ContactPerson"))
                if (reader["ContactPerson"] != DBNull.Value)
                    ContactPerson = Convert.ToString(reader["ContactPerson"]);

            if (ColumnExists(reader, "Phone"))
                if (reader["Phone"] != DBNull.Value)
                    Phone = Convert.ToString(reader["Phone"]);

            if (ColumnExists(reader, "FAX"))
                if (reader["FAX"] != DBNull.Value)
                    FAX = Convert.ToString(reader["FAX"]);

            if (ColumnExists(reader, "emailID"))
                if (reader["emailID"] != DBNull.Value)
                    emailID = Convert.ToString(reader["emailID"]);

            if (ColumnExists(reader, "PAN"))
                if (reader["PAN"] != DBNull.Value)
                    PAN = Convert.ToString(reader["PAN"]);

            if (ColumnExists(reader, "TAN"))
                if (reader["TAN"] != DBNull.Value)
                    TAN = Convert.ToString(reader["TAN"]);

            if (ColumnExists(reader, "fk_PrincipalID"))
                if (reader["fk_PrincipalID"] != DBNull.Value)
                    PrincipalID = Convert.ToInt32(reader["fk_PrincipalID"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    UserID = Convert.ToInt32(reader["fk_UserAdded"]);

            if (ColumnExists(reader, "PartyStatus"))
                if (reader["PartyStatus"] != DBNull.Value)
                    PartyStatus = Convert.ToBoolean(reader["PartyStatus"]);
        }

        public PartyEntity(DataRow reader)
        {
            //Initalize();
            if (ColumnExists(reader, "CustID"))
                if (reader["CustID"] != DBNull.Value)
                    FwPartyID = Convert.ToInt32(reader["CustID"]);

            if (ColumnExists(reader, "fk_CompanyID"))
                if (reader["fk_CompanyID"] != DBNull.Value)
                    CompanyID = Convert.ToInt32(reader["fk_CompanyID"]);

            if (ColumnExists(reader, "fk_LocID"))
                if (reader["fk_LocID"] != DBNull.Value)
                    LocID = Convert.ToInt32(reader["fk_LocID"]);

            if (ColumnExists(reader, "CountryName"))
                if (reader["CountryName"] != DBNull.Value)
                    CountryName = Convert.ToString(reader["CountryName"]);

            if (ColumnExists(reader, "LocName"))
                if (reader["LocName"] != DBNull.Value)
                    LocName = Convert.ToString(reader["LocName"]);
            if (ColumnExists(reader, "PartyName"))
                if (reader["PartyName"] != DBNull.Value)
                    PartyName = Convert.ToString(reader["PartyName"]);

            if (ColumnExists(reader, "PartyType"))
                if (reader["PartyType"] != DBNull.Value)
                    PartyType = Convert.ToInt32(reader["PartyType"]);

            if (ColumnExists(reader, "PartyTypeName"))
                if (reader["PartyTypeName"] != DBNull.Value)
                    PartyTypeName = Convert.ToString(reader["PartyTypeName"]);

            if (ColumnExists(reader, "PartyAddress"))
                if (reader["PartyAddress"] != DBNull.Value)
                    PartyAddress = Convert.ToString(reader["PartyAddress"]);

            if (ColumnExists(reader, "fk_CountryID"))
                if (reader["fk_CountryID"] != DBNull.Value)
                    CountryID = Convert.ToInt32(reader["fk_CountryID"]);

            if (ColumnExists(reader, "fk_fLineID"))
                if (reader["fk_fLineID"] != DBNull.Value)
                    fLineID = Convert.ToInt32(reader["fk_fLineID"]);

            if (ColumnExists(reader, "LineName"))
                if (reader["LineName"] != DBNull.Value)
                    LineName = Convert.ToString(reader["LineName"]);

            if (ColumnExists(reader, "ContactPerson"))
                if (reader["ContactPerson"] != DBNull.Value)
                    ContactPerson = Convert.ToString(reader["ContactPerson"]);

            if (ColumnExists(reader, "Phone"))
                if (reader["Phone"] != DBNull.Value)
                    Phone = Convert.ToString(reader["Phone"]);

            if (ColumnExists(reader, "FAX"))
                if (reader["FAX"] != DBNull.Value)
                    FAX = Convert.ToString(reader["FAX"]);

            if (ColumnExists(reader, "emailID"))
                if (reader["emailID"] != DBNull.Value)
                    emailID = Convert.ToString(reader["emailID"]);

            if (ColumnExists(reader, "PAN"))
                if (reader["PAN"] != DBNull.Value)
                    PAN = Convert.ToString(reader["PAN"]);

            if (ColumnExists(reader, "TAN"))
                if (reader["TAN"] != DBNull.Value)
                    TAN = Convert.ToString(reader["TAN"]);

            if (ColumnExists(reader, "fk_PrincipalID"))
                if (reader["fk_PrincipalID"] != DBNull.Value)
                    PrincipalID = Convert.ToInt32(reader["fk_PrincipalID"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    UserID = Convert.ToInt32(reader["fk_UserAdded"]);

            if (ColumnExists(reader, "PartyStatus"))
                if (reader["PartyStatus"] != DBNull.Value)
                    PartyStatus = Convert.ToBoolean(reader["PartyStatus"]);
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper() == columnName.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        public bool ColumnExists(DataRow reader, string columnName)
        {
            try { var row = reader[columnName];
            return true;
            }
            catch { }
            return false;
        }
    }

}
