import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit,ViewChild } from '@angular/core';
import { StudentOnlineTestService } from '../../../services/student/student-online-test.service';
import {StudentOnlineTest} from '../../../models/student';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
@Component({
  selector: 'app-view-online-test',
  templateUrl: './view-online-test.component.html',
  styleUrls: ['./view-online-test.component.scss']
})
export class ViewOnlineTestComponent implements OnInit {
  StudentID: number;
  studentOnlineTest:StudentOnlineTest;
  studentOnlineTestModel: any = {};

  //Element For Material
  displayedColumns: string[] = ['select', 'position', 'name', 'weight', 'symbol'];
  // displayedColumns: string[] = ['select','testseriesid', 'testseries', 'totaltest', 'description'];
  
  // dataSource = new MatTableDataSource<studentOnlineTest>(ELEMENT_DATA);
  // selection = new SelectionModel<studentOnlineTest>(true, []);
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
  selection = new SelectionModel<PeriodicElement>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  ///////////////////////
  constructor(private studentOnlineTestService: StudentOnlineTestService) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.getOnlineTestByStudentID(5110);
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

 
  getOnlineTestByStudentID(StudentID){
    debugger;
    this.studentOnlineTestService.getOnlineTestByStudentID(StudentID)
    .subscribe(data => {
      if (data.Message === 'Success') {
        this.studentOnlineTest = data.Object;
       }
     }, error => {
       alert('error');
       console.log(error);
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