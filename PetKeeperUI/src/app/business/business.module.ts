import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowBuisnessComponent } from './show-buisness/show-buisness.component';
import { EditBuisnessComponent } from './edit-buisness/edit-buisness.component';
import { AddBuisnessComponent } from './add-buisness/add-buisness.component';
import { BusinessRoutingModule } from './business.routing';
import { FormsModule, ReactiveFormsModule, ControlValueAccessor } from '@angular/forms';
// import { MatCheckboxModule } from '@angular/material/checkbox';

@NgModule({
  declarations: [
    ShowBuisnessComponent,
    EditBuisnessComponent,
    AddBuisnessComponent
  ],
  imports: [
    CommonModule,
    BusinessRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class BusinessModule { }
