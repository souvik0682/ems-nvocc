using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL.DbManager;
using EMS.Entity;

namespace EMS.DAL
{
    public class PartyDAL
    {
         public static List<IParty> GetParty(SearchCriteria searchCriteria)
         {
             string strExecution = "[fwd].[uspGetParty]";
             List<IParty> lstParty = new List<IParty>();
             using (DbQuery oDq = new DbQuery(strExecution))
             {
                 oDq.AddBigIntegerParam("@PartyId", searchCriteria.PartyID);
                 oDq.AddVarcharParam("@SchLocAbbr", 70, searchCriteria.LocAbbr);
                 oDq.AddVarcharParam("@SchPartyName", 10, searchCriteria.PartyName);
                 oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                 oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                 DataTableReader reader = oDq.GetTableReader();

                 while (reader.Read())
                 {
                     IParty iParty = new PartyEntity(reader);
                     lstParty.Add(iParty);
                 }

                 reader.Close();
             }
             return lstParty;
         }

         public static long SaveParty(IParty Party, string Mode)
         {
             string strExecution = "[fwd].[uspGetParty]";
             long Partyid = 0;

             using (DbQuery oDq = new DbQuery(strExecution))
             {
                 oDq.AddVarcharParam("@Mode", 1, Mode);
                 oDq.AddIntegerParam("@userID", Party.UserID);
                 oDq.AddIntegerParam("@PartyID", Party.FwPartyID);
                 oDq.AddIntegerParam("@fk_CompanyID", Party.CompanyID);
                 oDq.AddIntegerParam("@LocId", Party.LocID);
                 oDq.AddIntegerParam("@fk_CountryID", Party.CountryID);
                 oDq.AddVarcharParam("@PartyType", 1, Party.PartyType);
                 oDq.AddVarcharParam("@PartyName", 60, Party.PartyName);
                 oDq.AddVarcharParam("@PartyAddress", 500, Party.PartyAddress);
                 oDq.AddIntegerParam("@fk_FlineID", Party.fLineID);
                 oDq.AddVarcharParam("@Fax", 100, Party.FAX);
                 oDq.AddVarcharParam("@Phone", 100, Party.Phone);
                 oDq.AddVarcharParam("@ContactPerson", 100, Party.ContactPerson);
                 oDq.AddVarcharParam("@PAN", 100, Party.PAN);
                 oDq.AddVarcharParam("@TAN", 100, Party.TAN);
                 oDq.AddVarcharParam("@EmailID", 100, Party.emailID);
                 oDq.AddBigIntegerParam("@fk_PrincipalID", Party.PrincipalID);
                 Partyid = Convert.ToInt64(oDq.GetScalar());
             }
             return Partyid;
         }

         public static int DeleteParty(int PartyID, int UserID, int CompanyID)
         {
             string strExecution = "[fwd].[uspManageParty]";
             int ret = 0;
             using (DbQuery oDq = new DbQuery(strExecution))
             {
                 oDq.AddBigIntegerParam("@PartyId", PartyID);
                 oDq.AddVarcharParam("@Mode", 1, "D");
                 oDq.AddBigIntegerParam("@UserID", UserID);

                 ret = Convert.ToInt32(oDq.GetScalar());
             }

             return ret;
         }
    }
}
