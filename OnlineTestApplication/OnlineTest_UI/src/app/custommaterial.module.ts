import { NgModule } from "@angular/core";
import {
  MatButtonModule,
  MatCheckboxModule,
  MatInputModule,
  MatCardModule,
  MatDividerModule,
  MatFormFieldModule,
  MatSelectModule,
  MatTableModule,
  MatDatepickerModule,
  MatNativeDateModule
} from '@angular/material';
const Modules = [
  MatButtonModule,
  MatCheckboxModule,
  MatInputModule,
  MatCardModule,
  MatDividerModule,
  MatFormFieldModule,
  MatSelectModule,
  MatTableModule,
  MatDatepickerModule,
  MatNativeDateModule
];

@NgModule({
  imports: [
    Modules,
  ],
  exports: [
    Modules,
  ],
})
export class CustomMaterialModule { }