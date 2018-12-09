import { Component, OnInit, ChangeDetectionStrategy ,ViewChild} from '@angular/core';
import {OnlineTestService} from '../../../services/admin/online-test.service';
import {TestSeriesService} from '../../../services/admin/test-series.service';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {Stream,Course,Batch,TestType,Session} from '../../../models/master';
import {OnlineTest,TestSeries} from '../../../models/test';
import { MatSnackBar } from "@angular/material";
import { NgForm } from "@angular/forms";
var $:any;
@Component({
  selector: 'app-online-test',
  templateUrl: './online-test.component.html',
  styleUrls: ['./online-test.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush

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

  // @ViewChild('onlineTestForm') exampleform :NgForm;
  // form = this.exampleform;

  // @ViewChild('onlineTestForm') exampleform :NgForm;
 
  changeShowStatus() {
    debugger;
    this.getStream();
    this.getTestType();
    this.getTestSeries();
    this.getSession(); 
    this.showAddDiv = !this.showAddDiv;
    this.Title = "Add Test";
    //this.onlineTestModel = {};
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
    this.onlineTest = this.onlineTestModel;
    this.onlineTestService.addUpdateOnlineTest(this.onlineTest)
    .subscribe(data => {
      if (data === 'Success') {
        this.onlineTestModel = {};
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
  getOnlineTestById(OnlineTestModel){
    debugger;
    this.getStream();
    this.getTestType();
    this.getTestSeries();
    this.getSession(); 
    if(this.showAddDiv==false)
      {
        this.showAddDiv=true;
      }
    
    this.Title="Edit Test";
    this.onlineTestService.getOnlineTestById(OnlineTestModel.OnlineTestID)
    .subscribe(data => {
      if (data.Message === 'Success') {
        this.onlineTestModel = data.Object;
        this.onChangeStream(this.onlineTestModel.StreamID);
        this.onChangeCourse(this.onlineTestModel.CourseID);
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
