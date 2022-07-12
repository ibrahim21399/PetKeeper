import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { EditUserComponent } from './profile/edit-user/edit-user.component';
import{NgbModule} from '@ng-bootstrap/ng-bootstrap'

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    EditUserComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgbModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
  ]
})
export class CoreModule { }
