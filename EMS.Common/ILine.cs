using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ILine : ICommon
    {

        int NVOCCID { get; set; }
        string NVOCCName { get; set; }
        bool NVOCCActive { get; set; }
        int DefaultFreeDays { get; set; }
        string ContAgentCode { get; set; }
        int UserAdded { get; set; }
        int UserLastEdited { get; set; }
        DateTime? AddedOn { get; set; }
        DateTime? EditedOn { get; set; }
        string LogoPath { get; set; }


    }
}
