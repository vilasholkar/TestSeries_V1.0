using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Settings;

namespace DataAccessLayer
{
   public interface IDGeneralSettings
    {
        List<GeneralSettingsViewModel> GetGeneralSettings();
        string ChangePassword(ChangePasswordViewModel changepassworddata);

    }
}
