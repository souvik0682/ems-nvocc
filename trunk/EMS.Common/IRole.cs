﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IRole : IBase<int>
    {
        string RoleType { get; set; }
    }
}
