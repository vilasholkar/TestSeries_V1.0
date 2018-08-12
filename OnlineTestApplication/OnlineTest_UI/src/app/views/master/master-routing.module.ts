import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TestTypeComponent } from './test-type/test-type.component';
import { TestSeriesComponent } from './test-series/test-series.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Test'
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
        path: 'test-series',
        component: TestSeriesComponent,
        data: {
          title: 'Test Series'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule {}
