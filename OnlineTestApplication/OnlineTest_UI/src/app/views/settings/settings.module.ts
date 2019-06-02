// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NgxSpinnerModule } from 'ngx-spinner';


import { SettingsRoutingModule } from './settings-routing.module';
import { ChangePasswordComponent } from './change-password/change-password.component';

@NgModule({
    imports: [
      CommonModule,
      NgxSpinnerModule,
      FormsModule,
      SettingsRoutingModule,
      CustomMaterialModule,
      PerfectScrollbarModule
    ],
    declarations: [
    ChangePasswordComponent],
  //  exports:[ViewResultComponent],
    providers: [HttpClientModule]
  })
  export class SettingsModule { }
