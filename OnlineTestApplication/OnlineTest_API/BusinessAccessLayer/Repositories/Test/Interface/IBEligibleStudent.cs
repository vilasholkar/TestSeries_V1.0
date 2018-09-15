using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
    public interface IBEligibleStudent
    {
        Response<List<EligibleStudentViewModel>> GetEligibleStudent(int OnlineTestID);
    }
}
