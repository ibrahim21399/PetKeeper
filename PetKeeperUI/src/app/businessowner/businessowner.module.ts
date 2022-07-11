import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowOwnerComponent } from './show-owner/show-owner.component';
import { BusinessownerRoutingModule } from './businessowner.routing';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
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
