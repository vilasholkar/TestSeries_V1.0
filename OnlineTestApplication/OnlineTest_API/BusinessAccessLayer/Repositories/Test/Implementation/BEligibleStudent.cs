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
        public EligibleStudentViewModel[] GetEligibleStudent(int OnlineTestID)
        {
            try
            {
                return _iDEligibleStudent.GetEligibleStudent(OnlineTestID).ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string AddEligibleStudent(List<EligibleStudentViewModel> EligibleStudentData)
        {
            return _iDEligibleStudent.AddEligibleStudent(EligibleStudentData);
        }
    }
}
