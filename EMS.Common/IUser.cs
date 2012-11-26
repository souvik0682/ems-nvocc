using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Common
{
    public interface IUser : IBase<int>, ICommon
    {
        string Password { get; set; }
        string NewPassword { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserFullName { get; }
        string EmailId { get; set; }
        char IsActive { get; set; }
    }
}
