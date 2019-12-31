// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from "../../custommaterial.module";

// OnlineTest Component
import { TestTypeComponent } from './test-type/test-type.component';
import { TestSeriesComponent } from './test-series/test-series.component';

import {TestTypeService} from '../../services/admin/test-type.service';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NgxSpinnerModule } from 'ngx-spinner';
// Components Routing
import { MasterRoutingModule } from './master-routing.module';
import { TopicComponent } from './topic/topic.component';
import { SubTopicComponent } from './sub-topic/sub-topic.component';
import { StudyMaterialComponent } from './study-material/study-material.component';
import { SliderComponent } from './slider/slider.component';
import { NotificationComponent } from './notification/notification.component';

@NgModule({
    imports: [
      NgxSpinnerModule,
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MasterRoutingModule,
      CustomMaterialModule,
      PerfectScrollbarModule
    ],
    declarations: [
      TestTypeComponent,
      TestSeriesComponent,
      TopicComponent,
      SubTopicComponent,
      StudyMaterialComponent,
      SliderComponent,
      NotificationComponent
    ],
    providers: [HttpClientModule,TestTypeService]
  })
  export class MasterModule { }
