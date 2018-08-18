using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Master;

namespace DataAccessLayer
{
   public class DMaster:IDMaster
    {
      public List<StreamViewModel> GetStream()
       {
           try
           {
               DataTable dt = DGeneric.RunSP_ReturnDataSet("exec sp_StreamSelect", null, null).Tables[0];
               List<StreamViewModel> StreamsList = new List<StreamViewModel>();
               foreach (DataRow dr in dt.Rows)
               {
                   StreamsList.Add(
                      new StreamViewModel
                      {
                          StreamID = Convert.ToInt32(dr["StreamID"]),
                          Stream = Convert.ToString(dr["Stream"]),
                      });
               }
               return StreamsList;

           }
           catch (Exception ex)
           {
               throw new Exception("Procedure::sp_StreamSelect::Error occured.", ex.InnerException);
           }
       }

    }
}
