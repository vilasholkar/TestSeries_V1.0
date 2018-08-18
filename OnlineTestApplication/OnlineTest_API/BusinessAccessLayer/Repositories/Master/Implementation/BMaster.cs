using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Master;

namespace BusinessAccessLayer
{
   public class BMaster :IBMaster
    {
        private readonly IDMaster _iDMaster;
       public BMaster(IDMaster iDMaster)
       {
           _iDMaster = iDMaster;
       }
        public Response<List<StreamViewModel>> GetStream()
        {
            try
            {
                var masterData = _iDMaster.GetStream();
                if (masterData != null)
                {
                    return new Response<List<StreamViewModel>>
                    {
                        IsSuccessful = true,
                        Object = masterData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<StreamViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
