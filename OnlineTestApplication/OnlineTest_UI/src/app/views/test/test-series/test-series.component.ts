import { Component, OnInit, ElementRef } from '@angular/core';
import { TestSeriesService } from '../../../services/admin/test-series.service';
import { TestSeries } from '../../../models/test';
import {FormControl, Validators} from '@angular/forms';
@Component({
  selector: 'app-test-series',
  templateUrl: './test-series.component.html',
  styleUrls: ['./test-series.component.scss']
})
export class TestSeriesComponent implements OnInit {
  showAddDiv: any;
  testSeries: TestSeries;
  testSeriesModel: any = {};
  rootNode: any;
  isTestSeriesReadonly: any = true;
  constructor(private testSeriesService: TestSeriesService, rootNode: ElementRef) {
    this.showAddDiv = false;
  }

  ngOnInit() {
    this.getTestSeries();
  }
  Edit(model: TestSeries) {
    this.isTestSeriesReadonly = false;
  }
  changeShowStatus() {
    this.showAddDiv = !this.showAddDiv;
  }

//  TestSeries = new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]);
//     getErrorMessage() {
//       return this.TestSeries.hasError('required') ? 'You must enter a value' :
//          this.TestSeries.hasError('minLength') ? 'This must be at least 4 characters long.' :
//               '';
//     }
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
  UpdateTestSeries(model: TestSeries) {
    this.testSeries = model;
    this.testSeriesService.addUpdateTestSeries(this.testSeries)
   .subscribe(data => {
    if (data === 'Success') {
      this.testSeriesModel = {};
      this.getTestSeries();
      this.showAddDiv = false;
      this.isTestSeriesReadonly = true;
      alert('Record Saved Successfully.');
     }
   }, error => {
     alert('error');
     console.log(error);
   });
  }
  AddTestSeries() {
    this.testSeries = this.testSeriesModel;
    this.testSeriesService.addUpdateTestSeries(this.testSeries)
   .subscribe(data => {
    if (data === 'Success') {
      this.testSeriesModel = {};
      this.getTestSeries();
      this.showAddDiv = false;
      alert('Record Added Successfully.');
      this.isTestSeriesReadonly = true;
     }
   }, error => {
     alert('error');
     console.log(error);
   });
  }
  DeleteTestSeries(model: TestSeries) {
    this.testSeries = model;
    this.testSeriesService.deleteTestSeriesById(this.testSeries)
   .subscribe(data => {
    if (data === 'Success') {
    this.getTestSeries();
    this.showAddDiv = false;
    alert('Record Deleted Successfully.');
    this.isTestSeriesReadonly = true;
    }
   }, error => {
     alert('error');
     console.log(error);
   });
  }
}
