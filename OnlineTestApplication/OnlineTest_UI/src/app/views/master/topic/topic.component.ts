import { Component, OnInit, ViewChild, TemplateRef, ElementRef, Input } from '@angular/core';
import { Topic,Subject, Session, Stream, Course, Batch } from '../../../models/master';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.scss'
  ]
})
export class TopicComponent implements OnInit {
  IsEmpty: boolean = false;
  Title: any;
  btnAddNew:boolean=true;
  PaginationConfig: any;
  showAddDiv: any;
  topic: Topic;
  topicModel: any={} ;
  subjectModel: Subject;
  sessionModel: Session;
  streamModel: Stream;
  courseModel: Course;
  batchModel: Batch;
  rootNode: any;
  isTopicReadonly: any = true;
  displayedColumns: string[] = ['Topic','Subject','Visible', 'button'];
  dataSource: any = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(rootNode: ElementRef, private helperSvc: HelperService, private dialog: MatDialog) {
    this.showAddDiv = false;
    this.rootNode = rootNode;
  }
  ngOnInit() {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    this.getTopic();
    this.getMasterData();
   
  }
  changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
    this.topicModel = {};
    this.Title = "Add Topic";
    this.topicModel.IsActive=true;
  }
  getTopic() {
    debugger;
    this.helperSvc.getService(APIUrl.GetTopic)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.dataSource = new MatTableDataSource(res.Object as Topic[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          !this.dataSource.data.length ? this.IsEmpty = true : this.IsEmpty = false;
        }
      }, error => {
        this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
  // getTopicByID(model: Topic) {
  //   debugger;
  //   this.btnAddNew=false;
  //   this.showAddDiv = true;
  //   this.Title = "Edit Topic";
  //   this.topicModel = model;
  // }
  getTopicByID(TopicID) {
    debugger;
    this.btnAddNew=false;
    this.showAddDiv = true;
    this.Title = "Edit Test";
    this.helperSvc.getService(APIUrl.GetTopicByID+"?TopicID="+TopicID)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.streamModel = data.Object.MasterData.Stream;
          this.sessionModel = data.Object.MasterData.Session;
          this.topicModel = data.Object.TopicData;
          this.courseModel = data.Object.TopicData.Course;
          this.batchModel = data.Object.TopicData.Batch;
        }
      }, error => {
        this.helperSvc.errorHandler(error);
      });
  }
  AddTopic() {
   // this.topic = this.topicModel;
    //  this.topicService.addUpdateTopics(this.topic)
    this.helperSvc.postService(APIUrl.AddUpdateTopic, this.topicModel)
      .subscribe(data => {
        if (data === 'Success') {
          this.topicModel = {};
          this.getTopic();
          this.showAddDiv = !this.showAddDiv;
          this.btnAddNew=true;
          this.helperSvc.notifySuccess("Record Saved Successfully.");
          // this.isTopicReadonly = true;
        }else{
          this.helperSvc.notifyError(data);

        }
      }, error => {
        this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
  }
  DeleteTopic(model: Topic) {
    if (confirm("Are you sure to delete " + model.Topic)) {
      this.topic = model;
      //  this.topicService.deleteTopicById(this.topic)
      this.helperSvc.postService(APIUrl.DeleteTopic, this.topic)
        .subscribe(data => {
          if (data === 'Success') {
            this.getTopic();
            this.showAddDiv = false;
            this.helperSvc.notifySuccess("Record Deleted Successfully.");
            // this.isTopicReadonly = true;
          }
        }, error => {
          this.helperSvc.errorHandler(error.error);
          console.log(error);
        });

    }
  }
  onChangeStream(streamId) {
    debugger
    this.helperSvc.postService_WithoutSpinner(APIUrl.GET_CourseByStream, streamId)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.courseModel = data.Object;
      }, error => {
        alert('error');
      })
  }
  onChangeCourse(courseId) {
    this.helperSvc.postService_WithoutSpinner(APIUrl.GET_BatchByCourse,courseId)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.batchModel = data.Object;
      }, error => {
        alert('error');
      })
  }
  getMasterData() {
    debugger;
    this.helperSvc.getService(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.subjectModel = data.Object.Subject;
          this.sessionModel = data.Object.Session;
          this.streamModel = data.Object.Stream;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }
  // UpdateTopic(model: Topic) {
  //   this.topic = model;
  //   // this.topicService.addUpdateTopics(this.topic)
  //   this.helperSvc.postService(APIUrl.AddUpdateTopics, this.topic)
  //     .subscribe(data => {
  //       if (data === 'Success') {
  //         this.topicModel = {};
  //         this.getTopic();
  //         this.showAddDiv = false;
  //         this.isTopicReadonly = true;
  //         this.helperSvc.notifySuccess("Record Saved Successfully.");
  //       }
  //     }, error => {
  //       this.helperSvc.errorHandler(error.error);
  //       console.log(error);
  //     });
  // }

}
