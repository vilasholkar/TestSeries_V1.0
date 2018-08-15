import { Component, OnInit } from '@angular/core';
import {TestTypeService} from '../../../services/admin/test-type.service';
import {TestType} from '../../../models/master';

@Component({
  selector: 'app-test-type',
  templateUrl: './test-type.component.html',
  styleUrls: ['./test-type.component.scss']
})
export class TestTypeComponent implements OnInit {
  constructor(private testTypeService: TestTypeService) { }
  TestType: any = [];
  ngOnInit() {
    this.getTestType();
  }
  // To get all the master data
   getTestType() {
     debugger
     this.testTypeService.getTestTypes()
     .subscribe(data => {
      if (data.Message === 'Success') {
         this.TestType = data.Object;
       }
     }, error => {
       alert('error');
       console.log(error);
     });
   }
    AddEditTestType(model: TestType) {
      debugger
      this.testTypeService.updateTestTypes(model)
     .subscribe(data => {
      if (data.Message === 'Success') {
         this.TestType = data.Object;
       }
     }, error => {
       alert('error');
       console.log(error);
     });
    }
    DeleteTestType(id: any) {
      debugger
      this.testTypeService.deleteTestTypeById(id)
     .subscribe(data => {
      if (data.Message === 'Success') {
      // show alert for data updated successfully
      }
     }, error => {
       alert('error');
       console.log(error);
     });
    }

}
