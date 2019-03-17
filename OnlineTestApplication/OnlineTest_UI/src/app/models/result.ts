
export interface OT_Result {
    ResultID: any;
    StudentID: any;
    StudentName: any;
    TestID: any;
    TestName: any;
    TestSeriesID: any;
    TestSeriesName: any;
    TestTypeID: any;
    TestTypeName: any;
    TestDate: any;
    Physics_Total: any;
    Physics_Right: any;
    Physics_Wrong: any;
    Chemistry_Total: any;
    Chemistry_Right: any;
    Chemistry_Wrong: any;
    Biology_Total: any;
    Biology_Right: any;
    Biology_Wrong: any;
    TotalCorrect: any;
    TotalWrong: any;
    TotalAttempt: any;
    TotalMarksObtained: any;
    Percentage: any;
    Rank: any;
    TotalMarks: any;
    QualifyingMarks: any;
    IsActive: any;
    IsPresent: any;
    // constructor(data: any) {
    //     data = data || {};
    //     this.ResultID = data.ResultID;
    //     this.StudentID = data.StudentID;
    //     this.StudentName = data.StudentName;
    //     this.TestID = data.TestID;
    //     this.TestName = data.TestName;
    //     this.TestSeriesID = data.TestSeriesID;
    //     this.TestSeriesName = data.TestSeriesName;
    //     this.TestTypeID = data.TestTypeID;
    //     this.TestTypeName = data.TestTypeName;
    //     this.TestDate = data.TestDate;
    //     this.Physics_Total=data.Physics_Total;
    //     this.Physics_Right=data.Physics_Right;
    //     this.Physics_Wrong=data.Physics_Wrong;
    //     this.Chemistry_Total=data.Chemistry_Total;
    //     this.Chemistry_Right=data.Chemistry_Right;
    //     this.Chemistry_Wrong=data.Chemistry_Wrong;
    //     this.Biology_Total=data.Biology_Total;
    //     this.Biology_Right=data.Biology_Right;
    //     this.Biology_Wrong=data.Biology_Wrong;
    //     this.TotalCorrect=data.TotalCorrect;
    //     this.TotalWrong=data.TotalWrong;
    //     this.TotalAttempt=data.TotalAttempt;
    //     this.TotalMarksObtained=data.TotalMarksObtained;
    //     this.Percentage=data.Percentage;
    //     this.Rank=data.Rank;
    //     this.TotalMarks=data.TotalMarks;
    //     this.QualifyingMarks=data.QualifyingMarks;
    // }
}

export class PaperAnalysis {
    PaperAnalysisID: any;
    TestID: any;
    TotalEasy: any;
    TotalMedium: any;
    TotalDifficult: any;
    EasyQuestionList: any;
    MediumQuestionList: any;
    DifficultQuestionList: any;

    constructor(data: any) {
        data = data || {};
        this.PaperAnalysisID = data.PaperAnalysis;
        this.TestID = data.TestID;
        this.TotalEasy = data.TotalEasy;
        this.TotalMedium = data.TotalMedium;
        this.TotalDifficult = data.TotalDifficult;
        this.EasyQuestionList = data.EasyQuestionList;
        this.MediumQuestionList = data.MediumQuestionList;
        this.DifficultQuestionList = data.DifficultQuestionList;

    }
}

export interface StudentAttempt {
    StudentAttemptID: any;
    StudentID: any;
    TestID: any;
    EasyCorrect: any;
    EasyInCorrect: any;
    EasyNotAttempt: any;
    MediumCorrect: any;
    MediumInCorrect: any;
    MediumNotAttempt: any;
    DifficultCorrect: any;
    DifficultInCorrect: any;
    DifficultNotAttempt: any;
}

export class Topper_Average {
    Topper_AverageID: any;
    Topper_Average: any;
    TestID: any;
    Physics_Right: any;
    Physics_Wrong: any;
    Chemistry_Right: any;
    Chemistry_Wrong: any;
    Biology_Right: any;
    Biology_Wrong: any;
    TotalCorrect: any;
    TotalWrong: any;
    TotalAttempt: any;
    TotalMarksObtained: any;
    Percentage: any;

    constructor(data: any) {
        data = data || {};
        this.Topper_AverageID = data.Topper_AverageID;
        this.Topper_Average = data.Topper_Average;
        this.TestID = data.TestID;
        this.Physics_Right = data.Physics_Right;
        this.Physics_Wrong = data.Physics_Wrong;
        this.Chemistry_Right = data.Chemistry_Right
        this.Chemistry_Wrong = data.Chemistry_Wrong;
        this.Biology_Right = data.Biology_Right;
        this.Biology_Wrong = data.Biology_Wrong;
        this.TotalCorrect = data.TotalCorrect;
        this.TotalWrong = data.TotalWrong;
        this.TotalAttempt = data.TotalAttempt;
        this.TotalMarksObtained = data.TotalMarksObtained;
        this.Percentage = data.Percentage;

    }
}

