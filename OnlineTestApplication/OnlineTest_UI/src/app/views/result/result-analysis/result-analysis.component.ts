import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';

import { ResultAnalysisService } from '../../../services/student/result-analysis.service';
import { MatSnackBar } from '@angular/material';
import { ResultAnalysis, PaperAnalysis, StudentAttempt, OnlineTestResult, Topper_Average, StudentRank } from "../../../models/result";
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-result-analysis',
  templateUrl: './result-analysis.component.html',
  styleUrls: ['./result-analysis.component.scss']
})
export class ResultAnalysisComponent implements OnInit {
  StudentID: any;
  TestID: any;
  testName: any;
  testDate: any;
  enrollmentNo: any;
  studentName: any;
  studentCaste: any;

  totalEasy: any;
  totalMedium: any;
  totalDifficult: any;
  resultAnalysis: ResultAnalysis;
  studentAttempt: StudentAttempt;
  onlineTestResult: OnlineTestResult[];
  onlineTestResultModel: any = {};
  topper_Average: Topper_Average[];
  topper_AverageModel: any = {};
  topperPercentage: number;
  averagePercentage: number;
  studentPercentage: number;
  AIR_UR: number;
  SR_UR: number;
  AIR_CAT_RANK: number;
  SR_CAT_RANK: number;
  constructor(
    private route: ActivatedRoute,
    public snackBar: MatSnackBar,
    private resultAnalysisService: ResultAnalysisService,
    private router: Router,
    private helperSvc: HelperService
  ) { }

  // Doughnut
  public EasyQuestionData: number[] = [];
  public MediumQuestionData: number[] = [];
  public DifficultQuestionData: number[] = [];
  public doughnutLabelEasy: string[] = [];
  public doughnutLabelMedium: string[] = [];
  public doughnutLabelDifficult: string[] = [];

  public doughnutChartType = 'doughnut';
  public doughnutChartLegend: boolean = true;
  public doughnutChartColor: any[] = [{ backgroundColor: ["#49de94", "#ff8589", "#ffe54f"] }];
  public doughnutChartOption: any = { legend: { position: 'right' } }
  // events
  public chartClicked(e: any): void {
    console.log(e);
  }
  public chartHovered(e: any): void {
    console.log(e);
  }
  // barChart
  public barChartOptions: any = {
    scaleShowVerticalLines: true,
    responsive: true,
    scales: {
      yAxes: [{
        stacked: true,
        ticks: {
          suggestedMin: 0,
          suggestedMax: 100
        },
        gridLines: {
          display: true,
          //color: "rgba(255,99,132,0.2)"
        }
      }],
      xAxes: [{
        barThickness: 50,
        // gridLines: {
        //   display: true
        // }
      }]
    }
  };
  public barChartColor: any[] = [{ backgroundColor: ["#d6ecfb", "#ffe0e6", "#ebe0ff"], borderWidth: 1, borderColor: ['#88d6d6', '#ff98ae', '#a477ff'] }];
  public barChartLabels: string[] = [];
  public barChartType = 'bar';
  public barChartLegend = false;
  public barChartData: any[] = [];

