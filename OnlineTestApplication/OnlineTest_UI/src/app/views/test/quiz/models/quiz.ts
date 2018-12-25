import { QuizConfig } from './quiz-config';
import { Question } from './question';

export class Quiz {
    StudentID:any;
    onlineTestID: number;
    testName: string;
    instructions: string;
    testDuration:string;
    config: QuizConfig;
    questions: Question[];
    constructor(data: any) {
        if (data) {
            this.onlineTestID = data.OnlineTestID;
            this.testName = data.TestName;
            this.instructions = data.Instructions;
            this.config = new QuizConfig(data.config);
            this.questions =[];
            data.Questions.forEach(q => {
                this.questions.push(new Question(q));
            });
        }
    }
}
