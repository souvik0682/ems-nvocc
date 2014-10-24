using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IParty
    {
        int FwPartyID { get; set; }
        int CompanyID { get; set; }
        int LocID { get; set; }
        string LocName { get; set; }
        string PartyName { get; set; }
        string PartyType { get; set; }
        string PartyAddress { get; set; }
        string CountryName { get; set; }
        int CountryID { get; set; }
        int fLineID { get; set; }
        string LineName { get; set; }
        string ContactPerson { get; set; }
        string Phone { get; set; }
        string FAX { get; set; }
        string emailID { get; set; }
        string PAN { get; set; }
        string TAN { get; set; }
        int PrincipalID { get; set; }
        int UserID { get; set; }
        bool PartyStatus { get; set; }
    }
}
