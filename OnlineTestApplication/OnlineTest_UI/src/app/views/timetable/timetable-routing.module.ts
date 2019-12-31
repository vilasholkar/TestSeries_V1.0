import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TimeTableComponent } from './time-table/time-table.component';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Test'
    },
     children: [
      {
       path: 'time-table',
       component: TimeTableComponent,
       data: {
         title: 'Time Table'
       }
     }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TimeTableRoutingModule {}
