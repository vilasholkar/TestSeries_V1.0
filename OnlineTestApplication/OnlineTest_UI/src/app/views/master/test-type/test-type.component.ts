import { Component, OnInit } from '@angular/core';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {TestType} from '../../../models/master';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-test-type',
  templateUrl: './test-type.component.html',
  styleUrls: ['./test-type.component.scss']
})
export class TestTypeComponent implements OnInit {
  showAddDiv: any;
  testType: TestType;
  testTypeModel: any = {};
  constructor(private testTypeService: TestTypeService) {
    this.showAddDiv = false;
  }
  ngOnInit() {
    this.getTestType();
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

      }
     }, error => {
       alert('error');
       console.log(error);
     });
    }

}
