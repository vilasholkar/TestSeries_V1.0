import { Component, OnInit , ElementRef} from '@angular/core';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {TestType} from '../../../models/master';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-test-type',
  templateUrl: './test-type.component.html',
  styleUrls: ['./test-type.component.scss'
]
})
export class TestTypeComponent implements OnInit {
  showAddDiv: any;
  testType: TestType;
  testTypeModel: any = {};
  rootNode: any;
  isTestTypeReadonly: any = true;
  constructor(private testTypeService: TestTypeService, rootNode: ElementRef , public snackBar: MatSnackBar) {
    this.showAddDiv = false;
    this.rootNode = rootNode;
  }
  ngOnInit() {
    this.getTestType();
  }
    Edit(model: TestType ) {
    this.isTestTypeReadonly = false;
    }
    changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
    }
    getTestType() {
     this.testTypeService.getTestTypes()
     .subscribe(data => {
      if (data.Message === 'Success') {
         this.testType = data.Object;
       }
     }, error => {
      this.openSnackBar("Error.", "Close");
       console.log(error);
     });
    }
    UpdateTestType(model: TestType) {
      this.testType = model;
      this.testTypeService.addUpdateTestTypes(this.testType)
     .subscribe(data => {
      if (data === 'Success') {
        this.testTypeModel = {};
        this.getTestType();
        this.showAddDiv = false;
        this.isTestTypeReadonly = true;
        this.openSnackBar("Record Saved Successfully.","Close");
       }
     }, error => {
      this.openSnackBar("Error.", "Close");
       console.log(error);
     });
    }
    AddTestType() {
      this.testType = this.testTypeModel;
      this.testTypeService.addUpdateTestTypes(this.testType)
     .subscribe(data => {
      if (data === 'Success') {
        this.testTypeModel = {};
        this.getTestType();
        this.showAddDiv = false;
        this.openSnackBar("Record Added Successfully.","Close");
        this.isTestTypeReadonly = true;
       }
     }, error => {
      this.openSnackBar("Error.", "Close");
       console.log(error);
     });
    }
    DeleteTestType(model: TestType) {
      if (confirm("Are you sure to delete " + model.TestTypeName)) {
      this.testType = model;
      this.testTypeService.deleteTestTypeById(this.testType)
     .subscribe(data => {
      if (data === 'Success') {
      this.getTestType();
      this.showAddDiv = false;
      this.openSnackBar("Record Deleted Successfully.","CLose");
      this.isTestTypeReadonly = true;
      }
     }, error => {
      this.openSnackBar("Error.", "Close");
       console.log(error);
     });

    }
  }
    openSnackBar(message: string, action: string) {
      this.snackBar.open(message, action, {
        duration: 2000,
      });
    }
}
