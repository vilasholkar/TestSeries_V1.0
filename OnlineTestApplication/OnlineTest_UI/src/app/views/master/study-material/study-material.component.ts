import { Component, OnInit, ViewChild } from '@angular/core';
import { Subject, Topic, SubTopic, StudyMaterial } from '../../../models/master';
import { OnlineTest } from '../../../models/test';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from "@angular/material";
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service';
import { APIUrl } from "../../../shared/API-end-points";
import { MomentDateAdapter } from '@angular/material-moment-adapter';

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
  selector: 'app-study-material',
  templateUrl: './study-material.component.html',
  styleUrls: ['./study-material.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
  providers: [
    // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
    // application's root module. We provide it at the component level here, due to limitations of
    // our example generation script.
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})

export class StudyMaterialComponent implements OnInit {
  IsEmpty: boolean = false;
  btnAddNew: boolean = true;
  showAddDiv: any = false;
  subject: Subject;
  topic: Topic;
  subTopic: SubTopic;
  studyMaterial: StudyMaterial;
  studyMaterialModel: any = {};
  Title: any;
  dataSource: any = [];
  displayedColumns = ['Tittle', 'SubTittle', 'Subject', 'Topic', 'SubTopic', 
  // 'Session', 'Stream', 'Course', 'Batch', 
  'URL_English', 'Edit', 'Delete'];
  selection = new SelectionModel<OnlineTest>(true, []);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  PaginationConfig: any;
  fileToUpload: File = null;
  constructor(private helperSvc: HelperService) { }

  ngOnInit() {
    debugger;
     this.getStudyMaterial();
    this.PaginationConfig = this.helperSvc.PaginationConfig;
  }
  ///
  error: any = { isError: false, errorMessage: '' };
  // compareTwoDates() {
  //   debugger;
  //   if (new Date(this.onlineTestModel.EndDate) < new Date(this.onlineTestModel.StartDate)) {
  //     this.error = { isError: true, errorMessage: 'End Date can not before start date' };
  //   }
  //   else {
  //     this.error = { isError: false, errorMessage: '' };
  //   }
  // }
  handleFileInput(files: FileList) {
    debugger;
    this.fileToUpload = files.item(0);
    var reader = new FileReader();
    reader.readAsDataURL(this.fileToUpload);
  }


  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
  }
  changeShowStatus() {
    debugger;
    this.studyMaterialModel = {};
    this.helperSvc.getService_WithoutSpinner(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.subject = data.Object.Subject;
        }
      }, () => {
        alert('error');
      })
    this.showAddDiv = !this.showAddDiv;
    this.Title = "Add Study Material";
  }

  GetMasterData()
  {
    this.helperSvc.getService_WithoutSpinner(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.subject = data.Object.Subject;
          this.topic = data.Object.Topic;
          this.subTopic = data.Object.SubTopic;
        }
      }, () => {
        alert('error');
      })
  }
  onChangeSubject(SubjectID) {
    debugger;
    this.helperSvc.getService_WithoutSpinner(APIUrl.GetTopicBySubject+"?SubjectID="+ SubjectID)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.topic = data.Object;
      }, () => {
        alert('error');
      })
  }
  onChangeTopic(TopicID) {
    this.helperSvc.getService_WithoutSpinner(APIUrl.GetSubTopicByTopic+"?TopicID="+ TopicID)
      .subscribe(data => {
        if (data.Message === 'Success')
          this.subTopic = data.Object;
      }, () => {
        alert('error');
      })
  }
  // onChangeStream(streamId) {
  //   debugger
  //   this.helperSvc.postService_WithoutSpinner(APIUrl.GET_CourseByStream, streamId)
  //     .subscribe(data => {
  //       if (data.Message === 'Success')
  //         this.course = data.Object;
  //     }, error => {
  //       alert('error');
  //     })
  // }
  // onChangeCourse(courseId) {
  //   this.helperSvc.postService_WithoutSpinner(APIUrl.GET_BatchByCourse, courseId)
  //     .subscribe(data => {
  //       if (data.Message === 'Success')
  //         this.batch = data.Object;
  //     }, error => {
  //       alert('error');
  //     })
  // }

  getStudyMaterial() {
    debugger;
    this.helperSvc.getService(APIUrl.GETStudyMaterial)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.dataSource = new MatTableDataSource(data.Object as StudyMaterial[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
           this.studyMaterial = data.Object;
          !this.dataSource.filteredData.length ? this.IsEmpty = true : this.IsEmpty = false;
        }
      }, error => {
        this.helperSvc.errorHandler("Error : " + error);
      })
  }
  addStudyMaterial() {
    debugger;
    if(this.fileToUpload!=null)
    {
      this.helperSvc.postFile(APIUrl.UploadFile,this.fileToUpload)
      .subscribe(data => {
        this.studyMaterialModel.URL_English = data;
        this.helperSvc.postService(APIUrl.AddUpdateStudyMaterial, this.studyMaterialModel)
          .subscribe(data => {
            if (data === 'Success') {
              this.studyMaterialModel = {};
              //this.exampleform.reset();
              this.showAddDiv = !this.showAddDiv;
              this.getStudyMaterial();
              this.helperSvc.notifySuccess('Record Saved Successfully.');
              this.btnAddNew = true;
            }
          }, error => {
            this.helperSvc.errorHandler("Error : " + error);
            console.log(error);
          });
      }, error => {
        this.helperSvc.errorHandler("Error : " + error);
        console.log(error);
      });

    }
    else
    {
      this.helperSvc.postService(APIUrl.AddUpdateStudyMaterial, this.studyMaterialModel)
          .subscribe(data => {
            if (data === 'Success') {
              this.studyMaterialModel = {};
              //this.exampleform.reset();
              this.showAddDiv = !this.showAddDiv;
              this.getStudyMaterial();
              this.helperSvc.notifySuccess('Record Saved Successfully.');
              this.btnAddNew = true;
            }
          }, error => {
            this.helperSvc.errorHandler("Error : " + error);
            console.log(error);
          });
    }
    
  }
  deleteStudyMaterial(studyMaterialModel:StudyMaterial) {
    //this.onlineTest = model;
    debugger;
    if (confirm("Are you sure to delete " + studyMaterialModel.Tittle)) {
      this.helperSvc.postService(APIUrl.DeleteStudyMaterial,studyMaterialModel)
        .subscribe(data => {
          if (data === 'Success') {
            this.getStudyMaterial();
            //this.showAddDiv = false;
            // alert('Record Deleted Successfully.');
            this.helperSvc.notifySuccess('Record Deleted Successfully.');
            //this.isTestTypeReadonly = true;
          }
        }, error => {
          this.helperSvc.errorHandler("Error : " + error);
          console.log(error);
        });
    }
  }
  getStudyMaterialById(StudyMaterialID) {
    debugger;
    this.btnAddNew = false;
    this.showAddDiv = true;
    this.Title = "Edit Study Material";
    this.helperSvc.getService(APIUrl.GetStudyMaterialByID + "?StudyMaterialID=" + StudyMaterialID)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.GetMasterData();
          this.studyMaterialModel = data.Object;
        }
      }, error => {
        this.helperSvc.errorHandler(error);
      });
  }
  DownloadFile(filePath:string):void
  {
    debugger;
    this.helperSvc.getFile(APIUrl.DownloadFile+'?filePath='+filePath)
    .subscribe(fileData => 
      {
      let b:any = new Blob([fileData], { type: 'application/pdf' });
      var url= window.URL.createObjectURL(b);
        window.open(url);
      }
    );
  }
 
  
}
