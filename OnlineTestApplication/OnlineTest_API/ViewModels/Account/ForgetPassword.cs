using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Account
{
   public class ForgetPassword
    {
        public string UserName { get; set; }
        public int UserTypeID { get; set; }
        public string ConfirmOTP { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        
    }
}
