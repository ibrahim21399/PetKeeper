import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowBuisnessComponent } from './show-buisness/show-buisness.component';
import { EditBuisnessComponent } from './edit-buisness/edit-buisness.component';
import { AddBuisnessComponent } from './add-buisness/add-buisness.component';
import { BusinessRoutingModule } from './business.routing';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ShowBuisnessComponent,
    EditBuisnessComponent,
    AddBuisnessComponent
  ],
  imports: [
    CommonModule,
    BusinessRoutingModule,
    FormsModule
  ]
})
export class BusinessModule { }
