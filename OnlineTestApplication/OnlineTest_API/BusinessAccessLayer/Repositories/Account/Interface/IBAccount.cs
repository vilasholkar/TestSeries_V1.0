using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Account;

namespace BusinessAccessLayer
{
   public interface IBAccount
    {
       Login GetUserDetails(Login user);
    }
}
