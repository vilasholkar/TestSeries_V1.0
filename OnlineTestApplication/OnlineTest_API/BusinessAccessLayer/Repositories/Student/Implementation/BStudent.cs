using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ViewModels.Student;
using ViewModels;

namespace BusinessAccessLayer
{
    public class BStudent : IBStudent
    {
        private readonly IDStudent _iDStudent;
        public BStudent(IDStudent iDStudent)
        {
            _iDStudent = iDStudent;
        }
        public Response<List<StudentViewModel>> GetStudentDetails()
        {
            var studentData = _iDStudent.GetStudentDetails();
            if (studentData != null)
            {
                return new Response<List<StudentViewModel>>
                {
                    IsSuccessful = true,
                    Object = studentData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<StudentViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
    }
}
