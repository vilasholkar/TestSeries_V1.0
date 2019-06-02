import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthGuard} from './shared/auth-guard/auth.guard'
// Import Containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { PlayerComponent } from './views/video/player/player.component';
import { ForgetPasswordComponent } from './views/account/forget-password/forget-password.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
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
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {
    path: 'player/:VideoID',
    component: PlayerComponent,
    data: {
      title: 'Player'
    }
  },
  {
    path: 'forget-password',
    component: ForgetPasswordComponent,
    data: {
      title: 'Account'
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
        loadChildren: './views/student/student.module#StudentModule',canActivate:[AuthGuard]
      },
      {
        path: 'test',
        loadChildren: './views/test/test.module#TestModule',canActivate:[AuthGuard]
      },
      {
        path: 'result',
        loadChildren: './views/result/result.module#ResultModule',canActivate:[AuthGuard]
      },
      {
        path: 'master',
        loadChildren: './views/master/master.module#MasterModule',canActivate:[AuthGuard]
      },
      {
        path: 'settings',
        loadChildren: './views/settings/settings.module#SettingsModule',canActivate:[AuthGuard]
      },
      {
        path: 'base',
        loadChildren: './views/base/base.module#BaseModule',
      },
      {
        path: 'buttons',
        loadChildren: './views/buttons/buttons.module#ButtonsModule',
      },
      {
        path: 'charts',
        loadChildren: './views/chartjs/chartjs.module#ChartJSModule',
      },
      {
        path: 'dashboard',
        loadChildren: './views/dashboard/dashboard.module#DashboardModule',
      },
      {
        path: 'icons',
        loadChildren: './views/icons/icons.module#IconsModule',
      },
      {
        path: 'notifications',
        loadChildren: './views/notifications/notifications.module#NotificationsModule',
      },
      {
        path: 'theme',
        loadChildren: './views/theme/theme.module#ThemeModule',
      },
      {
        path: 'widgets',
        loadChildren: './views/widgets/widgets.module#WidgetsModule',
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
