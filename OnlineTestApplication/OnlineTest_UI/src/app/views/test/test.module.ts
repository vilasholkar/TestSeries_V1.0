// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";


// OnlineTest Component
import { TestSeriesComponent } from './test-series/test-series.component';
import { OnlineTestComponent } from './online-test/online-test.component';
import { QuizComponent } from './quiz/quiz.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
// Components Routing
import { TestRoutingModule } from './test-routing.module';
import { ViewQuestionComponent } from './view-question/view-question.component';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      TestRoutingModule,
      CustomMaterialModule,PerfectScrollbarModule,
      TabsModule
    ],
    declarations: [
      TestSeriesComponent,
      OnlineTestComponent,
      ViewQuestionComponent,
      QuizComponent
    ],
    providers: [HttpClientModule]
  })
  export class TestModule { }
