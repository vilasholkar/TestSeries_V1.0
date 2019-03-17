import { Component, OnInit } from '@angular/core';
import { HelperService } from "../../../services/helper.service";
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.scss']
})
export class StudentDashboardComponent implements OnInit {
  TotalTest: any;
  Completed: any;
  Started: any;
  NotStarted: any;
  TestPercentage: any = [];
  TestName: any = [];
  Percentage: any = [];
  constructor(private helperSvc: HelperService) { }

  ngOnInit() {
    let sessionStudentID = sessionStorage.getItem("StudentID");
    if (!!sessionStudentID) {
      this.getOnlineTestByStudentID(sessionStudentID);
    }
  }

  // lineChart
  // public lineChartData: Array<any> = [
  //   { data: [65, 59, 80, 81, 56, 55, 40], label: 'Percentage' },
  // ];
  // public lineChartLabels: Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  
  public lineChartData: Array<any> = [];
  public lineChartLabels: Array<any> = [];
  public lineChartOptions: any = {
    animation: false,
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
      // xAxes: [{
      //   barThickness: 50,
      //   // gridLines: {
      //   //   display: true
      //   // }
      // }]
    }
  };
  public lineChartColours: Array<any> = [
    { // purple
      backgroundColor: 'rgba(164,119,255,0.2)',
      borderColor: 'rgba(164,119,255,1)',
      pointBackgroundColor: 'rgba(164,119,255,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(164,119,255,0.8)'
    }
  ];
  public lineChartLegend = true;
  public lineChartType = 'line';
  // events
  public chartClicked(e: any): void {
    console.log(e);
  }
  public chartHovered(e: any): void {
    console.log(e);
  }

  getOnlineTestByStudentID(StudentID: any) {
    debugger;
    this.helperSvc.getService(APIUrl.GetStudentDashboardDetail + "?StudentID=" + StudentID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          //For Active Test
          this.TotalTest = res.Object.TotalTest;
          this.Completed = res.Object.Completed;
          this.Started = res.Object.Started;
          this.NotStarted = res.Object.NotStarted;
          this.TestPercentage = res.Object.TestPercentage;
          this.TestPercentage.forEach(element => {
            this.TestName.push(element.TestName);
            this.Percentage.push(element.Percentage);
          });
          // this.lineChartData= [ { data: [65, 59, 80, 81, 56, 55, 40], label: 'Percentage' }];
          // this.lineChartLabels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
          this.lineChartData = [ { data: this.Percentage, label: 'Percentage' }];
          this.lineChartLabels = this.TestName;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }

}
