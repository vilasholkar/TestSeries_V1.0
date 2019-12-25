using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;
using ViewModels.TimeTable;

namespace ViewModels.Master
{
    public class MasterViewModel
    {
        public List<SessionViewModel> Session { get; set; }

        public List<StreamViewModel> Stream { get; set; }

        public List<TestSeriesViewModel> TestSeries { get; set; }

        public List<TestTypeViewModel> TestType { get; set; }

        public List<SubjectViewModel> Subject { get; set; }
        public List<TopicViewModel> Topic { get; set; }
        public List<SubTopicViewModel> SubTopic { get; set; }
        public List<BatchViewModel> Batch { get; set; }
        public List<LectureModel> DefaultLecture { get; set; }
        public List<UserViewModel> UserList { get; set; }
        public List<FacultyViewModel> FacultyList { get; set; }
    }
    public class SessionViewModel
    {
        public int SessionID { get; set; }
        public string Session { get; set; }
    }
    public class StreamViewModel
    {
        public int StreamID { get; set; }
        public string Stream { get; set; }
    }
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Course { get; set; }
        public int StreamID { get; set; }
    }
    public class BatchViewModel
    {
        public int BatchID { get; set; }
        public string Batch { get; set; }
        public int CourseID { get; set; }
    }

    public class UserViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PhotoURL { get; set; }
        public string AdharNo { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public int CreatedByUserID { get; set; }
        public string CreatedOnDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class FacultyViewModel
    {
        public int FacultyID { get; set; }
        public string Faculty { get; set; }
    }
}
