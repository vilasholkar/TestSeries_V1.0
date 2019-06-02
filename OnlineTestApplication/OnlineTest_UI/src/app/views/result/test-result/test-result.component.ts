import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { OT_Result } from '../../../models/result';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
import { Router } from '@angular/router';
@Component({
  selector: 'app-test-result',
  templateUrl: './test-result.component.html',
  styleUrls: ['./test-result.component.scss']
})
export class TestResultComponent implements OnInit {
  TestType: string;
  IsEmpty: boolean = false;
  PaginationConfig: any;
  @Input() OnlineTestID: any;
  displayedColumns: string[] = ['StudentName',
    'TotalAttempt', 'TotalCorrect', 'TotalWrong', 'TotalMarks', 'TotalMarksObtained', 'Percentage',
    'Physics_Right', 'Physics_Wrong', 'Chemistry_Right', 'Chemistry_Wrong', 'Biology_Right', 'Biology_Wrong','button', 'button1'];
  dataSource: any = [];
  selection = new SelectionModel<OT_Result>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private helperSvc: HelperService, private router: Router
  ) { }
  ngOnInit() {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
  }
  ngOnChanges() {
    if (!!this.OnlineTestID) {
      this.GetOnlineTestResultByTestID(this.OnlineTestID);
    }
  }
  NavigateToResponse(StudentID, TestID) {
    ///result/result-analysis/
    this.router.navigate(['/result/student-response/' + StudentID + '/' + TestID], { queryParams: { StudentID: StudentID, TestID: TestID }, skipLocationChange: true });
  }
  NavigateToAnalysis(StudentID, TestID) {
    ///result/result-analysis/
    this.router.navigate(['/result/result-analysis/' + StudentID + '/' + TestID], { queryParams: { StudentID: StudentID, TestID: TestID }, skipLocationChange: true });
  }
  GetOnlineTestResultByTestID(TestID: any) {
    debugger;
    this.helperSvc.getService(APIUrl.GetOnlineTestResultByID + "?StudentID=" + 0 + "&TestID=" + TestID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          if (res.Object.length > 0) {
            if (res.Object[0].TestTypeName === 'NEET') {
              this.displayedColumns=['StudentName',
              'TotalAttempt', 'TotalCorrect', 'TotalWrong', 'TotalMarks', 'TotalMarksObtained', 'Percentage',
              'Physics_Right', 'Physics_Wrong', 'Chemistry_Right', 'Chemistry_Wrong', 'Biology_Right', 'Biology_Wrong','button', 'button1'];
            }
           else if (res.Object[0].TestTypeName === 'AIIMS') {
              this.displayedColumns=['StudentName','TotalAttempt', 'TotalCorrect', 'TotalWrong', 'TotalMarks', 'TotalMarksObtained', 'Percentage',
              'Physics_Right', 'Physics_Wrong', 'Chemistry_Right', 'Chemistry_Wrong', 'Biology_Right', 'Biology_Wrong','Aptitude_Right', 'Aptitude_Wrong','button', 'button1'];
            }
          }
          this.dataSource = new MatTableDataSource(res.Object as OT_Result[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
        !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
      }, error => {
        alert('error');
        this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
}