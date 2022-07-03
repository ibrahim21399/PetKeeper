import { registerLocaleData } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { CreateCommentDto } from 'src/app/_Models/CreateCommentDto';
import Swal from 'sweetalert2';

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

  text:string ='' ;
  val:number = 0;
  applicationUserId:any = null;
  comment:CreateCommentDto = new CreateCommentDto('',0,this.applicationUserId);

  date:Date = new Date();

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

  Book(){
    Swal.fire({  
      title: 'Book an appointment',  
      html: '<input type="date" [(ngModel)]="date" class="form-control">',
      showCancelButton: true,
    }).then((result) => {
      if (result.value) {
        this.Serv.Book(this.date).subscribe(d=>{
          console.log(d.message);
          console.log(d.data);
        });
      }
    });
  }

  Submit(){
    this.Serv.AddComment(this.Businessid,this.comment).subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
    })
  };

  delete(){
    this.Serv.DeleteComment(this.Businessid).subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
    })
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
