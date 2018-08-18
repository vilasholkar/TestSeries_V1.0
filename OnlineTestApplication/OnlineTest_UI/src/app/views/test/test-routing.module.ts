import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuizComponent } from './quiz/quiz.component';

import { TestSeriesComponent } from './test-series/test-series.component';
import { OnlineTestComponent } from './online-test/online-test.component';
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
          title: 'online-test'
        }
      },
       {
         path: 'quiz',
         component: QuizComponent,
         data: {
           title: 'Quiz'
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
