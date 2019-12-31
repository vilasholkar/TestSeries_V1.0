import { Component, OnInit, ViewChild, TemplateRef, ElementRef, Input } from '@angular/core';
import { Topic,SubTopic } from '../../../models/master';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-sub-topic',
  templateUrl: './sub-topic.component.html',
  styleUrls: ['./sub-topic.component.scss'
  ]
})
export class SubTopicComponent implements OnInit {
  IsEmpty: boolean = false;
  Title: any;
  btnAddNew:boolean=true;
  PaginationConfig: any;
  showAddDiv: any;
  subTopic: SubTopic;
  subTopicModel: any={} ;
  topicModel: Topic;
  rootNode: any;
  isTopicReadonly: any = true;
  displayedColumns: string[] = ['SubTopic','Topic','Visible', 'button'];
  dataSource: any = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(rootNode: ElementRef, private helperSvc: HelperService, private dialog: MatDialog) {
    this.showAddDiv = false;
    this.rootNode = rootNode;
  }
  ngOnInit() {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    this.getSubTopic();
    this.getTopic();
   
  }
  changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
    this.subTopicModel = {};
    this.Title = "Add SubTopic";
    this.subTopicModel.IsActive=true;
  }
  getSubTopic() {
    debugger;
    this.helperSvc.getService(APIUrl.GetSubTopic)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.dataSource = new MatTableDataSource(res.Object as SubTopic[]);
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
  getSubTopicByID(model: SubTopic) {
    this.btnAddNew=false;
    this.showAddDiv = true;
    this.Title = "Edit Sub Topic";
    this.subTopicModel = model;
  }
 
  AddSubTopic() {
    debugger;
    this.helperSvc.postService(APIUrl.AddUpdateSubTopic, this.subTopicModel)
      .subscribe(data => {
        if (data === 'Success') {
          this.subTopicModel = {};
          this.getSubTopic();
          this.showAddDiv = !this.showAddDiv;
          this.btnAddNew=true;
          this.helperSvc.notifySuccess("Record Saved Successfully.");
          // this.isTopicReadonly = true;
        }
        else{
          this.helperSvc.notifyError(data);

        }
      }, error => {
        this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
  }
  DeleteSubTopic(model: SubTopic) {
    if (confirm("Are you sure to delete " + model.SubTopic)) {
      this.subTopic = model;
            this.helperSvc.postService(APIUrl.DeleteSubTopic, this.subTopic)
        .subscribe(data => {
          if (data === 'Success') {
            this.getSubTopic();
            this.showAddDiv = false;
            this.helperSvc.notifySuccess("Record Deleted Successfully.");
          }
        }, error => {
          this.helperSvc.errorHandler(error.error);
          console.log(error);
        });

    }
  }

  getTopic() {
    debugger;
    this.helperSvc.getService(APIUrl.GetMasterData)
      .subscribe(data => {
        if (data.Message === 'Success') {
          this.topicModel = data.Object.Topic;
        }
      }, error => {
        alert('error');
        console.log(error);
      });
  }
}
