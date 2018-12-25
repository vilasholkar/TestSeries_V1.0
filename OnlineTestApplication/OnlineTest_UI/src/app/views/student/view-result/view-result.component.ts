import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { OT_Result } from '../../../models/result';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-view-result',
  templateUrl: './view-result.component.html',
  styleUrls: ['./view-result.component.scss']
})
export class ViewResultComponent implements OnInit {
  PaginationConfig:any;
  @Input() StudentID: any;
  //Element For Material
  displayedColumns: string[] = ['Rank', 'TestName', 
  //'TestSeriesName', 'TestTypeName', 'TestDate', 
  'TotalAttempt','TotalCorrect', 'TotalWrong', 'TotalMarks', 'TotalMarksObtained','Percentage',  
  'Physics_Right', 'Physics_Wrong', 'Chemistry_Right', 'Chemistry_Wrong', 'Biology_Right', 'Biology_Wrong', 'button'];
  dataSource: any = [];
  selection = new SelectionModel<OT_Result>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private helperSvc: HelperService 
  ) { }

  ngOnInit() {
    this.PaginationConfig=this.helperSvc.PaginationConfig;
    let sessionStudentID = sessionStorage.getItem("StudentID");
    if(!!this.StudentID){
      this.GetOnlineTestResultByStudentID(this.StudentID);
     //this.GetOnlineTestResultByStudentID(5110);
    }
    else if(!!sessionStudentID){
      this.GetOnlineTestResultByStudentID(sessionStudentID);
     //this.GetOnlineTestResultByStudentID(5110);
    }
  }
  ngOnChanges() {
    if(!!this.StudentID){
     this.GetOnlineTestResultByStudentID(this.StudentID);
    }
  }
  GetOnlineTestResultByStudentID(StudentID: any) {
    this.helperSvc.getService(APIUrl.GetOnlineTestResultByID+"?StudentID="+StudentID+"&TestID="+0)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.dataSource = new MatTableDataSource(res.Object as OT_Result[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      }, error => {
        alert('error');
         this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
    // this.resultAnalysisService.GetOnlineTestResultByStudentID(parseInt(StudentID)).subscribe(res => {
    //   this.dataSource = new MatTableDataSource(res);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    // })
    
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}