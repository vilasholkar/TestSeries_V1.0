// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";

import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';

// OnlineTest Component
import { TestSeriesComponent } from './test-series/test-series.component';
import { OnlineTestComponent } from './online-test/online-test.component';
import { QuizComponent } from './quiz/quiz.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import {TestSeriesService} from '../../services/admin/test-series.service';
import {TestTypeService} from '../../services/admin/test-type.service';// Components Routing
import { TestRoutingModule } from './test-routing.module';
import { ViewQuestionComponent } from './view-question/view-question.component';
import { NgxSpinnerModule } from 'ngx-spinner';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { EligibleStudentComponent } from './eligible-student/eligible-student.component';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      TestRoutingModule,
      CustomMaterialModule,PerfectScrollbarModule,
      TabsModule,
      OwlDateTimeModule,
      OwlNativeDateTimeModule,
      NgxSpinnerModule
    ],
    declarations: [
      TestSeriesComponent,
      OnlineTestComponent,
      ViewQuestionComponent,
      QuizComponent,
      EligibleStudentComponent
    ],
    providers: [HttpClientModule,TestSeriesService,TestTypeService]
  })
  export class TestModule { }
