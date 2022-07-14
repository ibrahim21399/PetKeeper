import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Subscription } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { DropDownId } from 'src/app/_Models/DropDownId';
import { GetBusinessDto } from 'src/app/_Models/GetBusinessDto';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})
export class ServicesComponent implements OnInit,OnDestroy {

  Serviceid:Guid = Guid.create();
  sub:Subscription = new Subscription();

  cities:DropDownId[] = [];
  areas:DropDownId[] = [];
  CityId:number = 0;
  AreaId:number = 0;

  ServiceName:string|undefined = '';

  FilteredBusinesses:GetBusinessDto[] = [];

  SelectedRow:any = null;

  inp:string = '';

  src:string = '';

  imgURL:string = 'https://localhost:7293/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(public route: ActivatedRoute, public serv:SharedService,private sanitizer: DomSanitizer) {}
  
  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params=>{
      this.Serviceid = params['id'];
      console.log(this.Serviceid);
    });

    this.serv.getAllCities().subscribe(c=>{
      this.cities = c.data;
      console.log(this.cities);
    });

    this.serv.getAllServices().subscribe(s=>{
      this.ServiceName = s.data.find(s=>s.id == this.Serviceid)?.name;
    })

  }

  SetAreas(cityId:number){
    this.serv.getAllAreas(cityId).subscribe(c=>{
      this.areas = c.data;
      console.log(this.areas);
    });
  }

  Filter(){
    this.serv.FilterBusiness(this.Serviceid,this.CityId,this.AreaId).subscribe(bs=>{
      this.FilteredBusinesses = bs.data;
      console.log(this.FilteredBusinesses);
    })
  }

  RowSelected(sr:any){
    this.SelectedRow = sr;
    console.log(this.SelectedRow);
  }

  send() {
    this.inp = this.SelectedRow.cityName+this.SelectedRow.areaName+this.SelectedRow.businessName;
    this.src = "https://maps.google.com/maps?q="+this.inp+"&t=&z=13&ie=UTF8&iwloc=&output=embed";
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }  
}
