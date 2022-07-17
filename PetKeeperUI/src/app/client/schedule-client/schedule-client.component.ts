import { DatePipe, registerLocaleData } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Subscription } from 'rxjs';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { SharedService } from 'src/app/shared.service';
import { BookDto } from 'src/app/_Models/BookDto';
import { CreateCommentDto } from 'src/app/_Models/CreateCommentDto';
import { GetAdminBusinessDetailsDto } from 'src/app/_Models/GetAdminBusinessDetailsDto';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-schedule-client',
  templateUrl: './schedule-client.component.html',
  styleUrls: ['./schedule-client.component.css']
})
export class ScheduleClientComponent implements OnInit {

  sub:Subscription = new Subscription();
  Businessid:any = null;
  SelectedBusiness:GetAdminBusinessDetailsDto = new GetAdminBusinessDetailsDto(this.Businessid,'','','','','','',0,this.Businessid,'','');
  
  inp:string = '';
  src:string = '';

  imgURL:string = 'https://localhost:7293/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  schedule:any = null;

  text:string ='' ;
  val:number = 0;
  applicationUserId:any = null;
  book!:BookDto;

  comment:CreateCommentDto = new CreateCommentDto('',0,this.applicationUserId);

  Comments:any = null;
  length:number = 0;
  date:Date=new Date();

  role:any ='';
  username:any = '';
  usersids:any = null;

  constructor(private sanitizer: DomSanitizer,public route:ActivatedRoute,public Serv:SharedService,public router:Router,public _sweetalertService: SweetalertService) { }

  ngOnInit(): void {
    if(localStorage.getItem('currentUser')){
      this.Serv.getUser().subscribe(d=>{
        this.applicationUserId = d.data.id;
        console.log(this.applicationUserId);
      });

      this.role = localStorage.getItem("Role");
      this.role = this.role.replace(/['"]+/g, '');
    };

    this.sub = this.route.params.subscribe(params=>{
      this.Businessid = params['id'];
      console.log(this.Businessid);
    });

    this.Serv.getBusinessDetails(this.Businessid).subscribe(d=>{
      this.SelectedBusiness = d.data;
      console.log(this.Businessid);

      console.log(d.data);
      console.log(this.SelectedBusiness);
      this.inp = this.SelectedBusiness.cityName+this.SelectedBusiness.areaName+this.SelectedBusiness.businessName;
      this.src = "https://maps.google.com/maps?q="+this.inp+"&t=&z=13&ie=UTF8&iwloc=&output=embed";


      console.log(this.imgURL);
      console.log(this.SelectedBusiness);
      console.log(this.SelectedBusiness.businessPic);
    });

    this.Serv.GetAllSchedule(this.Businessid).subscribe(d=>{
      this.schedule = d.data;
      console.log(d.message);
      console.log(d.data);
    });

    this.Serv.GetComment(this.Businessid).subscribe(d=>{
      this.Comments = d.data;
      this.length = d.data.length;
      console.log(d.message);
      console.log(d.data);
    });

    
  }

  Book(id:Guid){
    if(localStorage.getItem('currentUser')){
      Swal.fire({  
        title: 'Book an appointment',  
        html: '<input type="date" [(ngModel)]="date" class="form-control">',
        showCancelButton: true,
      }).then((result) => {
        if (result.value) {
          console.log(this.book)
          this.book=new BookDto(this.date,this.Businessid,id);
          console.log(this.book)
          this.Serv.Book(this.book).subscribe(d=>{
            console.log(d.message);
            console.log(d.data);
            if(d.success){
              this._sweetalertService.RunAlert(d.message, true);

            }else{
              this._sweetalertService.RunAlert(d.message, true);
            }
          });
        }
      });
      console.log(this.date);
      console.log(this.Businessid);
      console.log(this.schedule[0].id);
      console.log(id);

    }
    else{
      this.router.navigate(["/login"])
    }
    
  }

  Submit(){
    this.comment.comment = this.text;
    this.comment.rate = this.val;
    this.comment.applicationUserId = this.applicationUserId;
    console.log(this.text);
    console.log(this.val);
    console.log(this.applicationUserId);
    console.log(this.comment);
    this.Serv.AddComment(this.Businessid,this.comment).subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
      this._sweetalertService.RunAlert(d.message, true);
    })
  };

  delete(commentId:Guid){
    this.Serv.DeleteComment(commentId).subscribe(d=>{
      console.log(d.message);
      console.log(d.data);
    })
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
