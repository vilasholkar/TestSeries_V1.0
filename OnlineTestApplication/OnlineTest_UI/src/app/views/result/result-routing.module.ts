import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ResultAnalysisComponent } from './result-analysis/result-analysis.component';
import { TestResultComponent } from './test-result/test-result.component';
import { ResultComponent } from './result/result.component';
import { GenerateResultComponent } from './generate-result/generate-result.component';
import { StudentResponseComponent } from './student-response/student-response.component';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Result'
    },
    children: [
      {
        path: 'result',
        component: ResultComponent,
        data: {
          title: 'Result'
        }
      },
      {
        path: 'result-analysis/:StudentID/:TestID',
        component: ResultAnalysisComponent,
        data: {
          title: 'Result Analysis'
        }
      },
      {
        path: 'test-result',
        component: TestResultComponent,
        data: {
          title: 'Test Result'
        }
      },
      {
        path: 'generate-result',
        component: GenerateResultComponent,
        data: {
          title: 'Generate Result'
        }
      },
      {
        path: 'student-response/:StudentID/:TestID',
        component: StudentResponseComponent,
        data: {
          title: 'Student Response'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ResultRoutingModule { }