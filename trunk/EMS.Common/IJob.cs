using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IJob : ICommon
    {
        long JobID { get; set; }
        bool JobStatus { get; set; }
        DateTime JobDate { get; set; }
        string JobType { get; set; }
        int JobTypeID { get; set; }
        string JobPrefix { get; set; }
        string PJobNo { get; set; }
        string JobNo { get; set; }
        int OpsLocID { get; set; }
        int jobLocID { get; set; }
        int SalesmanID { get; set; }
        int SmodeID { get; set; }
        string JobLoc { get; set; }
        string OpsLoc { get; set; }
        string ShippingMode { get; set; }
        int PrDocID { get; set; }
        bool PrintHBL { get; set; }
        int HBLFormatID { get; set; }
        int ttl20 { get; set; }
        int ttl40 { get; set; }
        decimal grwt { get; set; }
        decimal VolWt { get; set; }
        decimal weightMT { get; set; }
        decimal volCBM { get; set; }
        decimal RevTon { get; set; }
        int FLineID { get; set; }
        string PlaceOfReceipt { get; set; }
        int fk_LportID { get; set; }
        int fk_DportID { get; set; }
        string PlaceOfDelivery { get; set; }
        int fk_CustID { get; set; }
        int fk_CustAgentID { get; set; }
        int fk_TransID { get; set; }
        int fk_OSID { get; set; }
        decimal EstPayable { get; set; }
        decimal EstReceivable { get; set; }
        char JobActive { get; set; }
        char CargoSource { get; set; }
        int JobScopeID { get; set; }
        int CreditDays { get; set; }
    }
}
