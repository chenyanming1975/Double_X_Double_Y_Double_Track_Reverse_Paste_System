using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.UserManager
{
    public enum UserRole : int
    {
        SuperRoot = -1,
        Admin = 1,
        Engineer = 2,
        Technician = 3,
        Operator = 4,
        Visitor = 5,
    }
}
