import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePipe } from "@angular/common";
import { StudentOnlineTestService } from '../../../services/student/student-online-test.service';
import { StudentOnlineTest } from '../../../models/student';
import { Router } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { HelperService } from "../../../services/helper.service";
import { APIUrl } from "../../../shared/API-end-points";
import { FormBuilder, FormGroup } from '@angular/forms';
import * as moment from 'moment';

@Component({
  selector: 'app-view-online-test',
  templateUrl: './view-online-test.component.html',
  styleUrls: ['./view-online-test.component.scss']
})
export class ViewOnlineTestComponent implements OnInit {
  IsEmpty: boolean = false;
  IsEmpty1: boolean = false;
  RedirectToURL: string;
  studentOnlineTest: StudentOnlineTest;
  studentOnlineTest1: StudentOnlineTest[] = null;
  studentOnlineTestModel: any = {};
  PaginationConfig: any;
  //Element For Material
  displayedColumns: string[] = ['OnlineTestNo', 'TestName', 'TestSeriesName', 'TestTypeName', 'TestDuration', 'StartDate', 'StartTime', 'EndDate', 'EndTime', 'TestMarks', 'button'];
  displayedColumns1: string[] = ['OnlineTestNo', 'TestName', 'TestSeriesName', 'TestTypeName', 'TestDuration', 'StartDate', 'StartTime', 'EndDate', 'EndTime', 'TestMarks', 'TestStatus'];
  dataSource: any = [];
  dataSource1: any = [];
  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('sort') sort: MatSort;
  @ViewChild('paginator1') paginator1: MatPaginator;
  @ViewChild('sort1') sort1: MatSort;
  constructor(private helperSvc: HelperService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    let sessionStudentID = sessionStorage.getItem("StudentID");
    if (!!sessionStudentID) {
      this.getOnlineTestByStudentID(sessionStudentID);
      // this.getOnlineTestByStudentID(sessionStudentID);
      // this.getOnlineTestByStudentID1(sessionStudentID);
    }
    // this.getOnlineTestByStudentID(34);

  }
  redirectToTest(TestId: any) {
    this.RedirectToURL = "/#/test/quiz/" + TestId;
    window.open(this.RedirectToURL, '_blank', 'location=no,addressbar=no,height=700,width=1200,scrollbars=yes,status=yes');
    this.router.navigate(['/dashboard/student-dashboard']);
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
  applyFilter1(filterValue: string) {
    this.dataSource1.filter = filterValue.trim().toLowerCase();
    !this.dataSource1.filteredData.length ? this.IsEmpty1 = true : this.IsEmpty1 = false;
  }
  getOnlineTestByStudentID(StudentID: any) {
    debugger;
    this.helperSvc.getService(APIUrl.GetOnlineTestByStudentID + "?StudentID=" + StudentID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          //For Active Test
          // let start = moment(res.Object[1].StartDate + ' ' + res.Object[1].StartTime,"DD/MM/YYYY HH:mm a");
          // let end = moment(res.Object[1].EndDate + ' ' + res.Object[1].EndTime,"DD/MM/YYYY HH:mm a");
          // console.log(moment());
          // console.log(moment().isBetween(start, end));
          this.dataSource = new MatTableDataSource(res.Object.filter(f => f.TestStatusID == 1
            && moment().isBetween(moment(f.StartDate + ' ' + f.StartTime,"DD/MM/YYYY HH:mm a"), moment(f.EndDate + ' ' + f.EndTime,"DD/MM/YYYY HH:mm a"))
          ) as StudentOnlineTest[]);
          //console.log(moment(res.Object[1].StartTime,"hh:mm a").format("HH:mm") <= moment().format("HH:mm"));
          // this.dataSource = new MatTableDataSource(res.Object.filter(f => f.TestStatusID == 1
          //    && moment(f.StartDate).format("DD/MM/YYYY") <= moment().format("DD/MM/YYYY") 
          //   && moment(f.StartTime,"hh:mm a").format("HH:mm")<=moment().format("HH:mm")
          //  //  && moment(f.EndDate).format("DD/MM/YYYY") >= moment().format("DD/MM/YYYY")
          //   //&& moment(f.EndTime).format("hh:mm a")>=moment().format("hh:mm a")
          //   // && moment(f.EndDate).add(1,'days') >= moment()
          // ) as StudentOnlineTest[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
          // For History Test
          this.dataSource1 = new MatTableDataSource(res.Object as StudentOnlineTest[]);
          this.dataSource1.paginator = this.paginator1;
          this.dataSource1.sort = this.sort1;
          !this.dataSource1.filteredData.length ? this.IsEmpty1 = true : this.IsEmpty1 = false;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }

  // getOnlineTestByStudentID(StudentID: any) {
  //   debugger;
  //   this.helperSvc.getService(APIUrl.GetOnlineTestByStudentID + "?StudentID=" + StudentID)
  //     .subscribe(res => {
  //       if (res.Message === 'Success') {
  //         // var data=res.Object as StudentOnlineTest[];
  //         console.log(moment());
  //         var data = res.Object.filter(f => f.TestStatusID == 1 && moment(f.StartDate) <= moment() && moment(f.EndDate) >= moment()) as StudentOnlineTest[]
  //         this.dataSource = new MatTableDataSource(data);
  //         this.dataSource.paginator = this.paginator;
  //         this.dataSource.sort = this.sort;
  //         !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  //       }
  //     }, error => {
  //       alert('error');
  //       console.log(error);
  //     });
  // }
  // getOnlineTestByStudentID1(StudentID: any) {
  //   this.helperSvc.getService(APIUrl.GetOnlineTestByStudentID + "?StudentID=" + StudentID)
  //     .subscribe(data => {
  //       if (data.Message === 'Success') {
  //         this.dataSource1 = new MatTableDataSource(data.Object.filter(f => f.TestStatusID == 3) as StudentOnlineTest[]);
  //         this.dataSource1.paginator = this.paginator1;
  //         this.dataSource1.sort = this.sort1;
  //         !this.dataSource1.filteredData.length ? this.IsEmpty1 = true : this.IsEmpty1 = false;
  //       }
  //     }, error => {
  //       alert('error');
  //       console.log(error);
  //     });
  // }

}