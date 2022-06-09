import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteClientComponent } from './delete-client/delete-client.component';
import { ShowClientComponent } from './show-client/show-client.component';



@NgModule({
  declarations: [
    DeleteClientComponent,
    ShowClientComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ClientModule { }
