import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowBuisnessComponent } from './show-buisness/show-buisness.component';
import { EditBuisnessComponent } from './edit-buisness/edit-buisness.component';
import { AddBuisnessComponent } from './add-buisness/add-buisness.component';
import { BusinessRoutingModule } from './business.routing';
import { FormsModule, ReactiveFormsModule, ControlValueAccessor } from '@angular/forms';
import { CustomSteperComponent} from './custom-steper/custom-steper.component'
import {CdkStepperModule} from '@angular/cdk/stepper';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NgToggleModule } from 'ng-toggle-button';

@NgModule({
  declarations: [
    ShowBuisnessComponent,
    EditBuisnessComponent,
    AddBuisnessComponent,
    CustomSteperComponent

  ],
  imports: [
    CommonModule,
    BusinessRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CdkStepperModule,
    MatProgressBarModule,
    NgToggleModule
    
    
  ]
})
export class BusinessModule { }
