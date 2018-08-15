using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.TestType;

namespace DataAccessLayer
{
    public interface IDTestType
    {
        List<TestTypeViewModel> GetTestType();

        string AddUpdateTestType(TestTypeViewModel objTestType);

        string DeleteTestType(TestTypeViewModel objTestType);
    }
}
