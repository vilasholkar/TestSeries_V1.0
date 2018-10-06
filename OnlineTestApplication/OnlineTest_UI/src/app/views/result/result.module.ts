import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { StudentModule } from "../student/student.module";

// import {StudentOnlineTestService} from '../../services/student/student-online-test.service';
import { ResultRoutingModule } from './result-routing.module';
import { ResultAnalysisComponent } from './result-analysis/result-analysis.component';
import { TestResultComponent } from './test-result/test-result.component';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      ResultRoutingModule,
      CustomMaterialModule,
      PerfectScrollbarModule,
      ChartsModule,
      StudentModule
    ],
    declarations: [
       ResultAnalysisComponent,
       TestResultComponent
    ],
    providers: [HttpClientModule]
  })
  export class ResultModule { }
