// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

// OnlineTest Component
import { TestTypeComponent } from './test-type/test-type.component';
import { TestSeriesComponent } from './test-series/test-series.component';
import {TestTypeService} from '../../services/admin/test-type.service';


// Components Routing
import { MasterRoutingModule } from './master-routing.module';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      MasterRoutingModule
    ],
    declarations: [
      TestTypeComponent,
      TestSeriesComponent
    ],
    providers: [TestTypeService]
  })
  export class MasterModule { }
