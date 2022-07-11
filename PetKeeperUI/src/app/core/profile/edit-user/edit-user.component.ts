import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SharedService } from 'src/app/shared.service';
import { UserDto } from 'src/app/_Models/UserDto';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  // id:Guid = Guid.parse('fbbfc3dc-318e-46f1-e45a-08da56c60766');
  id:Guid = Guid.create();
  email:string = '';
  phoneNumber:string = '';
  fullName:string = '';
  userPic:any = null;

  User:UserDto = new UserDto(this.id,'','','','',this.userPic);

  src:string = 'https://localhost:7293/UsersPic/'+this.userPic;

  constructor(public Serv:SharedService,public router:Router) { }

  Edit(){
    this.User.email = this.email;
    this.User.phoneNumber = this.phoneNumber;
    this.User.fullName = this.fullName;
    this.User.userPic = this.userPic;
    console.log(this.User);
    this.Serv.EditUser(this.User).subscribe(d=>{
      console.log(d.data);
      console.log(d.message);
    })
  }

  onChange(event:any){
    this.userPic = event.target.files[0];
  }
  
  ngOnInit(): void {
    this.Serv.getUser().subscribe(d=>{
      this.fullName = d.data.fullName,
      this.email = d.data.email,
      this.phoneNumber = d.data.phoneNumber,
      this.userPic = d.data.userPic,
      console.log(d.data);
      console.log(d.message);
    })
    console.log(this.User);
  }
}
