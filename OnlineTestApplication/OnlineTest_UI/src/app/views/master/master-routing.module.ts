import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TestTypeComponent } from './test-type/test-type.component';


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
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
