import { Option } from './option';
//import { QuestionType } from './QuestionType';
export class Question {
    questionID: number;
    subjectID: number;
    questionTypeID: number;
    image_English: string;
    image_Hindi: string;
    options: Option[];
    answered: boolean;
    subject:string;
    TestQuestionNo:number;
    isDefaultQuestion:boolean;
    buttonColor:any;

    constructor(data: any) {
        data = data || {};
        this.questionID = data.QuestionID;
        this.subjectID = data.SubjectID;
        this.subject = data.Subject
        this.questionTypeID = data.QuestionTypeID;
        this.image_English = data.Image_English;
        this.image_Hindi = data.Image_Hindi;
        this.options = [];
        data.Options.forEach(o => {
            this.options.push(new Option(o));
        });
        this.TestQuestionNo = data.TestQuestionNo;
        this.isDefaultQuestion=data.IsDefaultQuestion;
        this.buttonColor='basic';
    }
}
