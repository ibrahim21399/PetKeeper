import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowClientComponent } from './show-client/show-client.component';
import { ClientRoutingModule } from './client.routing';
import { FormsModule } from '@angular/forms';
import { ScheduleClientComponent } from './schedule-client/schedule-client.component';
import { SharedModule } from '../shared/shared.module';
import {AccordionModule} from 'primeng/accordion';
import {RatingModule} from 'primeng/rating';
import { ShowAppointmentsComponent } from './show-appointments/show-appointments.component';

@NgModule({
  declarations: [
    ShowClientComponent,
    ScheduleClientComponent,
    ShowAppointmentsComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
    FormsModule,
    SharedModule,
    AccordionModule,
    RatingModule,
  ]
})
export class ClientModule { }
