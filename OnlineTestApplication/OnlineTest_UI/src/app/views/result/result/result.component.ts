import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith, debounceTime, distinctUntilChanged } from 'rxjs/operators';

import { MatTabChangeEvent } from '@angular/material';

import { OnlineTestService } from '../../../services/admin/online-test.service'
import { StudentService } from '../../../services/admin/student.service'
import { Student } from '../../../models/student';
import { OnlineTest } from '../../../models/test';

import { HelperService } from '../../../services/helper.service'
import { UserService } from '../../../services/auth-service/user.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {
  OnlineTestID: any;
  StudentID: any;
  tableStudentResult: any;
  tableTestResult: any;
  //Test Wise Search
  testList = new FormControl();
  testOptions: OnlineTest[] = [];
  filteredTestOptions: Observable<OnlineTest[]>;
  //Student Wise Search
  studentList = new FormControl();
  studentOptions: Student[] = [];
  filteredStudentOptions: Observable<Student[]>;

  constructor(
    private userService: UserService,
    private onlineTestService: OnlineTestService,
    private studentService: StudentService,
    private helperSvc: HelperService) { }

  ngOnInit() {
    this.getOnlineTest();
    this.getStudentDetails();
    this.tableTestResult = false;
    this.tableStudentResult = false;
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
    // localStorage.setItem("TestID", value.OnlineTestID);
    this.tableTestResult = true;
  }
  displayTestFn(onlineTest?: OnlineTest): string | undefined {
    return onlineTest ? onlineTest.TestName : undefined;
  }
  private _filtertestList(value: string): OnlineTest[] {
    const filterValue = value.toLowerCase();
    // return this.testOptions.filter(option => option.TestName.toLowerCase().includes(filterValue));
    return this.testOptions.filter(option => option.TestName.toLowerCase().indexOf(filterValue) === 0);
  }

  getStudentDetails() {
    this.helperSvc.getService(APIUrl.GetStudentDetails)
      .subscribe(data => {
        if (data.Message === 'Success')
          data.Object.forEach(element => {
            this.studentOptions.push(element);
          });
        this.filteredStudentOptions = this.studentList.valueChanges
          .pipe(
            startWith<string | Student>(''),
            debounceTime(200),
            distinctUntilChanged(),
            map(value => typeof value === 'string' ? value : value.FirstName),
            map(name => name ? this._filterstudentList(name) : this.studentOptions.slice())
          );
      }, error => {
        alert('error' + error.Message);
      })
  }
  studentSelected(value: any) {
    this.StudentID = value.StudentID;
    // localStorage.setItem("StudentID", value.StudentID);
    this.tableStudentResult = true;
  }
  displayStudentFn(student?: Student): string | undefined {
    return student ? student.FirstName + " " + student.LastName + " (" + student.EnrollmentNo + ")" : undefined;
  }
  private _filterstudentList(value: string): Student[] {
    const filterValue = value.toLowerCase();
    // return this.studentOptions.filter(option => option.FirstName.toLowerCase().includes(filterValue));
    return this.studentOptions.filter(option => option.FirstName.toLowerCase().indexOf(filterValue) === 0);

  }

  onTabLinkClick(event: MatTabChangeEvent) {
    this.tableTestResult = false;
    this.tableStudentResult = false;
  }

}