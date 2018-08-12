using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ViewModels.TestType;
using ViewModels;

namespace BusinessAccessLayer
{
   public class BTestType:IBTestType
    {
       private readonly IDTestType _iDTestType;
       public BTestType(IDTestType iDTestType)
       {
           _iDTestType = iDTestType;
       }
        public Response<List<TestTypeViewModel>> GetTestType()
        {
            try
            {
                var testTypeData = _iDTestType.GetTestType();
                if (testTypeData != null)
                {
                    return new Response<List<TestTypeViewModel>>
                    {
                        IsSuccessful = true,
                        Object = testTypeData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<TestTypeViewModel>>
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
