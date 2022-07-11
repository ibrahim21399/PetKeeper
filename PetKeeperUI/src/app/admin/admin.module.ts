import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApproveComponent } from './approve/approve.component';
import { FormsModule } from '@angular/forms';
import { AdminRoutingModule } from './admin.routing';



@NgModule({
  declarations: [
    ApproveComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    AdminRoutingModule,
  ]
})
export class AdminModule { }
