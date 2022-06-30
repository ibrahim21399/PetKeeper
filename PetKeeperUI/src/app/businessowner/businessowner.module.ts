import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditOwnerComponent } from './edit-owner/edit-owner.component';
import { ShowOwnerComponent } from './show-owner/show-owner.component';
import { BusinessownerRoutingModule } from './businessowner.routing';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    EditOwnerComponent,
    ShowOwnerComponent,
  ],
  imports: [
    CommonModule,
    BusinessownerRoutingModule,
    FormsModule,
    SharedModule,
  ]
})
export class BusinessownerModule { }