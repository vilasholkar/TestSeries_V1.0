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
    BatchName: any;    
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