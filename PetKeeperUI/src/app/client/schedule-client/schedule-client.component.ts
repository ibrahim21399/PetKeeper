import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Subscription } from 'rxjs';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-schedule-client',
  templateUrl: './schedule-client.component.html',
  styleUrls: ['./schedule-client.component.css']
})
export class ScheduleClientComponent implements OnInit {

  sub:Subscription = new Subscription();
  Businessid:any = null;
  SelectedBusiness:any = null;
  inp:string = '';
  src:string = '';
  imgURL:string = 'https://localhost:7293/UsersPic/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(private sanitizer: DomSanitizer,public route:ActivatedRoute,public Serv:SharedService) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params=>{
      this.Businessid = params['id'];
      console.log(this.Businessid);
    });

    this.Serv.getAllBusinesses().subscribe(d=>{
      this.SelectedBusiness = d.data.find(b=>b.id == this.Businessid);
      console.log(this.SelectedBusiness);
      this.inp = this.SelectedBusiness.cityName+this.SelectedBusiness.areaName+this.SelectedBusiness.businessName;
      this.src = "https://maps.google.com/maps?q="+this.inp+"&t=&z=13&ie=UTF8&iwloc=&output=embed";
    });
    
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
