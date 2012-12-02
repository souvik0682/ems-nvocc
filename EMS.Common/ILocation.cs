﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface ILocation : IBase<int>, ICommon
    {
        IAddress LocAddress { get; set; }
        string Abbreviation { get; set; }
        string Phone { get; set; }
        char IsActive { get; set; }
    }
}
