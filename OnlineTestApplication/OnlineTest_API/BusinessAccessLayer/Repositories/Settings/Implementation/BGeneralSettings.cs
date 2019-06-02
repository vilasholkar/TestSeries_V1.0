using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Settings;

namespace BusinessAccessLayer
{
   public class BGeneralSettings :IBGeneralSettings
    {
        private readonly IDGeneralSettings _iDGeneralSettings;
        public BGeneralSettings(IDGeneralSettings iDGeneralSettings)
        {
            _iDGeneralSettings = iDGeneralSettings;
        }
        public Response<List<GeneralSettingsViewModel>> GetGeneralSettings() 
        {
            var generalSettingsDetail = _iDGeneralSettings.GetGeneralSettings();
            if (generalSettingsDetail != null)
            {
                return new Response<List<GeneralSettingsViewModel>>
                {
                    IsSuccessful = true,
                    Object = generalSettingsDetail,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<GeneralSettingsViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }

       public Response<string> ChangePassword(ChangePasswordViewModel changepassworddata)
        {
            var resultData = _iDGeneralSettings.ChangePassword(changepassworddata);
            if (!string.IsNullOrEmpty(resultData))
            {
                return new Response<string>
                {
                    IsSuccessful = true,
                    Object = resultData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<string>
                {
                    IsSuccessful = false,
                    Object = null,
                    Message = "error"
                };
            }
        }
    }
}
