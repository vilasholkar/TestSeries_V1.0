import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { TestSeriesService } from '../../../services/admin/test-series.service';
import { TestSeries } from '../../../models/test';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-test-series',
  templateUrl: './test-series.component.html',
  styleUrls: ['./test-series.component.scss']
})
export class TestSeriesComponent implements OnInit {
  showAddDiv: any;
  testSeries: TestSeries;
  testSeriesModel: any = {};
  rootNode: any;
  isTestSeriesReadonly: any = true;
  //Element For Material
  displayedColumns: string[] = ['select', 'position', 'name', 'weight', 'symbol'];
  // displayedColumns: string[] = ['select','testseriesid', 'testseries', 'totaltest', 'description'];
  // dataSource = new MatTableDataSource<TestSeries>(ELEMENT_DATA);
  // selection = new SelectionModel<TestSeries>(true, []);
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
  selection = new SelectionModel<PeriodicElement>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  ///////////////////////

  constructor(private testSeriesService: TestSeriesService, rootNode: ElementRef, public snackBar: MatSnackBar) {
    this.showAddDiv = false;
  }

  ngOnInit() {
    // this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;
    this.getTestSeries();
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }


  Edit(model: TestSeries) {
    this.isTestSeriesReadonly = false;
  }
  changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
  }
  getTestSeries() {
    this.testSeriesService.getTestSeries()
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.testSeries = data.Object;
        }
      }, error => {
        this.openSnackBar("Error.", "Close");
        console.log(error);
      });
  }
  UpdateTestSeries(model: TestSeries) {
    this.testSeries = model;
    this.testSeriesService.addUpdateTestSeries(this.testSeries)
      .subscribe(data => {
        if (data === 'Success') {
          this.testSeriesModel = {};
          this.getTestSeries();
          this.showAddDiv = false;
          this.isTestSeriesReadonly = true;
          this.openSnackBar("Record Saved Successfully.", "Close");
        }
      }, error => {
        this.openSnackBar("Error.", "Close");
        console.log(error);
      });
  }
  AddTestSeries() {
    this.testSeries = this.testSeriesModel;
    this.testSeriesService.addUpdateTestSeries(this.testSeries)
      .subscribe(data => {
        if (data === 'Success') {
          this.testSeriesModel = {};
          this.getTestSeries();
          this.showAddDiv = false;
          this.openSnackBar("Record Added Successfully.", "Close");
          this.isTestSeriesReadonly = true;
        }
      }, error => {
        this.openSnackBar("Error.", "Close");
        console.log(error);
      });
  }
  DeleteTestSeries(model: TestSeries) {
    if (confirm("Are you sure to delete " + model.TestSeries)) {
      this.testSeries = model;
      this.testSeriesService.deleteTestSeriesById(this.testSeries)
        .subscribe(data => {
          if (data === 'Success') {
            this.getTestSeries();
            this.showAddDiv = false;
            this.openSnackBar("Record Deleted Successfully.", "Close");
            this.isTestSeriesReadonly = true;
          }
        }, error => {
          this.openSnackBar("Error.", "Close");
          console.log(error);
        });
    }


  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
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