export class ResultAnalysis {
    paperAnalysis: PaperAnalysis;
    studentAttempt: StudentAttempt;
    onlineTestResult: OnlineTestResult[];
    topper_Average: Topper_Average[];
    studentRank: StudentRank;

    constructor(data: any) {
        data = data || {};
        this.paperAnalysis = data.PaperAnalysis;
        this.studentAttempt = data.StudentAttempt;
        this.topper_Average = data.Topper_Average;
        this.onlineTestResult = data.OnlineTestResult;
        this.studentRank = data.StudentRank;
        // this.topper_Average=[];
        // data.topper_Average.forEach(o => {
        //     this.topper_Average.push(new Topper_Average(o));
        // });
    }
}
export class StudentRank {
    AIR_UR: number;
    SR_UR: number;
    AIR_CAT_RANK: number;
    SR_CAT_RANK: number;


    constructor(data: any) {
        data = data || {};
        this.AIR_UR = data.AIR_UR;
        this.SR_UR = data.SR_UR;
        this.AIR_CAT_RANK = data.AIR_CAT_RANK;
        this.SR_CAT_RANK = data.SR_CAT_RANK;

    }
}
export class OnlineTestResult {
    ResultID: any;
    StudentID: any;
    EnrollmentNo: any;
    StudentName: any;
    StudentCaste: any;
    TestID: any;
    TestName: any;
    TestSeriesID: any;
    TestSeriesName: any;
    TestTypeID: any;
    TestTypeName: any;
    TestDate: any;
    Physics_Total: any;
    Physics_Right: any;
    Physics_Wrong: any;
    Chemistry_Total: any;
    Chemistry_Right: any;
    Chemistry_Wrong: any;
    Biology_Total: any;
    Biology_Right: any;
    Biology_Wrong: any;
    TotalCorrect: any;
    TotalWrong: any;
    TotalAttempt: any;
    TotalMarksObtained: any;
    Percentage: any;
    Rank: any;
    TotalMarks: any;
    QualifyingMarks: any;
    IsActive: any;
    IsPresent: any;

    constructor(data: any) {
        data = data || {};
        this.ResultID = data.ResultID;
        this.StudentID = data.StudentID;
        this.EnrollmentNo = data.EnrollmentNo;
        this.StudentName = data.StudentName;
        this.StudentCaste = data.StudentCaste;
        this.TestID = data.TestID;
        this.TestName = data.TestName;
        this.TestSeriesID = data.TestSeriesID;
        this.TestSeriesName = data.TestSeriesName;
        this.TestTypeID = data.TestTypeID;
        this.TestTypeName = data.TestTypeName;
        this.TestDate = data.TestDate;
        this.Physics_Total = data.Physics_Total;
        this.Physics_Right = data.Physics_Right;
        this.Physics_Wrong = data.Physics_Wrong;
        this.Chemistry_Total = data.Chemistry_Total;
        this.Chemistry_Right = data.Chemistry_Right;
        this.Chemistry_Wrong = data.Chemistry_Wrong;
        this.Biology_Total = data.Biology_Total;
        this.Biology_Right = data.Biology_Right;
        this.Biology_Wrong = data.Biology_Wrong;
        this.TotalCorrect = data.TotalCorrect;
        this.TotalWrong = data.TotalWrong;
        this.TotalAttempt = data.TotalAttempt;
        this.TotalMarksObtained = data.TotalMarksObtained;
        this.Percentage = data.Percentage;
        this.Rank = data.Rank;
        this.TotalMarks = data.TotalMarks;
        this.QualifyingMarks = data.QualifyingMarks;
        this.IsActive = data.IsActive;
        this.IsPresent = data.IsPresent;
    }

}

export class StudentResponse {

    StudentResponseID: any;
    StudentID: any;
    TestID: any;
    QuestionID: any;
    SubjectID: any;
    OptionID: any;
    AnswerID: any;
    IsCorrect: any;
    TestQuestionNo: any;
    Image_English: any;
    Image_Hindi: any;
    
    constructor(data: any) {
        data = data || {};
        this.StudentID=data.StudentID;
        this.TestID=data.TestID;
        this.QuestionID=data.QuestionID;
        this.SubjectID=data.SubjectID;
        this.OptionID=data.OptionID;
        this.AnswerID=data.AnswerID;
        this.IsCorrect=data.IsCorrect;
        this.TestQuestionNo=data.TestQuestionNo;
        this.Image_English=data.Image_English;
        this.Image_Hindi=data.Image_Hindi;
    }
}