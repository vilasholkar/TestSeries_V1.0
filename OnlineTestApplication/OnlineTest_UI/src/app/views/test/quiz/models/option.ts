export class Option {
    optionID: number;
    questionID: number;
    option: string;
    isAnswer: boolean;
    selected: boolean;

    constructor(data: any) {
        data = data || {};
        this.optionID = data.OptionID;
        this.questionID = data.QuestionID;
        this.option = data.Option;
        this.isAnswer = data.IsAnswer;
        this.selected=false;
    }
}
