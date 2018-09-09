import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewOnlineTestComponent } from './view-online-test/view-online-test.component';
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
        }
      ]
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

  export class StudentRoutingModule { }