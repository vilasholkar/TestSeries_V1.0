import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { StudentOnlineTestService } from '../../../services/student/student-online-test.service';
import { OT_Result } from '../../../models/result';
import { MatPaginator, MatSort, MatTableDataSource, MatTabChangeEvent } from '@angular/material';


@Component({
  selector: 'app-test-result',
  templateUrl: './test-result.component.html',
  styleUrls: ['./test-result.component.scss']
})
export class TestResultComponent implements OnInit {
  tableStudentResult: any;
  tableTestResult: any;
  //Test Wise Search
  testList = new FormControl();
  testOptions: string[] = ['Minor Test 1', 'Minor Test 2', 'Minor Test 3', 'Major Test 1', 'Major Test 2', 'Major Test 3'];
  filteredTestOptions: Observable<string[]>;

  //Student Wise Search
  studentList = new FormControl();
  studentOptions: string[] = ['Rohan Yadav', 'Abhijeet Singh', 'Vilas Holkar', 'Vaibhav Patidar', 'Rohan Yadav', 'Abhijeet Singh', 'Vilas Holkar', 'Vaibhav Patidar'];
  filteredStudentOptions: Observable<string[]>;
  //Element For Material
  displayedColumns: string[] = ['Rank', 'StudentName', 'TotalAttempt',
    'TotalCorrect', 'TotalWrong', 'TotalMarks', 'TotalMarksObtained',
    'Percentage',
    // 'PhysicsRight', 'PhysicsWrong', 'ChemistryRight',
    // 'ChemistryWrong', 'BiologyRight', 'PhysicsWrong', 'BiologyWrong',
    'button'];
  dataSource = new MatTableDataSource<OT_Result>(ELEMENT_DATA);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor() { }

  ngOnInit() {
    this.filteredTestOptions = this.testList.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filtertestList(value))
      );
    this.filteredStudentOptions = this.studentList.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filterstudentList(value))
      );

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.tableTestResult = false;
    this.tableStudentResult = false;
  }
  onSearchResult(value: any) {
    //console.log(value)
    if(value=='TestResult')
    {
      this.tableTestResult = true;
    }
   else if(value=='StudentResult')
    {
    this.tableStudentResult = true;
    }
  };
  onTabLinkClick(event: MatTabChangeEvent) {
    this.tableTestResult = false;
    this.tableStudentResult = false;

  };
  private _filtertestList(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.testOptions.filter(option => option.toLowerCase().includes(filterValue));
  }
  private _filterstudentList(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.studentOptions.filter(option => option.toLowerCase().includes(filterValue));
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}

const ELEMENT_DATA: OT_Result[] = [
  { ResultID: 1, StudentID: 1, StudentName: 'Rohan Yadav', TestID: 1, TestName: 'Minor Test 1', TestSeriesID: 1, TestSeriesName: 'abc', TestTypeID: 1, TestTypeName: 'asd', TestDate: '20/09/2018', Physics_Total: '30', Physics_Right: '30', Physics_Wrong: '0', Chemistry_Total: '30', Chemistry_Right: '30', Chemistry_Wrong: '30', Biology_Total: '30', Biology_Right: '30', Biology_Wrong: '30', TotalCorrect: '30', TotalWrong: '30', TotalAttempt: '30', TotalMarksObtained: '30', Percentage: '30%', Rank: '1', TotalMarks: '30', QualifyingMarks: '30' },
  { ResultID: 1, StudentID: 1, StudentName: 'Vaibhav Patidar', TestID: 1, TestName: 'Minor Test 1', TestSeriesID: 1, TestSeriesName: 'def', TestTypeID: 1, TestTypeName: 'asd', TestDate: '20/09/2018', Physics_Total: '30', Physics_Right: '30', Physics_Wrong: '0', Chemistry_Total: '30', Chemistry_Right: '30', Chemistry_Wrong: '30', Biology_Total: '30', Biology_Right: '30', Biology_Wrong: '30', TotalCorrect: '30', TotalWrong: '30', TotalAttempt: '30', TotalMarksObtained: '30', Percentage: '30%', Rank: '2', TotalMarks: '30', QualifyingMarks: '30' },
  { ResultID: 1, StudentID: 1, StudentName: 'Vilas Holkar', TestID: 1, TestName: 'Minor Test 1', TestSeriesID: 1, TestSeriesName: 'ghi', TestTypeID: 1, TestTypeName: 'asd', TestDate: '20/09/2018', Physics_Total: '30', Physics_Right: '30', Physics_Wrong: '0', Chemistry_Total: '30', Chemistry_Right: '30', Chemistry_Wrong: '30', Biology_Total: '30', Biology_Right: '30', Biology_Wrong: '30', TotalCorrect: '30', TotalWrong: '30', TotalAttempt: '30', TotalMarksObtained: '30', Percentage: '30%', Rank: '3', TotalMarks: '30', QualifyingMarks: '30' },

];