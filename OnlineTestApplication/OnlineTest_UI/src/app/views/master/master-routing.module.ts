import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TestTypeComponent } from './test-type/test-type.component';
import { TopicComponent } from './topic/topic.component';
import { SubTopicComponent } from './sub-topic/sub-topic.component';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Master'
    },
    children: [
      {
        path: 'test-type',
        component: TestTypeComponent,
        data: {
          title: 'Test Type'
        }
      },
      {
        path: 'topic',
        component: TopicComponent,
        data: {
          title: 'Topic'
        }
      },
      {
        path: 'sub-topic',
        component: SubTopicComponent,
        data: {
          title: 'Sub Topic'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
