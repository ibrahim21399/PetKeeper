import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ApproveAppointmentComponent } from "./approve-appointment/approve-appointment.component";
import { GetBusinessOfOwnerComponent } from "./get-business-of-owner/get-business-of-owner.component";
import { ShowOwnerComponent } from "./show-owner/show-owner.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"getBusiness",component:GetBusinessOfOwnerComponent},
    {path:"delete",component:ShowOwnerComponent},
    {path:"approve",component:ApproveAppointmentComponent},
    {path:"approve/accept",component:ApproveAppointmentComponent},
    {path:"decline",component:ApproveAppointmentComponent},
    {path:"",component:ShowOwnerComponent},
  ]

@NgModule({
    imports:[
        RouterModule.forChild(routes),
    ],
    exports:[
        RouterModule,
        
    ]
})

export class BusinessownerRoutingModule{

}