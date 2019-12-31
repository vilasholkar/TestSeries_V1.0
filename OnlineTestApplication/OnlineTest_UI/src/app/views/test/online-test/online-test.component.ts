import { Component, OnInit, ViewChild } from '@angular/core';
import { OnlineTestService } from '../../../services/admin/online-test.service';
import { TestSeriesService } from '../../../services/admin/test-series.service';
import { TestTypeService } from '../../../services/admin/test-type.service';
import { Stream, Course, Batch, TestType, Session, Subject } from '../../../models/master';
import { OnlineTest, TestSeries } from '../../../models/test';
import {DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from "@angular/material";
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service';
import { APIUrl } from "../../../shared/API-end-points";
import {MomentDateAdapter} from '@angular/material-moment-adapter';
import * as _moment from 'moment';

import { DateTimeAdapter, OWL_DATE_TIME_FORMATS, OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';
import { MomentDateTimeAdapter, OWL_MOMENT_DATE_TIME_FORMATS } from 'ng-pick-datetime-moment';
const moment = (_moment as any).default ? (_moment as any).default : _moment;

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
  selector: 'app-online-test',
  templateUrl: './online-test.component.html',
  styleUrls: ['./online-test.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
  providers: [
    // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
    // application's root module. We provide it at the component level here, due to limitations of
    // our example generation script.
    {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS},
    
    {provide: DateTimeAdapter, useClass: MomentDateTimeAdapter, deps: [OWL_DATE_TIME_LOCALE]},
    {provide: OWL_DATE_TIME_FORMATS, useValue: OWL_MOMENT_DATE_TIME_FORMATS},
  ],
})

export class OnlineTestComponent implements OnInit {
  IsEmpty: boolean = false;
  btnAddNew:boolean=true;
  showAddDiv: any = false;
  stream: Stream;
  course: Course;
  batch: Batch;
  subject: Subject;
  testSeries: TestSeries;
  testType: TestType;
  session: Session;
  onlineTest: OnlineTest;
  onlineTestModel: any = {};
  dropdownSettings = {};
  Title: any;
  public minStartDate = new Date();
  public maxStartDate = new Date(2030, 3, 25);
  public minEndDate = moment();
  public maxEndDate = new Date(2030, 3, 25);
  dataSource: any = [];
  displayedColumns = ['TestNo', 'TestName', 'TestSeries', 'TestType', 'Duration', 'StartDate', 'EndDate', 'TestMarks', 'ViewQuestion', 'EligibleStudent', 'Edit', 'Delete'];
  testDuration = [
    { TestDuration: '900', TestDurationText: '15min' },
    { TestDuration: '1800', TestDurationText: '30min' },
    { TestDuration: '2700', TestDurationText: '45min' },
    { TestDuration: '3600', TestDurationText: '60min' },
    { TestDuration: '5400', TestDurationText: '90min' },
    { TestDuration: '7200', TestDurationText: '120min' },
    { TestDuration: '9000', TestDurationText: '150min' },
    { TestDuration: '10800', TestDurationText: '180min' },
  ];
  selection = new SelectionModel<OnlineTest>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  PaginationConfig: any;

  constructor(private onlineTestService: OnlineTestService, private testTypeService: TestTypeService,
    private testSeriesService: TestSeriesService, private helperSvc: HelperService) { }

  ngOnInit() {
    debugger;
    this.getOnlineTest();
    this.PaginationConfig = this.helperSvc.PaginationConfig;
  }
  ///
  error:any={isError:false,errorMessage:''};
  compareTwoDates(){
    debugger;
    if(new Date(this.onlineTestModel.EndDate)<new Date(this.onlineTestModel.StartDate)){
       this.error={isError:true,errorMessage:'End Date can not before start date'};
    }
    else{
      this.error={isError:false,errorMessage:''};
    }
 }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
  changeShowStatus() {
    debugger;
    this.onlineTestModel = {};
    this.onlineTestService.getMasterData()
      .subscribe(data => {
        if (data.Message === 'Success')
          this.stream = data.Object.Stream;
        this.testType = data.Object.TestType;
        this.testSeries = data.Object.TestSeries;
        this.session = data.Object.Session;
        this.subject = data.Object.Subject;
      }, error => {
        alert('error');
      })
    this.showAddDiv = !this.showAddDiv;
    this.Title = "Add Test";
  }
  getStream() {
    this.onlineTestService.getStream()
      .subscribe(data => {
        if (data.Message === 'Success')
          this.stream = data.Object;
      }, error => {
        alert('error');
      })
  }
  getTestType() {

    this.testTypeService.getTestTypes()
      .subscribe(data => {
        if (data.Message === 'Success') {

          this.testType = data.Object;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }
  getTestSeries() {
    this.testSeriesService.getTestSeries()
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.testSeries = data.Object;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }
  getSession() {
    this.onlineTestService.getSession()
      .subscribe(data => {
        if (data.Message === 'Success')
          this.session = data.Object;
      }, error => {
        alert('error');
      })
  }
  onChangeStream(streamId) {
    debugger
    this.onlineTestService.getCourseByStream(streamId)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.course = data.Object;
      }, error => {
        alert('error');
      })
  }
  onChangeCourse(courseId) {
    this.onlineTestService.getBatchByCourse(courseId)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.batch = data.Object;
      }, error => {
        alert('error');
      })
  }
  getOnlineTest() {
    debugger;
   // this.onlineTestService.getOnlineTest()
   this.helperSvc.getService(APIUrl.GET_OnlineTest)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.dataSource = new MatTableDataSource(data.Object as OnlineTest[]);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.onlineTest = data.Object;
        !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
      }, error => {
        this.helperSvc.errorHandler("Error : "+error);
      })
  }
  addOnlineTest() {
    debugger;
    //this.onlineTest = this.onlineTestModel;
    this.onlineTestModel.StartDate=moment(this.onlineTestModel.StartDate).format("DD/MM/YYYY");
    this.onlineTestModel.EndDate=moment(this.onlineTestModel.EndDate).format("DD/MM/YYYY");
    this.onlineTestModel.StartTime=moment(this.onlineTestModel.StartTime).utcOffset("+05:30").format("hh:mm a");
    this.onlineTestModel.EndTime=moment(this.onlineTestModel.EndTime).utcOffset("+05:30").format("hh:mm a");
    this.onlineTestService.addUpdateOnlineTest(this.onlineTestModel)
      .subscribe(data => {
        if (data === 'Success') {
          //this.onlineTestModel = {};
          //this.exampleform.reset();
          this.showAddDiv = !this.showAddDiv;
          this.getOnlineTest();
          this.helperSvc.notifySuccess('Record Saved Successfully.');
          this.btnAddNew=true;
        }
      }, error => {
        this.helperSvc.errorHandler("Error : "+error);
        console.log(error);
      });
  }
  deleteOnlineTest(OnlineTestModel) {
    //this.onlineTest = model;
    debugger;
    if (confirm("Are you sure to delete " + OnlineTestModel.TestName)) {
    //  this.onlineTestService.deleteOnlineTest(OnlineTestModel.OnlineTestID)
      this.helperSvc.postService(APIUrl.DeleteOnlineTest,OnlineTestModel.OnlineTestID)
        .subscribe(data => {
          if (data === 'Success') {
            this.getOnlineTest();
            //this.showAddDiv = false;
            // alert('Record Deleted Successfully.');
            this.helperSvc.notifySuccess('Record Deleted Successfully.');
            //this.isTestTypeReadonly = true;
          }
          else{
            this.helperSvc.notifyError(data);
          }
        }, error => {
          this.helperSvc.errorHandler(error);
          console.log(error);
        });
    }
  }
  getOnlineTestById(OnlineTestID) {
    debugger;
    this.btnAddNew=false;
    this.showAddDiv = true;
    this.Title = "Edit Test";
   // this.onlineTestService.getOnlineTestById(OnlineTestID)
    this.helperSvc.getService(APIUrl.GetOnlineTestById+"?OnlineTestId="+OnlineTestID)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.stream = data.Object.MasterData.Stream;
          this.testType = data.Object.MasterData.TestType;
          this.testSeries = data.Object.MasterData.TestSeries;
          this.session = data.Object.MasterData.Session;
          this.onlineTestModel = data.Object.OnlineTestData;
          this.onlineTestModel.StartDate=moment(data.Object.OnlineTestData.StartDate,"DD/MM/YYYY");
          this.onlineTestModel.EndDate=moment(data.Object.OnlineTestData.EndDate,"DD/MM/YYYY");
          this.onlineTestModel.StartTime=moment(data.Object.OnlineTestData.StartTime,"hh:mm a");
          this.onlineTestModel.EndTime=moment(data.Object.OnlineTestData.EndTime,"hh:mm a");
          this.course = data.Object.OnlineTestData.Course;
          this.batch = data.Object.OnlineTestData.Batch;
          this.subject = data.Object.OnlineTestData.Subject;
        }
      }, error => {
        this.helperSvc.errorHandler(error);
      });
  }
  // openSnackBar(message: string, action: string) {
  //   this.snackBar.open(message, action, {
  //     duration: 2000,
  //   });
  // }
}
