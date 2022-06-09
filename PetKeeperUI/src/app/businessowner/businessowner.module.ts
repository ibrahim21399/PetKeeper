import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditOwnerComponent } from './edit-owner/edit-owner.component';
import { DeleteOwnerComponent } from './delete-owner/delete-owner.component';
import { ShowOwnerComponent } from './show-owner/show-owner.component';



@NgModule({
  declarations: [
    EditOwnerComponent,
    DeleteOwnerComponent,
    ShowOwnerComponent
  ],
  imports: [
    CommonModule
  ]
})
export class BusinessownerModule { }
