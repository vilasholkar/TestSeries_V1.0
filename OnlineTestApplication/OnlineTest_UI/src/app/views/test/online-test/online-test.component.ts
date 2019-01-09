import { Component, OnInit, ViewChild } from '@angular/core';
import {OnlineTestService} from '../../../services/admin/online-test.service';
import {TestSeriesService} from '../../../services/admin/test-series.service';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {Stream,Course,Batch,TestType,Session} from '../../../models/master';
import {OnlineTest,TestSeries} from '../../../models/test';
import { MatSnackBar } from "@angular/material";
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service'

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
  dataSource: any = [];
  displayedColumns = ['TestNo', 'TestName', 'TestSeries', 'TestType', 'Duration', 'StartDate', 'TestMarks'];
  selection = new SelectionModel<OnlineTest>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  PaginationConfig:any;

  constructor(private onlineTestService: OnlineTestService, private testTypeService: TestTypeService,
  private testSeriesService: TestSeriesService,public snackBar: MatSnackBar,private helperSvc: HelperService) { }

  ngOnInit() {
    debugger;
    this.getOnlineTest();
    this.PaginationConfig=this.helperSvc.PaginationConfig;
  }
 
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
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
        this.dataSource = new MatTableDataSource(data.Object as OnlineTest[]);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
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
    debugger;
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
       else
       {
         alert("Error: "+data.Message);
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
  onSubmit(){
    debugger;
    this.onlineTestService.userInfo()
   .subscribe(data => {
     if(data === 'Success')
         alert('Data Saved Successfully');
         //this.eligibleStudentArray.length = 0;
         //this.router.navigate(['/test/online-test']); 
   },error => {
     alert(error);
   });    
} 
}
