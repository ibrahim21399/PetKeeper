import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Subscription } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { CreateBusinessDto } from 'src/app/_Models/CreateBusinessDto';
import { DropDownGuid } from 'src/app/_Models/DropDownGuid';
import { DropDownId } from 'src/app/_Models/DropDownId';
import { Schedule } from 'src/app/_Models/schedule';

@Component({
  selector: 'app-add-buisness',
  templateUrl: './add-buisness.component.html',
  styleUrls: ['./add-buisness.component.css']
})
export class AddBuisnessComponent implements OnInit {

  id:Guid = Guid.create();
  businessName:string = '';
  businessDesc:string = '';
  businussPhone:string = '';
  cityId:number = 0;
  areaId:number = 0;
  
  applicationUserId:Guid = Guid.create();
  sub:Subscription = new Subscription();

  isActive:boolean = true;
  serviceId:Guid[] = [];

  date:Date = new Date();
  schedules:Schedule[] = [new Schedule(this.id,this.date,'','','',this.id),new Schedule(this.id,this.date,'','','',this.id)];
  businessPic:any = null;
  licencePic:any = null;
  AddBusniess:CreateBusinessDto = new CreateBusinessDto(this.id,'','','',0,0,this.id,true,this.serviceId,this.schedules,this.businessPic,this.licencePic);
  cities:DropDownId[] = [];
  areas:DropDownId[] = [];
  services:DropDownGuid[] = [];
  servicesinObj:Guid[] = [];

  constructor(public sharedService:SharedService,public router:Router,public route: ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params=>{
      this.applicationUserId = params['id'];
      console.log(this.applicationUserId);
    });

    this.sharedService.getAllCities().subscribe(d=>{
      this.cities = d.data;
    });
    this.sharedService.getAllServices().subscribe(s=>{
      this.services = s.data;
    })
  }

  SetAreas(cityId:number){
    this.sharedService.getAllAreas(cityId).subscribe(c=>{
      this.areas = c.data;
      console.log(this.areas);
    });
  }

  add(){
    // this.AddBusniess.id = this.id;
    this.AddBusniess.businessName = this.businessName;
    this.AddBusniess.businessDesc = this.businessDesc;
    this.AddBusniess.businussPhone = this.businussPhone;
    this.AddBusniess.cityId = this.cityId;
    this.AddBusniess.areaId = this.areaId;
    this.AddBusniess.applicationUserId = this.applicationUserId;
    this.AddBusniess.isActive = this.isActive;
    this.AddBusniess.serviceId = this.servicesinObj;
    this.AddBusniess.schedules = this.schedules;
    this.AddBusniess.businessPic = this.businessPic;
    this.AddBusniess.licencePic = this.licencePic;
    console.log(this.AddBusniess);
    this.sharedService.addBusiness(this.AddBusniess).subscribe(d=>{
      this.router.navigate(['/businessowner']);
      console.log(d.message);
    })
  }

  onChange(event:any) {
    this.businessPic = event.target.files[0];
    this.licencePic = event.target.files[0];
  }

  onCheck(id:Guid){
    if (!this.servicesinObj.includes(id)) {
      this.servicesinObj.push(id);
    } else {
      var index = this.servicesinObj.indexOf(id);
      if (index > -1) {
        this.servicesinObj.splice(index, 1);
      }
    }
    console.log(this.servicesinObj);
  }
}
