import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuizComponent } from './quiz/quiz.component';

import { TestSeriesComponent } from './test-series/test-series.component';
import { OnlineTestComponent } from './online-test/online-test.component';
import { ViewQuestionComponent } from './view-question/view-question.component';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Test'
    },
     children: [
      {
        path: 'test-series',
        component: TestSeriesComponent,
        data: {
          title: 'Test Series'
        }
      },
      {
        path: 'online-test',
        component: OnlineTestComponent,
        data: {
          title: 'Online Test'
        }
      },
       {
         path: 'quiz/:testID',
         component: QuizComponent,
         data: {
           title: 'Quiz'
         }
       },
       {
         path: 'view-question/:id',
         component: ViewQuestionComponent,
         data: {
           title: 'View Question'
         }
       }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestRoutingModule {}
