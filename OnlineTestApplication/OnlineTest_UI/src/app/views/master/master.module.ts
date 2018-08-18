// Angular
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// OnlineTest Component
import { TestTypeComponent } from './test-type/test-type.component';

import {TestTypeService} from '../../services/admin/test-type.service';
import { CustomMaterialModule } from "../../custommaterial.module";


// Components Routing
import { MasterRoutingModule } from './master-routing.module';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      MasterRoutingModule,
      CustomMaterialModule
    ],
    declarations: [
      TestTypeComponent
      
    ],
    providers: [TestTypeService]
  })
  export class MasterModule { }
