import { Component, OnInit, ViewChild, TemplateRef, ElementRef, Input } from '@angular/core';
import { TimeTable, Lecture, DefaultLecture } from '../../../models/timetable';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from "@angular/material";
import * as _moment from 'moment';

import { Session, Stream, Course, Batch } from '../../../models/master';
import { HelperService } from '../../../services/helper.service';
import { APIUrl } from "../../../shared/API-end-points";
export const MY_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'YYYY',
  },
};
@Component({
  selector: 'app-time-table',
  templateUrl: './time-table.component.html',
  styleUrls: ['./time-table.component.scss'],
  providers: [
    // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
    // application's root module. We provide it at the component level here, due to limitations of
    // our example generation script.
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class TimeTableComponent implements OnInit {
  IsEmpty: boolean = false;
  divClass: any
  Title: any;
  btnAddNew: boolean = true;
  showAddDiv: any;
  showTemplateDiv: any;
  timeTable: TimeTable;
  timeTableModel: any = {};
  temp_timeTableModel: any = {};

  session: Session;
  stream: Stream;
  course: Course;
  batch: Batch[];
  batchModel: any = [];
  lectures: DefaultLecture[];
  lectureModel: any = [];
  lectureDataModel: any = [];
  temp_lectureDataModel: any = [];

  batchCount: any;
  lectureCount: any;
  public minFromDate = new Date();
  public maxFromDate = new Date(2020, 3, 25);
  public minToDate = new Date();
  public maxToDate = new Date(2020, 3, 25);
  isTopicReadonly: any = true;
  displayedColumns: string[] = ['TimeTableDate', 'Shift', 'Session', 'Edit', 'Delete'];
  dataSource: any = [];
  shift: any = [{ "ShiftID": 1, "Shift": "Morning" }, { "ShiftID": 2, "Shift": "Evening" }]
  Timming: any = ["08:00 AM","09:00 AM","10:00 AM","11:00 AM","12:00 PM",
                  "01:00 PM","02:00 PM","03:00 PM","04:00 PM","05:00 PM","06:00 PM","07:00 PM","08:00 PM"];
  faculty: any = [{ "FacultyID": 1, "Faculty": "ABC" }, { "FacultyID": 2, "Faculty": "XYZ" }]
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(rootNode: ElementRef, private helperSvc: HelperService, private dialog: MatDialog) {
    this.showAddDiv = false;
    this.showTemplateDiv = false;
  }
  ngOnInit() {
    this.GetTimeTable();
  }
  changeShowStatus() {
    debugger;
    this.showAddDiv = !this.showAddDiv;
    this.timeTableModel = {};
    this.timeTableModel.DateType = "single";
    this.helperSvc.getService_WithoutSpinner(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.session = data.Object.Session;
          this.stream = data.Object.Stream;
          this.batch = data.Object.Batch;
          this.lectures = data.Object.DefaultLecture;
          this.faculty=data.Object.FacultyList;
        }
      }, error => {
        alert('error');
      })

    this.Title = "Add Time Table";
  }
  // changeShowStatus() {
  //   debugger;
  //   this.onlineTestModel = {};
  //   this.onlineTestService.getMasterData()
  //     .subscribe(data => {
  //       if (data.Message === 'Success')
  //         this.stream = data.Object.Stream;
  //       this.testType = data.Object.TestType;
  //       this.testSeries = data.Object.TestSeries;
  //       this.session = data.Object.Session;
  //     }, error => {
  //       alert('error');
  //     })
  //   this.showAddDiv = !this.showAddDiv;
  //   this.Title = "Add Test";
  // }

  error: any = { isError: false, errorMessage: '' };
  compareTwoDates() {
    if (new Date(this.timeTableModel.ToDate) < new Date(this.timeTableModel.FromDate)) {
      this.error = { isError: true, errorMessage: 'To Date should be greater then From Date' };
    }
    else {
      this.error = { isError: false, errorMessage: '' };
    }
  }


  CreateTimeTable() {
    debugger;
    //     if (this.timeTableModel.TimeTableID > 0) {
    // this.timeTableModel.LectureList;
    // this.temp_timeTableModel.LectureList;
    //     }
    //     else {

    //   }
    this.temp_lectureDataModel = this.timeTableModel.LectureList;
    this.showAddDiv = true;
    this.showTemplateDiv = true;
    this.lectureDataModel = [];
    this.lectureModel = [];
    this.batchModel = [];
    if (this.timeTableModel.DateType == "single") {
      this.timeTableModel.ToDate = this.timeTableModel.FromDate;
    }

    this.batchCount = this.timeTableModel.BatchID.length;
    this.lectureCount = this.timeTableModel.LectureID.length;
    switch (this.lectureCount) {
      case 1:
        this.divClass = "col-sm-12 border"
        break;
      case 2:
        this.divClass = "col-sm-6 border"
        break;
      case 3:
        this.divClass = "col-sm-4 border"
        break;
      case 4:
        this.divClass = "col-sm-3 border"
        break;
      case 5:
        this.divClass = "col border"
        break;
      default:
        this.divClass = "col-sm-2 border"
        break;
    }

    for (let index = 0; index < this.timeTableModel.BatchID.length; index++) {
      for (let index1 = 0; index1 < this.batch.length; index1++) {
        if (this.timeTableModel.BatchID[index] === this.batch[index1].BatchID) {
          this.batchModel.push({
            BatchID: this.batch[index1].BatchID,
            Batch: this.batch[index1].Batch
          }
          );
        }
      }
    }

    for (let index = 0; index < this.timeTableModel.LectureID.length; index++) {
      for (let index1 = 0; index1 < this.lectures.length; index1++) {
        if (this.timeTableModel.LectureID[index] === this.lectures[index1].LectureID) {
          this.lectureModel.push({
            LectureID: this.lectures[index1].LectureID,
            Lecture: this.lectures[index1].Lecture
          }
          );
        }
      }
    }

    for (let index = 0; index < this.batchModel.length; index++) {
      for (let index1 = 0; index1 < this.lectureModel.length; index1++) {

        this.lectureDataModel.push({
          Batch: this.batchModel[index],
          Lecture: this.lectureModel[index1],
          Subject: '',
          FacultyID: 0
        });
      }
    }

    console.log(this.lectureDataModel);
    console.log(this.temp_lectureDataModel);

    this.lectureDataModel.forEach(lectureDateElement => {
      this.temp_lectureDataModel.forEach(tempLectureDataElement => {
        if (lectureDateElement.Batch.BatchID == tempLectureDataElement.Batch.BatchID &&
          lectureDateElement.Lecture.LectureID == tempLectureDataElement.Lecture.LectureID) {
          lectureDateElement.Batch = tempLectureDataElement.Batch;
          lectureDateElement.Lecture = tempLectureDataElement.Lecture;
          lectureDateElement.FacultyID = tempLectureDataElement.FacultyID;
          lectureDateElement.Subject = tempLectureDataElement.Subject;
        }
      });
    });
    this.lectureModel.length = 0;
    let array = this.lectureDataModel;
    var flags = [], output = [], l = array.length, i;
    for (i = 0; i < l; i++) {
      if (flags[array[i].Lecture.LectureID]) continue;
      flags[array[i].Lecture.LectureID] = true;
      this.lectureModel.push(array[i].Lecture);
    }
    console.log(this.lectureModel);

    // let lectureDataModel1 = this.lectureDataModel.map(item => item.Lecture)
    //   .filter((value, index, self) => self.indexOf(value) === index)

    // let groups = this.lectureDataModel.filter(({Lecture}) => this.timeTableModel.LectureID.includes(LectureID));

    // this.timeTableModel.LectureID.forEach(timeTableModelElement => {
    //   this.lectureDataModel.forEach(lectureDataElement => {
    //     if (timeTableModelElement == lectureDataElement.Lecture.LectureID) {
    //       this.lectureModel.push({
    //         LectureID: lectureDataElement.Lecture.LectureID,
    //         Lecture: lectureDataElement.Lecture.LectureName,
    //         Time_From: lectureDataElement.Lecture.Time_From,
    //         Time_To: lectureDataElement.Lecture.Time_To
    //       });
    //     }
    //   });
    // });

    console.log(this.lectureDataModel);

  }
  SaveTimeTable(lectureDataModel1) {
    debugger;
    console.log(this.lectureModel);
    ////////////////////////////////////////
    for (let index = 0; index < lectureDataModel1.length; index++) {
      for (let index1 = 0; index1 < this.lectureModel.length; index1++) {
        if (lectureDataModel1[index].Lecture.LectureID == this.lectureModel[index1].LectureID)
        {
          lectureDataModel1[index].Lecture.LectureID=this.lectureModel[index1].LectureID;
        //  lectureDataModel1[index].Lecture.Lecture=this.lectureModel[index1].Lecture;
          lectureDataModel1[index].Lecture.Time_From=this.lectureModel[index1].Time_From;
          lectureDataModel1[index].Lecture.Time_To=this.lectureModel[index1].Time_To;
        }
        
      }
    }
    ///////////////////////////////////////
   this.timeTableModel.FromDate=_moment(this.timeTableModel.FromDate).format("DD/MM/YYYY");
   this.timeTableModel.ToDate=_moment(this.timeTableModel.ToDate).format("DD/MM/YYYY");
   
   //this.timeTableModel.FromDate =this.helperSvc.convertDate(this.timeTableModel.FromDate);

   //this.timeTableModel.ToDate=this.timeTableModel.ToDate.toDate();
   
    this.timeTableModel.LectureList = lectureDataModel1;
    this.helperSvc.postService(APIUrl.AddUpdateTimeTable, this.timeTableModel)
      .subscribe(data => {
        if (data === 'Success') {
          this.showAddDiv = !this.showAddDiv;
          this.showTemplateDiv = !this.showTemplateDiv;
          this.GetTimeTable();
          this.helperSvc.notifySuccess('Record Saved Successfully.');
          this.btnAddNew = true;
        }
      }, error => {
        this.helperSvc.errorHandler("Error : " + error);
        console.log(error);
      });

  }
  GetTimeTable() {
    debugger;
    this.helperSvc.getService(APIUrl.GetTimeTable)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.dataSource = new MatTableDataSource(data.Object as TimeTable[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      }, error => {
        this.helperSvc.errorHandler(error);
      });
  }
  GetTimeTableByDate(element) {
    debugger;
    this.timeTableModel = {};
    this.temp_timeTableModel = {};
    this.lectureDataModel = [];
    this.lectureModel = [];
    this.batchModel = [];
    let TimeTableDate = element.FromDate;
    this.btnAddNew = false;
    this.showAddDiv = true;
    this.showTemplateDiv = true;
    this.Title = "Edit Time Table";
    this.helperSvc.postService(APIUrl.GetTimeTableByDate ,element)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.timeTableModel = data.Object;
          this.timeTableModel.FromDate=_moment(data.Object.FromDate,"DD/MM/YYYY");
          this.timeTableModel.ToDate=_moment(data.Object.ToDate,"DD/MM/YYYY");
          console.log(_moment(data.Object.ToDate,"DD/MM/YYYY"));
          this.temp_timeTableModel = data.Object;

          for (let index = 0; index < this.timeTableModel.BatchID.length; index++) {
            for (let index1 = 0; index1 < data.Object.Batch.length; index1++) {
              if (this.timeTableModel.BatchID[index] === data.Object.Batch[index1].BatchID) {
                this.batchModel.push({
                  BatchID: data.Object.Batch[index1].BatchID,
                  Batch: data.Object.Batch[index1].Batch
                }
                );
              }
            }
          }
          let array = this.timeTableModel.LectureList;
          var flags = [], output = [], l = array.length, i;
          for (i = 0; i < l; i++) {
            if (flags[array[i].Lecture.LectureID]) continue;
            flags[array[i].Lecture.LectureID] = true;
            this.lectureModel.push(array[i].Lecture);
          }
          console.log(this.lectureModel);

          switch (this.lectureModel.length) {
            case 1:
              this.divClass = "col-sm-12 border"
              break;
            case 2:
              this.divClass = "col-sm-6 border"
              break;
            case 3:
              this.divClass = "col-sm-4 border"
              break;
            case 4:
              this.divClass = "col-sm-3 border"
              break;
            case 5:
              this.divClass = "col border"
              break;
            default:
              this.divClass = "col-sm-2 border"
              break;
          }
          this.lectures = data.Object.Lecture;
          this.batch = data.Object.Batch;
          this.lectureDataModel = data.Object.LectureList;
        }
      }, error => {
        this.helperSvc.errorHandler(error);
      });

      this.helperSvc.getService_WithoutSpinner(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.faculty=data.Object.FacultyList;
        }
      }, error => {
        alert('error');
      })
  }
  // ----------------------------Unused Code-------------------------------
  onChangeStream(streamId) {
    debugger
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

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
}