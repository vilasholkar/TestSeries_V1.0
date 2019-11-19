using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Student
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            AttendenceList = new List<Attendance>();
        }
        public int StudentAccountID { get; set; }
        //public string Password { get; set; }
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string EnrollmentDate { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string FatherMobile { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherEmail { get; set; }
        public string Aadhar { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        //public Nullable<int> CountryID { get; set; }
        //public Nullable<int> StateID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Pincode { get; set; }
        public string Caste { get; set; }
        public string School { get; set; }
        public string PhotoUrl { get; set; }
        public string Medium { get; set; }
        //public bool IsConfigure { get; set; }
        //public bool IsActive { get; set; }
        //public int SessionID { get; set; }
        //public string Session { get; set; }
        //public int CourseID { get; set; }
        //public string Course { get; set; }
        //public int StreamID { get; set; }
        //public string Stream { get; set; }
        //public int BatchID { get; set; }
        //public string Batch { get; set; }
        public List<Attendance> AttendenceList { get; set; }
    }
}
