export class Option {
    optionID: number;
    questionID: number;
    option: string;
    isAnswer: boolean;
    selected: boolean;

    constructor(data: any) {
        data = data || {};
        this.optionID = data.OptionId;
        this.questionID = data.QuestionId;
        this.option = data.Option;
        this.isAnswer = data.IsAnswer;
    }
}
