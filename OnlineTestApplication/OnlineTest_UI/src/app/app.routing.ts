import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthGuard} from './shared/auth-guard/auth.guard'
// Import Containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    component: RegisterComponent,canActivate:[AuthGuard],
    data: {
      title: 'Register Page'
    }
  },
  {
    path: '',
    component: DefaultLayoutComponent,canActivate:[AuthGuard],
    data: {  
      title: 'Home'
    },
    children: [
      {
        path: 'student',
        loadChildren: './views/student/student.module#StudentModule',canActivate:[AuthGuard],
      },
      {
        path: 'test',
        loadChildren: './views/test/test.module#TestModule',canActivate:[AuthGuard],
      },
      {
        path: 'result',
        loadChildren: './views/result/result.module#ResultModule'
      },
      {
        path: 'master',
        loadChildren: './views/master/master.module#MasterModule',canActivate:[AuthGuard],
      },
      {
        path: 'base',
        loadChildren: './views/base/base.module#BaseModule',canActivate:[AuthGuard],
      },
      {
        path: 'buttons',
        loadChildren: './views/buttons/buttons.module#ButtonsModule',canActivate:[AuthGuard],
      },
      {
        path: 'charts',
        loadChildren: './views/chartjs/chartjs.module#ChartJSModule',canActivate:[AuthGuard],
      },
      {
        path: 'dashboard',
        loadChildren: './views/dashboard/dashboard.module#DashboardModule',canActivate:[AuthGuard],
      },
      {
        path: 'icons',
        loadChildren: './views/icons/icons.module#IconsModule',canActivate:[AuthGuard],
      },
      {
        path: 'notifications',
        loadChildren: './views/notifications/notifications.module#NotificationsModule',canActivate:[AuthGuard],
      },
      {
        path: 'theme',
        loadChildren: './views/theme/theme.module#ThemeModule',canActivate:[AuthGuard],
      },
      {
        path: 'widgets',
        loadChildren: './views/widgets/widgets.module#WidgetsModule',canActivate:[AuthGuard],
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
