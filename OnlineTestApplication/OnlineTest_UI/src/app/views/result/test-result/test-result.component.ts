import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild,Input } from '@angular/core';
import { OT_Result } from '../../../models/result';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-test-result',
  templateUrl: './test-result.component.html',
  styleUrls: ['./test-result.component.scss']
})
export class TestResultComponent implements OnInit {
  PaginationConfig:any;
  @Input() OnlineTestID: any;
  displayedColumns: string[] = ['Rank', 'StudentName', 
  'TotalAttempt','TotalCorrect', 'TotalWrong', 'TotalMarks', 'TotalMarksObtained', 'Percentage', 
  'Physics_Right', 'Physics_Wrong', 'Chemistry_Right', 'Chemistry_Wrong', 'Biology_Right', 'Biology_Wrong',
  'button'];
  dataSource: any = [];
  selection = new SelectionModel<OT_Result>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor( 
    private helperSvc:HelperService 
    ) { }
  ngOnInit() {
    this.PaginationConfig=this.helperSvc.PaginationConfig;
  }
  ngOnChanges() {
    if(!!this.OnlineTestID){
      this.GetOnlineTestResultByTestID(this.OnlineTestID);
    }
  }
  GetOnlineTestResultByTestID(TestID:any) {
    debugger;
    this.helperSvc.getService(APIUrl.GetOnlineTestResultByID+"?StudentID="+0+"&TestID="+TestID)
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
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}