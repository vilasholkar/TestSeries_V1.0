import { QuizConfig } from './quiz-config';
import { Question } from './question';

export class Quiz {
    StudentID:any;
    onlineTestID: number;
    testName: string;
    testSeriesID: number;
    testSeries: string;
    testTypeID: number;
    testType: string;
    instructions: string;
    testDuration:string;
    totalMarks:number;
    passingPercentage:string;
    config: QuizConfig;
    PhysicsQuestionCount:number
    ChemistryQuestionCount:number
    BiologyQuestionCount:number
    AptitudeQuestionCount:number
    questions: Question[];
    constructor(data: any) {
        if (data) {
            this.onlineTestID = data.OnlineTestID;
            this.testName = data.TestName;
            this.testSeriesID = data.TestSeriesID;
            this.testSeries = data.TestSeries;
            this.testTypeID = data.TestTypeID;
            this.testType = data.TestType;
            this.instructions = data.Instructions;
            this.totalMarks = data.TotalMarks;
            this.passingPercentage = data.PassingPercentage;
            this.config = new QuizConfig(data.config);
            this.questions =[];
            data.Questions.forEach(q => {
                this.questions.push(new Question(q));
            });
            this.PhysicsQuestionCount = data.PhysicsQuestionCount
            this.ChemistryQuestionCount = data.ChemistryQuestionCount
            this.BiologyQuestionCount = data.BiologyQuestionCount
            this.AptitudeQuestionCount = data.AptitudeQuestionCount
        }
    }
}
