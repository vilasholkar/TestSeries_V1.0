import { Component, OnInit, ViewChild } from '@angular/core';
import { OnlineTest } from '../../../models/test';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from "../../../services/helper.service";
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-generate-result',
  templateUrl: './generate-result.component.html',
  styleUrls: ['./generate-result.component.scss']
})
export class GenerateResultComponent implements OnInit {
  IsEmpty: boolean = false;
  PaginationConfig: any;
  data: OnlineTest[];
  displayedColumns: string[] = ['OnlineTestNo', 'TestName', 'TestSeriesName', 'TestTypeName', 'TestDuration', 'StartDate', 'StartTime', 'EndDate', 'EndTime', 'TestMarks', 'button'];
  dataSource: any = [];
  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild('sort') sort: MatSort;
  constructor(private helperSvc: HelperService) { }

  ngOnInit() {
    this.OnLoad();  
  }
  OnLoad()
  {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    this.getOnlineTest()
  }
  getOnlineTest() {
    debugger;
    this.helperSvc.getService(APIUrl.GetOnlineTest_ForGenerateResult)
      .subscribe(res => {
        if (res.Message === 'Success') {
          // this.data = res.Object as OnlineTest[];
          // this.data= this.data.filter(option => option.EndDate.toLowerCase().indexOf("2018-08-24") === 0)
          this.dataSource = new MatTableDataSource(res.Object as OnlineTest[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          !this.dataSource.data.length ? this.IsEmpty = true : this.IsEmpty = false;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }
  generateResultAnalysis(data: any) {
    debugger;
    if (confirm("Are you sure want to generate " + data.TestName+ " Result")) {
      this.helperSvc.getService(APIUrl.GenerateResultAnalysis + "?TestID=" + data.OnlineTestID)
        .subscribe(res => {
          if (res.Message === 'Success') {
            this.helperSvc.notifySuccess(res.Object);
            this.OnLoad();
          }          

        }, error => {
          alert('error');
          console.log(error);
        });
    }
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
}
