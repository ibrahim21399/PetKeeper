import { Component, OnInit  } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
 
  auth:boolean = false;
  currentUser: any = null;
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
