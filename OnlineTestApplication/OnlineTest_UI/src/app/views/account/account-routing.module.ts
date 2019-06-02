import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
// import { ForgetPasswordComponent } from './forget-password/forget-password.component';
const routes: Routes = [
    {
      path: '',
      data: {
        title: 'Account'
      },
       children: [
        // {
        //   path: 'forget-password',
        //   component: ForgetPasswordComponent,
        //   data: {
        //     title: 'Forget Password'
        //   }
        // },
        // {
        //   path: 'view-result',
        //   component: ViewResultComponent,
        //   data: {
        //     title: 'View Result'
        //   }
        // }
      ]
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

  export class AccountRoutingModule { }