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
        public  List<IParty> GetParty(int partyid, ISearchCriteria searchCriteria)
        {
            return PartyDAL.GetParty(partyid, searchCriteria);
        }

        public  int SaveParty(IParty Party, string Mode)
        {
            return PartyDAL.SaveParty(Party, Mode);
        }

        public  int DeleteParty(int PartyID, int UserID, int CompanyID)
        {
            return PartyDAL.DeleteParty(PartyID, UserID, CompanyID);
        }

        public int DeleteGroup(int GroupID, int UserID)
        {
            return PartyDAL.DeleteGroup(GroupID, UserID);
        }

        public List<IGroup> GetGroup(int partyid, ISearchCriteria searchCriteria)
        {
            return PartyDAL.GetGroup(partyid, searchCriteria);
        }

        public int SaveGroup(IGroup Party, string Mode)
        {
            return PartyDAL.SaveGroup(Party, Mode);
        }

    }
}
