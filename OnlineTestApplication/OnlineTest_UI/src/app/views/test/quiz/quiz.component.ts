import { Component, OnInit, TemplateRef, ViewChild, Renderer2 } from '@angular/core';
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
import { ImageDialogComponent } from '../../master/image-dialog/image-dialog.component';
// import {ToggleFullscreenDirective} from "../../../toggle-fullscreen-directive.directive";

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
  providers: [QuizService]
})
export class QuizComponent implements OnInit {
  buttonColor: any = "basic"
  IsTestSubmit: boolean = false;
  showQuiz: boolean = false;
  testDuration: any;
  correctCount = 0;
  totalMarksObtained: number = 0;
  percentage: number = 0;
  totalQuestions: number;
  noOfQuestionAttempted: number = 0;
  noOfQuestionNotAttempted: number = 0;
  countOfCorrectQues: number = 0;
  countOfIncorrectQues: number = 0;
  today = new Date();
  QuestionTypeIsSingleChoice: any;
  quizes: any[];
  question: Question;
  dynamicQuestionArray: Question[];
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
    index: 1,
    size: 1,
    count: 1
  };
  timer: any = null;
  startTime: Date;
  endTime: Date;
  ellapsedTime = '00:00';
  duration: any;
  IsEnglish: any;
  languageName: any;
  testID: any;
  chemistryCount: number = 0;
  physicsCount: number = 0;
  biologyCount: number = 0;
  aptitudeCount: number = 0;
  subjectName: string = 'Physics';

  constructor(private router: Router, private route: ActivatedRoute
    , private spinner: NgxSpinnerService
    , private quizService: QuizService
    , private dialog: MatDialog
    , private helperSvc: HelperService
    , private renderer: Renderer2) {
  }
  ngOnInit() {
    this.helperSvc.hide_Sidebar();
    this.renderer.removeClass(document.body, 'sidebar-lg-show');
    this.testID = +this.route.snapshot.paramMap.get('testID');
  }
  onNavigate(URL: String) {
    let link = URL + `/SiteAssets/Pages/help.aspx#/help`;
    window.open(link, "_blank");
  }
  OpenQuiz() {
    debugger;
    // window.open(document.URL, '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes')
    this.showQuiz = true;
    this.loadQuiz(this.testID);
    this.languageName = 'english';
    this.IsEnglish = true;
  }
  changeLanguage(languageName: string) {
    if (languageName === 'english')
      this.IsEnglish = true;
    else
      this.IsEnglish = false;
  }
  openDialogWithTemplateRef(templateRef: TemplateRef<any>) {
    this.dialog.open(templateRef);
  }
  openDialogWithTemplateRef2(templateRef: TemplateRef<any>) {
    this.dialog.open(templateRef);
  }
  loadQuiz(testID: any) {
    this.spinner.show();
    let sessionStudentID = sessionStorage.getItem("StudentID")
    this.quizService.getQuiz(testID, sessionStudentID).subscribe(res => {
      if (res.Message === "Success") {
        var data = res.Object;
        this.quiz = new Quiz(data);
        this.spinner.hide();
        this.testDuration = data.TestDuration;
        data.Questions.forEach(x => x.QuestionTypeId === 1 ? this.QuestionTypeIsSingleChoice = true : this.QuestionTypeIsSingleChoice = false)
        this.pager.count = this.quiz.questions.length;
        this.totalQuestions = this.quiz.questions.length;
        this.startTime = new Date();
        this.timer = setInterval(() => { this.tick(); }, 1000);
        this.duration = this.parseTime(this.testDuration);
        this.dynamicQuestionArray = this.quiz.questions.filter(f => f.subject == 'Physics');
        sessionStorage.setItem('IsTestStarted', 'true');
      }
      else {
        this.helperSvc.notifyError("Already Given Test");
        window.close();
      }

    });
    //$('.navbar-toggler-icon').click()

    this.mode = 'quiz';
  }
  tick() {
    if (this.IsTestSubmit == false) {
      const now = new Date();
      const diff = (now.getTime() - this.startTime.getTime()) / 1000;
      if (diff >= this.testDuration) {
        this.onSubmit();
        this.IsTestSubmit = true;
      }
      this.ellapsedTime = this.parseTime(diff);
    }
  }
  parseTime(totalSeconds: number) {
    let mins: string | number = Math.floor(totalSeconds / 60);
    let secs: string | number = Math.round(totalSeconds % 60);
    mins = (mins < 10 ? '0' : '') + mins;
    secs = (secs < 10 ? '0' : '') + secs;
    return `${mins}:${secs}`;
  }
  get filteredQuestions() {
    return (this.quiz.questions) ? this.quiz.questions.slice(this.pager.index - 1, this.pager.index - 1 + this.pager.size) : [];
  }
  onSelect(question: Question, option: Option, isChecked) {
    debugger;
    if (isChecked.checked) {
      question.buttonColor = 'accent';
    }
    else
      question.buttonColor = 'warn';
    if (question.questionTypeID === 1) {
      question.options.forEach((x) => {
        if (x.questionID !== option.questionID)
          x.selected = false;
      });
    }
    if (this.config.autoMove) {
      this.goTo(this.pager.index + 1);
    }

    if (isChecked.checked) {
      if (question.subject === 'Physics')
        this.physicsCount = this.physicsCount + 1;
      else if (question.subject === 'Chemistry')
        this.chemistryCount = this.chemistryCount + 1;
      else if (question.subject === 'Biology')
        this.biologyCount = this.biologyCount + 1;
      else if (question.subject === 'Aptitude')
        this.aptitudeCount = this.aptitudeCount + 1;
    }
    else {
      if (question.subject === 'Physics')
        this.physicsCount = this.physicsCount - 1;
      else if (question.subject === 'Chemistry')
        this.chemistryCount = this.chemistryCount - 1;
      else if (question.subject === 'Biology')
        this.biologyCount = this.biologyCount - 1;
      else if (question.subject === 'Aptitude')
        this.aptitudeCount = this.aptitudeCount - 1;
    }
  }
  goTo(index: number) {
    if (parseInt(index.toString()) > 0 && parseInt(index.toString()) <= this.pager.count) {
      this.pager.index = parseInt(index.toString());
      this.mode = 'quiz';
    }
  }
  goToSave(index: number) {
    debugger;
    let data = this.quiz.questions.filter(f => f.TestQuestionNo == index);
    for (var i = 0; i < data.length; i++) {
      for (let j = 0; j < data[i].options.length; j++) {
        if (!data[i].options[j].selected)
          data[i].buttonColor = 'warn';
        else {
          data[i].buttonColor = 'accent';
          break;
        }
      }
    }

    this.goTo(index);
  }
  NavigateToSubject(index: number, subjectValue) {
    this.subjectName = subjectValue;
    if (subjectValue === 'Physics')
      this.dynamicQuestionArray = this.quiz.questions.filter(f => f.subject === "Physics");

    else if (subjectValue == 'Chemistry')
      this.dynamicQuestionArray = this.quiz.questions.filter(f => f.subject === "Chemistry");
    else if (subjectValue == 'Biology')
      this.dynamicQuestionArray = this.quiz.questions.filter(f => f.subject === "Biology");
    else
      this.dynamicQuestionArray = this.quiz.questions.filter(f => f.subject === "Aptitude");

    this.goTo(index + 1);
  }

  isAnswered(question: Question) {
    return question.options.find(x => x.selected) ? 'Answered' : question.isDefaultQuestion ? 'Default' : 'Not Answered';
  }
  isCorrect(question: Question) {
    return question.options.every(x => x.selected == x.isAnswer) ? 'Correct' : question.options.every(e => !e.selected) ? 'NotAttempted' : 'Wrong';
  }

  // markForReview(question) {
  //   $('question.questionID').prop('checked', true);
  // }

  getStyle(index: number, question) {
    debugger;
    let data = this.quiz.questions.filter(f => f.TestQuestionNo == question.TestQuestionNo);
    for (var i = 0; i < data.length; i++) {
      for (let index = 0; index < data[i].options.length; index++) {
        if (!data[i].options[index].selected)
          data[i].buttonColor = 'warn';
        else {
          data[i].buttonColor = 'accent';
          break;
        }
      }
    }

    this.goTo(index);
  }
  // public ConvertToPDF() {
  //   this.spinner.show();
  //   var data = document.getElementById('contentToConvert');
  //   html2canvas(data).then(canvas => {
  //     // Few necessary setting options  
  //     var imgWidth = 208;
  //     var pageHeight = 295;
  //     var imgHeight = canvas.height * imgWidth / canvas.width;
  //     var heightLeft = imgHeight;

  //     const contentDataURL = canvas.toDataURL('image/png')
  //     let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
  //     var position = 0;
  //     pdf.addImage(contentDataURL, 'JPEG', 0, position, imgWidth, imgHeight)

  //     pdf.save(this.quiz.testName + '_' + this.today + '.pdf'); // Generated PDF   
  //     this.router.navigate(['/dashboard']);
  //     this.spinner.hide();
  //   });
  // }

  onSubmit() {
    debugger;
    this.spinner.show();
    this.helperSvc.show_Sidebar();
    const answers = [];
    this.quiz.questions.forEach(x => answers.push({ 'quizId': this.quiz.onlineTestID, 'questionId': x.questionID, 'answered': x.answered }));
    this.quiz.StudentID = sessionStorage.getItem("StudentID");
    for (let i = 0; i < this.quiz.questions.length; i++) {
      var status = this.quiz.questions[i].options.every(x => x.selected == x.isAnswer) ? 'correct' : 'wrong'
      if (status == 'correct') {
        this.countOfCorrectQues++;
      }
      for (let j = 0; j < this.quiz.questions[i].options.length; j++) {
        if (this.quiz.questions[i].options[j].selected)
          this.noOfQuestionAttempted++;
      }
    }
    // this.quiz.questions.forEach(element => {
    //   element.options.forEach(data => {
    //     data.status = !data.selected ? data.isAnswer ? 'correct' : '' : data.selected === data.isAnswer ? 'correct' : '';
    //   });
    // });
    this.noOfQuestionNotAttempted = this.totalQuestions - this.noOfQuestionAttempted;
    this.countOfIncorrectQues = this.noOfQuestionAttempted - this.countOfCorrectQues;
    this.totalMarksObtained = (this.countOfCorrectQues * 4) - this.countOfIncorrectQues;
    this.percentage = (this.totalMarksObtained * 100)/this.quiz.totalMarks;

    this.quizService.SubmitQuiz(this.quiz)
      .subscribe(data => {
        if (data === 'Success') {
          this.mode = 'result';
          this.spinner.hide();
          sessionStorage.setItem('IsTestStarted', 'false');
        }
      }, error => {
        alert('error');
      });

  }

  openDialog(image_url: string): void {
    const dialogRef = this.dialog.open(ImageDialogComponent, {
      // height: '400px',
      // width: '600px',
      data: { name: 'vaibhav', image_url: image_url }

    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }
}
