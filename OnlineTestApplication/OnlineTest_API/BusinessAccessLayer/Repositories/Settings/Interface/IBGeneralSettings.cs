using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Settings;

namespace BusinessAccessLayer
{
   public interface IBGeneralSettings
    {
        Response<List<GeneralSettingsViewModel>> GetGeneralSettings();
        Response<string> ChangePassword(ChangePasswordViewModel changepassworddata);
    }
}
