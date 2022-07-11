import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowOwnerComponent } from './show-owner/show-owner.component';
import { BusinessownerRoutingModule } from './businessowner.routing';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { ApproveAppointmentComponent } from './approve-appointment/approve-appointment.component';



@NgModule({
  declarations: [
    ShowOwnerComponent,
    ApproveAppointmentComponent,
  ],
  imports: [
    CommonModule,
    BusinessownerRoutingModule,
    FormsModule,
    SharedModule,
  ]
})
export class BusinessownerModule { }
