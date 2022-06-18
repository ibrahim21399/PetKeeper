import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowClientComponent } from './show-client/show-client.component';
import { EditClientComponent } from './edit-client/edit-client.component';
import { ClientRoutingModule } from './client.routing';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ShowClientComponent,
    EditClientComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
    FormsModule
  ]
})
export class ClientModule { }
