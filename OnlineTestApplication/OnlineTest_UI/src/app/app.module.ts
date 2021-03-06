import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { Headers, Response, RequestOptions } from '@angular/http';
import { AuthGuard } from './shared/auth-guard/auth.guard'
import { AuthInterceptor } from './shared/auth-guard/auth.interceptors'

// Import Services
import { UserService } from './services/auth-service/user.service'
import { HelperService } from './services/helper.service'
import { ImageDialogComponent } from './views/master/image-dialog/image-dialog.component'


const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};
import { AppComponent } from './app.component';
// Import containers
import { DefaultLayoutComponent } from './containers';
import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { PlayerComponent } from './views/video/player/player.component';
import { ForgetPasswordComponent } from './views/account/forget-password/forget-password.component';

const APP_CONTAINERS = [
  DefaultLayoutComponent
];
import { AppAsideModule, AppBreadcrumbModule, AppHeaderModule, AppFooterModule, AppSidebarModule, } from '@coreui/angular';

// Import routing module
import { AppRoutingModule } from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { CustomMaterialModule } from "./custommaterial.module";
import { DatePipe } from '@angular/common';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    HttpClientModule,
    HttpModule,
    NgxSpinnerModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-top-center',
      preventDuplicates: true,
    }),
    CustomMaterialModule,FormsModule
    // AmazingTimePickerModule
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    P404Component,
    P500Component,
    LoginComponent,
    RegisterComponent,
    ImageDialogComponent,
    PlayerComponent,ForgetPasswordComponent
  ],
  entryComponents: [
    ImageDialogComponent
  ],
  providers: [
    UserService, HelperService,DatePipe,
    AuthGuard,
    // {
    //   provide : HTTP_INTERCEPTORS,
    //   useClass : AuthInterceptor,
    //   multi : true
    // },
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    }, HttpClientModule
  ],

  bootstrap: [AppComponent],
  // providers: [{
  //   provide: LocationStrategy,
  //   useClass: HashLocationStrategy
  // },
  //   HttpClientModule],
  // bootstrap: [ AppComponent ]
})
export class AppModule { }
