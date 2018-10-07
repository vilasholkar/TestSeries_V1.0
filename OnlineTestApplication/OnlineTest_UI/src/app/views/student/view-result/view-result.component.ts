import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit,ViewChild } from '@angular/core';
import { StudentOnlineTestService } from '../../../services/student/student-online-test.service';
import {OT_Result} from '../../../models/result';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
@Component({
  selector: 'app-view-result',
  templateUrl: './view-result.component.html',
  styleUrls: ['./view-result.component.scss']
})
export class ViewResultComponent implements OnInit {

  //Element For Material
  displayedColumns: string[] = ['Rank', 'TestName','TestSeriesName','TestTypeName', 'TestDate', 'TotalAttempt',
  'TotalCorrect', 'TotalWrong', 'TotalMarks', 'TotalMarksObtained',
   'Percentage',
  // 'PhysicsRight', 'PhysicsWrong', 'ChemistryRight',
  // 'ChemistryWrong', 'BiologyRight', 'PhysicsWrong', 'BiologyWrong',
      'button'];

dataSource = new MatTableDataSource<OT_Result>(ELEMENT_DATA);
selection = new SelectionModel<OT_Result>(true, []);
@ViewChild(MatPaginator) paginator: MatPaginator;
@ViewChild(MatSort) sort: MatSort;
  constructor() { }

  ngOnInit() {
    let studentID= localStorage.getItem("studentID");
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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
}

const ELEMENT_DATA: OT_Result[] = [
    { ResultID: 1,StudentID: 1,StudentName:'Rohan Yadav',TestID: 1,TestName: 'Minor Test 1',TestSeriesID:1,TestSeriesName: 'abc',TestTypeID: 1,TestTypeName:'asd',TestDate:'20/09/2018', Physics_Total: '30', Physics_Right: '30', Physics_Wrong: '0', Chemistry_Total: '30', Chemistry_Right: '30', Chemistry_Wrong: '30', Biology_Total: '30', Biology_Right: '30', Biology_Wrong: '30', TotalCorrect: '30', TotalWrong: '30', TotalAttempt: '30', TotalMarksObtained: '30', Percentage: '30%', Rank: '30', TotalMarks: '30', QualifyingMarks: '30' },
    { ResultID: 1,StudentID: 1,StudentName:'Rohan Yadav',TestID: 2,TestName: 'Minor Test 2',TestSeriesID:1,TestSeriesName: 'def',TestTypeID: 1,TestTypeName:'asd',TestDate:'20/09/2018', Physics_Total: '30', Physics_Right: '30', Physics_Wrong: '0', Chemistry_Total: '30', Chemistry_Right: '30', Chemistry_Wrong: '30', Biology_Total: '30', Biology_Right: '30', Biology_Wrong: '30', TotalCorrect: '30', TotalWrong: '30', TotalAttempt: '30', TotalMarksObtained: '30', Percentage: '30%', Rank: '30', TotalMarks: '30', QualifyingMarks: '30' },
    { ResultID: 1,StudentID: 1,StudentName:'Rohan Yadav',TestID: 3,TestName: 'Minor Test 3',TestSeriesID:1,TestSeriesName: 'ghi',TestTypeID: 1,TestTypeName:'asd',TestDate:'20/09/2018', Physics_Total: '30', Physics_Right: '30', Physics_Wrong: '0', Chemistry_Total: '30', Chemistry_Right: '30', Chemistry_Wrong: '30', Biology_Total: '30', Biology_Right: '30', Biology_Wrong: '30', TotalCorrect: '30', TotalWrong: '30', TotalAttempt: '30', TotalMarksObtained: '30', Percentage: '30%', Rank: '30', TotalMarks: '30', QualifyingMarks: '30' },
    
  ];