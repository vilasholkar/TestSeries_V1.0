export interface TestSeries {
    TestSeriesID: any;
    TestSeries: any;
    TotalTest: any;
    Description: any;
}
export interface OnlineTest {
    OnlineTestID: any;
    OnlineTestNo: any;
    TestSeriesID: any;
    TestTypeID: any;
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
    StartingDate: any;
    StartingTime: any;
    EndDate: any;
    EndTime: any;
    IsVisible: any;
    IsActive: any;
    CreatedByUserID: any;
    CreatedOnDate: any;
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