import { ViewportScroller } from '@angular/common';
import { Component, HostListener, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { DropDownGuid } from 'src/app/_Models/DropDownGuid';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
 
  services:DropDownGuid[] = [];
  scrolled:boolean =false;

  constructor(public busServ:SharedService, public router:Router,public viewportScroller: ViewportScroller) {}

  ngOnInit(): void {
     this.busServ.getAllServices().subscribe(a=>{this.services = a.data });
     console.log("inside business list");
     console.log(this.services);
     console.log(this.services[1]);
   const element = document.querySelector("#home");
   if (element) element.scrollIntoView({ behavior: 'smooth', block: 'start' })


  }
  scrollTo() {
    this.viewportScroller.scrollToAnchor('ser');
  }

  @HostListener('window:scroll',['$event']) onscroll(){
    if(window.scrollY > 50)
    {
      this.scrolled = true;
    }
    else
    {
      this.scrolled = false;
    }
  }
  back(){
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
  }
  

}
