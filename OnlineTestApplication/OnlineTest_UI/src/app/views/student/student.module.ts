// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';


import {StudentOnlineTestService} from '../../services/student/student-online-test.service';
import { StudentRoutingModule } from './student-routing.module';
import { ViewOnlineTestComponent } from './view-online-test/view-online-test.component';
import { ViewResultComponent } from './view-result/view-result.component';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      StudentRoutingModule,
      CustomMaterialModule,
      PerfectScrollbarModule
    ],
    declarations: [
        ViewOnlineTestComponent,
        ViewResultComponent
    ],
    exports:[ViewResultComponent],
    providers: [HttpClientModule,StudentOnlineTestService]
  })
  export class StudentModule { }
