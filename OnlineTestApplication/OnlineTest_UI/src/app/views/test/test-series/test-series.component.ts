import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { TestSeriesService } from '../../../services/admin/test-series.service';
import { TestSeries } from '../../../models/test';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-test-series',
  templateUrl: './test-series.component.html',
  styleUrls: ['./test-series.component.scss']
})
export class TestSeriesComponent implements OnInit {
  IsEmpty: boolean = false;
  Title: any;
  btnAddNew: boolean = true;
  PaginationConfig: any;
  showAddDiv: any;
  testSeries: TestSeries;
  testSeriesModel: any = {};
  isTestSeriesReadonly: any = true;
  //Element For Material
  displayedColumns: string[] = ['TestSeries', 'button', 'button1'];
  dataSource: any = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private testSeriesService: TestSeriesService, private helperSvc: HelperService) {
    this.showAddDiv = false;
  }

  ngOnInit() {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    this.getTestSeries();
  }
  changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
    this.testSeriesModel = {};
    this.Title = "Add Test Series";
  }
  getTestSeries() {
    this.helperSvc.getService(APIUrl.GET_TestSeries)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.dataSource = new MatTableDataSource(res.Object as TestSeries[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          !this.dataSource.data.length ? this.IsEmpty = true : this.IsEmpty = false;
        }
      }, error => {
        this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
    // this.testSeriesService.getTestSeries()
    //   .subscribe(data => {
    //     if (data.Message === 'Success') {
    //       this.testSeries = data.Object;
    //     }
    //   }, error => {
    //     this.openSnackBar("Error.", "Close");
    //     console.log(error);
    //   });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
  getTestSeriesByID(model: TestSeries) {
    this.btnAddNew = false;
    this.showAddDiv = true;
    this.Title = "Edit Test Type";
    this.testSeriesModel = model;
  }
  
  AddTestSeries() {
    this.testSeries = this.testSeriesModel;
  //  this.testSeriesService.addUpdateTestSeries(this.testSeries)
    this.helperSvc.postService(APIUrl.AddUpdateTestSeries, this.testSeries)
      .subscribe(data => {
        if (data === 'Success') {
          this.testSeriesModel = {};
          this.getTestSeries();
          this.showAddDiv = false;
          this.btnAddNew=true;
          this.helperSvc.notifySuccess("Record Added Successfully.");
        }
      }, error => {
        this.helperSvc.errorHandler(error);
        console.log(error);
      });
  }
  
  DeleteTestSeries(model: TestSeries) {
    if (confirm("Are you sure to delete " + model.TestSeries)) {
      this.testSeries = model;
      // this.testSeriesService.deleteTestSeriesById(this.testSeries)
      this.helperSvc.postService(APIUrl.DeleteTestSeries, this.testSeries)
        .subscribe(data => {
          if (data === 'Success') {
            this.getTestSeries();
            this.showAddDiv = false;
            this.helperSvc.notifySuccess("Record Deleted Successfully.");
            this.isTestSeriesReadonly = true;
          }
        }, error => {
          this.helperSvc.errorHandler(error);
          console.log(error);
        });
    }
  }
  // UpdateTestSeries(model: TestSeries) {
  //   this.testSeries = model;
  //   this.testSeriesService.addUpdateTestSeries(this.testSeries)
  //     .subscribe(data => {
  //       if (data === 'Success') {
  //         this.testSeriesModel = {};
  //         this.getTestSeries();
  //         this.showAddDiv = false;
  //         this.isTestSeriesReadonly = true;
  //         this.openSnackBar("Record Saved Successfully.", "Close");
  //       }
  //     }, error => {
  //       this.openSnackBar("Error.", "Close");
  //       console.log(error);
  //     });
  // }
  // openSnackBar(message: string, action: string) {
  //   this.snackBar.open(message, action, {
  //     duration: 2000,
  //   });
  // }
}

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
  { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
  { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
  { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
  { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
  { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
  { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
  { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
  { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
  { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];
