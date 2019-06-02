using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Settings
{
   public class ChangePasswordViewModel
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        

    }
}
