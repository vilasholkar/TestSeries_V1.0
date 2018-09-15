using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Test;

namespace BusinessAccessLayer
{
    public class BEligibleStudent : IBEligibleStudent
    {
        private readonly IDEligibleStudent _iDEligibleStudent;
        public BEligibleStudent(IDEligibleStudent iDEligibleStudent)
        {
            _iDEligibleStudent = iDEligibleStudent;
        }
        public Response<List<EligibleStudentViewModel>> GetEligibleStudent(int OnlineTestID)
        {
            try
            {
                var eligibleStudentData = _iDEligibleStudent.GetEligibleStudent(OnlineTestID);
                if (eligibleStudentData != null)
                {
                    return new Response<List<EligibleStudentViewModel>>
                    {
                        IsSuccessful = true,
                        Object = eligibleStudentData,
                        Message = "Success"
                    };
                }
                else
                {
                    return new Response<List<EligibleStudentViewModel>>
                    {
                        IsSuccessful = false,
                        Message = "error",
                        Object = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<EligibleStudentViewModel>>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Object = null
                };
            }
        }
    }
}
