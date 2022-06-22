import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { DropDownGuid } from 'src/app/_Models/DropDownGuid';
import { ServiceResponse } from 'src/app/_Models/ServiceResponse';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  services:DropDownGuid[] = [];
  constructor(public busServ:SharedService, public router:Router) { }

  ngOnInit(): void {
    this.busServ.getAllServices().subscribe(a=>{this.services = a.data });
    console.log("inside business list");
    console.log(this.services);
    console.log(this.services[1]);
  }

}
