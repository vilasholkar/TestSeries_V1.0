using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Account;

namespace BusinessAccessLayer
{
   public interface IBAccount
    {
       Response<Login> GetUserDetails(Login user);
       Response<ForgetPassword> ForgetPassword(ForgetPassword objForgetPassword);

    }
}
