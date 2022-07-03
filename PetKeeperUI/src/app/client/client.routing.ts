import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { EditClientComponent } from "./edit-client/edit-client.component";
import { ScheduleClientComponent } from "./schedule-client/schedule-client.component";
import { ShowClientComponent } from "./show-client/show-client.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"edit/:id",component:EditClientComponent},
    {path:"delete",component:ShowClientComponent},
    {path:"schedule/:id",component:ScheduleClientComponent},
    {path:"comment/delete",component:ScheduleClientComponent},
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