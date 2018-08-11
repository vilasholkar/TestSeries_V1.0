using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Student;

namespace DataAccessLayer
{
   public interface IDStudent
    {
       List<StudentViewModel> GetStudent();
    }
}
