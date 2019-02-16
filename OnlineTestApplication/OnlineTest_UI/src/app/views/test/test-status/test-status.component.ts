import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith, debounceTime, distinctUntilChanged } from 'rxjs/operators';

import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
import { MatTabChangeEvent } from '@angular/material';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Student } from '../../../models/student';
import { OnlineTest } from '../../../models/test';
import { EligibleStudent } from '../../../models/test';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-test-status',
  templateUrl: './test-status.component.html',
  styleUrls: ['./test-status.component.scss']
})
export class TestStatusComponent implements OnInit {
 //Test Wise Search
 IsEmpty: boolean = false;
 testList = new FormControl();
 testOptions: OnlineTest[] = [];
 filteredTestOptions: Observable<OnlineTest[]>;
 OnlineTestID: any;
 eligibleStudentModel: EligibleStudent[];
 PaginationConfig:any;
 selectedValue: string="1";
  dataSource: any = [];
  displayedColumns = ['select', 'EnrollmentNo', 'StudentName', 'Gender', 'MobileNumber', 'TestStatus'];
  selection = new SelectionModel<EligibleStudent>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  //eligibleStudentArray=[];
  eligibleStudentArray: any = [];
  buttonState: any;
  constructor(private helperSvc:HelperService, private router: Router ) { }

  ngOnInit() {
    this.getOnlineTest();
    this.PaginationConfig=this.helperSvc.PaginationConfig;
  }
  getOnlineTest() {
    this.helperSvc.getService(APIUrl.GET_OnlineTest)
      .subscribe(data => {
        if (data.Message === 'Success')
          data.Object.forEach(element => {
            this.testOptions.push(element);
          });
        this.filteredTestOptions = this.testList.valueChanges
          .pipe(
            startWith<string | OnlineTest>(''),
            debounceTime(200),
            distinctUntilChanged(),
            map(value => typeof value === 'string' ? value : value.TestName),
            map(name => name ? this._filtertestList(name) : this.testOptions.slice())
          );
      }, error => {
        alert('error' + error.Message);
      })
  }
  testSelected(value: any) {
    this.OnlineTestID = value.OnlineTestID;
    this.getEligibleStudent(this.OnlineTestID);
  }
  displayTestFn(onlineTest?: OnlineTest): string | undefined {
    return onlineTest ? onlineTest.TestName : undefined;
  }
  private _filtertestList(value: string): OnlineTest[] {
    const filterValue = value.toLowerCase();
    // return this.testOptions.filter(option => option.TestName.toLowerCase().includes(filterValue));
    return this.testOptions.filter(option => option.TestName.toLowerCase().indexOf(filterValue) === 0);
  }

  /////////////////////////////////
 
  getEligibleStudent(OnlineTestID: any) {
    debugger;
    this.helperSvc.getService(APIUrl.GetEligibleStudentByTestID + "?OnlineTestID=" + OnlineTestID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.eligibleStudentModel = res.Object as EligibleStudent[];
          this.dataSource = new MatTableDataSource(this.eligibleStudentModel);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
          // this.eligibleStudentModel.filter(f => f.IsEligible).forEach(element => {
          //   this.eligibleStudentArray.push({
          //     OnlineTestID: element.OnlineTestID,
          //     StudentID: element.StudentID, EnrollmentNo: element.EnrollmentNo, StudentName: element.StudentName,
          //     Gender: element.Gender, MobileNumber: element.MobileNumber, TestStatusID: element.TestStatusID,
          //      TestStatus: element.TestStatus
          //   });
          // });
        }
      }, error => {
        // alert('error');
        this.helperSvc.errorHandler(error.error);
        console.log(error.error);
      });
    this.buttonState = this.eligibleStudentArray.length > 0 ? false : true;
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;

    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    debugger;
    if (!this.isAllSelected()) {
      this.eligibleStudentArray.length = 0;
      this.dataSource.data.forEach(row => {
        this.eligibleStudentArray.push({
          StudentID: row.StudentID,OnlineTestID:row.OnlineTestID
        });
      });
    }
    else {
      this.eligibleStudentArray.length = 0;
    }
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
    this.buttonState = this.eligibleStudentArray.length > 0 ? false : true;

  }

  pushEligibleStudent(data, isChecked) {
    debugger;
    if (isChecked.checked) {
      this.eligibleStudentArray.push({
        StudentID: data.StudentID,OnlineTestID:data.OnlineTestID
      });
    }
    else {
      this.eligibleStudentArray = this.eligibleStudentArray.filter(f => f.StudentID !== data.StudentID);
    }
    this.buttonState = this.eligibleStudentArray.length > 0 ? false : true;
  }

  TestStatus = [
    { TestStatusID : '1', TestStatus: 'Not Started'},
    { TestStatusID : '2', TestStatus: 'Started'},
    { TestStatusID : '3', TestStatus: 'Completed'}
  ];
  addEligibleStudent() {
    debugger
    if (this.eligibleStudentArray.length > 0) {
      this.selectedValue;
    // this.eligibleStudentService.addEligibleStudent(this.eligibleStudentArray)
    this.helperSvc.postService(APIUrl.UpdateEligibleStudentTestStatus,this.eligibleStudentArray)
        .subscribe(data => {
          if (data === 'Success')
          this.eligibleStudentArray.length = 0;
          this.helperSvc.notifySuccess('Test Status Changed Successfully');
        }, error => {
          //alert(error);
          this.helperSvc.errorHandler(error.error);
          console.log(error.error);
        });
    }
  }
}
