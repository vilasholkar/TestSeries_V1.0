import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {OnlineTestService} from '../../../services/admin/online-test.service';
import {TestSeriesService} from '../../../services/admin/test-series.service';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {Stream,Course,Batch,TestType,Session} from '../../../models/master';
import {OnlineTest,TestSeries} from '../../../models/test';
import { MatSnackBar } from "@angular/material";

@Component({
  selector: 'app-online-test',
  templateUrl: './online-test.component.html',
  styleUrls: ['./online-test.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
})

export class OnlineTestComponent implements OnInit {
  public dateTime: Date;
  showAddDiv:any=false;
  stream : Stream;
  course : Course;
  batch : Batch;
  testSeries : TestSeries;
  testType : TestType;
  session : Session;
  onlineTest: OnlineTest;
  onlineTestModel: any = {};
  dropdownSettings = {};
  Title:any;
  public minStartDate = new Date();
  public maxStartDate = new Date(2020, 3, 25);
  public minEndDate =new Date();
  public maxEndDate = new Date(2020, 3, 25);
  constructor(private onlineTestService: OnlineTestService, private testTypeService: TestTypeService,
  private testSeriesService: TestSeriesService,public snackBar: MatSnackBar) { }

  ngOnInit() {
    debugger;
    this.getOnlineTest();
  }
 
  changeShowStatus() {
    debugger;
    this.onlineTestModel = {};
    this.onlineTestService.getMasterData()
    .subscribe(data => {
      if(data.Message === 'Success')
        this.stream = data.Object.Stream;
        this.testType = data.Object.TestType;
        this.testSeries = data.Object.TestSeries;
        this.session = data.Object.Session;
    },error => {
      alert('error');
    })
    this.showAddDiv = !this.showAddDiv;
    this.Title = "Add Test";
  }
  getStream(){
    this.onlineTestService.getStream()
    .subscribe(data => {
      if(data.Message === 'Success')
        this.stream = data.Object;
    },error => {
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
  getSession(){
    this.onlineTestService.getSession()
    .subscribe(data => {
      if(data.Message === 'Success')
        this.session = data.Object;
    },error => {
      alert('error');
    })
  }
  onChangeStream(streamId){
    this.onlineTestService.getCourseByStream(streamId)
    .subscribe(data => {
      if(data.Message === 'Success')
        this.course = data.Object;
    },error => {
      alert('error');
    })
  }
  onChangeCourse(courseId){
    this.onlineTestService.getBatchByCourse(courseId)
    .subscribe(data => {
      if(data.Message === 'Success')
        this.batch = data.Object;
    },error => {
      alert('error');
    })
  }
  getOnlineTest(){
    debugger
    this.onlineTestService.getOnlineTest()
    .subscribe(data => {
      if(data.Message === 'Success')
        this.onlineTest = data.Object;
    },error => {
      alert('error');
    })
  }
  error:any={isError:false,errorMessage:''};

  addOnlineTest(){
    debugger;
    //this.onlineTest = this.onlineTestModel;
    this.onlineTestService.addUpdateOnlineTest(this.onlineTestModel)
    .subscribe(data => {
      if (data === 'Success') {
        //this.onlineTestModel = {};
        //this.exampleform.reset();
        this.showAddDiv = !this.showAddDiv;
        this.getOnlineTest();
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
  getOnlineTestById(OnlineTestID){
    debugger;
    this.showAddDiv=true;
    this.Title="Edit Test";
    this.onlineTestService.getOnlineTestById(OnlineTestID)
    .subscribe(data => {
      if (data.Message === 'Success') {
        this.stream = data.Object.MasterData.Stream;
        this.testType = data.Object.MasterData.TestType;
        this.testSeries = data.Object.MasterData.TestSeries;
        this.session = data.Object.MasterData.Session;
        this.onlineTestModel = data.Object.OnlineTestData;
        this.course = data.Object.OnlineTestData.Course;
        this.batch = data.Object.OnlineTestData.Batch;
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
