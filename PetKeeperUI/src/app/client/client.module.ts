import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowClientComponent } from './show-client/show-client.component';
import { EditClientComponent } from './edit-client/edit-client.component';
import { ClientRoutingModule } from './client.routing';
import { FormsModule } from '@angular/forms';
import { ScheduleClientComponent } from './schedule-client/schedule-client.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ShowClientComponent,
    EditClientComponent,
    ScheduleClientComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
    FormsModule,
    SharedModule,
  ]
})
export class ClientModule { }
