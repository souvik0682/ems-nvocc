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
        public static List<IParty> GetParty(ISearchCriteria searchCriteria)
         {
             string strExecution = "[fwd].[uspGetParty]";
             List<IParty> lstParty = new List<IParty>();
             using (DbQuery oDq = new DbQuery(strExecution))
             {
                 oDq.AddBigIntegerParam("@PartyId", searchCriteria.PartyID);
                 oDq.AddVarcharParam("@SchLocAbbr", 3, searchCriteria.LocAbbr);
                 oDq.AddVarcharParam("@SchPartyName", 60, searchCriteria.PartyName);
                 oDq.AddVarcharParam("@SchPhoneNo", 10, searchCriteria.StringParams[1]);
                 oDq.AddVarcharParam("@SchPartyType", 1, searchCriteria.StringParams[0]);
                 oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                 oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                 DataSet reader = oDq.GetTables();

                 if (reader != null && reader.Tables.Count > 0 && reader.Tables[0].Rows.Count > 0)
                 {
                     
                     for (int i = 0; i < reader.Tables[0].Rows.Count; i++) {
                         IParty iParty = new PartyEntity(reader.Tables[0].Rows[i]);
                         lstParty.Add(iParty);
                     }
                 }
             }
             return lstParty;
         }

         public static int SaveParty(IParty Party, string Mode)
         {
             string strExecution = "[fwd].[uspManageParty]";
             int Partyid = 0;

             using (DbQuery oDq = new DbQuery(strExecution))
             {
                 oDq.AddVarcharParam("@Mode", 1, Mode);
                 oDq.AddIntegerParam("@PartyId", Party.FwPartyID);
                 oDq.AddIntegerParam("@fk_CompanyID", Party.CompanyID);

                 oDq.AddIntegerParam("@LocId", Party.LocID);
                 oDq.AddVarcharParam("@PartyType", 1, Party.PartyType);
                 oDq.AddVarcharParam("@PartyName", 60, Party.PartyName);

                 oDq.AddVarcharParam("@PartyAddress", 500, Party.PartyAddress);
                 oDq.AddIntegerParam("@fk_CountryID", Party.CountryID);
                 oDq.AddIntegerParam("@fk_FlineID", Party.fLineID);

                 oDq.AddVarcharParam("@Fax", 100, Party.FAX);
                 oDq.AddVarcharParam("@Phone", 100, Party.Phone);
                 oDq.AddVarcharParam("@ContactPerson", 100, Party.ContactPerson);

                 oDq.AddVarcharParam("@PAN", 100, Party.PAN);
                 oDq.AddVarcharParam("@TAN", 100, Party.TAN);
                 oDq.AddVarcharParam("@EmailID", 100, Party.emailID);

                 oDq.AddBigIntegerParam("@fk_PrincipalID", Party.PrincipalID);                 
                 oDq.AddIntegerParam("@UserID", Party.UserID);
                 int result = 0;
                 oDq.AddIntegerParam("@Result",result, QueryParameterDirection.Output);
                 Partyid = Convert.ToInt32(oDq.GetScalar());
                 if (Mode != "A") {
                     return 1;
                 }
             }
             return Partyid;
         }

         public static int DeleteParty(int PartyID, int UserID, int CompanyID)
         {
             string strExecution = "[fwd].[uspManageParty]";
             int ret = 0;
             using (DbQuery oDq = new DbQuery(strExecution))
             {
                 oDq.AddVarcharParam("@Mode", 1, "D");
                 oDq.AddIntegerParam("@PartyId", PartyID);
                 oDq.AddIntegerParam("@fk_CompanyID", CompanyID);

                 oDq.AddIntegerParam("@LocId", 0);
                 oDq.AddVarcharParam("@PartyType", 1, "");
                 oDq.AddVarcharParam("@PartyName", 60,"");

                 oDq.AddVarcharParam("@PartyAddress", 500, "");
                 oDq.AddIntegerParam("@fk_CountryID", 0);
                 oDq.AddIntegerParam("@fk_FlineID", 0);

                 oDq.AddVarcharParam("@Fax", 100, "");
                 oDq.AddVarcharParam("@Phone", 100,"");
                 oDq.AddVarcharParam("@ContactPerson", 100,"");

                 oDq.AddVarcharParam("@PAN", 100, "");
                 oDq.AddVarcharParam("@TAN", 100,"");
                 oDq.AddVarcharParam("@EmailID", 100, "");

                 oDq.AddBigIntegerParam("@fk_PrincipalID", 0);
                 oDq.AddIntegerParam("@UserID", UserID);
                 int result = 0;
                 oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                                

                 ret = Convert.ToInt32(oDq.GetScalar());
             }

             return ret;
         }
    }
}
