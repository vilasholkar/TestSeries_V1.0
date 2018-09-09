import { Component, OnInit } from '@angular/core';
import {OnlineTestService} from '../../../services/admin/online-test.service';
import {TestSeriesService} from '../../../services/admin/test-series.service';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {Stream,Course,Batch,TestType,Session} from '../../../models/master';
import {OnlineTest,TestSeries} from '../../../models/test';
import { MatSnackBar } from "@angular/material";

@Component({
  selector: 'app-online-test',
  templateUrl: './online-test.component.html',
  styleUrls: ['./online-test.component.scss']
})
export class OnlineTestComponent implements OnInit {
  showAddDiv:any;
  stream : Stream;
  course : Course;
  batch : Batch;
  testSeries : TestSeries;
  testType : TestType;
  session : Session;
  onlineTest: OnlineTest;
  onlineTestModel: any = {};
  minStartDate = new Date(2018, 0, 1);
  maxStartDate = new Date(2020, 0, 1);

  constructor(private onlineTestService: OnlineTestService, private testTypeService: TestTypeService,
  private testSeriesService: TestSeriesService,public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.getOnlineTest();
    this.getStream();
    this.getTestType();
    this.getTestSeries();
    this.getSession();
  }
  changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
  }
  getStream(){
    debugger;
    this.onlineTestService.getStream()
    .subscribe(data => {
      if(data.Message === 'Success')
        this.stream = data.Object;
    },error => {
      alert('error');
    })
  }
  getTestType() {
    debugger;
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
  getSession(){
    debugger;
    this.onlineTestService.getSession()
    .subscribe(data => {
      if(data.Message === 'Success')
        this.session = data.Object;
    },error => {
      alert('error');
    })
  }
  onChangeStream(streamId){
    debugger;
    this.onlineTestService.getCourseByStream(streamId)
    .subscribe(data => {
      if(data.Message === 'Success')
        this.course = data.Object;
    },error => {
      alert('error');
    })
  }
  onChangeCourse(courseId){
    debugger;
    this.onlineTestService.getBatchByCourse(courseId)
    .subscribe(data => {
      if(data.Message === 'Success')
        this.batch = data.Object;
    },error => {
      alert('error');
    })
  }
  getOnlineTest(){
    debugger;
    this.onlineTestService.getOnlineTest()
    .subscribe(data => {
      if(data.Message === 'Success')
        this.onlineTest = data.Object;
    },error => {
      alert('error');
    })
  }
  addOnlineTest(){
    debugger;
    this.onlineTest = this.onlineTestModel;
    this.onlineTestService.addUpdateOnlineTest(this.onlineTest)
    .subscribe(data => {
      if (data === 'Success') {
        this.onlineTestModel = {};
        this.getOnlineTest();
        this.getStream();
        this.getTestType();
        this.getTestSeries();
        this.getSession();
        this.showAddDiv = false;
        this.openSnackBar("Record Saved Successfully.", "Close");
       }
     }, error => {
       alert('error');
       console.log(error);
     });
  }
  deleteOnlineTest(OnlineTestModel){
    //this.onlineTest = model;
    if (confirm("Are you sure to delete " + OnlineTestModel.TestName)) {
    this.onlineTestService.deleteOnlineTest(OnlineTestModel.OnlineTestID)
    .subscribe(data => {
      if (data === 'Success') {
        this.getOnlineTest();
        //this.showAddDiv = false;
        alert('Record Deleted Successfully.');
        //this.isTestTypeReadonly = true;
       }
     }, error => {
       alert('error');
       console.log(error);
     });
  }
}
  getOnlineTestById(OnlineTestModel){
    debugger;
    this.showAddDiv=true;
    this.onlineTestService.getOnlineTestById(OnlineTestModel.OnlineTestID)
    .subscribe(data => {
      if (data.Message === 'Success') {
        this.onlineTestModel = data.Object;
        this.onChangeStream(data.Object.StreamID);
        this.onChangeCourse(data.Object.CourseID);
        //this.getOnlineTest();
        //this.showAddDiv = false;
        //alert('Record Deleted Successfully.');
        //this.isTestTypeReadonly = true;
       }
     }, error => {
       alert('error');
       console.log(error);
     });
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
