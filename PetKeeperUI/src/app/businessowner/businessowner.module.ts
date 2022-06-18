import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditOwnerComponent } from './edit-owner/edit-owner.component';
import { ShowOwnerComponent } from './show-owner/show-owner.component';
import { BusinessownerRoutingModule } from './businessowner.routing';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    EditOwnerComponent,
    ShowOwnerComponent
  ],
  imports: [
    CommonModule,
    BusinessownerRoutingModule,
    FormsModule
  ]
})
export class BusinessownerModule { }
