using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Student;

namespace BusinessAccessLayer
{
    public interface IBStudent
    {
        Response<List<StudentViewModel>> GetStudentDetails();
    }
}
