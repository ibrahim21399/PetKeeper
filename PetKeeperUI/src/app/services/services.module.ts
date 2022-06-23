import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServicesComponent } from './services/services.component';
import { ServicesRoutingModule } from './services.routing';
import { FormsModule } from '@angular/forms';
import { SafePipe } from '../safe.pipe';

@NgModule({
  declarations: [
    ServicesComponent,
    SafePipe,
  ],
  imports: [
    CommonModule,
    ServicesRoutingModule,
    FormsModule,
  ]
})
export class ServicesModule { }
