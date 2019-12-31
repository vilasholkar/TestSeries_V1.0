export interface TestType {
    TestTypeID: any;
    TestTypeName: any;
}

export interface Subject {
    SubjectID: any;
    Subject: any;
    Remark: any;
}
export interface QuestionType {
    QuestionTypeID: any;
    QuestionType: any;
}
export interface Stream {
    StreamID: any;
    StreamName: any;
}
export class Course {
    CourseID: any;
    CourseName: any;
}
export class Batch {
    BatchID: any;
    Batch: any;
}
export interface Session {
    SessionID: any;
    SessionName: any;
}
export interface Topic {
    TopicID: any;
    Topic: any;
    Description: any;
    SubjectID: any;
    Subject: any;
    IsActive: any;
}
export interface SubTopic {
    SubTopicID: any;
    SubTopic: any;
    Description: any;
    TopicID: any;
    Topic: any;
    IsActive: any;
}

export interface StudyMaterial {
    StudyMaterialID: number;
    Tittle: any;
    SubTittle: any;
    Description: any;
    SubjectID: Int32Array;
    Subject: any;
    TopicID: Int32Array;
    Topic: any;
    SubTopicID: Int32Array;
    SubTopic: any;
    SessionID: any;
    StreamID: Int32Array;
    CourseID: Int32Array;
    BatchID: Int32Array;
    Thumbnail: any;
    URL: any;
    IsActive: any;
    CreatedByUserID: any;
    CreatedOnDate: any;
}

export interface Slider {
    SliderID: any;
    SliderNo: any;
    Tittle: any;
    SliderImage: any;
    IsActive: any;
    CreatedByUserID: any;
    CreatedOnDate: any;
}

export interface Notification {
    NotificationID: any;
    ReciverID: any;
    NotificationDate: any;
    Title: any;
    Description: any;
    ImageURL: any;
    RedirectToURL: any;
    IsRead: any;
}