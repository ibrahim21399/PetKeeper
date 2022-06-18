import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { EditOwnerComponent } from "./edit-owner/edit-owner.component";
import { ShowOwnerComponent } from "./show-owner/show-owner.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"edit/:id",component:EditOwnerComponent},
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