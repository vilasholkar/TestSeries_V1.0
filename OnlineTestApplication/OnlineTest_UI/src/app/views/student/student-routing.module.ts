import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewOnlineTestComponent } from './view-online-test/view-online-test.component';
import { ViewResultComponent } from './view-result/view-result.component';
import { ViewTimeTableComponent } from './view-time-table/view-time-table.component';
const routes: Routes = [
    {
      path: '',
      data: {
        title: 'Student'
      },
       children: [
        {
          path: 'view-online-test',
          component: ViewOnlineTestComponent,
          data: {
            title: 'View Online Test'
          }
        },
        {
          path: 'view-result',
          component: ViewResultComponent,
          data: {
            title: 'View Result'
          }
        },
        {
          path: 'view-time-table',
          component: ViewTimeTableComponent,
          data: {
            title: 'View TimeTable'
          }
        }
      ]
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

  export class StudentRoutingModule { }