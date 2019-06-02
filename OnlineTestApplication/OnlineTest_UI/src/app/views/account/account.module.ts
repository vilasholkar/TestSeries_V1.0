// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NgxSpinnerModule } from 'ngx-spinner';


import { AccountRoutingModule } from './account-routing.module';
// import { ForgetPasswordComponent } from './forget-password/forget-password.component';

@NgModule({
    imports: [
      CommonModule,
      NgxSpinnerModule,
      FormsModule,
      AccountRoutingModule,
      CustomMaterialModule,
      PerfectScrollbarModule
    ],
    declarations: [
// ForgetPasswordComponent
],
  //  exports:[ViewResultComponent],
    providers: [HttpClientModule]
  })
  export class AccountModule { }
