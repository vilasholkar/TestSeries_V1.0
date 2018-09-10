import { Component, OnInit } from '@angular/core';

// import { QuizService } from '../../../services/admin/quiz.service';
import { QuizService } from '../../../services/admin/quiz.service';
import { HelperService } from '../../../services/helper.service';
import { Option, Question, Quiz, QuizConfig } from './models';
// import {GlobalVariables} from '../../../models/global-variables';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
  providers: [QuizService]
})
export class QuizComponent implements OnInit {
  QuestionTypeIsSingleChoice:any;
  quizes: any[];
  quiz: Quiz = new Quiz(null);
  mode = 'quiz';
  quizName: string;
  config: QuizConfig = {
    'allowBack': true,
    'allowReview': true,
    'autoMove': false,  // if true, it will move to next question automatically when answered.
    'duration': 12000,  // indicates the time (in secs) in which quiz needs to be completed. 0 means unlimited.
    'pageSize': 1,
    'requiredAll': false,  // indicates if you must answer all the questions before submitting.
    'richText': false,
    'shuffleQuestions': false,
    'shuffleOptions': false,
    'showClock': true,
    'showPager': true,
    'theme': 'none'
  };
  pager = {
    index: 0,
    size: 1,
    count: 1
  };
  timer: any = null;
  startTime: Date;
  endTime: Date;
  ellapsedTime = '00:00';
  duration = '';
  IsEnglish:any;
  languageName:any;
  constructor(private quizService: QuizService) {
   }

  ngOnInit() {
    this.quizes = this.quizService.getAll();
    this.quizName = this.quizes[0].id;
    this.loadQuiz(this.quizName);
    this.languageName = 'english';
    this.IsEnglish = true;
  }
  changeLanguage(languageName:string)
  {
    if(languageName==='english')
    this.IsEnglish=true;
    else
    this.IsEnglish=false;
  }
  loadQuiz(quizName: string) {
    this.quizService.getQuiz(quizName).subscribe(res => {
      this.quiz = new Quiz(res);
      console.log("res",res)
      console.log(this.quiz);
      res.Question.forEach(x=>x.QuestionTypeId===1 ? this.QuestionTypeIsSingleChoice = true : this.QuestionTypeIsSingleChoice = false )
      this.pager.count = this.quiz.questions.length;
      this.startTime = new Date();
      this.timer = setInterval(() => { this.tick(); }, 1000);
      this.duration = this.parseTime(this.config.duration);
    });
    this.mode = 'quiz';
  }
  tick() {
    const now = new Date();
    const diff = (now.getTime() - this.startTime.getTime()) / 1000;
    if (diff >= this.config.duration) {
      this.onSubmit();
    }
    this.ellapsedTime = this.parseTime(diff);
  }
  parseTime(totalSeconds: number) {
    let mins: string | number = Math.floor(totalSeconds / 60);
    let secs: string | number = Math.round(totalSeconds % 60);
    mins = (mins < 10 ? '0' : '') + mins;
    secs = (secs < 10 ? '0' : '') + secs;
    return `${mins}:${secs}`;
  }
  get filteredQuestions() {
    
    return (this.quiz.questions) ?
      this.quiz.questions.slice(this.pager.index, this.pager.index + this.pager.size) : [];
  }
  onSelect(question: Question, option: Option) {
    debugger
    if (question.questionTypeID === 1) {
      question.options.forEach((x) => { if (x.questionID !== option.questionID) x.selected = false; });
      debugger
    }
    if (this.config.autoMove) {
      this.goTo(this.pager.index + 1);
    }
  }
  goTo(index: number) {
    if (index >= 0 && index < this.pager.count) {
      this.pager.index = index;
      this.mode = 'quiz';
    }
  }
  isAnswered(question: Question) {
    return question.options.find(x => x.selected) ? 'Answered' : 'Not Answered';
  }
  isCorrect(question: Question) {
    return question.options.every(x => x.selected === x.isAnswer) ? 'correct' : 'wrong';
  }
  onSubmit() {
    const answers = [];
    this.quiz.questions.forEach(x => answers.push({ 'quizId': this.quiz.onlineTestID, 'questionId': x.questionID, 'answered': x.answered }));
    // Post your data to the server here. answers contains the questionId and the users' answer.
    console.log(this.quiz.questions);
    //this.mode = 'result';
  }
  // resetAnswer() {
  //  option.selected = null;
  // }
}
