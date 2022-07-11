import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ScheduleClientComponent } from "./schedule-client/schedule-client.component";
import { ShowAppointmentsComponent } from "./show-appointments/show-appointments.component";
import { ShowClientComponent } from "./show-client/show-client.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"delete",component:ShowClientComponent},
    {path:"schedule/:id",component:ScheduleClientComponent},
    {path:"comment/delete",component:ScheduleClientComponent},
    {path:"appointments",component:ShowAppointmentsComponent},
    {path:"",component:ShowClientComponent},
  ]

@NgModule({
    imports:[
        RouterModule.forChild(routes),
    ],
    exports:[
        RouterModule,
    ]
})

export class ClientRoutingModule{

}