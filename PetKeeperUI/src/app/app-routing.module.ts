import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './core/home/home.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { LoginComponent } from './sign/login/login.component';
import { LogoutComponent } from './sign/logout/logout.component';
import { RegisterComponent } from './sign/register/register.component';

const routes: Routes = [
  {path:'home',component:HomeComponent},
  {path:"login",component:LoginComponent},
  {path:"register",component:RegisterComponent},

  {path:"business",loadChildren:()=>import('./business/business.module').then(m=>m.BusinessModule)},
  {path:"businessowner",loadChildren:()=>import('./businessowner/businessowner.module').then(m=>m.BusinessownerModule)},
  {path:"client",loadChildren:()=>import('./client/client.module').then(m=>m.ClientModule)},
  {path:"services",loadChildren:()=>import('./services/services.module').then(m=>m.ServicesModule)},

  {path:"",redirectTo:"home",pathMatch:"full"},
  {path:"**",component:NotfoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


