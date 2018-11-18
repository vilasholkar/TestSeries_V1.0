using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Account;

namespace BusinessAccessLayer
{
   public class BAccount : IBAccount
    {
         private readonly IDAccount _iDAccount;
         public BAccount(IDAccount iDAccount)
        {
            _iDAccount = iDAccount;
        }
      public Login GetUserDetails(Login user)
       {
           return _iDAccount.GetUserDetails(user);
       }
    }
}
