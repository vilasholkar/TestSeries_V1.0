export interface TestSeries {
    TestSeriesID: any;
    TestSeries: any;
    TotalTest: any;
    Description: any;
}
export interface OnlineTest {
    OnlineTestID: number;
    OnlineTestNo: any;
    TestSeriesID: any;
    TestSeriesName: any;
    TestTypeID: any;
    TestTypeName: any;
    TestName: any;
    TestDuration: any;
    SessionID: any;
    StreamID: Int32Array;
    CourseID: Int32Array;
    BatchID: Int32Array;
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
}

export interface Question {
    QuestionID: any;
    QuestionNo: any;
    TestID: any;
    TestQuestionNo: any;
    QuestionTypeID: any;
    SubjectID: any;
    IsImage: any;
    Text_English: any;
    Image_English: any;
    Text_Hindi: any;
    Image_Hindi: any;
    IsOptions: any;
    Marks: any;
    NegativeMarks: any;
    Remark: any;
    IsActive: any;
    CreatedByUserID: any;
    CreatedOnDate: any;
}
export interface EligibleStudent{
    StudentID: Int32Array;
    OnlineTestID: Int32Array;
    EnrollmentNo: string;
    StudentName: string;
    Gender: string;
    MobileNumber: string;
    FatherMobileNo: string;
    IsEligible: boolean;
}