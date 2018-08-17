import { Component, OnInit , ElementRef} from '@angular/core';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {TestType} from '../../../models/master';
import { NgForm } from '@angular/forms';
import * as $ from 'jquery';
const dt = require ('datatables.net');
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
  constructor(private testTypeService: TestTypeService, rootNode: ElementRef ) {
    this.showAddDiv = false;
    this.rootNode = rootNode;
  }
  ngOnInit() {
    this.getTestType();
    // let el = $(this.rootNode.nativeElement).find('#TestTypeForm')[0];
    // $('#TestTypeForm').DataTable();
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
       alert('error');
       console.log(error);
     });
    }
    EditTestType(model: TestType) {
      this.testType = model;
      this.testTypeService.addEditTestTypes(this.testType)
     .subscribe(data => {
      if (data === 'Success') {
        this.testTypeModel = {};
        this.getTestType();
        this.showAddDiv = false;
        this.isTestTypeReadonly = true;
        alert('Record Saved Successfully.');
       }
     }, error => {
       alert('error');
       console.log(error);
     });
    }
    AddTestType() {
      this.testType = this.testTypeModel;
      this.testTypeService.addEditTestTypes(this.testType)
     .subscribe(data => {
      if (data === 'Success') {
        this.testTypeModel = {};
        this.getTestType();
        this.showAddDiv = false;
        alert('Record Added Successfully.');
        this.isTestTypeReadonly = true;
       }
     }, error => {
       alert('error');
       console.log(error);
     });
    }
    DeleteTestType(model: TestType) {
      this.testType = model;
      this.testTypeService.deleteTestTypeById(this.testType)
     .subscribe(data => {
      if (data === 'Success') {
      this.getTestType();
      this.showAddDiv = false;
      alert('Record Deleted Successfully.');
      this.isTestTypeReadonly = true;
      }
     }, error => {
       alert('error');
       console.log(error);
     });
    }

}
