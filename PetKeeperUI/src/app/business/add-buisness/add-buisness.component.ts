import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { endWith, Subscription } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { CreateBusinessDto } from 'src/app/_Models/CreateBusinessDto';
import { DropDownGuid } from 'src/app/_Models/DropDownGuid';
import { DropDownId } from 'src/app/_Models/DropDownId';
import { Schedule } from 'src/app/_Models/schedule';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { ScheduleDto } from 'src/app/_Models/ScheduleDto';




@Component({
  selector: 'app-add-buisness',
  templateUrl: './add-buisness.component.html',
  styleUrls: ['./add-buisness.component.css']
})
export class AddBuisnessComponent implements OnInit {
  AddBusinessForm!: FormGroup;
  businessId:string = Guid.create().toString();
  itemImageUrl:string ="../../../assets/images/pp.png"; 
  days:string[]=["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
  sub:Subscription = new Subscription();
  imgurl:any;

  isActive:boolean = false;
  serviceId:Guid[] = [];
  newSchedule:any;
  schedules:ScheduleDto[] = [];
  businessPic:any = null;
  licencePic:any = null;
  cities:DropDownId[] = [];
  areas:DropDownId[] = [];
  services:DropDownGuid[] = [];
  servicesinObj:Guid[] = [];

    constructor(public sharedService:SharedService,public router:Router,public route: ActivatedRoute,public formBuilder: FormBuilder,
      public _sweetalertService: SweetalertService

    ) {

    this.AddBusinessForm = this.formBuilder.group({
      id:[this.businessId],
      businessName: ['', [Validators.required]],
      businussPhone: ['',[Validators.required,Validators.pattern('[- +()0-9]+')]],
      cityId: ['',[Validators.required]],
      areaId: ['',[Validators.required]],
      serviceId: [this.servicesinObj],
      schedules:[this.schedules],
      businessDesc:['',[Validators.required]],
      businessPic: [''],
      licencePic: [''],

    });
    this.AddBusinessForm.get('cityId')?.valueChanges
    .subscribe((value) => {
      console.log(value); 
      this.SetAreas(value);
    });
   }
  ngOnInit(): void {
    this.sharedService.getAllCities().subscribe(d=>{
      this.cities = d.data;
    });

    this.sharedService.getAllServices().subscribe(s=>{
      this.services = s.data;
    });
    console.log(this.businessId);
  }

  SetAreas(cityId:number){
    this.sharedService.getAllAreas(cityId).subscribe(c=>{
      this.areas = c.data;
      console.log(this.areas);
    });
  }
  
  onChange(event:any) {
    this.businessPic = event.target.files[0];
    console.log(this.businessPic);
   localStorage.setItem('imgData',this.businessPic);

   const reader =new FileReader();
   reader.addEventListener("load",()=>{
    var x=reader.result!;
    console.log(reader.result);
    localStorage.setItem("Bpic",x.toString()); 
    this.itemImageUrl= reader.result?.toString() !;

    this.licencePic=event.target.files[0];

   });

  

   
   reader.readAsDataURL( event.target.files[0]);

        console.log(this.businessPic);
        console.log(this.businessPic);


    this.licencePic = event.target.files[0];
  }
  onselectS(day:string,Start:string){
    this.schedules.forEach(a=>{
      if(a.dayOfWeek==day)
      {
        a.startTime=Start;
      }
      
    });
      
  }

  onselect(day:string,End:string){

    this.schedules.forEach(a=>{
      if(a.dayOfWeek==day)
      {
        a.endTime=End;
      }else{
        return;
      }

    });
  }

  checked(day:string){
    this.newSchedule=new ScheduleDto(day,'','',this.businessId);
    this.schedules.push(this.newSchedule);
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

  onSubmit() {
    console.log(this.AddBusinessForm.value);
    if (this.AddBusinessForm.invalid) {
      return;
    }
    let business: CreateBusinessDto = this.AddBusinessForm.value;
    business.businessPic =this.businessPic;
    business.licencePic=this.licencePic;
    business.isActive=false;
    console.log(business);
    this.sharedService.addBusiness(business).subscribe((res) => {
        if (res.success) {
          this._sweetalertService.RunAlert(res.message, true);
          this.AddBusinessForm.value==null;
          this.sharedService.getAllCities();
          this.sharedService.getAllServices();
        } else {
          this._sweetalertService.RunAlert(res.message, false);
        }
      });




  }

  


 


}
