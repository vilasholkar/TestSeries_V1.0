import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OnlineTestComponent } from './online-test/online-test.component';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Test'
    },
    children: [
      {
        path: 'online-test',
        component: OnlineTestComponent,
        data: {
          title: 'Online Test'
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
