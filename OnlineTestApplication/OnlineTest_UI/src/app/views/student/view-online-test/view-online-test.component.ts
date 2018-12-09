import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentOnlineTestService } from '../../../services/student/student-online-test.service';
import { StudentOnlineTest } from '../../../models/student';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { HelperService } from "../../../services/helper.service";
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-view-online-test',
  templateUrl: './view-online-test.component.html',
  styleUrls: ['./view-online-test.component.scss']
})
export class ViewOnlineTestComponent implements OnInit {
  studentOnlineTest: StudentOnlineTest;
  studentOnlineTest1: StudentOnlineTest[] = null;
  studentOnlineTestModel: any = {};
  PaginationConfig:any;
  //Element For Material
  displayedColumns: string[] = ['OnlineTestNo', 'TestName', 'TestSeriesName', 'TestTypeName', 'TestDuration', 'StartDate', 'EndDate', 'TestMarks', 'button'];
  displayedColumns1: string[] = ['OnlineTestNo', 'TestName', 'TestSeriesName', 'TestTypeName', 'TestDuration', 'StartDate', 'EndDate', 'TestMarks', 'button'];
  dataSource: any = [];
  dataSource1: any = [];
  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('sort') sort: MatSort;
  @ViewChild('paginator1') paginator1: MatPaginator;
  @ViewChild('sort1') sort1: MatSort;
  constructor(private studentOnlineTestService: StudentOnlineTestService,
     private helperSvc: HelperService,
     private spinner: NgxSpinnerService
    ) { }

  ngOnInit() {
    this.PaginationConfig=this.helperSvc.PaginationConfig;
    let sessionStudentID = sessionStorage.getItem("StudentID");
    if (!!sessionStudentID) {
      this.getOnlineTestByStudentID1(34);
      this.getOnlineTestByStudentID2(34);
    }
   // this.getOnlineTestByStudentID(34);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  applyFilter1(filterValue: string) {
    this.dataSource1.filter = filterValue.trim().toLowerCase();
  }
  getOnlineTestByStudentID(StudentID: any) {
    this.spinner.show();
    this.studentOnlineTestService.getOnlineTestByStudentID(StudentID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.studentOnlineTest = res.Object;
          this.spinner.hide();
        }
      }, error => {
        alert('error');
        console.log(error);
      });

  }
  getOnlineTestByStudentID1(StudentID: any) {
    this.helperSvc.getService(APIUrl.GetOnlineTestByStudentID+"?StudentID="+StudentID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.dataSource = new MatTableDataSource(res.Object as StudentOnlineTest[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
    // this.studentOnlineTestService.getOnlineTestByStudentID(StudentID)
      // .subscribe(data => {
      //   if (data.Message === 'Success') {
      //     //this.studentOnlineTest1 = data.Object as StudentOnlineTest[];
      //     this.dataSource = new MatTableDataSource(data.Object as StudentOnlineTest[]);
      //     this.dataSource.paginator = this.paginator;
      //     this.dataSource.sort = this.sort;
          
      //   }
      // }, error => {
      //   alert('error');
      //   console.log(error);
      // });

  }
  getOnlineTestByStudentID2(StudentID: any) {
    this.helperSvc.getService(APIUrl.GetOnlineTestByStudentID+"?StudentID="+StudentID)
      .subscribe(data => {
        if (data.Message === 'Success') {
          //this.studentOnlineTest1 = data.Object as StudentOnlineTest[];
          this.dataSource1 = new MatTableDataSource(data.Object as StudentOnlineTest[]);
          this.dataSource1.paginator = this.paginator1;
          this.dataSource1.sort = this.sort1;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }

}