  ngOnInit() {
    this.StudentID = +this.route.snapshot.paramMap.get('StudentID');
    this.TestID = +this.route.snapshot.paramMap.get('TestID');
    this.GetResultAnalysis(this.StudentID, this.TestID);
  }
  GetResultAnalysis(StudentID: any, TestID: any) {
    this.helperSvc.getService(APIUrl.GetResultAnalysis + "?StudentID=" + StudentID + "&TestID=" + TestID)
      .subscribe(data => {
        debugger;
         if (data.Message === 'Success') {
          this.resultAnalysis = new ResultAnalysis(data.Object as ResultAnalysis);
          //Paper Analysis
          this.totalEasy = this.resultAnalysis.paperAnalysis.TotalEasy;
          this.totalMedium = this.resultAnalysis.paperAnalysis.TotalMedium;
          this.totalDifficult = this.resultAnalysis.paperAnalysis.TotalDifficult;
          //Student Attempt Analysis
          this.studentAttempt = this.resultAnalysis.studentAttempt;
          this.EasyQuestionData = [(this.studentAttempt.EasyCorrect as number / this.totalEasy as number) * 100, (this.studentAttempt.EasyInCorrect as number / this.totalEasy as number) * 100, (this.studentAttempt.EasyNotAttempt as number / this.totalEasy as number) * 100];
          this.MediumQuestionData = [(this.studentAttempt.MediumCorrect as number / this.totalMedium as number) * 100, (this.studentAttempt.MediumInCorrect as number / this.totalMedium as number) * 100, (this.studentAttempt.MediumNotAttempt as number / this.totalMedium as number) * 100];
          this.DifficultQuestionData = [(this.studentAttempt.DifficultCorrect as number / this.totalDifficult as number) * 100, (this.studentAttempt.DifficultInCorrect as number / this.totalDifficult as number) * 100, (this.studentAttempt.DifficultNotAttempt as number / this.totalDifficult as number) * 100];
          this.doughnutLabelEasy = ['Correct-' + this.studentAttempt.EasyCorrect as string, 'Incorrect-' + this.studentAttempt.EasyInCorrect as string, 'Not Attempt-' + this.studentAttempt.EasyNotAttempt as string];
          this.doughnutLabelMedium = ['Correct-' + this.studentAttempt.MediumCorrect as string, 'Incorrect-' + this.studentAttempt.MediumInCorrect as string, 'Not Attempt-' + this.studentAttempt.MediumNotAttempt as string];
          this.doughnutLabelDifficult = ['Correct-' + this.studentAttempt.DifficultCorrect as string, 'Incorrect-' + this.studentAttempt.DifficultInCorrect as string, 'Not Attempt-' + this.studentAttempt.DifficultNotAttempt as string];
          //Mark Review Analysis
          this.onlineTestResult = this.resultAnalysis.onlineTestResult;
          this.testName = this.onlineTestResult[0].TestName;
          this.testDate = this.onlineTestResult[0].TestDate;
          this.enrollmentNo = this.onlineTestResult[0].EnrollmentNo;
          this.studentName = this.onlineTestResult[0].StudentName;
          //Topper_Average Analysis
          this.topper_Average = this.resultAnalysis.topper_Average;
          //BarChart Analysis
          this.topperPercentage = this.topper_Average[0].Percentage;
          this.averagePercentage = this.topper_Average[1].Percentage;
          this.studentPercentage = this.onlineTestResult[0].Percentage;
          this.barChartData = [
            { data: [this.topperPercentage, this.averagePercentage, this.studentPercentage], label: 'Percentage' }
          ];
          this.barChartLabels = ['Topper-' + this.topperPercentage, 'Average-' + this.averagePercentage, 'Student-' + this.studentPercentage];
          // Student Rank
          this.studentCaste = this.onlineTestResult[0].StudentCaste;
          this.AIR_UR = this.resultAnalysis.studentRank.AIR_UR;
          this.SR_UR = this.resultAnalysis.studentRank.SR_UR;
          this.AIR_CAT_RANK = this.resultAnalysis.studentRank.AIR_CAT_RANK;
          this.SR_CAT_RANK = this.resultAnalysis.studentRank.SR_CAT_RANK;
         }
      }, error => {
        this.helperSvc.errorHandler(error.error);
        this.helperSvc.notifyError(error.error);
        console.log(error);
      });
  }
  
  public ConvertToPDF() {
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
      pdf.save('resultanalysis.pdf'); // Generated PDF   
      this.router.navigate(['/dashboard']);
    });
  }
  // Pie
  // public pieChartLabels: string[] = ['Download Sales', 'In-Store Sales', 'Mail Sales'];
  // public pieChartData: number[] = [300, 500, 100];
  // public pieChartColor: Array<any> = [{backgroundColor: ["#ffe54f", "#ff8589", "#49de94"]}];
  // public pieChartLegend:boolean=false;
  //  public pieChartOption:any=this.options = {
  //     maintainAspectRatio: false,
  //     scales: {
  //       yAxes: [{
  //         stacked: true,
  //         gridLines: {
  //           display: true,
  //           color: "rgba(255,99,132,0.2)"
  //         }
  //       }],
  //       xAxes: [{
  //         gridLines: {
  //           display: false
  //         }
  //       }]
  //     }
  //   };
  //public pieChartType = 'pie';

}