import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ServicesComponent } from "./services/services.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:":id",component:ServicesComponent},
  ]

@NgModule({
    imports:[
        RouterModule.forChild(routes),
    ],
    exports:[
        RouterModule,
    ]
})

export class ServicesRoutingModule{

}