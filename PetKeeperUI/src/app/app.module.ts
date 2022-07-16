import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { HomeComponent } from './core/home/home.component';
import { CoreModule } from './core/core.module';
import { RegisterComponent } from './sign/register/register.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './sign/login/login.component';
import { JwtInterceptor } from './jwt.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProfileComponent } from './core/profile/profile.component';
import { GetBusinessOfOwnerComponent } from './businessowner/get-business-of-owner/get-business-of-owner.component';
import { ChangePasswordComponent } from './core/change-password/change-password.component';
import { AboutUsComponent } from './about-us/about-us.component';






@NgModule({
  declarations: [
    AppComponent,
    NotfoundComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    ProfileComponent,
    GetBusinessOfOwnerComponent,
    ChangePasswordComponent,
    AboutUsComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    CoreModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,

  ],
  exports: [
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
