import { Component, HostListener, OnInit, ViewChild  } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import {NgbCollapse} from "@ng-bootstrap/ng-bootstrap"
import { ApplicationUserDto } from 'src/app/_Models/ApplicationUserDto';
import { HomeComponent } from '../home/home.component';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  navbarFixed:boolean=false;
 
  public isCollapsed = true;  
  
  fullName!: string;
  currentUser: any;
  auth:boolean = false;
  role:any = "";

  constructor(public authServ:SharedService,public router:Router,public viewportScroller:ViewportScroller) { 
    if(localStorage.getItem("currentUser")){
      this.auth = true;
    }
    else{
      this.auth = false;
    }
  }

  ngOnInit(): void {
    
    console.log(this.currentUser)
    if(localStorage.getItem('Role')){
      this.role = localStorage.getItem('Role');
      this.role = this.role.replace(/['"]+/g, '');
      console.log(this.role);
      this.authServ.getUser().subscribe(s=>{
        this.currentUser=s.data;
        console.log(s.data)
      });
    }
  }

  logout(){
    this.auth = false;
    this.authServ.Logout();
    this.router.navigateByUrl("/");
  }

  scrollTo() {
    this.viewportScroller.scrollToAnchor('ser');
  }
  scrollTo2() {
    this.viewportScroller.scrollToAnchor('home');
  }

  @HostListener('window:scroll',['$event'])onScroll(){
    if(window.scrollY>100){
      this.navbarFixed=true;
    }
    else {
      this.navbarFixed=false;

    }
  }



}
