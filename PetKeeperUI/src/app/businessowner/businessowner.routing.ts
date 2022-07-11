import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ShowOwnerComponent } from "./show-owner/show-owner.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"delete",component:ShowOwnerComponent},
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