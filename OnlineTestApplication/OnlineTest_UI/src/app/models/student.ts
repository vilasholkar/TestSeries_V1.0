export class StudentOnlineTest {

    EligibleStudentID: any;
    StudentID: any;
    TestStatusID:any;
    OnlineTestID: any;
    OnlineTestNo: any;
    TestSeriesID: any;
    TestSeries: any;
    TestTypeID: any;
    TestType: any;
    TestName: any;
    TestDuration: any;
    SessionID: any;
    StreamID: any;
    CourseID: any;
    BatchID: any;
    SubjectID: any;
    Topic: any;
    Instructions: any;
    TestMarks: any;
    PassingPercentage: any;
    IsNegativeMarking: any;
    StartDate: any;
    StartTime: any;
    EndDate: any;
    EndTime: any;
    IsVisible: any;
    IsActive: any;
    CreatedByUserID: any;
    CreatedOnDate: any;
    TestStatus:any;
   
    constructor(data:any)
    {
        data = data || {};
        this.EligibleStudentID = data.EligibleStudentID;
        this.StudentID = data.StudentID;
        this.TestStatusID=data.TestStatusID;
        this.OnlineTestID = data.OnlineTestID;
        this.OnlineTestNo = data.OnlineTestNo;
        this.TestSeriesID = data.TestSeriesID;
        this.TestSeries = data.TestSeries;
        this.TestTypeID = data.TestTypeID;
        this.TestType = data.TestType;
        this.TestName = data.TestName;
        this.TestDuration = data.TestDuration;
        this.SessionID = data.SessionID;
        this.StreamID = data.StreamID;
        this.StreamID = data.StreamID;
        this.BatchID = data.BatchID;
        this.SubjectID = data.SubjectID;
        this.Topic = data.Topic;
        this.Instructions = data.Instructions;
        this.TestMarks = data.TestMarks;
        this.PassingPercentage = data.PassingPercentage;
        this.IsNegativeMarking = data.IsNegativeMarking;
        this.IsNegativeMarking = data.StartDateIsNegativeMarking;
        this.StartDate = data.StartDate;
        this.StartTime = data.StartTime;
        this.EndDate = data.EndDate;
        this.EndTime = data.EndTime;
        this.IsVisible = data.IsVisible;
        this.IsActive = data.IsActive;
        this.CreatedByUserID = data.CreatedByUserID;
        this.CreatedOnDate = data.CreatedOnDate;
        this.TestStatus = data.TestStatus;
    }
}

export class SA {
    StudentID: any;
    FirstName: any;
    EnrollmentNo: any;

    constructor(data:any)
    {
        data = data || {};
        this.StudentID = data.StudentID;
        this.FirstName = data.FirstName;
        this.EnrollmentNo = data.EnrollmentNo;

    }
  }
export class Student {
    StudentAccountID: any;
    StudentID: any;
    EnrollmentNo: any;
    EnrollmentDate: any;
    FirstName: any;
    MiddleName: any;
    LastName: any;
    Gender: any;
    DOB: any;
    MobileNo: any;
    PhoneNo: any;
    Email: any;
    FatherName: any;
    FatherMobile: any;
    FatherOccupation: any;
    FatherEmail: any;
    Aadhar: any;
    Address: any;
    Landmark: any;
    CityID: any;
    Pincode: any;
    Caste: any;
    School: any;
    PhotoUrl: any;
    Medium: any;
    DeviceToken: any;

    Session:any;
    Stream:any;
    Course:any;
    Batch:any;

    constructor(data: any) {
        data = data || {};

        this.StudentAccountID = data.StudentAccountID;
        this.StudentID = data.StudentID;
        this.EnrollmentNo = data.EnrollmentNo;
        this.EnrollmentDate = data.EnrollmentDate;
        this.FirstName = data.FirstName;
        this.MiddleName = data.MiddleName;
        this.LastName = data.LastName;
        this.Gender = data.Gender;
        this.DOB = data.DOB;
        this.MobileNo = data.MobileNo;
        this.PhoneNo = data.PhoneNo;
        this.Email = data.Email;
        this.FatherName = data.FatherName;
        this.FatherMobile = data.FatherMobile;
        this.FatherOccupation = data.FatherOccupation;
        this.FatherEmail = data.FatherEmail;
        this.Aadhar = data.Aadhar;
        this.Address = data.Address;
        this.Landmark = data.Landmark;
        this.CityID = data.CityID;
        this.Pincode = data.Pincode;
        this.Caste = data.Caste;
        this.School = data.School;
        this.PhotoUrl = data.PhotoUrl;
        this.Medium = data.Medium;
        this.DeviceToken = data.DeviceToken;
        this.Session=data.Session;
        this.Stream=data.Stream;
        this.Course=data.Course;
        this.Batch=data.Batch;
    }
}