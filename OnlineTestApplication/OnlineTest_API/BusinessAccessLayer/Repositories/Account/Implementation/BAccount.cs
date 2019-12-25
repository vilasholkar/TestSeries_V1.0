using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
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
        public Response<Login> GetUserDetails(Login user)
        {
            var loginData = _iDAccount.GetUserDetails(user);
            if (loginData != null)
            {
                return new Response<Login>
                {
                    IsSuccessful = true,
                    Object = loginData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<Login>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }

        public Response<string> UpdateDeviceToken(DeviceTokenViewModel objDeviceToken)
        {
            var updateDeviceToken = _iDAccount.UpdateDeviceToken(objDeviceToken);
            if (updateDeviceToken == "Success")
            {
                return new Response<string>
                {
                    IsSuccessful = true,
                    Object = updateDeviceToken,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<string>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }


        public Response<ForgetPassword> ForgetPassword(ForgetPassword objForgetPassword)
        {
            var forgetPasswordData = _iDAccount.ForgetPassword(objForgetPassword);
            if (forgetPasswordData != null)
            {
                return new Response<ForgetPassword>
                {
                    IsSuccessful = true,
                    Object = forgetPasswordData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<ForgetPassword>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }

    }
}
