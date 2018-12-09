using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Account
{
    public  class Login
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
    }
}
