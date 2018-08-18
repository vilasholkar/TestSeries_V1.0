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


// Components Routing
import { TestRoutingModule } from './test-routing.module';
import { ViewQuestionComponent } from './view-question/view-question.component';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      TestRoutingModule,
      CustomMaterialModule
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
