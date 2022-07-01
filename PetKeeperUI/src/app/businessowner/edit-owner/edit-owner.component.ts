import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { SharedService } from 'src/app/shared.service';
import { UserDto } from 'src/app/_Models/UserDto';

@Component({
  selector: 'app-edit-owner',
  templateUrl: './edit-owner.component.html',
  styleUrls: ['./edit-owner.component.css']
})
export class EditOwnerComponent implements OnInit {

  // id:Guid = Guid.parse('fbbfc3dc-318e-46f1-e45a-08da56c60766');
  id:Guid = Guid.create();
  email:string = '';
  phoneNumber:string = '';
  userName:string = '';
  fullName:string = '';
  userPic:any = null;

  User:UserDto = new UserDto(this.id,'','','','',this.userPic);

  src:string = 'https://localhost:7293/UsersPic/'+this.userPic;

  constructor(public Serv:SharedService,public router:Router) { }

  Edit(){
    this.User.email = this.email;
    this.User.phoneNumber = this.phoneNumber;
    this.User.userName = this.userName;
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
      console.log(d.data);
      console.log(d.message);
    })
  }

}
