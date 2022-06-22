import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServicesComponent } from './services/services.component';
import { ServicesRoutingModule } from './services.routing';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ServicesComponent
  ],
  imports: [
    CommonModule,
    ServicesRoutingModule,
    FormsModule,
  ]
})
export class ServicesModule { }
