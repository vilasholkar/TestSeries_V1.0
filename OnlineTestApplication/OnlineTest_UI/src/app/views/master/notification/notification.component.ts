import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { HelperService } from "../../../services/helper.service";
import { APIUrl } from '../../../shared/API-end-points';
import { Session, Stream, Course, Batch, Notification } from "../../../models/master";
import { Student } from "../../../models/student";

import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import * as _moment from 'moment';
const moment = (_moment as any).default ? (_moment as any).default : _moment;

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {
  title:any;
  description:any;
  IsEmpty: boolean = false;
  filterModel: any = {};
  dataModel: any = {};
  studentModel: any = {};
  studentArray: any = [];
  notificationModel: any = [];
  session: Session;
  stream: Stream;
  course: Course;
  batch: Batch;
  dataSource: any = [];
  displayedColumns = ['select', 'EnrollmentNo', 'StudentName', 'Stream', 'Course', 'Batch'];
  PaginationConfig: any;
  selection = new SelectionModel<Student>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  filterValues = {
    SessionID: "",
    StreamID: ""
  };
  constructor(private helperSvc: HelperService) {

  }

  ngOnInit() {
    this.getMasterData();
    this.getStudentData();
    this.PaginationConfig = this.helperSvc.PaginationConfig;
  }
  getMasterData() {
    this.filterModel = {};
    this.helperSvc.getService(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.stream = data.Object.Stream;
        this.session = data.Object.Session;
      }, error => {
        alert('error');
      })
  }

  getStudentData() {
    this.studentModel = {};
    this.helperSvc.postService(APIUrl.GetFilteredStudent, this.filterModel)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.studentModel = data.Object as Student[];
        this.dataSource = new MatTableDataSource(this.studentModel);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, error => {
        alert('error');
      })
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
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: Student): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.StudentID + 1}`;
  }

  sendNotification() {
    debugger;
    this.dataSource.data.forEach(row => this.studentArray.push(row));
    console.log(this.selection);
    this.selection.selected.forEach(row => {
      this.notificationModel.push({
        ReciverID: row.StudentID, NotificationDate:moment().format('DD/MM/YYYY hh:mm:ss a'), Title: this.dataModel.Title, Description: this.dataModel.Description, ImageURL: '',
        RedirectToUrl: '', IsRead: false,DeviceToken:row.DeviceToken
        });
    });

    console.log(this.notificationModel);

    if (this.notificationModel.length > 0) {
      this.helperSvc.postService(APIUrl.AddUpdateNotification, this.notificationModel)
        .subscribe(data => {
          if (data === 'Success')
            this.helperSvc.notifySuccess('Data Saved Successfully');
            this.notificationModel.length = 0;
            //  this.router.navigate(['/test/online-test']);
        }, error => {
          this.helperSvc.errorHandler(error.error);
          console.log(error.error);
        });
    }
  }
  onChangeStream(streamId: number) {
    debugger;
    this.helperSvc.postService_WithoutSpinner(APIUrl.GET_CourseByStream, streamId)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.course = data.Object;
      }, error => {
        alert('error');
      })
  }
  onChangeCourse(courseId) {
    this.helperSvc.postService_WithoutSpinner(APIUrl.GET_BatchByCourse, courseId)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.batch = data.Object;
      }, error => {
        alert('error');
      })
  }


}
