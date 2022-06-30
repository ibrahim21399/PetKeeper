import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ScheduleComponent } from "../businessowner/schedule/schedule.component";
import { EditClientComponent } from "./edit-client/edit-client.component";
import { ShowClientComponent } from "./show-client/show-client.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"edit/:id",component:EditClientComponent},
    {path:"delete",component:ShowClientComponent},
    {path:"schedule",component:ScheduleComponent},
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