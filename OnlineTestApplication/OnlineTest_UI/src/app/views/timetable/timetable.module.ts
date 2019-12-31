// Angular
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
// OnlineTest Component
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TimeTableRoutingModule } from './timetable-routing.module';
import { NgxSpinnerModule } from 'ngx-spinner';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { TimeTableComponent } from './time-table/time-table.component';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      TimeTableRoutingModule,
      CustomMaterialModule,PerfectScrollbarModule,
      TabsModule,
      OwlDateTimeModule,
      OwlNativeDateTimeModule,
      NgxSpinnerModule
    ],
    declarations: [
    TimeTableComponent],
    providers: [HttpClientModule]
  })
  export class TimeTableModule { }
