import { CdkStepper } from '@angular/cdk/stepper';
import { Component } from '@angular/core';
import { MatProgressBar } from '@angular/material/progress-bar';



@Component({
  selector: 'app-custom-steper',
  templateUrl: './custom-steper.component.html',
  styleUrls: ['./custom-steper.component.css'],
  providers: [{provide: CdkStepper, useExisting:CustomSteperComponent}],


})
export class CustomSteperComponent extends CdkStepper {
  stepvalue:number=20;
  selectStepByIndex(index: number): void {
    this.selectedIndex = index;
  }
  x(){
    if(this.stepvalue!=100)
      this.stepvalue+=20;
  }
  y(){
    if(this.stepvalue!=20)
    this.stepvalue-=20;
}
}