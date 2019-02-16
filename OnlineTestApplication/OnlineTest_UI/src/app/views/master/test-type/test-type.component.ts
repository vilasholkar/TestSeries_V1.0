import { Component, OnInit, ViewChild, TemplateRef, ElementRef } from '@angular/core';
import { TestType } from '../../../models/master';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-test-type',
  templateUrl: './test-type.component.html',
  styleUrls: ['./test-type.component.scss'
  ]
})
export class TestTypeComponent implements OnInit {
  IsEmpty: boolean = false;
  Title: any;
  btnAddNew:boolean=true;
  PaginationConfig: any;
  showAddDiv: any;
  testType: TestType;
  testTypeModel: any = {};
  rootNode: any;
  isTestTypeReadonly: any = true;
  displayedColumns: string[] = ['TestType', 'button', 'button1'];
  dataSource: any = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(rootNode: ElementRef, private helperSvc: HelperService, private dialog: MatDialog) {
    this.showAddDiv = false;
    this.rootNode = rootNode;
  }
  ngOnInit() {
    this.PaginationConfig = this.helperSvc.PaginationConfig;
    this.getTestType();
  }
  changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
    this.testTypeModel = {};
    this.Title = "Add Test Type";
  }
  getTestType() {
    this.helperSvc.getService(APIUrl.GET_TestTypes)
      .subscribe(res => {
        if (res.Message === 'Success') {
          this.dataSource = new MatTableDataSource(res.Object as TestType[]);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          // this.testType = data.Object;
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
  getTestTypeByID(model: TestType) {
    this.btnAddNew=false;
    this.showAddDiv = true;
    this.Title = "Edit Test Type";
    this.testTypeModel = model;
  }
 
  AddTestType() {
   // this.testType = this.testTypeModel;
    //  this.testTypeService.addUpdateTestTypes(this.testType)
    this.helperSvc.postService(APIUrl.AddUpdateTestTypes, this.testTypeModel)
      .subscribe(data => {
        if (data === 'Success') {
          this.testTypeModel = {};
          this.getTestType();
          this.showAddDiv = !this.showAddDiv;
          this.btnAddNew=true;
          this.helperSvc.notifySuccess("Record Saved Successfully.");
          // this.isTestTypeReadonly = true;
        }
      }, error => {
        this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
  }
  DeleteTestType(model: TestType) {
    if (confirm("Are you sure to delete " + model.TestTypeName)) {
      this.testType = model;
      //  this.testTypeService.deleteTestTypeById(this.testType)
      this.helperSvc.postService(APIUrl.DeleteTestTypes, this.testType)
        .subscribe(data => {
          if (data === 'Success') {
            this.getTestType();
            this.showAddDiv = false;
            this.helperSvc.notifySuccess("Record Deleted Successfully.");
            // this.isTestTypeReadonly = true;
          }
        }, error => {
          this.helperSvc.errorHandler(error.error);
          console.log(error);
        });

    }
  }
  // UpdateTestType(model: TestType) {
  //   this.testType = model;
  //   // this.testTypeService.addUpdateTestTypes(this.testType)
  //   this.helperSvc.postService(APIUrl.AddUpdateTestTypes, this.testType)
  //     .subscribe(data => {
  //       if (data === 'Success') {
  //         this.testTypeModel = {};
  //         this.getTestType();
  //         this.showAddDiv = false;
  //         this.isTestTypeReadonly = true;
  //         this.helperSvc.notifySuccess("Record Saved Successfully.");
  //       }
  //     }, error => {
  //       this.helperSvc.errorHandler(error.error);
  //       console.log(error);
  //     });
  // }

}
