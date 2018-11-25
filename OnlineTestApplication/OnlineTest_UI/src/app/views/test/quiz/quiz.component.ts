import { Component, OnInit ,TemplateRef, ViewChild} from '@angular/core';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { QuizService } from '../../../services/admin/quiz.service';
import { HelperService } from '../../../services/helper.service';
import { Option, Question, Quiz, QuizConfig } from './models';
import * as jspdf from 'jspdf';  
import html2canvas from 'html2canvas';  
declare var $: any;
import { NgxSpinnerService } from 'ngx-spinner';
import {ToggleFullscreenDirective} from "../../../toggle-fullscreen-directive.directive";

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
  providers: [QuizService]
})
export class QuizComponent implements OnInit {
  showQuiz:boolean=false;
  testDuration:any;
  correctCount=0;
  totalQuestions:number;
  noOfQuestionAttempted:number=0;
  countOfCorrectQues:number=0;
  countOfIncorrectQues:number=0;
  today=new Date();
  QuestionTypeIsSingleChoice:any;
  quizes: any[];
  question:Question;
  quiz: Quiz = new Quiz(null);
  mode = 'quiz';
  quizName: string;
  config: QuizConfig = {
 
    'allowBack': true,
    'allowReview': true,
    'autoMove': false,  // if true, it will move to next question automatically when answered.
    // 'duration': this.testDuration,  // indicates the time (in secs) in which quiz needs to be completed. 0 means unlimited.
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
  duration:any;
  IsEnglish:any;
  languageName:any;
  testID:any;

  constructor(private router : Router,private route: ActivatedRoute
    ,private spinner: NgxSpinnerService,private quizService: QuizService,private dialog: MatDialog) {
   }
  ngOnInit() {
    this.testID= +this.route.snapshot.paramMap.get('testID');
  }
  onNavigate(URL:String)
  {
      let link = URL+`/SiteAssets/Pages/help.aspx#/help`;
      window.open(link, "_blank");
  }
  OpenQuiz()
  {
    // window.open(document.URL, '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes')
    this.showQuiz=true;
    this.loadQuiz(this.testID);
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
  openDialogWithTemplateRef(templateRef: TemplateRef<any>) {
    this.dialog.open(templateRef);
  }
  loadQuiz(testID: any) {
      this.spinner.show();
      this.quizService.getQuiz(testID).subscribe(res => {
      this.quiz = new Quiz(res);
      this.spinner.hide();
      this.testDuration=res.TestDuration;
      res.Questions.forEach(x=>x.QuestionTypeId===1 ? this.QuestionTypeIsSingleChoice = true : this.QuestionTypeIsSingleChoice = false )
      this.pager.count = this.quiz.questions.length;
      this.totalQuestions=this.quiz.questions.length;
      this.startTime = new Date();
      this.timer = setInterval(() => { this.tick(); }, 1000);
      this.duration = this.parseTime(this.testDuration);
     
    });
    //$('.navbar-toggler-icon').click()

    this.mode = 'quiz';
  }
  tick() {
    const now = new Date();
    const diff = (now.getTime() - this.startTime.getTime()) / 1000;
    if (diff >= this.testDuration) {
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
    return (this.quiz.questions) ? this.quiz.questions.slice(this.pager.index, this.pager.index + this.pager.size) : [];
  }
  onSelect(question: Question, option: Option) {   
    if (question.questionTypeID === 1) {
      question.options.forEach((x) => 
      { 
        if (x.questionID !== option.questionID) 
        x.selected = false;
      });
       this.noOfQuestionAttempted += 1;
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
     var status= question.options.every(x => x.selected == x.isAnswer) ? 'correct' : 'wrong'
     if(status==='correct'){
       //this.countOfCorrectQues++;
     }
    return question.options.every(x => x.selected == x.isAnswer) ? 'correct' : 'wrong';
   
  }
  markForReview(question){
    
      $('question.questionID').prop('checked', true);
  }
  getStyle(question) {
    var isanswered=this.isAnswered(question);
    if(isanswered=="Answered") {
      return 'primary';
       } 
    if(isanswered=="Not Answered") {
      return 'basic';
       } 
  }
  public ConvertToPDF()  
    {  
      this.spinner.show();
      var data = document.getElementById('contentToConvert');  
      html2canvas(data).then(canvas => {  
        // Few necessary setting options  
        var imgWidth = 208;   
        var pageHeight = 295;    
        var imgHeight = canvas.height * imgWidth / canvas.width;  
        var heightLeft = imgHeight;  
    
        const contentDataURL = canvas.toDataURL('image/png')  
        let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
        var position = 0;  
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)  
        pdf.save(this.quiz.testName+'_'+this.today+'.pdf'); // Generated PDF   
        this.router.navigate(['/dashboard']);
        this.spinner.hide();
      });  
    }  
  onSubmit() {
    this.spinner.show();
    const answers = [];
    this.quiz.questions.forEach(x => answers.push({ 'quizId': this.quiz.onlineTestID, 'questionId': x.questionID, 'answered': x.answered }));    
    console.log("res",this.quiz.questions)
      this.quizService.SubmitQuiz(this.quiz)
       .subscribe(data => {
        if (data === 'Success') {
         }
       }, error => {
         alert('error');
       });
    this.mode = 'result';
    this.spinner.hide();
  }
}
