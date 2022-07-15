import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SweetalertService } from 'src/app/services/Shared/sweetalert.service';
import { SharedService } from 'src/app/shared.service';
import { GetUserAccountDto } from 'src/app/_Models/GetUserAccountDto';
import { GetUserDto } from 'src/app/_Models/GetUserDto';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  id!:Guid;
  email:string = '';
  phoneNumber:string = '';
  fullName:string = '';
  userPic:any = null;

  User:GetUserAccountDto = new GetUserAccountDto(this.id,'','','','',this.userPic);

  route:string = '';
  clients:GetUserDto[] = [];

  role:any = '';

  imgURL:string = 'https://localhost:7293/';
  thumbnail = this.sanitizer.bypassSecurityTrustUrl(this.imgURL);

  constructor(public Serv:SharedService,public router:Router,public sanitizer:DomSanitizer, public _sweetalertService: SweetalertService) {}

  ngOnInit(): void {
    this.Serv.getAllClients().subscribe(d=>{
      this.clients = d;
    });

    for(let client of this.clients){
      if(client.id == this.User.id){
        this.route = '/client/edit/'+this.User.id;
      }else{
        this.route = '/businessowner/edit/'+this.User.id;
      }
    };



    this.Serv.getUser().subscribe(d=>{
      // this.fullName = d.data.fullName,
      // this.email = d.data.email,
      // this.phoneNumber = d.data.phoneNumber,
      // this.userPic = d.data.userPic,
      this.User = d.data;
      this.role = localStorage.getItem('Role');
      this.role = this.role.replace(/['"]+/g, '');
      // this.role = this.role.slice(1, -1);
      console.log(d.data);
      console.log(this.role);
      console.log(d.message);
    })
  };

  Delete(){
    this.Serv.DeleteAccount();
    this.Serv.Logout();
  }

}
