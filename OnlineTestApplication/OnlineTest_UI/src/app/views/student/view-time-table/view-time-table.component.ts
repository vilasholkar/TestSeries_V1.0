import { Component, OnInit } from '@angular/core';

import { HelperService } from '../../../services/helper.service';
import { APIUrl } from "../../../shared/API-end-points";

import { Session, Stream, Course, Batch } from '../../../models/master';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from "@angular/material";
import * as _moment from 'moment';
import { TimeTable } from '../../../models/timetable';

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
  selector: 'app-view-time-table',
  templateUrl: './view-time-table.component.html',
  styleUrls: ['./view-time-table.component.scss'],
  providers: [
    // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
    // application's root module. We provide it at the component level here, due to limitations of
    // our example generation script.
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class ViewTimeTableComponent implements OnInit {
  Title: any = "Search Time Table";
  timetable: TimeTable;
  session: Session;
  stream: Stream;
  course: Course;
  batch: Batch[];
  batchModel: any = [];
  timeTableModel: any = {};
  lectureDataModel: any = [];
  public minFromDate = new Date(2019, 3, 25);
  public maxFromDate = new Date(2020, 3, 25);
  shift: any = [{ "ShiftID": 1, "Shift": "Morning" }, { "ShiftID": 2, "Shift": "Evening" }]
  constructor(private helperSvc: HelperService) { }

  ngOnInit() {
    this.onLoad();

  }
  onLoad() {
    debugger;
    var tempBatchID = Number(sessionStorage.getItem("BatchID"));
    if (tempBatchID > 0) {
      this.timeTableModel.FromDate = _moment();
      this.timeTableModel.BatchID = tempBatchID;
      this.GetTimeTableByDate();
    }

    this.helperSvc.getService_WithoutSpinner(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.session = data.Object.Session;
          this.stream = data.Object.Stream;
          this.batch = data.Object.Batch;
        }
      }, error => {
        alert('error');
      })
  }
  GetTimeTableByDate() {
    debugger;
    this.lectureDataModel = [];
    // this.timeTableModel.FromDate = this.timeTableModel.FromDate.format('YYYY-MM-DD');
    let tempdate = this.timeTableModel.FromDate;
    this.timeTableModel.FromDate = _moment(this.timeTableModel.FromDate).format("DD/MM/YYYY");

    this.helperSvc.postService(APIUrl.GetTimeTableByDate, this.timeTableModel)
      .subscribe(data => {
        if (data.Message === 'Success') {
          if (data.Object.LectureList != null) {
            this.lectureDataModel = data.Object.LectureList.filter(item => item.Batch.BatchID == this.timeTableModel.BatchID);
            if (this.lectureDataModel.length == 0) {
              this.helperSvc.notifyError("No time table found.");
            }
          }
          else {
            this.helperSvc.notifyError("No time table found.");
          }
          this.timeTableModel.FromDate = tempdate;
        }
      }, error => {
        this.helperSvc.errorHandler(error);
      });
  }
}
