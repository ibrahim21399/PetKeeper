import { Component, OnInit  } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import {NgbCollapse} from "@ng-bootstrap/ng-bootstrap"
import { ApplicationUserDto } from 'src/app/_Models/ApplicationUserDto';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
 
  public isCollapsed = true;  
  
  fullName!: string;
  currentUser: any;
  auth:boolean = false;
  role:any = "";

  constructor(public authServ:SharedService,public router:Router) { 
    if(localStorage.getItem("currentUser")){
      this.auth = true;
    }
    else{
      this.auth = false;
    }
  }

  ngOnInit(): void {
this.authServ.getUser().subscribe(s=>{
  this.currentUser=s.data;
  console.log(s.data)
});
  console.log(this.currentUser)
    if(localStorage.getItem('Role')){
      this.role = localStorage.getItem('Role');
      this.role = this.role.replace(/['"]+/g, '');
      console.log(this.role);
    }
  }

  logout(){
    this.auth = false;
    this.authServ.Logout();
    this.router.navigateByUrl("/");
  }

}
