import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ApproveComponent } from "./approve/approve.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"approve",component:ApproveComponent},
    {path:"",component:ApproveComponent},
  ]

@NgModule({
    imports:[
        RouterModule.forChild(routes),
    ],
    exports:[
        RouterModule,
    ]
})

export class AdminRoutingModule{
}
