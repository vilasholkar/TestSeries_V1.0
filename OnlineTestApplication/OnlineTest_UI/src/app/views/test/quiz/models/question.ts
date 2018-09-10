import { Option } from './option';
import {QuestionType} from './QuestionType'
export class Question {
    questionID: number;
    questionTypeID: number;
    image_English: string;
    image_Hindi: string;
    options: Option[];
    answered: boolean;
    
    
    constructor(data: any) {
        data = data || {};
        this.questionID = data.QuestionId;
        this.questionTypeID = data.QuestionTypeId;
        this.image_English = data.Image_English;
        this.image_Hindi = data.Image_Hindi;
        this.options = [];
        data.Options.forEach(o => {
            this.options.push(new Option(o));
        });
    }
}
