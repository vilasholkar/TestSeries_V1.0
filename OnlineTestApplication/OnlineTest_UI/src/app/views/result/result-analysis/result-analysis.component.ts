import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-result-analysis',
  templateUrl: './result-analysis.component.html',
  styleUrls: ['./result-analysis.component.scss']
})
export class ResultAnalysisComponent implements OnInit {
  id: any;
  options: any;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.getResultAnalysisByTestID();
  }
  getResultAnalysisByTestID() {

    this.id = +this.route.snapshot.paramMap.get('id');
    //   this.viewQuestionService.GetQuestionsByTestId(this.id as number)
    //   .subscribe(data => {
    //     if(data.Message === 'Success')
    //     this.quizModel = data.Object;
    // },error => {
    // alert(this.id);
    //   });
  }

  // Doughnut
  public EasyQuestionData: number[] = [60, 20, 20];
  public MediumQuestionData: number[] = [40, 10, 20];
  public DifficultQuestionData: number[] = [5, 4, 1];

  public doughnutChartLabels: string[] = ['Correct', 'Incorrect', 'Not Attempt'];
  public doughnutChartType = 'doughnut';
  public doughnutChartLegend: boolean = true;
  public doughnutChartColor: any[] = [{ backgroundColor: ["#49de94", "#ff8589", "#ffe54f"] }];
  public doughnutChartOption: any = {legend: { position: 'right' } }

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
              barThickness:50,
              // gridLines: {
              //   display: true
              // }
            }]
          }
  };
  public barChartColor: any[] = [{ backgroundColor: ["#d6ecfb", "#ffe0e6", "#ebe0ff"],borderWidth:1,borderColor:['#88d6d6','#ff98ae','#a477ff'] }];
  public barChartLabels: string[] = ['Topper', 'Average', 'Student'];
  public barChartType = 'bar';
  public barChartLegend = false;
  public barChartData: any[] = [
    {data: [65, 59, 80], label: 'Percentage'}
  ];
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