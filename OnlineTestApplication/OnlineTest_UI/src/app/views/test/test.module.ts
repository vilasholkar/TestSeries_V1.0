// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

// OnlineTest Component
import { OnlineTestComponent } from './online-test/online-test.component';

// Components Routing
import { TestRoutingModule } from './test-routing.module';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      TestRoutingModule
    ],
    declarations: [
      OnlineTestComponent
    ]
  })
  export class TestModule { }
