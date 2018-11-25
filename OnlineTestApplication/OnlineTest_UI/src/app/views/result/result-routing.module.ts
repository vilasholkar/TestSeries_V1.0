import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ResultAnalysisComponent } from './result-analysis/result-analysis.component';
import { TestResultComponent } from './test-result/test-result.component';
//import { ResultComponent } from './result/result.component';
const routes: Routes = [
    {
      path: '',
      data: {
        title: 'Result'
      },
       children: [
        
        {
          path: 'result-analysis/:id',
          component: ResultAnalysisComponent,
          data: {
            title: 'Result Analysis'
          },
        },
        {
          path: 'test-result',
          component: TestResultComponent,
          data: {
            title: 'Test Result'
          },
        }
      ]
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

  export class ResultRoutingModule { }