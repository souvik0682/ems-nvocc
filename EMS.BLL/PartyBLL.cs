using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Common;
using EMS.DAL;
using EMS.Entity;

namespace EMS.BLL
{
   public class PartyBLL
    {
        public  List<IParty> GetParty(ISearchCriteria searchCriteria)
        {
            return PartyDAL.GetParty(searchCriteria);
        }

        public  int SaveParty(IParty Party, string Mode)
        {
            return PartyDAL.SaveParty(Party, Mode);
        }

        public  int DeleteParty(int PartyID, int UserID, int CompanyID)
        {
            return PartyDAL.DeleteParty(PartyID, UserID, CompanyID);
        }
    }
}
