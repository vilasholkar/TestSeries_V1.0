using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Student
{
   public class StudentReport
    {
        public int StudentID { get; set; }
        public string StudentCode { get; set; }
        public int EnquiryStatusID { get; set; }
        public string EnrollmentNo { get; set; }
        public string EnrollmentDate { get; set; }
        public string EnrollmentDate_From { get; set; }
        public string EnrollmentDate_To { get; set; }
        public string AdharNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pin { get; set; }
        public string Medium { get; set; }
        public string Cast { get; set; }
        public string PhotoUrl { get; set; }
        public string SessionID { get; set; }
        public string Session { get; set; }
        public string[] StreamID { get; set; }
        public string Stream { get; set; }
        public string[] CourseID { get; set; }
        public string Course { get; set; }
        public string[] BatchID { get; set; }
        public string Batch { get; set; }
        public string School { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherMobile { get; set; }
        public string FatherEmail { get; set; }
        public string Status { get; set; }
        public bool IsJoined { get; set; }
        public bool IsEnquiry { get; set; }
        public bool IsNotJoined { get; set; }
        public string DeviceToken { get; set; }

    }
}
