import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddBuisnessComponent } from "./add-buisness/add-buisness.component";
import { EditBuisnessComponent } from "./edit-buisness/edit-buisness.component";
import { ShowBuisnessComponent } from "./show-buisness/show-buisness.component";
// import { LoginGuard } from "../login.guard";

const routes: Routes = [
    {path:"add/:id",component:AddBuisnessComponent},
    {path:"edit/:id",component:EditBuisnessComponent},
    {path:"delete",component:ShowBuisnessComponent},
    {path:"",component:ShowBuisnessComponent},
  ]

@NgModule({
    imports:[
        RouterModule.forChild(routes),
    ],
    exports:[
        RouterModule,
        
    ]
})

export class BusinessRoutingModule{

}