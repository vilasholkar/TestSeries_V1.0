using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Settings;

namespace DataAccessLayer
{
   public class DGeneralSettings : IDGeneralSettings
    {
       public List<GeneralSettingsViewModel> GetGeneralSettings()
       {
           DataTable dt = DGeneric.GetData("Select * from GeneralSettings").Tables[0];

           if (dt.Rows.Count > 0)
               return DGeneric.BindDataList<GeneralSettingsViewModel>(dt);
           else
               return new List<GeneralSettingsViewModel>();
       }
       public string ChangePassword(ChangePasswordViewModel CP)
       {
           List<SqlParameter> sqlParameterList = new List<SqlParameter>();
           sqlParameterList.Add(new SqlParameter("UserID", CP.UserID));
           sqlParameterList.Add(new SqlParameter("UserTypeID", CP.UserTypeID));
           sqlParameterList.Add(new SqlParameter("CurrentPassword", CP.CurrentPassword));
           sqlParameterList.Add(new SqlParameter("NewPassword", CP.NewPassword));
           return DGeneric.RunSP_ReturnScaler("sp_ChangePassword", sqlParameterList);
       }
    }
}
