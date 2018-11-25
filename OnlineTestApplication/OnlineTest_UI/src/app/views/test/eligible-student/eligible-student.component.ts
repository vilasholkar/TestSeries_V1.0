import { Component, OnInit, ViewChild } from '@angular/core';
import { EligibleStudentService } from '../../../services/admin/eligible-student.service';
import { EligibleStudent } from '../../../models/test';
import { ActivatedRoute } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { DataSource, SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';
import 'rxjs/operator/filter';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eligible-student',
  templateUrl: './eligible-student.component.html',
  styleUrls: ['./eligible-student.component.scss']
})
export class EligibleStudentComponent implements OnInit {

  eligibleStudent: EligibleStudent;
  id: any;
  dataSource: any = [];
  displayedColumns = ['select','EnrollmentNo', 'StudentName', 'Gender', 'MobileNumber', 'FatherMobileNo'];
  selection = new SelectionModel<EligibleStudent>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  //eligibleStudentArray=[];
  eligibleStudentArray: Array<{ OnlineTestID: number, StudentID: number }> = [];
  constructor(private eligibleStudentService: EligibleStudentService,private spinner: NgxSpinnerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.spinner.show();
    this.id = +this.route.snapshot.paramMap.get('id');
    localStorage.setItem("OnlineTestID", this.id);
    this.eligibleStudentService.getEligibleStudent(parseInt(this.id)).subscribe(res => {
      this.dataSource =  new MatTableDataSource(res);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.spinner.hide();
    })
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;

    this.selection.selected.forEach(element => {
      if(this.selection.isSelected)
      this.eligibleStudentArray.push({ 'OnlineTestID': element.OnlineTestID, 'StudentID': element.StudentID });
    else
    this.eligibleStudentArray.splice(this.eligibleStudentArray.indexOf(element.StudentID), 1);
    });

    return numSelected === numRows;


  }

  isSelected(row){
    debugger;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  testfunction(){
    debugger;
  }
  // //   getEligibleStudent(){
  //     debugger;
  //   // this.id= +this.route.snapshot.paramMap.get('id');
  //   // localStorage.setItem("OnlineTestID",this.id);
  //   this.eligibleStudentService.getEligibleStudent(this.id as number)
  //   .subscribe(data => {
  //     if(data.Message === 'Success')
  //     this.eligibleStudent = data.Object;
  //     const ELEMENT_DATA:EligibleStudent = this.eligibleStudent;
  //   },error =>{
  //     alert('error');
  //   })
  // }
  addEligibleStudent() {
    debugger;
    this.eligibleStudentService.addEligibleStudent(this.eligibleStudentArray);
    alert('Data Saved Successfully');
  }
  onChangeEligibleStudent(onlineTestID: any, studentID: any, isChecked: boolean) {
    debugger;
    if (isChecked) {
      this.eligibleStudentArray.push({ 'OnlineTestID': onlineTestID, 'StudentID': studentID });
    }
    else {
      this.eligibleStudentArray.splice(this.eligibleStudentArray.indexOf(studentID), 1);
    }
  }
}

export class EligibleStudentDataSource extends DataSource<any> {
  eligibleStudentData: EligibleStudent[];

  constructor(private eligibleStudentService: EligibleStudentService, private paginator: MatPaginator, private sort: MatSort) {
    super();
  }

  connect(): Observable<EligibleStudent[]> {
    debugger;
    let id = localStorage.getItem("OnlineTestID");

    const dataMutations = [
      this.paginator.page,
      this.sort.sortChange
    ];
    //set the page length
    //this.eligibleStudentData = this.eligibleStudentService.getEligibleStudent(parseInt(id));
    //this.paginator.length = this.eligibleStudentService.getEligibleStudent(parseInt(id));

    return this.eligibleStudentService.getEligibleStudent(parseInt(id));
  }
  disconnect() { }
}
