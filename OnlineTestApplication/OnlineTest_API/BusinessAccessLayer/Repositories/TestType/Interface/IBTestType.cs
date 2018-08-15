using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.TestType;

namespace BusinessAccessLayer
{
    public interface IBTestType
    {
       Response<List<TestTypeViewModel>> GetTestType();
       string AddUpdateTestType(TestTypeViewModel objTestType);
       string DeleteTestType(string queryString);
    }
}
