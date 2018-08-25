import { NgModule } from "@angular/core";
// import { MatTabsModule } from "@angular/material/tabs";

import {
  MatButtonModule,MatButtonToggleModule,
  MatCheckboxModule,MatRadioModule,
  MatInputModule,
  MatCardModule,
  MatDividerModule,
  MatFormFieldModule,
  MatSelectModule,
  MatTableModule, MatPaginatorModule, MatSortModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatIconModule,
  MatSnackBarModule,
  MatTooltipModule
  
} from '@angular/material';

const Modules = [
  MatButtonModule,MatButtonToggleModule,
  MatCheckboxModule,MatRadioModule,
  MatInputModule,
  MatCardModule,
  MatDividerModule,
  MatFormFieldModule,
  MatSelectModule,
  MatTableModule, MatPaginatorModule, MatSortModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatIconModule,
  MatSnackBarModule,
  MatTooltipModule
];

@NgModule({
  imports: [
    Modules

  ],
  exports: [
    Modules

  ],
})
export class CustomMaterialModule { }