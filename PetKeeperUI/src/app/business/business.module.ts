import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowBuisnessComponent } from './show-buisness/show-buisness.component';
import { EditBuisnessComponent } from './edit-buisness/edit-buisness.component';
import { DeleteBuisnessComponent } from './delete-buisness/delete-buisness.component';
import { AddBuisnessComponent } from './add-buisness/add-buisness.component';



@NgModule({
  declarations: [
    ShowBuisnessComponent,
    EditBuisnessComponent,
    DeleteBuisnessComponent,
    AddBuisnessComponent
  ],
  imports: [
    CommonModule
  ]
})
export class BusinessModule { }
