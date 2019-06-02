using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Account;

namespace DataAccessLayer
{
    public interface IDAccount
    {
        Login GetUserDetails(Login user);
        ForgetPassword ForgetPassword(ForgetPassword objForgetPassword);
    }
}
