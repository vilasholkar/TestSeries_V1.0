import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { EligibleStudent } from '../../../models/test';
import { ActivatedRoute, Router } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

// import 'rxjs/operator/filter';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-eligible-student',
  templateUrl: './eligible-student.component.html',
  styleUrls: ['./eligible-student.component.scss']
})
export class EligibleStudentComponent implements OnInit {
  IsEmpty: boolean = false;
  PaginationConfig: any;
  eligibleStudentModel: EligibleStudent[];
  id: any;
  dataSource: any = [];
  displayedColumns = ['select', 'EnrollmentNo', 'StudentName', 'Stream', 'Course', 'Batch'];
  selection = new SelectionModel<EligibleStudent>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  //eligibleStudentArray=[];
  eligibleStudentArray: any = [];
  buttonState: any;
  selectedCount: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private helperSvc: HelperService
  ) { }

  ngOnInit() {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    this.id = +this.route.snapshot.paramMap.get('id');
    localStorage.setItem("OnlineTestID", this.id);
    this.getEligibleStudent(this.id);
  }

  getEligibleStudent(OnlineTestID: any) {
    debugger;
    this.helperSvc.getService(APIUrl.GetEligibleStudent + "?OnlineTestID=" + OnlineTestID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.eligibleStudentModel = res.Object as EligibleStudent[];
          this.dataSource = new MatTableDataSource(this.eligibleStudentModel);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.eligibleStudentModel.filter(f => f.IsEligible).forEach(element => {
            this.selectedCount += 1;
            this.eligibleStudentArray.push({
              OnlineTestID: element.OnlineTestID,TestName: element.TestName,StartDate: element.StartDate,EndDate: element.EndDate,
              StudentID: element.StudentID, EnrollmentNo: element.EnrollmentNo, StudentName: element.StudentName,
              Gender: element.Gender, MobileNumber: element.MobileNumber, FatherMobileNo: element.FatherMobileNo,
              IsEligible: element.IsEligible, TestStatusID: element.TestStatusID
            });
          });
          this.selection.selected.length = this.selectedCount;
          this.isAllSelected();
          !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
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
    //const numSelected = this.selection.selected.length;
    //const numRows = this.dataSource.data.length;
    return this.selection.selected.length === this.dataSource.data.length ? true : false;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    debugger;
    if (!this.isAllSelected()) {
      this.eligibleStudentArray.length = 0;
      this.dataSource.data.forEach(row => {
        row.TestStatusID = 1;
        this.eligibleStudentArray.push({
          OnlineTestID: row.OnlineTestID,TestName: row.TestName,StartDate: row.StartDate,EndDate: row.EndDate,
          StudentID: row.StudentID, EnrollmentNo: row.EnrollmentNo, StudentName: row.StudentName,
          Gender: row.Gender, MobileNumber: row.MobileNumber, FatherMobileNo: row.FatherMobileNo,
          IsEligible: row.IsEligible, TestStatusID: row.TestStatusID
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
      data.TestStatusID = 1;
      if (this.eligibleStudentArray.filter(f => f.StudentID === data.StudentID).length > 0) {
        this.eligibleStudentArray[this.eligibleStudentArray.findIndex(f => f.StudentID === data.StudentID)] = data;
      }
      else {
        this.eligibleStudentArray.push({
          StudentID: data.StudentID, OnlineTestID: data.OnlineTestID,TestName: data.TestName,StartDate: data.StartDate,EndDate: data.EndDate,
           EnrollmentNo: data.EnrollmentNo, StudentName: data.StudentName,
          Gender: data.Gender, MobileNumber: data.MobileNumber, FatherMobileNo: data.FatherMobileNo,
          IsEligible: data.IsEligible, TestStatusID: data.TestStatusID
        });
      }
    }
    else {
      data.TestStatusID = 0;
      //this.eligibleStudentArray = this.eligibleStudentArray.filter(f => f.StudentID !== data.StudentID);
      this.eligibleStudentArray[this.eligibleStudentArray.findIndex(f => f.StudentID === data.StudentID)] = data;
    }
    this.buttonState = this.eligibleStudentArray.length > 0 ? false : true;
  }

  addEligibleStudent() {
    debugger;
    if (this.eligibleStudentArray.length > 0) {
      //  this.eligibleStudentService.addEligibleStudent(this.eligibleStudentArray)
      this.helperSvc.postService(APIUrl.AddEligibleStudent, this.eligibleStudentArray)
        .subscribe(data => {
          if (data === 'Success')
            // alert('Data Saved Successfully');
            this.helperSvc.notifySuccess('Data Saved Successfully');
          this.eligibleStudentArray.length = 0;
          this.router.navigate(['/test/online-test']);
        }, error => {
          //alert(error);
          this.helperSvc.errorHandler(error.error);
          console.log(error.error);
        });
    }
  }
}

