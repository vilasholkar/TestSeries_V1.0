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
  PaginationConfig:any;
  eligibleStudentModel: EligibleStudent[];
  id: any;
  dataSource: any = [];
  displayedColumns = ['select', 'EnrollmentNo', 'StudentName', 'Gender', 'MobileNumber', 'FatherMobileNo'];
  selection = new SelectionModel<EligibleStudent>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  //eligibleStudentArray=[];
  eligibleStudentArray: any = [];
  buttonState: any;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private helperSvc: HelperService 
  ) { }

  ngOnInit() {
    this.PaginationConfig=this.helperSvc.PaginationConfig;
    this.id = +this.route.snapshot.paramMap.get('id');
    localStorage.setItem("OnlineTestID", this.id);
    this.getEligibleStudent(this.id);
  }

  getEligibleStudent(OnlineTestID: any) {
    this.helperSvc.getService(APIUrl.GetEligibleStudent+"?OnlineTestID="+OnlineTestID)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.eligibleStudentModel=res.Object as EligibleStudent[];
          this.dataSource = new MatTableDataSource(this.eligibleStudentModel);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.eligibleStudentModel.filter(f => f.IsEligible).forEach(element => {
                this.eligibleStudentArray.push({
                  OnlineTestID: element.OnlineTestID,
                  StudentID: element.StudentID, EnrollmentNo: element.EnrollmentNo, StudentName: element.StudentName,
                  Gender: element.Gender, MobileNumber: element.MobileNumber, FatherMobileNo: element.FatherMobileNo,
                  IsEligible: element.IsEligible
                });
              });
        }
      }, error => {
       // alert('error');
         this.helperSvc.errorHandler(error.error);
        console.log(error.error);
      });
    // this.eligibleStudentService.getEligibleStudent(parseInt(this.id)).subscribe(res => {
    //   this.dataSource = new MatTableDataSource(res);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    //   res.filter(f => f.IsEligible).forEach(element => {
    //     this.eligibleStudentArray.push({
    //       OnlineTestID: element.OnlineTestID,
    //       StudentID: element.StudentID, EnrollmentNo: element.EnrollmentNo, StudentName: element.StudentName,
    //       Gender: element.Gender, MobileNumber: element.MobileNumber, FatherMobileNo: element.FatherMobileNo,
    //       IsEligible: element.IsEligible
    //     });
    //   });
    // });
     this.buttonState = this.eligibleStudentArray.length > 0 ? false : true;
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
    debugger;
    if (!this.isAllSelected()) {
      this.eligibleStudentArray.length = 0;
      this.dataSource.data.forEach(row => {
        this.eligibleStudentArray.push({
          OnlineTestID: row.OnlineTestID,
          StudentID: row.StudentID, EnrollmentNo: row.EnrollmentNo, StudentName: row.StudentName,
          Gender: row.Gender, MobileNumber: row.MobileNumber, FatherMobileNo: row.FatherMobileNo,
          IsEligible: row.IsEligible
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
        StudentID: data.StudentID, OnlineTestID: data.OnlineTestID
        , EnrollmentNo: data.EnrollmentNo, StudentName: data.StudentName,
        Gender: data.Gender, MobileNumber: data.MobileNumber, FatherMobileNo: data.FatherMobileNo
      });
    }
    else {
      this.eligibleStudentArray = this.eligibleStudentArray.filter(f => f.StudentID !== data.StudentID);
    }
    this.buttonState = this.eligibleStudentArray.length > 0 ? false : true;
  }

  addEligibleStudent() {
    debugger;
    if (this.eligibleStudentArray.length > 0) {
    //  this.eligibleStudentService.addEligibleStudent(this.eligibleStudentArray)
    this.helperSvc.postService(APIUrl.AddEligibleStudent,this.eligibleStudentArray)
        .subscribe(data => {
          if (data === 'Success')
            alert('Data Saved Successfully');
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

