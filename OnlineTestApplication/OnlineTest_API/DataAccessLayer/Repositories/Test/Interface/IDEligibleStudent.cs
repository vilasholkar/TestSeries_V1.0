using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
    public interface IDEligibleStudent
    {
        List<EligibleStudentViewModel> GetEligibleStudent(int OnlineTestID);

        string AddEligibleStudent(List<EligibleStudentViewModel> EligibleStudentData);
    }
}
